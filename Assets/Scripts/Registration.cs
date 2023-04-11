using UnityEngine;
using UnityEngine.UI;
using Scripts;

public class Registration : MonoBehaviour
{
    [SerializeField] private Button registrationButton;
    [SerializeField] private InputManager inputManager;
    private SessionManager sessionManager;
    private SceneController sceneController;
    private string username = "";
    private string password = "";
    private string repeatPassword = "";

    private void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        sceneController = FindObjectOfType<SceneController>();
    }

    public void CreateUser()
    {
        if (sessionManager.HasUser(username))
        {
            inputManager.ShowErrorMessage(true);
        }
        else
        {
            sessionManager.HandleRegistration(username, password);
            sceneController.LoadHomeScene();
        }
    }

    public void ResetRegistrationForm()
    {
        inputManager.ClearInputs();
        inputManager.ShowErrorMessage(false);
    }

    public void SetRegistrationButtonInteractableState()
    {
        if (Validator.IsUserameValid(username) && Validator.IsPasswordValid(password) && password == repeatPassword)
        {
            registrationButton.interactable = true;
        }
        else if (registrationButton.IsInteractable())
        {
            registrationButton.interactable = false;
        }
    }

    public void SetUsername(string input)
    {
        username = input;
    }

    public void SetPassword(string input)
    {
        password = input;
    }

    public void SetRepeatPassword(string input)
    {
        repeatPassword = input;
    }

    public void ShowNameRequirementsMessage()
    {
        inputManager.ShowNameRequirementMessage(!string.IsNullOrEmpty(username) && !Validator.IsUserameValid(username));
    }

    public void ShowPasswordRequirementsMessage()
    {
        inputManager.ShowPasswordRequirementsMessage(!string.IsNullOrEmpty(password) && !Validator.IsPasswordValid(password));
    }

    public void ShowPasswordMatchRequirementMessage()
    {
        inputManager.ShowPasswordMatchMessage(password != repeatPassword);
    }
}