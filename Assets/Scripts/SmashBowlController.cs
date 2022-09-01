using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBowlController : MonoBehaviour
{
    [SerializeField] private float workTime = 5f;
    [SerializeField] private Animator grapeSmashGirl;
    public bool active = false;
    public bool working = false;
    private bool once = false;
    
    private int grapeNeeded = 4;
    public int grapeCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeInHierarchy)
        {
            active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grapeCounter == grapeNeeded)
        {
            working = true;
            if (!once)
            {
                StartCoroutine(SmashWorking());
                grapeSmashGirl.SetBool("working", true);
                once = true;
            }
        }
    }

    IEnumerator SmashWorking()
    {
        yield return new WaitForSeconds(workTime);
        grapeSmashGirl.SetBool("working", false);
        working = false;
        once = false;
        grapeCounter = 0;
        yield return null;
    }
}
