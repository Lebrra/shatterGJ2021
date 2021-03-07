using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PhaseSlider : MonoBehaviour
{
    [SerializeField] private Material phaseMaterial = null;
    [SerializeField] private string propertyName = null; 
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueChangeCheck();
    }

    public void ValueChangeCheck()
    {   
        phaseMaterial.SetFloat(propertyName, slider.value);
    }
}
