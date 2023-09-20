using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{   
    public Sprite[] images; // Массив с различными спрайтами
    public float changeInterval = 35f; // Интервал смены спрайта
    public float fadeDuration = 1.0f; // Длительность плавной смены спрайта
    private Image imageComponent;
    private int currentImageIndex = 0;
    private Image backgroundImage;

    private void Start()
    {
        imageComponent = GetComponent<Image>();

        // Создаем и настраиваем объект Image для фона
        GameObject backgroundObject = new GameObject("Background");
        backgroundObject.transform.SetParent(transform, false);
        backgroundImage = backgroundObject.AddComponent<Image>();
        backgroundImage.rectTransform.sizeDelta = imageComponent.rectTransform.sizeDelta;
        backgroundImage.color = new Color(1f, 1f, 1f, 0f); // Начальный альфа-канал фона

        // Запускаем корутину для смены спрайта
        StartCoroutine(ChangeImageRoutine());
    }

    private IEnumerator ChangeImageRoutine()
    {
        while (!Player.lose)
        {
            // Запускаем плавную смену спрайта
            yield return ChangeImageSmoothly();

            // Переход к следующему спрайту
            currentImageIndex = (currentImageIndex + 1) % images.Length;

            // Ждем заданный интервал перед сменой следующего спрайта
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private IEnumerator ChangeImageSmoothly()
    {
        // Запоминаем текущий спрайт и выбираем следующий спрайт из массива
        Sprite currentSprite = imageComponent.sprite;
        Sprite newSprite = images[currentImageIndex];

        // Интерполируем альфа-канал фона для плавной смены
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            backgroundImage.color = new Color(1f, 1f, 1f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем новый спрайт и снова интерполируем альфа-канал фона
        imageComponent.sprite = newSprite;
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            backgroundImage.color = new Color(1f, 1f, 1f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем альфа-канал фона в 0
        backgroundImage.color = new Color(1f, 1f, 1f, 0f);
    }
}
