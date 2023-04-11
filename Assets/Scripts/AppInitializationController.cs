using System.Globalization;
using UnityEngine;

public class AppInitializationController : MonoBehaviour
{
    [SerializeField] private SessionManager sessionManager;
    [SerializeField] private SceneController sceneController;

    private void Start()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        if (sessionManager.HasActiveUser())
        {
            sessionManager.LoadLoggedUser();
            sceneController.LoadHomeScene();
        }
    }
}