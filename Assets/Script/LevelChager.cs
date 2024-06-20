using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;
    private int previousSceneIndex;
    public GameObject canvasSetting;
    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {

    }

    public void fadeToNextScene()
    {

    }

    public void FadeToLevel(int LevelIndex)
    {
        levelToLoad = LevelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Play()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
        animator.SetTrigger("FadeOut");
    }

    public void mainMenu()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
        animator.SetTrigger("FadeOut");
    }

    public void Retry(string sceneName)
    {
        PlayerPrefs.DeleteKey("CheckpointPos");
        PlayerPrefs.Save();
        Time.timeScale = 1;

        SceneManager.LoadScene(sceneName);
    }

    public void Settings()
    {
        canvasSetting.SetActive(true);
        animator.SetTrigger("FadeOut");
    }

    public void Credits()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        FadeToLevel(2);
        animator.SetTrigger("FadeOut");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void backSetting()
    {
        canvasSetting.SetActive(false);
        animator.SetTrigger("FadeOut");
    }

    public void backCredits()
    {
        Debug.Log("Returning to Scene Index: " + previousSceneIndex);
        if (previousSceneIndex >= 0)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogError("No previous scene index stored.");
        }
        animator.SetTrigger("FadeOut");
    }
}
