using System.Collections.Generic;
using UnityEngine;

public class FullBarrelArea : ObjectID
{
    public bool barrelsMax =false;
    public bool barIsWorkable =false;
    public int barrelCount = 0;
    private int barrelCountMax = 6;
    
    public List<Transform> Barrels;
    private ObjectID _otherId;
    [SerializeField] private PlayerStackList _playerStackList;
    
    public static FullBarrelArea current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
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
        barrelCount = Mathf.Clamp(barrelCount + value, 0, barrelCountMax);
        Barrels[barrelCount-1].gameObject.SetActive(true);
    }

    void Update()
    {
        if (barrelCount>0)
        {
            Barrels[0].gameObject.SetActive(true);
        }
        if (barrelCount == barrelCountMax)
        {
            barrelsMax = true;
        }
        else
        {
            barrelsMax = false;
        }
        
        if (Barrels[0].gameObject.activeInHierarchy)
        {
            barIsWorkable = true;
        }
        else
        {
            barIsWorkable = false;
        }
    }

    public void BarrelControl()
    {
        Barrels[barrelCount-1].gameObject.SetActive(false);
        barrelCount--;
    }

}
