using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scriptMenu : MonoBehaviour
{
    public AudioSource musicMenu;
    public AudioSource buttonSound;
    public GameObject buttonObject;

    void Awake(){
        DontDestroyOnLoad(buttonObject);
    }
    void Start()
    {
        musicMenu.volume = PlayerPrefs.GetFloat("soundVol",1);
        buttonSound.volume = PlayerPrefs.GetFloat("soundVol",1);
    }
    public void GoToLevel()
    {
        SceneManager.LoadScene("Game");
        buttonSound.Play();
        musicMenu.Stop();
    }
    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
    }
}
