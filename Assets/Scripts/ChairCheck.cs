using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChairCheck : MonoBehaviour
{
    public DeskState deskState;

    private void OnEnable()
    {
        StateChanger(DeskState.Empty);
    }

    public void StateChanger(DeskState state)
    {
        deskState = state;
        switch (state)
        {
            case DeskState.None:
                break;
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
       None,
       Empty,
       Full,
   }
}
