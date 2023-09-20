using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float normalSpeed;
    private float moveInput;

    public static bool lose = false;

    private int ExtraJump;
    public int extraJumpValue;
    public float cheakRadius; 
    public float Jumpforce;
    private bool isGround;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheak;
    public LayerMask WhatIsGround;

    public Transform entryPoint;
    public Transform exitPoint;
    private bool isInsideScreen = false;
    private float initialY;

    
    void Start()
    {
        speed = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } 
    

    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheak.position, cheakRadius, WhatIsGround);
        if (isGround == true)
        {
            ExtraJump = extraJumpValue;
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (speed != 0f)
        {
            anim.SetInteger("anim", 2);
        }
        
    }


    public void OnLeftButtonDown()
    {
        if (speed >= 0f)
        {
            speed = -normalSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }


    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    public void OnJumpButtonDown()
    {
        if (isGround == true)
        {
            rb.velocity = Vector2.up * Jumpforce;
        }

    }


    public void OnButtonUp()
    {
        speed = 0f;
        anim.SetInteger("anim", 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenBoarder") && !isInsideScreen)
        {
            // Игрок входит на экран
            isInsideScreen = true;
            initialY = transform.position.y;

            if (collision.transform == entryPoint)
            {
                // Если это точка входа, телепортируем игрока на точку выхода, сохраняя текущую Y-координату
                Vector3 newPosition = exitPoint.position;
                newPosition.y = initialY;
                transform.position = newPosition;
            }
            else if (collision.transform == exitPoint)
            {
                // Если это точка выхода, телепортируем игрока на точку входа, сохраняя текущую Y-координату
                Vector3 newPosition = entryPoint.position;
                newPosition.y = initialY;
                transform.position = newPosition;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenBoarder") && isInsideScreen)
        {
            // Игрок выходит с экрана
            isInsideScreen = false;
        }
    }
}
