using UnityEngine;
using UnityEngine.AI;

public class Aula_PlayerClickMove : MonoBehaviour
{
    public Camera cam; // A câmera principal
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (cam == null)
            cam = Camera.main; // Pega a câmera principal se não tiver sido atribuída
    }

    void Update()
    {
        // Verifica se o botão esquerdo do mouse foi clicado
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Faz um raycast para detectar onde você clicou no chão
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point); // Define o destino do NavMeshAgent
            }
        }
    }
}
