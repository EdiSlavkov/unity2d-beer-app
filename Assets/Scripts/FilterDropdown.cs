using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FilterDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown monthsDropdown;
    [SerializeField] private TMP_Dropdown yearsDropdown;
    [SerializeField] private int yearsToAdd = 10;
    [SerializeField] private int monthsToAdd = 12;

    private void Start()
    {
        PopulateDropdownWithMonths();
        PopulateDropdownWithYears();
    }

    private void PopulateDropdownWithYears()
    {
        List<string> years = new List<string>();
        years.Add("YYYY");
        int currentYear = DateTime.Now.Year;
        for (int i = 0; i < yearsToAdd; i++)
        {
            years.Add($"{currentYear - i}");
        }
        yearsDropdown.AddOptions(years);
    }

    private void PopulateDropdownWithMonths()
    {
        List<string> months = new List<string>();
        months.Add("MM");
        for (int i = 1; i <= monthsToAdd; i++)
        {
            months.Add(i < 10 ? i.ToString("00") : i.ToString());
        }
        monthsDropdown.AddOptions(months);
    }
}
