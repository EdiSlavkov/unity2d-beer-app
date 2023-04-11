using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator sceneAnimator;
    [SerializeField] private AnimationClip sceneTransitionClip;
    private const int LoginSceneIndex = 0;
    private const int HomeSceneIndex = 1;
    private const int SettingsSceneIndex = 2;
    private const int FavoritesSceneIndex = 3;
    private const int RandomBeerSceneIndex = 4;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != LoginSceneIndex)
        {
            sceneAnimator.SetTrigger("playEnterSceneAnimation");
        }
    }

    public void LoadRandomBeerScene()
    {
        StartCoroutine(Load(RandomBeerSceneIndex));
    }

    public void LoadHomeScene()
    {
        StartCoroutine(Load(HomeSceneIndex));
    }

    public void LoadLoginScene()
    {
        StartCoroutine(Load(LoginSceneIndex));
    }

    public void LoadSettingsScene()
    {
        StartCoroutine(Load(SettingsSceneIndex));
    }

    public void LoadFavoritesScene()
    {
        StartCoroutine(Load(FavoritesSceneIndex));
    }

    public IEnumerator Load(int index)
    {
        sceneAnimator.SetTrigger("play");
        yield return new WaitForSeconds(sceneTransitionClip.length);
        SceneManager.LoadScene(index);
    }
}
    