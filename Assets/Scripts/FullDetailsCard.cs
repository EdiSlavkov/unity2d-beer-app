using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullDetailsCard : CatalogCard
{
    [SerializeField] private TextMeshProUGUI fullDescription;
    [SerializeField] private TextMeshProUGUI firstBrewed;
    [SerializeField] private TextMeshProUGUI alcohol;
    [SerializeField] private TextMeshProUGUI biterness;
    [SerializeField] private TextMeshProUGUI malt;
    [SerializeField] private TextMeshProUGUI hops;
    [SerializeField] private TextMeshProUGUI yeast;
    [SerializeField] private GameObject foodOption;
    [SerializeField] private GameObject foodOptionsContainer;
    [SerializeField] private TextMeshProUGUI brewerTip;
    [SerializeField] private TextMeshProUGUI contributedBy;
    [SerializeField] private ScrollRect scrollRect;
    public Action OnFavoriteButtonClickedCallback;

    public override void SetCardDetails(Beer beer)
    {
        base.SetCardDetails(beer);
        scrollRect.verticalNormalizedPosition = 1;
        fullDescription.text = beer.FullDescription;
        firstBrewed.text = $"<b>First Brewed:</b> {beer.FirstBrewed}";
        alcohol.text = $"<b>Alcohol By Volume:</b> {beer.Abv} %";
        biterness.text = $"<b>Biterness:</b> {beer.Ibu} IBU";
        malt.text = $"<b>Malt:</b> {GetIngredients(beer.Ingredients.Malt)}";
        hops.text = $"<b>Hops:</b> {GetIngredients(beer.Ingredients.Hops)}";
        yeast.text = $"<b>Yeast:</b> {beer.Ingredients.Yeast}";
        FillFoodOptionsContainer(beer.FoodPairing);
        brewerTip.text = $"<uppercase><b>Brewer's tip:</b></uppercase> \"{beer.BrewersTips}\"";
        contributedBy.text = $"<b>Contributed by:</b> \n{GetContributerName(beer.ContributedBy)}";
    }

    public void DisplayBeer(Beer beer, bool isInFavorites)
    {
        this.isInFavorites = isInFavorites;
        ClearFoodOptionsContainer();
        SetCardDetails(beer);
        ChangeFavoritesButtonColor();
        GetComponent<Toggle>().ChangeToggleStatus();
    }

    private string GetContributerName(string text)
    {
        return Regex.Replace(text, "<.*?>", "");
    }

    public override void OnFavoritesButtonClicked()
    {
        isInFavorites = !isInFavorites;
        ChangeFavoritesButtonColor();
        OnFavoriteButtonClickedCallback?.Invoke();
    }

    private string GetIngredients<Ingredients>(List<Ingredients> ingredientList)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < ingredientList.Count; i++)
        {
            stringBuilder.Append(ingredientList[i].ToString());
            stringBuilder.Append(i == ingredientList.Count - 1 ? "." : ", ");
        }
        return stringBuilder.ToString();
    }


    private void FillFoodOptionsContainer(List<string> foodPairingList)
    {
        for (int i = 0; i < foodPairingList.Count; i++)
        {
            GameObject instance = Instantiate(foodOption, foodOptionsContainer.transform);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = foodPairingList[i];
            if (i % 2 == 0)
            {
                instance.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
            }
        }
    }

    private void ClearFoodOptionsContainer()
    {
        foreach (Transform foodOption in foodOptionsContainer.transform)
        {
            Destroy(foodOption.gameObject);
        }
    }
}