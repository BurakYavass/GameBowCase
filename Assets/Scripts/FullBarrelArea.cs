using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBarrelArea : ObjectID
{
    public bool barrelsMax =false;
    public bool barIsWorkable =false;
    public int barrelCount;
    private int barrelCountMax = 6;
    
    public List<Transform> Barrels;
    private ObjectID _otherId;
    [SerializeField] private PlayerStackList _playerStackList;
    
    public static FullBarrelArea current;

    private void Awake()
    {
        current = this;
    }
    
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
        if (Barrels[0].gameObject.activeInHierarchy)
        {
            barIsWorkable = true;
        }
        else
        {
            barIsWorkable = false;
        }
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
