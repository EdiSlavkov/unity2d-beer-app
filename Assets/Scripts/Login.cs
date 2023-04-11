using Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject errorMsg;
    [SerializeField] private Button button;
    private string username = "";
    private string password = "";
    private SessionManager sessionManager;
    private SceneController sceneController;

    private void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        sceneController = FindObjectOfType<SceneController>();
    }

    public void SetUsername(string input)
    {
        username = input;
    }  
    
    public void SetPassword(string input)
    {
        password = input;
    }

    public void HideErrorMessage()
    {
        if (errorMsg.activeSelf)
        {
            errorMsg.SetActive(false);
        }
    }

    public void ValidateInputs()
    {
        if (username.Length > 0 && password.Length > 0)
        {
            button.interactable = true;
        }
        else if (button.IsInteractable())
        {
            button.interactable = false;
        }
    }

    public void SignIn()
    {
        User user = sessionManager.GetUser(username);
        if (user != null && user.password == password)
        {
            sessionManager.SetLoggedUser(user);
            sceneController.LoadHomeScene();
        }
        else
        {
            errorMsg.SetActive(true);
        }
    }
}