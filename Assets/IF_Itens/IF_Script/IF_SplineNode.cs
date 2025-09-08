using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class IF_SplineNode : MonoBehaviour
{
    [Header("Spline dessa rua")]
    private SplineContainer spline;

    public SplineContainer Spline => spline;


    [Header("Próximas ruas possíveis")]
    public List<IF_SplineNode> proximas; // conexões possíveis

    private void Start()
    {
        spline = GetComponent<SplineContainer>();
    }
    public SplineContainer GetProximaSpline()
    {
        if (proximas.Count == 0) return null;

        // escolhe uma rua aleatória entre as permitidas
        return proximas[Random.Range(0, proximas.Count)].spline;
    }
}
