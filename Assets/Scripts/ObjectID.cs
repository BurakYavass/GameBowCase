using UnityEngine;

public class ObjectID : MonoBehaviour
{
    public enum ObjectType
    {
        Player,
        Grape,
        SmashTrigger,
        Desk,
        Basket,
        GrapeSmash,
        BarrelArea,
        Barrel,
        Bar,
    }
    public ObjectType Type;
}
