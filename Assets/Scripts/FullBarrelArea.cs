using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBarrelArea : ObjectID
{
    public List<Transform> Barrels;
    [SerializeField] private GameObject barBarrel;
    [SerializeField] private PlayerStackList _playerStackList;

    public bool barrelsMax =false;

    private ObjectID _otherId;

    public int barrelCount;
    private int barrelCountMax = 6;

    // Start is called before the first frame update
    void Start()
    {
        _playerStackList.Dropping += PlayerBarrelDropping;
    }

    private void OnDestroy()
    {
        _playerStackList.Dropping -= PlayerBarrelDropping;
    }

    private void PlayerBarrelDropping(int value)
    {
        //barrelCount = value;
        barrelCount = Mathf.Clamp(barrelCount + value, 0, barrelCountMax);
        Barrels[barrelCount-1].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // if (barrelCount > 0)
        // {
        //     barBarrel.SetActive(true);
        // }
        if (barrelCount == 6)
        {
            barrelsMax = true;
        }
        else
        {
            barrelsMax = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_otherId == null || !_playerStackList)
        {
            _otherId = other.gameObject.GetComponent<ObjectID>();
            _playerStackList = other.gameObject.GetComponent<PlayerStackList>();
        }
            
        if (_otherId.Type == ObjectType.Player && !barrelsMax)
        {
            GameEventHandler.current.BarrelDropping();
        }
    }
}
