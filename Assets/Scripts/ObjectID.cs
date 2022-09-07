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
        Customer,
        BarrelSpawnArea,
        DustBin,
        PlayerUpgrade,
        Waiter,
    }
    public ObjectType Type;
}
