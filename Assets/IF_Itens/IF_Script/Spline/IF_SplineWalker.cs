using UnityEngine;
using UnityEngine.Splines;

public class IF_SplineWalker : MonoBehaviour
{
    [Header("Configuração de movimento")]
    public bool isDiferente = false;
    public float velocidadeLocal = 5f;

    [Header("Configuração do Manager")]
    public string chaveVelocidade; // string escolhida no Inspector

    private IF_VelocidadeManager manager;
    private float progress = 0f;

    [Header("Spline Animate")]
    private SplineAnimate splineAnimate;

    public IF_SplineNode SplineNodeAtual
    {
        get
        {
            if (splineAnimate == null || splineAnimate.Container == null)
                return null;

            return splineAnimate.Container.GetComponent<IF_SplineNode>();
        }
    }

    private int indiceSpline = 0;

    private void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();

        // procura o objeto com a tag "Velocidade Manager"
        GameObject go = GameObject.FindGameObjectWithTag("Velocidade Manager");
        if (go != null)
        {
            manager = go.GetComponent<IF_SetVelocidade>()?.manager;
            if (manager == null)
            {
                Debug.LogWarning("O objeto com tag 'Velocidade Manager' não tem IF_SetVelocidade ou não está com manager configurado.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com a tag 'Velocidade Manager' foi encontrado na cena.");
        }
    }

    void Update()
    {
        var splineNodeAtual = SplineNodeAtual;
        if (splineNodeAtual == null || splineNodeAtual.Spline == null) return;

        var spline = splineNodeAtual.Spline;

        // pega velocidade
        float velocidade = isDiferente ? velocidadeLocal : (manager != null ? manager.GetVelocidade(chaveVelocidade) : velocidadeLocal);

        // avança no caminho
        float length = spline.CalculateLength(indiceSpline);
        progress += (velocidade / length) * Time.deltaTime;

        // chegou no fim
        if (progress >= 1f)
        {
            TrocarSpline();
            return;
        }

        // posição e rotação
        Vector3 pos = (Vector3)spline.EvaluatePosition(indiceSpline, progress);
        Vector3 tangent = ((Vector3)spline.EvaluateTangent(indiceSpline, progress)).normalized;
        Quaternion rot = tangent != Vector3.zero ? Quaternion.LookRotation(tangent) : transform.rotation;

        transform.SetPositionAndRotation(pos, rot);
    }

    void TrocarSpline()
    {
        var splineNodeAtual = SplineNodeAtual;
        if (splineNodeAtual == null) return;

        SplineContainer nova = splineNodeAtual.GetProximaSpline();
        if (nova != null)
        {
            splineAnimate.Container = nova;
            splineAnimate.Restart(true);
            progress = 0f;
        }
    }
}
