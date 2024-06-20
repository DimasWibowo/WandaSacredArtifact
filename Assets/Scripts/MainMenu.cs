using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NewGame()
    {
        // Reset PlayerPrefs
        PlayerPrefs.DeleteKey("CheckpointPos");
        PlayerPrefs.Save();

        // Load scene permainan baru (misalnya "GameScene")
        SceneManager.LoadScene("Part2");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("CheckpointPos"))
        {
            // Load scene permainan (misalnya "GameScene")
            SceneManager.LoadScene("Stage 1 Yang hilang");
        }
        else
        {
            Debug.Log("No saved game found.");
        }
    }

    public void exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
