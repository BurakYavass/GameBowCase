using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    [SerializeField] private PlayerGrapeStackList grapeStackList;
    private ObjectID _objectID;

    private void OnCollisionEnter(Collision collision)
    {
        if (_objectID == null)
            _objectID = collision.gameObject.GetComponent<ObjectID>();
        
        if (_objectID.Type == ObjectType.Basket && !grapeStackList.grapeStackMax)
        {
            collision.collider.isTrigger = true;
            grapeStackList.basketList.Add(collision.gameObject.transform);
        }
    }
}
