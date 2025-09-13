using UnityEngine;
using UnityEngine.Splines;

public class splineWalker : MonoBehaviour
{
    [Header("Configuracao de movimento")]
    public float speed = 5f;
    private float progress = 0f;

    [Header("Spline Animate")]
    private SplineAnimate splineAnimate;

    // splineNodeAtual agora é obtido dinamicamente
    public splineNode SplineNodeAtual
    {
        get
        {
            if (splineAnimate == null || splineAnimate.Container == null)
                return null;

            return splineAnimate.Container.GetComponent<splineNode>();
        }
    }

    private int indiceSpline = 0;

    private void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
    }

    void Update()
    {
        var splineNodeAtual = SplineNodeAtual; // pega a spline atual do SplineAnimate
        if (splineNodeAtual == null || splineNodeAtual.Spline == null) return;

        var spline = splineNodeAtual.Spline;

        // avança no caminho
        float length = spline.CalculateLength(indiceSpline);
        progress += (speed / length) * Time.deltaTime;

        // chegou no fim
        if (progress >= 1f)
        {
            TrocarSpline();
            return;
        }

        // posicao e rotacao
        Vector3 pos = (Vector3)spline.EvaluatePosition(indiceSpline, progress);
        Vector3 tangent = ((Vector3)spline.EvaluateTangent(indiceSpline, progress)).normalized;
        Quaternion rot = tangent != Vector3.zero ? Quaternion.LookRotation(tangent) : transform.rotation;

        transform.SetPositionAndRotation(pos, rot);
    }

    void TrocarSpline()
    {
        var splineNodeAtual = SplineNodeAtual;
        if (splineNodeAtual == null) return;

        // pega a proxima spline
        SplineContainer nova = splineNodeAtual.GetProximaSpline();
        if (nova != null)
        {
            // atualiza a spline no SplineAnimate
            splineAnimate.Container = nova;
            splineAnimate.Restart(true); // reinicia animacao
            progress = 0f;
        }
    }
}
