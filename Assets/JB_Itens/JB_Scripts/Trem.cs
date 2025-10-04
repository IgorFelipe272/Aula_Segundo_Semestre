using UnityEngine;
using UnityEngine.AI;

// Exemplo genérico de spline
public interface ISpline
{
    float GetLength();
    Vector3 GetPoint(float t);      // t: 0 (início) a 1 (fim)
    Quaternion GetRotation(float t);
}

public class Trem : MonoBehaviour
{
    public float velocidadeNormal = 10f;
    public float velocidadeEstacao = 3f;
    public float tempoParadoNaEstacao = 5f; // tempo em segundos parado

    public MonoBehaviour trilhoComponente; // arraste seu componente de spline aqui no Inspector
    private ISpline Trilho => trilhoComponente as ISpline;

    private float progressoSpline = 0f; // 0 = início, 1 = fim

    private float velocidadeAtual;
    private NavMeshAgent agente;
    private bool paradoNaEstacao = false;
    private float tempoParado = 0f;
    private bool jaParouNaEstacao = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updatePosition = false;
        agente.updateRotation = false;

        velocidadeAtual = velocidadeNormal;
    }

    void Update()
    {
        if (paradoNaEstacao)
        {
            velocidadeAtual = 0f;
            tempoParado += Time.deltaTime;
            if (tempoParado >= tempoParadoNaEstacao)
            {
                paradoNaEstacao = false;
                tempoParado = 0f;
                jaParouNaEstacao = true;
            }
        }
        else
        {
            // Verifica a área do NavMesh embaixo do trem
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                int stationMask = 1 << NavMesh.GetAreaFromName("StationArea");
                Debug.Log("NavMeshHit mask: " + hit.mask + " | StationMask: " + stationMask);
                if ((hit.mask & stationMask) != 0 && !jaParouNaEstacao)
                {
                    paradoNaEstacao = true;
                    velocidadeAtual = 0f;
                }
                else
                {
                    velocidadeAtual = Mathf.Lerp(velocidadeAtual, velocidadeNormal, Time.deltaTime * 2f);
                }

                // Se saiu da estação, reseta a flag para poder parar novamente na próxima estação
                if ((hit.mask & stationMask) == 0)
                {
                    jaParouNaEstacao = false;
                }
            }
        }

        AtualizarSpline(velocidadeAtual);
    }

    void AtualizarSpline(float vel)
    {
        if (Trilho != null)
        {
            float comprimentoSpline = Trilho.GetLength();
            float deltaProgresso = (vel * Time.deltaTime) / comprimentoSpline;
            progressoSpline += deltaProgresso;
            progressoSpline = Mathf.Clamp01(progressoSpline);

            transform.position = Trilho.GetPoint(progressoSpline);
            transform.rotation = Trilho.GetRotation(progressoSpline);
        }
    }
}