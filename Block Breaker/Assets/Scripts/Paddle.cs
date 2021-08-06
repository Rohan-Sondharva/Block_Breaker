using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float jumpHeight;
    [SerializeField] float dashLength;

    // cached reference
    GameSession gameSession;
    Ball ball;
    Rigidbody2D myRigidBody;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myRigidBody = GetComponent<Rigidbody2D>();
        ball = FindObjectOfType<Ball>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y)
        {
            x = Mathf.Clamp(GetXPos(), minX, maxX)
        };
        transform.position = paddlePos;
        Jump();
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myRigidBody.velocity = new Vector2(dashLength, 0f);        
        }
    }

    public float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = new Vector2(0f, jumpHeight);
        }
    }
}
