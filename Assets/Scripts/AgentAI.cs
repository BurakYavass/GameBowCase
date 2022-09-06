using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : ObjectID
{
    [SerializeField] private GameObject _uiGameObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    private Camera camera;
    public Vector3 destinationPoint;
    public Vector3 forward;
    public Transform dropPoint;

    public bool arriveDestination = false;
    public bool waitingServe;

    void Start()
    {
        camera = Camera.main;
        AnimationAndUIControl();
    }
    
    void Update()
    {
        AnimationAndUIControl();
        _agent.destination = destinationPoint;
        if (Math.Abs(transform.position.x - destinationPoint.x) < 0.5f)
        {
            arriveDestination = true;
            _agent.updateRotation = false;
            waitingServe = true;
            _agent.transform.rotation = Quaternion.Euler(forward);
        }
        else
        {
            waitingServe = false;
        }
    }

    private void AnimationAndUIControl()
    {
        if (arriveDestination)
        {
            _animator.SetBool("Walking",false);
            _animator.SetBool("Sitting",true);
            _uiGameObject.SetActive(true);
            _uiGameObject.transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
                                                    camera.transform.rotation * Vector3.up);
        }
        else
        {
            _uiGameObject.SetActive(false);
            _animator.SetBool("Walking",true);
            _animator.SetBool("Sitting",false);
        }
    }
}
