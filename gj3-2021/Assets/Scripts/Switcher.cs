using UnityEngine;

public class Switcher : MonoBehaviour
{
    private int currentIndex = 0;

    void Start()
    {
        transform.GetChild(currentIndex).gameObject.SetActive(true);
    }

    public void SwitchTo(float index)
    {
        SwitchTo((int)index);
    }

    public void SwitchTo(int index)
    {
        if(currentIndex != index)
        {
            transform.GetChild(currentIndex).gameObject.SetActive(false);
            transform.GetChild(index).gameObject.SetActive(true);
            currentIndex = index;
        }
    }
}
