using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    Image timerBar;
    public float maTime;

    float timeLeft;
    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject GameUI;
    
    void Start()
    {
        timerBar = GetComponent<Image>();
        LosePanel.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1;
        timeLeft = maTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maTime;
        }
        
        else
        {
            LosePanel.SetActive(true);
            GameUI.SetActive(false);
            Time.timeScale = 0;
        }
    }
}
