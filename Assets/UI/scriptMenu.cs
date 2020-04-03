using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class scriptMenu : MonoBehaviour
{
    public void GoToLevel()
    {
        SceneManager.LoadScene("Level Apeiron");
    }
}
