using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public void language_Rus()
    {
        string language = "Rus";
        PlayerPrefs.SetString("Language", language);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void language_Eng()
    {
        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
}
