using UnityEngine;

public class ObjectID : MonoBehaviour
{
    public ObjectType Type;
    public enum ObjectType
    {
        Player,
        Grape,
        SmashArea,
        Desk,
        Basket,
    }
}
