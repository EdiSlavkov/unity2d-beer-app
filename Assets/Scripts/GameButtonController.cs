using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonController : MonoBehaviour
{
    private List<int> userFavoritesBeerIdList;
    private SceneController sceneController;
    private const int MinimumBeersInFavorites = 3;
    private Button gameButton;

    private void Start()
    {
        gameButton = GetComponent<Button>();
        userFavoritesBeerIdList = FindObjectOfType<SessionManager>().GetLoggedUserFavorites();
        sceneController = FindObjectOfType<SceneController>();
        ChangeButtonInteractableState();
    }

    public void OnGameButtonClicked()
    {
        if (userFavoritesBeerIdList.Count < MinimumBeersInFavorites)
        {
            sceneController.LoadRandomBeerScene();
        }
    }

    public void ChangeButtonInteractableState()
    {
        if (gameButton.interactable != userFavoritesBeerIdList.Count > 0)
        {
            gameButton.interactable = userFavoritesBeerIdList.Count > 0;
        }
    }
}
