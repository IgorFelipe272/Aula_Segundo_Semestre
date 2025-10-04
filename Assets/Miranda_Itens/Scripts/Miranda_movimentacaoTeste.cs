using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class Miranda_movimentacaoTeste : MonoBehaviour
{
    public List<Transform> wayPoint;

    public NavMeshAgent agent;

    public int currentWayPointIndex = 0;



    // Update is called once per frame
    void Update()
    {
        Walking();
    }

    public void Walking()
    {
        if(wayPoint.Count == 0)
        {
            return;
        }

        float distanceToWayPoint = Vector3.Distance(wayPoint[currentWayPointIndex].position, transform.position);

        if(distanceToWayPoint <= 1.5)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoint.Count;
        }

        agent.SetDestination(wayPoint[currentWayPointIndex].position);
    }
}
