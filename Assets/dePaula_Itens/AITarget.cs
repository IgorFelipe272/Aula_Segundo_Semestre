using UnityEngine;
using UnityEngine.AI;

namespace dePaula
{
    [RequireComponent (typeof(NavMeshAgent))]
    public class AITarget : MonoBehaviour
    {
        [SerializeField] Transform target;
        NavMeshAgent agent;
        Animator animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            agent.destination = target.position;

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