using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerCreator : MonoBehaviour
{
    public List<GameObject> customers = new List<GameObject>();
    [SerializeField] private Transform customerSpawnPoint;
    void Start()
    {
        GameEventHandler.current.ActiveEmptyDesk += CustomerGenerate;
    }

    private void OnDestroy()
    {
        GameEventHandler.current.ActiveEmptyDesk -= CustomerGenerate;
    }
    
    private void CustomerGenerate(Transform bosmasa,Vector3 deneme)
    {
        var clone = Instantiate(customers[Random.Range(0,customers.Count)],customerSpawnPoint.position,customerSpawnPoint.transform.rotation)as GameObject;
        var agent = clone.GetComponent<AgentAI>();
        agent.destinationPoint = bosmasa.transform;
        agent.forward = deneme;
    }
}
