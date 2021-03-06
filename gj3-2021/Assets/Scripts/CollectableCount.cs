using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CollectableCount : MonoBehaviour
{
    private int total = 0;
    private TextMeshProUGUI textMeshPro = null;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public static CollectableCount operator ++(CollectableCount count)
    {
        count.total++;
        count.textMeshPro.text = count.total.ToString();
        return count;
    }

}
