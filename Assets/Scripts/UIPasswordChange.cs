using Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPasswordChange : MonoBehaviour
{
    [SerializeField] private Button changeConfirmButton;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Animator popUpAnimator;
    [SerializeField] private GameObject clickBlocker;
    private string oldPassword = "";
    private string newPassword = "";
    private string confirmPassword = "";
    private SessionManager sessionManager;

    private void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
    }

    private void SetChangeConfirmButtonInteractableState()
    {
        if (newPassword == confirmPassword
            && Validator.IsPasswordValid(newPassword)
            && !string.IsNullOrEmpty(oldPassword))
        {
            changeConfirmButton.interactable = true;
        }
        else if (changeConfirmButton.IsInteractable())
        {
            changeConfirmButton.interactable = false;
        }
    }

    public void ChangePassword()
    {
        User user = sessionManager.GetLoggedUser();
        bool passwordsMatched = user.password == oldPassword;
        if (passwordsMatched)
        {
            sessionManager.ChangePassword(newPassword);
            inputManager.ClearInputs();
            inputManager.ShowOldPasswordMessage(false);
            inputManager.ShowPasswordRequirementsMessage(false);
            StartCoroutine(HideChangePasswordPanel());
        }
        inputManager.ShowinvalidPasswordMessage(!passwordsMatched);
    }

    private IEnumerator HideChangePasswordPanel()
    {
        inputManager.ShowSuccessMessage(true);
        yield return new WaitForSeconds(1f);
        clickBlocker.SetActive(false);
        popUpAnimator.SetTrigger("popOut");
        inputManager.ShowSuccessMessage(false);
    }

    public void OnInputOldPasswordChange(string input)
    {
        oldPassword = input;
        inputManager.ShowOldPasswordMessage(string.IsNullOrEmpty(oldPassword));
        SetChangeConfirmButtonInteractableState();
    }

    public void OnInputNewPasswordChange(string input)
    {
        newPassword = input;
        inputManager.ShowPasswordRequirementsMessage(!string.IsNullOrEmpty(newPassword) && !Validator.IsPasswordValid(newPassword));
        ShowPasswordMatchMessage();
        SetChangeConfirmButtonInteractableState();
    }

    public void OnInputConfirmPasswordChange(string input)
    {
        confirmPassword = input;
        ShowPasswordMatchMessage();
        SetChangeConfirmButtonInteractableState();
    }

    private void ShowPasswordMatchMessage()
    {
        inputManager.ShowPasswordMatchMessage(newPassword != confirmPassword);
        SetChangeConfirmButtonInteractableState();
    }
}