using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    [SerializeField] private PlayerGrapeStackList grapeStackList;
    private ObjectType _objectID;

    private void OnCollisionEnter(Collision collision)
    {   
        _objectID = collision.gameObject.GetComponent<ObjectType>();
        
        if (_objectID == ObjectType.Basket && !grapeStackList.grapeStackMax)
        {
            collision.collider.isTrigger = true;
            grapeStackList.basketList.Add(collision.gameObject.transform);
        }
    }
}
