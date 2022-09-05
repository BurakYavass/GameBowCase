using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Vector3 deskPoint;

    private Vector3 forward;

    public bool arriveDestination = false;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameEventHandler.current.ActiveEmptyDesk += FixRotationAndPosition;
        AnimationControl();
    }

    private void FixRotationAndPosition(Vector3 bos,Vector3 rotation)
    {
        _agent.destination = deskPoint;
        forward = rotation;
    }
    void Update()
    {
        //_agent.SetDestination(deskPoint);
        if (Math.Abs(transform.position.x - deskPoint.x) < 0.5f)
        {
            arriveDestination = true;
            _agent.updateRotation = false;
            _agent.transform.rotation = Quaternion.Euler(forward);
        }
    }

    private void AnimationControl()
    {
        if (arriveDestination)
        {
            
        }
    }
}
