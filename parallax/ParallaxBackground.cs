using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;
    public float parallaxSpeed = -0.1f;
    public float smoothing = 1f; // Параметр сглаживания
    private Vector3 previousPlayerPosition;
    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (player == null)
        {
            // Если игрок не указан, найдем его автоматически
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        previousPlayerPosition = player.position;
        targetPosition = transform.position;

        // Создаем и настраиваем компонент SpriteRenderer для фона
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Вычисление смещения игрока с последнего кадра
        Vector3 playerMovement = player.position - previousPlayerPosition;

        // Вычисление смещения фона с учетом параллакс-эффекта
        Vector3 parallaxOffset = playerMovement * parallaxSpeed;

        // Обновление целевой позиции фона
        targetPosition += new Vector3(parallaxOffset.x, parallaxOffset.y, 0);

        // Плавное перемещение фона к целевой позиции с использованием сглаживания
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);

        previousPlayerPosition = player.position;
    }
}
