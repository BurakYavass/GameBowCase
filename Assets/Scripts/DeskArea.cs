using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskArea : ObjectID
{
    public static DeskArea current;
    public List<ChairCheck> Desks;
    private bool once = false;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }
    
    
    void Update()
    {
        for (var i = 0; i < Desks.Count; i++)
        {
            if (Desks[i].deskState == ChairCheck.DeskState.Empty && !once )
            {
                GameEventHandler.current.EmptyDesk(Desks[i].transform, Desks[i].transform.eulerAngles);
                Desks[i].StateChanger(ChairCheck.DeskState.Full);
                StopCoroutine(Waiter());
                StartCoroutine(Waiter());
                return;
            }
        }
    }

    public void DeskStateChange(string emptyDesk)
    {
        for (var i = 0; i < Desks.Count; i++)
        {
            if (Desks[i].deskState == ChairCheck.DeskState.Full && once)
            {
                var findIndex = Desks.FindIndex(x => x.name == emptyDesk);
                Desks[findIndex].deskState = ChairCheck.DeskState.Empty;
                return;
            }
        }
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(10f);
        once = false;
        yield return null;
    }
}
