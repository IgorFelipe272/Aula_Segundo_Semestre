using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class IF_SplineNode : MonoBehaviour
{
    [Header("Spline dessa rua")]
    private SplineContainer spline;

    public SplineContainer Spline => spline;


    [Header("Pr�ximas ruas poss�veis")]
    public List<IF_SplineNode> proximas; // conex�es poss�veis

    private void Start()
    {
        spline = GetComponent<SplineContainer>();
    }
    public SplineContainer GetProximaSpline()
    {
        if (proximas.Count == 0) return null;

        // escolhe uma rua aleat�ria entre as permitidas
        return proximas[Random.Range(0, proximas.Count)].spline;
    }
}
