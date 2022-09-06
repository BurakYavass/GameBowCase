using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : ObjectID
{
    [SerializeField] private GameObject _uiGameObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform killPoint;
    private Camera camera;
    private Vector3 firstPoint;
    public Vector3 destinationPoint;
    public Vector3 forward;
    public Transform dropPoint;

    public bool arriveDestination = false;
    public bool waitingServe;
    public bool agentLeaving = false;
    public int wine = 0;

    void Start()
    {
        camera = Camera.main;
        firstPoint = transform.position;
        AgentControl();
    }
    
    void Update()
    {
        AgentControl();
        _agent.destination = destinationPoint;
        if (Math.Abs(transform.position.x - destinationPoint.x) < 0.2f)
        {
            arriveDestination = true;
            _agent.updateRotation = false;
            _agent.transform.rotation = Quaternion.Euler(forward);
        }
    }

    private void AgentControl()
    {
        if (arriveDestination)
        {
            waitingServe = true;
            _animator.SetBool("Walking",false);
            _animator.SetBool("Sitting",true);
            _uiGameObject.SetActive(true);
            _uiGameObject.transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
                                                    camera.transform.rotation * Vector3.up);
        }
        else
        {
            waitingServe = false;
            _animator.SetBool("Walking",true);
            _animator.SetBool("Sitting",false);
        }

        if (agentLeaving)
        {
            destinationPoint = firstPoint;
            _uiGameObject.SetActive(false);
            _agent.updateRotation = true;
            waitingServe = false;
            _animator.SetBool("GetUp",true);
            _animator.SetBool("Walking",true);
            StartCoroutine(KillingHimself());
        }
    }

    public void StateChange(bool value)
    {
        wine += 1;
        agentLeaving = true;
        StopCoroutine(Drink());
        StartCoroutine(Drink());
    }

    IEnumerator Drink()
    {
        yield return new WaitForSeconds(5.0f);
        //DeskArea.current.DeskStateChange();
        agentLeaving = true;
        arriveDestination = false;
        GameManager.current.playerGold += 10.0f;
        yield return null;
    }

    IEnumerator KillingHimself()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
        yield return null;
    }
}
