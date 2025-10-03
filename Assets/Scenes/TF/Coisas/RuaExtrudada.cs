using UnityEngine;
using UnityEngine.Splines;

public class RuaExtrudada : MonoBehaviour
{
    public float larguraRua = 4f;
    public int segmentos = 50;

    void Start()
    {
        var spline = GetComponent<SplineContainer>().Spline;
        var mesh = new Mesh();
        var vertices = new Vector3[segmentos * 2];
        var triangles = new int[(segmentos - 1) * 6];

        for (int i = 0; i < segmentos; i++)
        {
            float t = i / (float)(segmentos - 1);
            Vector3 pos = (Vector3)spline.EvaluatePosition(t);
            Vector3 tangent = ((Vector3)spline.EvaluateTangent(t)).normalized;
            Vector3 up = SplineUtility.CalculateUpVector(spline, t);
            Vector3 normal = Vector3.Cross(tangent, up).normalized;

            vertices[i * 2] = pos + normal * (larguraRua / 2f);
            vertices[i * 2 + 1] = pos - normal * (larguraRua / 2f);

            if (i < segmentos - 1)
            {
                int idx = i * 6;
                int v = i * 2;
                triangles[idx] = v;
                triangles[idx + 1] = v + 2;
                triangles[idx + 2] = v + 1;
                triangles[idx + 3] = v + 1;
                triangles[idx + 4] = v + 2;
                triangles[idx + 5] = v + 3;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        var mf = gameObject.AddComponent<MeshFilter>();
        var mr = gameObject.AddComponent<MeshRenderer>();
        mf.mesh = mesh;
        mr.material = new Material(Shader.Find("Standard"));
    }
}
