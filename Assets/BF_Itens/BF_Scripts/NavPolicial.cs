using UnityEngine;
using UnityEngine.AI;

public class NavPolicial : MonoBehaviour
{
    public Transform carro;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        agent.destination = carro.position;
    }
}