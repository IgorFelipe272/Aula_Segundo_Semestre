using UnityEngine;
using UnityEngine.AI;

public class NavPersegue : MonoBehaviour
{
    public Transform perseguido;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        agent.destination = perseguido.position;
    }
}