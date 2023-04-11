using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogCard : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image favoritesButtonImage;
    [SerializeField] private TextMeshProUGUI heading;
    [SerializeField] private TextMeshProUGUI shortDescription;
    private int id;
    private bool isEmpty = true;
    protected FullDetailsCard fullCard;
    protected SessionManager sessionManager;
    protected bool isInFavorites;

    private void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        fullCard = FindObjectOfType<FullDetailsCard>();
        isInFavorites = sessionManager.IsBeerInFavorites(id);
        ChangeFavoritesButtonColor();
    }

    private void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            image.sprite = sprite;
        }
    }

    private void SetHeading(string input)
    {
        heading.text = input;
    }

    private void SetDescription(string input)
    {
        shortDescription.text = input;
    }

    private void SetIid(int id)
    {
        this.id = id;
    }

    public virtual void SetCardDetails(Beer beer)
    {
        isEmpty = false;
        SetIid(beer.Id);
        SetHeading(beer.Name);
        SetDescription(beer.ShortDescription);
        if (beer.ImageUrl != null)
        {
            StartCoroutine(RequestAPI.MakeBeerSpriteRequest(beer.ImageUrl, SetImage));
        }
    }

    public int GetId()
    {
        return id;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public virtual void OnFavoritesButtonClicked()
    {
        isInFavorites = sessionManager.ToggleFavoriteBeer(id);
        ChangeFavoritesButtonColor();
    }

    protected void ChangeFavoritesButtonColor()
    {
        favoritesButtonImage.color = isInFavorites ? Color.red : Color.white;
    }

    public void OnCardClicked()
    {
        StartCoroutine(RequestAPI.MakeBeersRequest($"ids={id}", DisplayOneBeer, null));
    }

    private void DisplayOneBeer(List<Beer> beers)
    {
        if (beers.Count > 0)
        {
            fullCard.DisplayBeer(beers[0], isInFavorites);
            fullCard.OnFavoriteButtonClickedCallback = OnFavoritesButtonClicked;
        }
    }
}
