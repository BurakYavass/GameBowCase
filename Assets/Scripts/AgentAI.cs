using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    public Vector3 deskPoint;
    // Start is called before the first frame update

    private NavMeshAgent _agent;

    public bool geldik = false;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = deskPoint;
        //_agent.SetDestination(deskPoint);
        if (Math.Abs(transform.position.x - deskPoint.x) < 1)
        {
            geldik = true;
        }
    }
}
