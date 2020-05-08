using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scriptOptions : MonoBehaviour
{

    private float soundVolume;

    public Slider soundVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        soundVolume = PlayerPrefs.GetFloat("soundVol",1);

        soundVolumeSlider.value = soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ChangeSoundValue(Slider slider) {
        soundVolume = slider.value;
        PlayerPrefs.SetFloat("soundVol", soundVolume);
        PlayerPrefs.Save();
    }

}
