using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableCount collectableCount = null;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collectableCount++;
            Destroy(gameObject);
        }
    }
}
