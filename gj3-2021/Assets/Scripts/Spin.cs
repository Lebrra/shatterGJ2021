using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float speed = 180;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
