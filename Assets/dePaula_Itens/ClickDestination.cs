using UnityEngine;
using UnityEngine.AI;

namespace dePaula
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickDestination : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo))
                {
                    agent.destination = hitInfo.point;
                }
            }

            if (agent.velocity.magnitude != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);

            }
        }
    }
}

