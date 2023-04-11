using UnityEngine;

public class LogOut : MonoBehaviour
{
    private SessionManager sessionManager;
    private SceneController sceneController;

    private void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        sceneController = FindObjectOfType<SceneController>();
    }

    public void Logout()
    {
        sessionManager.Logout();
        sceneController.LoadLoginScene();
    }
}
