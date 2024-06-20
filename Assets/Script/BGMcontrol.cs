using UnityEngine;

public class MainMenuBGMManager : MonoBehaviour
{
    private void Start()
    {
        LoadVolume();
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("musicVolume");
            AudioListener.volume = volume;
        }
    }
}
