using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
     public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

}
