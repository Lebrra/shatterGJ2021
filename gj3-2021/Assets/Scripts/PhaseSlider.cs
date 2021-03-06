using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PhaseSlider : MonoBehaviour
{
    [SerializeField] Material phaseMaterial = null;
    [SerializeField] float inTuneFrequency = 0.5f;
    [SerializeField] float range = 0.2f;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueChangeCheck();
    }

    public void ValueChangeCheck()
    {
        float solidity = Mathf.Clamp01(1 - Mathf.Abs(inTuneFrequency - slider.value) / range);
        phaseMaterial.SetFloat("_Solidity", solidity);
    }
}
