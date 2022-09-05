using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeskCheck : MonoBehaviour
{
    private bool deskEmpty = false;
    public DeskState deskState;

    private void StateChanger(DeskState state)
    {
        deskState = state;
        switch (state)
        {
            case DeskState.Empty:
                break;
            case DeskState.Full:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    public enum DeskState
   {
       Empty,
       Full,
   }
}
