using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskArea : ObjectID
{
    public static DeskArea current;
    public List<DeskCheck> Desks;

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
            if (Desks[i].deskState == DeskCheck.DeskState.Empty)
            {
                GameEventHandler.current.EmptyDesk(Desks[i].transform.position);
                return;
            }
        }
    }
}
