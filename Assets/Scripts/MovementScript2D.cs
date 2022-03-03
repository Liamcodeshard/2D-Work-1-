using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript2D : MonoBehaviour
{
    public SenseWithRays raycaster;
    public float speed;
    public float gravity;
    public float jumpApex;
    public float jumpSpeed;
    public float jumpTime;
   

    Vector2 currentWantedMovement;

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
