using UnityEngine;

public class ObjectID : MonoBehaviour
{
    public ObjectType Type;
    public enum ObjectType
    {
        Player,
        Grape,
        SmashTrigger,
        Desk,
        Basket,
        GrapeSmash,
    }
}
