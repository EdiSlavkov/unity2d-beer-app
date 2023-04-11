using Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSlider : MonoBehaviour
{
    [SerializeField] private Slider sliderForMin;
    [SerializeField] private Slider sliderForMax;
    [SerializeField] private TMP_InputField sliderMinValueText;
    [SerializeField] private TMP_InputField sliderMaxValueText;
    [SerializeField] private float gap;
    private float currentMinValue;
    private float currentMaxValue;

    public void OnSliderMinValueChanged()
    {
        currentMinValue = sliderForMin.value;
        if (currentMinValue + gap > currentMaxValue)
        {
            sliderForMin.value = currentMaxValue - gap;
        }
        sliderMinValueText.text = Utils.RoundToOneDecimalPlace(currentMinValue);
    }

    public void OnSliderMaxValueChanged()
    {
        currentMaxValue = sliderForMax.maxValue - sliderForMax.value;
        if (currentMaxValue - gap < currentMinValue)
        {
            sliderForMax.value = sliderForMax.maxValue - (currentMinValue + gap);

        }
        sliderMaxValueText.text = Utils.RoundToOneDecimalPlace(currentMaxValue);
    }

    public float GetCurrentMinValue()
    {
        return currentMinValue;
    }

    public float GetCurrentMaxValue()
    {
        return currentMaxValue;
    }

    public void SetSliderMaxValue(float value)
    {
        sliderForMin.maxValue = value;
        sliderForMin.value = 0;
        sliderForMax.maxValue = value;
        sliderForMax.value = 0;
        currentMaxValue = value;
    }

    public float GetSliderMaxValue()
    {
        return sliderForMax.maxValue;
    }
}
