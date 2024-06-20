using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuA : MonoBehaviour
{
    public GameObject pauseMenu;
    //public GameObject player; // Reference to the player GameObject
    public static bool isPaused;
    private int previousSceneIndex;

    //private List<RawImage[]> rawImageArrays = new List<RawImage[]>(); // List of arrays to store found RawImage components

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //FindRawImageArrays();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Find all sets of RawImage components under the parent GameObject of the player
    /*void FindRawImageArrays()
    {
        if (player != null)
        {
            Transform parent = player.transform.parent; // Get the parent GameObject of the player
            if (parent != null)
            {
                // Get all RawImage components under the parent GameObject and store them in arrays
                RawImage[] rawImages1 = parent.GetComponentsInChildren<RawImage>();
                rawImageArrays.Add(rawImages1);

                // Add more arrays if needed
                // RawImage[] rawImages2 = parent.GetComponentsInChildren<RawImage>();
                // rawImageArrays.Add(rawImages2);
            }
        }
    }*/

    // Pause the game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Hide all RawImages when the game is paused
        /*foreach (RawImage[] rawImages in rawImageArrays)
        {
            if (rawImages != null)
            {
                foreach (RawImage rawImage in rawImages)
                {
                    // Check if the GameObject is a RawImage before hiding it
                    if (rawImage.gameObject.GetComponent<RawImage>() != null)
                    {
                        rawImage.gameObject.SetActive(false);
                    }
                }
            }
        }*/
    }

    // Resume the game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Show all RawImages when the game is resumed
        /*foreach (RawImage[] rawImages in rawImageArrays)
        {
            if (rawImages != null)
            {
                foreach (RawImage rawImage in rawImages)
                {
                    // Check if the GameObject is a RawImage before showing it
                    if (rawImage.gameObject.GetComponent<RawImage>() != null)
                    {
                        rawImage.gameObject.SetActive(true);
                    }
                }
            }
        }*/
    }

    // Go to the main menu
    public void GoToMainMenu(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    
    public void Settings()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Previous Scene Index set to: " + previousSceneIndex);
        SceneManager.LoadScene(2);
        //animator.SetTrigger("FadeOut");
    }

    public void backSetting()
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
        //animator.SetTrigger("FadeOut");
    }

    // Quit the application
    public void Quit()
    {
        Application.Quit();
    }
}