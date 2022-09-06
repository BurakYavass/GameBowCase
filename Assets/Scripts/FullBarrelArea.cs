using System.Collections.Generic;
using UnityEngine;

public class FullBarrelArea : ObjectID
{
    public bool barrelsMax =false;
    public bool barIsWorkable =false;
    public int barrelCount = 1;
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
        Barrels[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (barrelCount > 0 )
        {
            if (barrelCount == 7)
            {
                barrelsMax = true;
            }
            else
            {
                barrelsMax = false;
            }
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

    // public void BarrelSetter()
    // {
    //     Barrels[barrelCount].gameObject.SetActive(false);
    //     barrelCount--;
    // }
}
