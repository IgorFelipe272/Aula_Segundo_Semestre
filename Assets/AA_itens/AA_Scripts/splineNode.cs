using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class splineNode : MonoBehaviour
{
    [Header("Spline dessa rua")]
    public SplineContainer spline;

    public SplineContainer Spline => spline;


    [Header("Proximas ruas possiveis")]
    public List<splineNode> proximas; // conex�es possoveis

    public void Start()
    {
        spline = GetComponent<SplineContainer>();
    }
    public SplineContainer GetProximaSpline()
    {
        if (proximas.Count == 0) return null;

        // escolhe uma rua aleatoria entre as permitidas
        return proximas[Random.Range(0, proximas.Count)].spline;
    }
}
