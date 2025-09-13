using UnityEngine;
using UnityEngine.AI;

public class Aula_PlayerClickMove : MonoBehaviour
{
    public Camera cam; // A c�mera principal
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (cam == null)
            cam = Camera.main; // Pega a c�mera principal se n�o tiver sido atribu�da
    }

    void Update()
    {
        // Verifica se o bot�o esquerdo do mouse foi clicado
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Faz um raycast para detectar onde voc� clicou no ch�o
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point); // Define o destino do NavMeshAgent
            }
        }
    }
}
