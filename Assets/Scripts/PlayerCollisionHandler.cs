using UnityEngine;

public class PlayerCollisionHandler : ObjectID
{
    [SerializeField] private PlayerGrapeStackList grapeStackList;
    private ObjectID otherId;

    private void OnCollisionEnter(Collision collision)
    {
        if (otherId == null)
                otherId = collision.gameObject.GetComponent<ObjectID>();
        
        if (otherId.Type == ObjectType.Basket && !grapeStackList.grapeStackMax)
        {
            collision.collider.isTrigger = true;
            grapeStackList.basketList.Add(collision.gameObject.transform);
        }
    }
}
