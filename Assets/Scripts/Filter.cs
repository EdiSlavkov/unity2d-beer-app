using Scripts;
using System.Text;
using TMPro;
using UnityEngine;

public class Filter : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField yeastInputField;
    [SerializeField] private TMP_InputField hopsInputField;
    [SerializeField] private TMP_InputField maltInputField;
    [SerializeField] private TMP_InputField foodInputField;
    [SerializeField] private DoubleSlider alcoholSlider;
    [SerializeField] private DoubleSlider biternessSlider;
    [SerializeField] private DoubleSlider eBCCSlider;
    [SerializeField] private TMP_Dropdown brewedBeforeMonthDropdown;
    [SerializeField] private TMP_Dropdown brewedBeforeYearDropdown;
    [SerializeField] private TMP_Dropdown brewedAfterMonthDropdown;
    [SerializeField] private TMP_Dropdown brewedAfterYearDropdown;
    public const string BrewedAfterTag = "brewed_after";
    public const string BrewedBeforeTag = "brewed_before";
    public const string YeastTag = "yeast";
    public const string HopsTag = "hops";
    public const string MaltTag = "malt";
    public const string BeerNameTag = "beer_name";
    public const string FoodTag = "food";
    public const string AlcoholTag = "abv";
    public const string BiternessTag = "ibu";
    public const string EbcTag = "ebc";
    public const int AlcoholMaxValue = 55;
    public const int BiternessMaxValue = 1157;
    public const int EBCMaxValue = 600;

    private void Start()
    {
        alcoholSlider.SetSliderMaxValue(AlcoholMaxValue);
        biternessSlider.SetSliderMaxValue(BiternessMaxValue);
        eBCCSlider.SetSliderMaxValue(EBCMaxValue);
    }

    private string CreateYeastURLParameter()
    {
        return CreatedURLParameter(YeastTag, yeastInputField.text);
    }

    private string CreateHopsURLParameter()
    {
        return CreatedURLParameter(HopsTag, hopsInputField.text);
    }

    private string CreateMaltURLParameter()
    {
        return CreatedURLParameter(MaltTag, maltInputField.text);
    }

    private string CreateNameURLParameter()
    {
        return CreatedURLParameter(BeerNameTag, nameInputField.text);
    }

    private string CreateFoodURLParameter()
    {
        return CreatedURLParameter(FoodTag, foodInputField.text);
    }

    private string CreatedURLParameter(string tag, string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            return $"&{tag}={input.Trim().Replace(" ", "_")}";
        }
        return "";
    }

    public string CreatePageURLParameter(int page, int resultsPerPage)
    {
        return $"&page={page}&per_page={resultsPerPage}";
    }

    private string CreateURLParameterFromSliderValue(string tag, DoubleSlider slider)
    {
        StringBuilder stringBuilder = new StringBuilder();
        float minValue = slider.GetCurrentMinValue();
        float maxValue = slider.GetCurrentMaxValue();
        if (minValue > 0)
        {
            stringBuilder.Append($"&{tag}_gt={Utils.RoundToOneDecimalPlace(minValue)}");
        }
        if (maxValue < slider.GetSliderMaxValue())
        {
            stringBuilder.Append($"&{tag}_lt={Utils.RoundToOneDecimalPlace(maxValue)}");
        }
        return stringBuilder.ToString();
    }

    private string CreateURLParameterFromDropdownValue(string searchTag, TMP_Dropdown month, TMP_Dropdown year)
    {
        string monthValue = month.options[month.value].text;
        string yearValue = year.options[year.value].text;
        if (month.value != 0 && year.value != 0)
        {
            return $"&{searchTag}={monthValue}-{yearValue}";
        }
        return "";
    }

    public void SetNameInputText(string value)
    {
        nameInputField.text = value;
    }

    public string GetNameInputText()
    {
        return nameInputField.text;
    }

    public string GetFilters()
    {
        StringBuilder filter = new StringBuilder();
        filter.Append(CreateNameURLParameter());
        filter.Append(CreateYeastURLParameter());
        filter.Append(CreateHopsURLParameter());
        filter.Append(CreateMaltURLParameter());
        filter.Append(CreateFoodURLParameter());
        filter.Append(CreateURLParameterFromSliderValue(AlcoholTag, alcoholSlider));
        filter.Append(CreateURLParameterFromSliderValue(BiternessTag, biternessSlider));
        filter.Append(CreateURLParameterFromSliderValue(EbcTag, eBCCSlider));
        filter.Append(CreateURLParameterFromDropdownValue(BrewedBeforeTag, brewedBeforeMonthDropdown, brewedBeforeYearDropdown));
        filter.Append(CreateURLParameterFromDropdownValue(BrewedAfterTag, brewedAfterMonthDropdown, brewedAfterYearDropdown));
        return filter.ToString();
    }

    public void ClearFilters()
    {
        nameInputField.text = "";
        yeastInputField.text = "";
        hopsInputField.text = "";
        maltInputField.text = "";
        foodInputField.text = "";
        alcoholSlider.SetSliderMaxValue(AlcoholMaxValue);
        biternessSlider.SetSliderMaxValue(BiternessMaxValue);
        eBCCSlider.SetSliderMaxValue(EBCMaxValue);
        brewedBeforeMonthDropdown.value = 0;
        brewedBeforeYearDropdown.value = 0;
        brewedAfterMonthDropdown.value = 0;
        brewedAfterYearDropdown.value = 0;
    }
}