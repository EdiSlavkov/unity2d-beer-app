using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBeerController : MonoBehaviour
{
    [SerializeField] private FavoriteCard randomBeerCard;
    [SerializeField] private Transform container;
    private int randomBeerId;
    private List<int> userFavoritesBeerIdList;

    private void Start()
    {
        userFavoritesBeerIdList = FindObjectOfType<SessionManager>().GetLoggedUserFavorites();
        int randomBeerIndex = Random.Range(0, userFavoritesBeerIdList.Count);
        randomBeerId = userFavoritesBeerIdList[randomBeerIndex];
        StartCoroutine(RequestAPI.MakeBeersRequest($"ids={randomBeerId}", HandleRequest, null));
    }

    private void HandleRequest(List<Beer> beers)
    {
        if (beers.Count > 0)
        {
            FillCard(beers[0]);
        }
    }

    private void FillCard(Beer beer)
    {
        FavoriteCard card = Instantiate(randomBeerCard, container);
        card.SetCardDetails(beer);
    }
}
