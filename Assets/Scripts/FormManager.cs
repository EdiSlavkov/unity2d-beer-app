using System.Collections;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    [SerializeField] private Animator logoAnimator;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject registrationForm;
    [SerializeField] private GameObject loginForm;
    [SerializeField] private GameObject buttonGroup;
    [SerializeField] private AnimationClip logoAnimationClip;
    private bool isFirstLoad = true;

    public void LoadRegistration()
    {
        StartCoroutine(LoadForm(true));
    }

    public void LoadLogin()
    {
        StartCoroutine(LoadForm(false));
    }

    private IEnumerator LoadForm(bool shouldLoadRegistration)
    {
        if (isFirstLoad)
        {
            buttonGroup.SetActive(false);
            logoAnimator.SetTrigger("play");
            yield return new WaitForSeconds(logoAnimationClip.length / 2);
            isFirstLoad = false;
        }
        registrationForm.SetActive(shouldLoadRegistration);
        loginForm.SetActive(!shouldLoadRegistration);
        transitionAnimator.SetTrigger("play");
    }
}