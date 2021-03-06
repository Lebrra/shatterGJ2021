using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetProgress(float progress)
    {
        rb.MovePosition(Vector2.Lerp(start.position, end.position, progress));
    }
}
