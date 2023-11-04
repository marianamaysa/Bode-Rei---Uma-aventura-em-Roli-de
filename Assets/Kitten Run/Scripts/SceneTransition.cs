using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Script to handle scene transitions.

    public Animator transition;

    void Start()
    {
        transition.Play("Transition Out");
    }

    public void Play()
    {
        StartCoroutine(LoadGameScene());
    }

    public void Menu()
    {
        StartCoroutine(LoadMenuScene());
    }

    public void Retry()
    {
        StartCoroutine(LoadRetryScene());
    }

    IEnumerator LoadGameScene()
    {
        transition.Play("Transition In");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadMenuScene()
    {
        transition.Play("Transition In");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator LoadRetryScene()
    {
        transition.Play("Transition In");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
