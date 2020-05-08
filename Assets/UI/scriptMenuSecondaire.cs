using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scriptMenuSecondaire : MonoBehaviour
{
    public AudioSource buttonSound;
    public GameObject buttonObject;

    void Awake(){
        DontDestroyOnLoad(buttonObject);
    }
    void Start()
    {
        buttonSound.volume = PlayerPrefs.GetFloat("soundVol",1);
    }

    public void GoToLevel()
    {
        SceneManager.LoadScene("Game");
        buttonSound.Play();
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
