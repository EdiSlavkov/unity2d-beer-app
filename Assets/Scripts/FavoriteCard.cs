using System;
using TMPro;
using UnityEngine;

public class FavoriteCard : CatalogCard
{
    [SerializeField] private TextMeshProUGUI fullDetails;
    [SerializeField] private TextMeshProUGUI alcohol;
    [SerializeField] private TextMeshProUGUI biterness;
    [SerializeField] private TextMeshProUGUI ebc;
    [SerializeField] private TextMeshProUGUI foodOptions;
    public Action OnCardStatusChanged;

    public override void SetCardDetails(Beer beer)
    {
        base.SetCardDetails(beer);
        fullDetails.text = beer.FullDescription;
        alcohol.text = $"Alcohol: {beer.Abv} %";
        biterness.text = $"Biterness: {beer.Ibu} IBU";
        ebc.text = $"EBC: {beer.Ebc}";
        foreach (string foodPairing in beer.FoodPairing)
        {
            foodOptions.text += $"* {foodPairing}.\n";
        }
    }

    public override void OnFavoritesButtonClicked()
    {
        base.OnFavoritesButtonClicked();
        gameObject.SetActive(isInFavorites);
        OnCardStatusChanged?.Invoke();
    }
}