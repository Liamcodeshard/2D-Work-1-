using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript2D : MonoBehaviour
{
    public SenseWithRays raycaster;
    public float speed;
    public float gravity;
    public float jumpTime;
    public float jumpSpeed;
   //
   //public float jumpApex;
  
   

    Vector2 currentWantedMovement;
    bool weAreJumping;
    float timeSinceJumped;

    private void Start()
    {
        weAreJumping = false;
    }

    void Update()
    {
        UpdateInput();
        UpdateHorizontalMovement();
        UpdateVerticalMovement();
    }
    void UpdateInput()
    {
        

 
    }
    void UpdateHorizontalMovement()
    {
        currentWantedMovement = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow)) currentWantedMovement.x++;
        if (Input.GetKey(KeyCode.LeftArrow)) currentWantedMovement.x--;

        HorizontalMove(speed * currentWantedMovement.x * Time.deltaTime);    
    }
   void UpdateVerticalMovement()
    {
        float currentVerticalMovement = 0f;
        if (weAreJumping)
        {
            currentVerticalMovement = jumpSpeed;
        }
        else
        {
            currentVerticalMovement = gravity * -1.0f;
        }
        // UpdateJump();
        VerticalMove(-1.0f * gravity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpStart();
        }

       JumpUpdate();
    }

    void JumpStart()
    {
        weAreJumping = true;
        timeSinceJumped = 0f;
    }

   void JumpUpdate()
    {
        if (weAreJumping)
        {
            timeSinceJumped += Time.deltaTime;
            if (timeSinceJumped > jumpTime)
            {
                weAreJumping = false;
            }
        }      
    }
  
    public void Move(Vector2 totalMovement)
    {
        HorizontalMove(totalMovement.x);
        VerticalMove(totalMovement.y);
    }

    public void HorizontalMove(float distance)
    {
        if (distance == 0)
        {
            return;
        }

       MovementDirection dir = MovementDirection.Right;
       if (distance < 0) dir = MovementDirection.Left;
  
       bool weAreColliding = raycaster.ThrowRays(dir, distance);
        if (!weAreColliding)
        {
            transform.Translate(Vector3.right * distance);
        }
    }
    public void VerticalMove(float distance)
    {
        if (distance == 0)
        {
            return;
        }

        MovementDirection dir = MovementDirection.Up;
        if (distance < 0) dir = MovementDirection.Down;

        bool weAreColliding = raycaster.ThrowRays(dir, distance);
        if (!weAreColliding)
            transform.Translate(Vector3.up * distance);
    }

}