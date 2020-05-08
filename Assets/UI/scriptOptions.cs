using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scriptOptions : MonoBehaviour
{

    private int musicVolume;
    private int sfxVolume;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicVolume = PlayerPrefs.GetInt("musicVolume",100);
        sfxVolume = PlayerPrefs.GetInt("sfxVolume",100);

        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeMusicValue(Slider slider) {
        musicVolume = (int)slider.value;
        PlayerPrefs.SetInt("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }
    public void ChangeSfxValue(Slider slider) {
        sfxVolume = (int)slider.value;
        PlayerPrefs.SetInt("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
