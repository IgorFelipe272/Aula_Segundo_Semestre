using UnityEngine;
using UnityEngine.AI;

public class JM_NPCWalk : MonoBehaviour
{
    public float raio = 10f;       
    public float tempoParado = 2f; 
    private NavMeshAgent agente;
    private float cronometro;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        cronometro = tempoParado;
    }

    void Update()
    {
        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
        {
            cronometro -= Time.deltaTime;

            if (cronometro <= 0f)
            {
                Vector3 novoDestino = PontoAleatorio(transform.position, raio);
                agente.SetDestination(novoDestino);
                cronometro = tempoParado;
            }
        }
    }

    Vector3 PontoAleatorio(Vector3 origem, float distancia)
    {
        Vector3 direcao = Random.insideUnitSphere * distancia;
        direcao += origem;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(direcao, out hit, distancia, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return origem;
    }
}
