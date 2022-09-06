using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBowlController : MonoBehaviour
{
    [SerializeField] private BarrelSpawnArea barrelSpawnArea;
    [SerializeField] private Animator grapeSmashGirl;
    [SerializeField] private int grapeNeeded = 4;
    [SerializeField] private float workTime = 5f;
    [SerializeField] private int grapeCounter = 0;
    
    public bool active = false;
    public bool working = false;
    private bool once = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            active = true;
        }
    }

    public void PlayerGrapeDropping(int value)
    {
        grapeCounter = Mathf.Clamp(grapeCounter + value, 0, grapeNeeded);
    }

    // Update is called once per frame
    void Update()
    {
        if (barrelSpawnArea.barrelAreaMax)
        {
            working = true;
        }
        else
        {
            working = false;
        }
        
        if (grapeCounter == grapeNeeded)
        {
            working = true;
            if (!once)
            {
                StartCoroutine(SmashWorking());
                once = true;
            }
        }
    }

    private IEnumerator SmashWorking()
    {
        grapeSmashGirl.SetBool("working", true);
        yield return new WaitForSeconds(workTime);
        grapeSmashGirl.SetBool("working", false);
        working = false;
        once = false;
        grapeCounter = 0;
        GameEventHandler.current.BarrelGenerator();
        yield return null;
    }
}
