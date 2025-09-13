using UnityEngine;
using UnityEngine.Splines;

public class IF_SplineWalker : MonoBehaviour
{
    [Header("Configuração de movimento")]
    //public float velocidadeLocal = 5f;

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
    private float progress = 0f;

    private void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }

    void Update()
    {
        var splineNodeAtual = SplineNodeAtual;
        if (splineNodeAtual == null || splineNodeAtual.Spline == null) return;

        var spline = splineNodeAtual.Spline;

        // avança no caminho usando somente velocidadeLocal
        float length = spline.CalculateLength(indiceSpline);

        progress += (splineAnimate.MaxSpeed / length) * Time.deltaTime;

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
