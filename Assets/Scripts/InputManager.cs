using Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject messageGroup;
    [SerializeField] private List<TMP_InputField> inputs;
    [SerializeField] private TextMeshProUGUI nameMessage;
    [SerializeField] private TextMeshProUGUI passwordMatchMessage;
    [SerializeField] private TextMeshProUGUI passwordMessage;
    [SerializeField] private TextMeshProUGUI oldPasswordMessage;
    [SerializeField] private TextMeshProUGUI invalidPasswordMessage;
    [SerializeField] private TextMeshProUGUI confirmPasswordMessage;
    [SerializeField] private TextMeshProUGUI successMessage;
    [SerializeField] private TextMeshProUGUI errorMessage;
    private int errorMessageCounter;

    private void Start()
    {
        if (nameMessage != null)
        {
            nameMessage.text = $"* Username: {Utils.UsernameRequirementLength} characters with uppercase and lowercase";
        }
        passwordMessage.text = $"* Password: {Utils.PasswordRequirementLength} characters with uppercase, lowercase and number";
    }

    public void ClearInputs()
    {
        foreach (TMP_InputField input in inputs)
        {
            input.text = "";
        }
    }

    public void ShowPasswordRequirementsMessage(bool shouldShow)
    {
        HandleMessageState(passwordMessage, shouldShow);
    }

    private void HandleMessageState(TextMeshProUGUI message, bool shouldShow)
    {
        if (!message.gameObject.activeSelf && shouldShow)
        {
            message.gameObject.SetActive(shouldShow);
            errorMessageCounter++;
        }
        else if (message.gameObject.activeSelf && !shouldShow)
        {
            message.gameObject.SetActive(shouldShow);
            errorMessageCounter--;
        }
        ShowMessageGroup();
    }

    public void ShowPasswordMatchMessage(bool shouldShow)
    {
        HandleMessageState(passwordMatchMessage, shouldShow);
    }

    public void ShowOldPasswordMessage(bool shouldShow)
    {
        HandleMessageState(oldPasswordMessage, shouldShow);
    }

    public void ShowinvalidPasswordMessage(bool shouldShow)
    {
        HandleMessageState(invalidPasswordMessage, shouldShow);
    }

    public void ShowNameRequirementMessage(bool shouldShow)
    {
        HandleMessageState(nameMessage, shouldShow);
    }

    public void ShowSuccessMessage(bool shouldShow)
    {
        HandleMessageState(successMessage, shouldShow);
    }

    public void ShowErrorMessage(bool shouldShow)
    {
        HandleMessageState(errorMessage, shouldShow);
    }

    private void ShowMessageGroup()
    {
        if (!messageGroup.activeSelf && errorMessageCounter > 0)
        {
            messageGroup.SetActive(true);
        }
        else if (messageGroup.activeSelf && errorMessageCounter <= 0)
        {
            messageGroup.SetActive(false);
        }
    }
}
