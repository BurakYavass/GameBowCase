using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaiterController : ObjectID
{
    public static WaiterController current;
    [SerializeField] private Animator waitorAnimator;
    [SerializeField] private WaiterStackList _waiterStackList;
    [SerializeField] private NavMeshAgent _waiterAgent;
    [SerializeField] private Transform barPoint;
    public List<Transform> CustomerPoint;

    private bool walking = false;

    private Transform customer;

    [SerializeField] private bool onWork =false;
    [SerializeField] private bool serving = false;
    private bool waitingWine = false;


    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }
    
    void Start()
    {
        GameEventHandler.current.CustomerServeWaiting += OnCustomerServeWaiting;
    }

    private void OnDestroy()
    {
        GameEventHandler.current.CustomerServeWaiting -= OnCustomerServeWaiting;
    }

    private void OnCustomerServeWaiting(Transform customerTransform)
    {
        //customer = customerTransform;
        CustomerPoint.Add(customerTransform);
    }
    
    void Update()
    {
        AnimationControl();

        AgentControl();

        WaitingCustomerCheck();
    }

    private void WaitingCustomerCheck()
    {
        for (int i = 0; i < CustomerPoint.Count; i++)
        {
            if (!onWork && _waiterStackList.stackList.Count > 1)
            {
                onWork = true;
                customer = CustomerPoint[i].transform;
                return;
            }
        }
    }

    private void AgentControl()
    {
        if (customer && _waiterStackList.stackList.Count >1)
        {
            waitingWine = false;
            if (!serving)
            {
                walking = true;
            }
            _waiterAgent.destination= customer.forward + Vector3.right;
            _waiterAgent.updateRotation = true;
            var dist = Vector3.Distance(transform.position, customer.position);
            if (dist < 7)
            {
                walking = false;
                _waiterAgent.isStopped = true;
                serving = true;
                // StartCoroutine
            }
        }
        else if(_waiterStackList.stackList.Count < 2)
        {
            serving = false;
            _waiterAgent.destination = barPoint.position;
            if (!waitingWine)
            {
                walking = true;
            }
            
            var dist = Vector3.Distance(transform.position, barPoint.position);
            if (dist < 3)
            {
                walking = false;
                waitingWine = true;
                _waiterAgent.isStopped = true;
                BarController.current.WaiterOnBar(1);
                _waiterAgent.updateRotation = false;
                _waiterAgent.transform.rotation = Quaternion.Euler(barPoint.forward);
            }
        }
        else if (!customer && _waiterStackList.stackList.Count > 1)
        {
            _waiterAgent.updateRotation = true;
            waitingWine = false;
            serving = false;
        }
    }
    

    private void AnimationControl()
    {
        if (walking)
        {
            waitorAnimator.SetBool("walking", true);

            waitorAnimator.SetBool("Idle", false);
            if (_waiterStackList.stackList.Count > 1)
            {
                waitorAnimator.SetBool("Idle", false);
                waitorAnimator.SetBool("carry", true);
                waitorAnimator.SetBool("walking", false);
                waitorAnimator.SetBool("carryidle", false);
            }
            else
            {
                waitorAnimator.SetBool("carry", false);
            }
        }
        else if (!walking)
        {
            waitorAnimator.SetBool("walking", false);
            waitorAnimator.SetBool("Idle", true);
            if (_waiterStackList.stackList.Count > 1)
            {
                waitorAnimator.SetBool("carryidle", true);
                waitorAnimator.SetBool("Idle", false);
                waitorAnimator.SetBool("carry", false);
                waitorAnimator.SetBool("walking", false);
            }
            else
            {
                waitorAnimator.SetBool("carryidle", false);
            }
        }
    }
}
