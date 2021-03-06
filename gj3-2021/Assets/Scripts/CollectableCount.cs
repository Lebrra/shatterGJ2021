using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CollectableCount : MonoBehaviour
{
    public int Total { get; private set; } = 0;
    private TextMeshProUGUI textMeshPro = null;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public static CollectableCount operator ++(CollectableCount count)
    {
        count.Total++;
        count.textMeshPro.text = count.Total.ToString();
        return count;
    }

    public void Clear()
    {
        Total = 0;
        textMeshPro.text = Total.ToString();
    }
}
