using System;
using Cinemachine;
using TMPro;
using ToonyColorsPro.ShaderGenerator;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    [SerializeField] private GameObject _uiGameObject;
    [SerializeField] private Animation _animation;
    private CinemachineVirtualCamera camera;
    private NavMeshAgent _agent;
    public Vector3 destinationPoint;
    public Vector3 forward;

    public bool arriveDestination = false;
    void Start()
    {
        _animation = GetComponent<Animation>();
        camera = FindObjectOfType<CinemachineVirtualCamera>();
        _agent = GetComponent<NavMeshAgent>();
        GameEventHandler.current.ActiveEmptyDesk += FixRotationAndPosition;
        AnimationAndUIControl();
    }

    private void FixRotationAndPosition(Vector3 deskposition,Vector3 rotation)
    {
        //destinationPoint = deskposition;
        //forward = rotation;
    }
    void Update()
    {
        AnimationAndUIControl();
        _agent.destination = destinationPoint;
        if (Math.Abs(transform.position.x - destinationPoint.x) < 0.5f)
        {
            arriveDestination = true;
            _agent.updateRotation = false;
            _agent.transform.rotation = Quaternion.Euler(forward);
        }
    }

    private void AnimationAndUIControl()
    {
        if (arriveDestination)
        {
            //_animation.clip = _animation.GetClip("SittingWomen");
            _animation["SittingWomen"].wrapMode = WrapMode.Once;
            _animation.Play();
            //_animation.SetBool("Sitting", true);
            _uiGameObject.SetActive(true);
            _uiGameObject.transform.LookAt(transform.position + camera.transform.rotation * Vector3.back,
                                                    camera.transform.rotation * Vector3.up);
        }
        else
        {
            _animation.Play("WalkingWomen");
            _uiGameObject.SetActive(false);
            //_animation.SetBool("Walking",true);
            //_animation.SetBool("Sitting",false);
        }
    }
}
