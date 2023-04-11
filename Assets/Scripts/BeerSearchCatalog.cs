using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeerSearchCatalog : MonoBehaviour
{
    [SerializeField] private CatalogCard card;
    [SerializeField] private GameObject cardContainer;
    [SerializeField] private GameObject noResultMessage;
    [SerializeField] private GameObject noMorePagesMessage;
    [SerializeField] private GameObject serverErrorMessage;
    [SerializeField] private GameObject loader;
    [SerializeField] private RectTransform cardContainerRectTransform;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TMP_InputField searchInputField;
    private List<CatalogCard> cardPool = new List<CatalogCard>();
    private bool isRequestFinished;
    private int currentPage = 1;
    private int resultsPerPage = 25;
    private Filter filter;

    private void Start()
    {
        filter = FindObjectOfType<Filter>();
        for (int i = 0; i < resultsPerPage; i++)
        {
            CatalogCard cardInstance = Instantiate(card, cardContainer.transform);
            cardInstance.gameObject.SetActive(false);
            cardPool.Add(cardInstance);
        }
        StartCoroutine(RequestAPI.MakeBeersRequest(filter.CreatePageURLParameter(currentPage, resultsPerPage), InstantiateCardsFromAPIResponse, HandleConnectionError));
    }

    private void SendRequestForBeers()
    {
        isRequestFinished = false;
        loader.SetActive(true);
        StartCoroutine(RequestAPI.MakeBeersRequest($"{filter.CreatePageURLParameter(currentPage, resultsPerPage)}{filter.GetFilters()}", InstantiateCardsFromAPIResponse, HandleConnectionError));
    }

    private void InstantiateCardsFromAPIResponse(List<Beer> beers)
    {
        isRequestFinished = true;
        if (beers.Count > 0)
        {
            foreach (Beer beer in beers)
            {
                CatalogCard card = GetAvailableCard(beer.Id);
                card.gameObject.SetActive(true);
                if (card.IsEmpty())
                {
                    card.SetCardDetails(beer);
                }
            }
        }
        noMorePagesMessage.SetActive(beers.Count == 0 && currentPage != 1);
        noResultMessage.SetActive(beers.Count == 0 && currentPage == 1);
        loader.SetActive(false);
    }

    private CatalogCard GetAvailableCard(int id)
    {
        foreach (CatalogCard card in cardPool)
        {
            if (card.IsEmpty() || card.GetId() == id)
            {
                return card;
            }
        }
        return CreateNewCardInstance();
    }

    private CatalogCard CreateNewCardInstance()
    {
        CatalogCard newCardInstance = Instantiate(card, cardContainer.transform);
        newCardInstance.gameObject.SetActive(false);
        cardPool.Add(newCardInstance);
        return newCardInstance;
    }

    public void OnScrollPanelDragLoadNextPage()
    {
        if (cardContainerRectTransform.anchoredPosition.y > cardContainerRectTransform.sizeDelta.y)
        {
            StartCoroutine(LoadNextPage());
        }
    }

    private void HandleConnectionError(bool hasError)
    {
        if (serverErrorMessage.activeSelf != hasError)
        {
            serverErrorMessage.SetActive(hasError);
        }
        if (hasError)
        {
            HideContentInContainer();
        }
    }

    public void OnSearchInputChange()
    {
        filter.SetNameInputText(searchInputField.text);
    }

    public void OnCloseFilterPanel()
    {
        searchInputField.text = filter.GetNameInputText();
    }

    public void OnSearchButtonClick()
    {
        currentPage = 1;
        SendRequestForBeers();
        HideContentInContainer();
    }

    public void OnApplyFilterButtonClick()
    {
        currentPage = 1;
        SendRequestForBeers();
        HideContentInContainer();
        OnCloseFilterPanel();
    }

    private void HideContentInContainer()
    {
        foreach (Transform card in cardContainer.transform)
        {
            card.gameObject.SetActive(false);
        }
    }

    private IEnumerator LoadNextPage()
    {
        scrollRect.enabled = false;
        currentPage++;
        SendRequestForBeers();
        while (!isRequestFinished)
        {
            yield return null;
        }
        yield return new WaitForSeconds(noMorePagesMessage.activeSelf ? 1f : 0f);
        if (noMorePagesMessage.activeSelf)
        {
            noMorePagesMessage.SetActive(false);
        }
        scrollRect.enabled = true;
    }
}