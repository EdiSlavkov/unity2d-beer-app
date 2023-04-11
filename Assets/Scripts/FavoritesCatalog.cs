using System.Collections.Generic;
using UI.Pagination;
using UnityEngine;

public class FavoritesCatalog : MonoBehaviour
{
    [SerializeField] private GameObject emptyFavoritesMessage;
    [SerializeField] private GameButtonController gameButtonController;
    [SerializeField] private PagedRect pageRect;
    private List<int> userFavoritesBeerIdList;

    private void Start()
    {
        userFavoritesBeerIdList = FindObjectOfType<SessionManager>().GetLoggedUserFavorites();
        if (userFavoritesBeerIdList.Count > 0)
        {
            string ids = string.Join("|", userFavoritesBeerIdList);
            StartCoroutine(RequestAPI.MakeBeersRequest($"ids={ids}", DisplayFavoriteBeers, null));
        }
        else
        {
            emptyFavoritesMessage.SetActive(true);
        }
    }

    private void DisplayFavoriteBeers(List<Beer> beers)
    {
        foreach (Beer beer in beers)
        {
            Page page = pageRect.AddPageUsingTemplate();
            FavoriteCard card = page.GetComponent<FavoriteCard>();
            card.SetCardDetails(beer);
            card.OnCardStatusChanged = OnChildStatusChanged;
        }
        pageRect.SetClosestPage();
    }

    public void OnChildStatusChanged()
    {
        emptyFavoritesMessage.SetActive(userFavoritesBeerIdList.Count == 0);
        pageRect.SetClosestPage();
        gameButtonController.ChangeButtonInteractableState();
    }
}