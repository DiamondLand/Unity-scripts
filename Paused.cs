using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Paused : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject countdownCanvas;
    [SerializeField] Text countdownText;

    private int countdownValue = 3;
    private bool isPaused = false; // Флаг для отслеживания активной паузы

    void Start()
    {
        pause.SetActive(false);
        countdownCanvas.SetActive(false);
        countdownText.gameObject.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void PauseOn()
    {
        if (!isPaused) // Проверяем, не активна ли уже пауза
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            isPaused = true; // Устанавливаем флаг активной паузы
        }
    }

    public void PauseOff()
    {
        pause.SetActive(false);
        countdownCanvas.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    public void Restart(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1;
    }

    public void Menu(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1;
    }

    public void InfoOn()
    {
        infoPanel.SetActive(true);
    }

    public void InfoOff()
    {
        infoPanel.SetActive(false);
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        while (countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();
            float countdownTimer = 1f;
            while (countdownTimer > 0)
            {
                countdownTimer -= Time.unscaledDeltaTime;
                yield return null;
            }
            countdownValue--;
        }

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
        countdownCanvas.SetActive(false);
        countdownValue = 3;
        isPaused = false; // Сбрасываем флаг активной паузы
    }
}
