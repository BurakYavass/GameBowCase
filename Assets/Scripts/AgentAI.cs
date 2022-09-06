using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : ObjectID
{
    //[SerializeField] private GameObject _uiGameObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    private Camera camera;
    public Transform destinationPoint;
    public Vector3 forward;
    public Transform dropPoint;

    public bool arriveDestination = false;
    public bool waitingServe;
    public bool agentLeaving = false;
    public int wine = 0;
    private Vector3 desiredPosition;

    void Start()
    {
        camera = Camera.main;
        AgentControl();
        desiredPosition = destinationPoint.position;
    }
    
    void Update()
    {
        AgentControl();
        _agent.destination = desiredPosition;
        if (Math.Abs(transform.position.x - destinationPoint.position.x) < 0.2f)
        {
            arriveDestination = true;
            _agent.updateRotation = false;
            _agent.transform.rotation = Quaternion.Euler(forward);
        }
    }

    private void AgentControl()
    {
        if (arriveDestination && wine == 0)
        {
            waitingServe = true;
            _animator.SetBool("Walking",false);
            _animator.SetBool("Sitting",true);
            //_uiGameObject.SetActive(true);
            //_uiGameObject.transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
                                                    //camera.transform.rotation * Vector3.up);
        }
        else
        {
            waitingServe = false;
            _animator.SetBool("Walking",true);
            _animator.SetBool("Sitting",false);
        }

        if (agentLeaving)
        {
            _agent.updateRotation = true;
            waitingServe = false;
            _animator.SetBool("Walking",true);
            StartCoroutine(KillingHimself());
        }
    }

    public void StateChange()
    {
        wine += 1;
        GameManager.current.PlayerMoneyIncrease(10);
        StopCoroutine(Drink());
        StartCoroutine(Drink());
    }

    private IEnumerator Drink()
    {
        //_uiGameObject.SetActive(false);
        yield return new WaitForSeconds(10.0f);
        _animator.SetBool("GetUp",true);
        agentLeaving = true;
        arriveDestination = false;
        DeskArea.current.DeskStateChange(destinationPoint.name);
        yield return null;
    }

    private IEnumerator KillingHimself()
    {
        desiredPosition = new Vector3(39.0f, 2.0f, -14.0f);
        if (_agent.destination == desiredPosition)
        {
            Destroy(this.gameObject);
        }
        else
        {
            yield return new WaitForSeconds(10.0f);
            Destroy(this.gameObject);
            yield return null;
        }
        
    }
}
