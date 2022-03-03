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
    public AnimationCurve gravityCurve;
   
   //public float jumpApex;
   //FULL RECAP OF JUMP AT 1:38 
   

    bool weAreJumping;
    float timeSinceJumped;
    bool isGrounded;

    private void Start()
    {
       // weAreJumping = false;
    }

    void Update()
    { 
        UpdateHorizontalMovement();
        UpdateVerticalMovement();
    }
    
    void UpdateHorizontalMovement() // TAKES FROM THE UPDATE AND TAKES THE INPUT TO GIVE A NEW VECTOR TO BE TRANSFORMED INTO HORIZONTAL MOVEMENT 
    {
        float currentMovement = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) currentMovement++;
        if (Input.GetKey(KeyCode.LeftArrow)) currentMovement--;
        HorizontalMove(speed * currentMovement * Time.deltaTime);    
    }
    void UpdateVerticalMovement()
    {
        float currentVerticalMovement = 0f;
        if (weAreJumping) //going up
        {
            currentVerticalMovement = jumpSpeed; // apply jump speed
        }
        else // going down
        {
            currentVerticalMovement = gravity * -1.0f;
        }
        
        VerticalMove(currentVerticalMovement * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpStart();
           
        }

       JumpUpdate();
    }// TAKES FROM THE UPDATE AND TAKES THE INPUT TO GIVE A NEW VECTOR TO BE TRANSFORMED INTO VERTICAL MOVEMENT 

    void JumpStart()
    {
        if (!isGrounded) return;
        
        isGrounded = false;
        weAreJumping = true;
        timeSinceJumped = 0f;
      
    } // TAKES FROM VERTICAL MOVEMENT. STARTS THE JUMP AND CHANGES SOME BOOLEAN VALUES

    void JumpUpdate() // TAKES FROM VERTICAL MOVEMENT AND ENSURES THE JUMP IS NOT TOO LONG
    {
       if (!weAreJumping) return;
        
 
       timeSinceJumped += Time.deltaTime;
       if (timeSinceJumped > jumpTime)
       {
           weAreJumping = false;
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
    } // TAKES FROM UPDATEHORIZONTALMOVEMENT AND TRANSFORMS THE MOVEMENT
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
        {
           transform.Translate(Vector3.up * distance);
           if (dir == MovementDirection.Down)
            {
                isGrounded = false;
            }
        }
        else if (dir == MovementDirection.Down)
        {
            weAreJumping = false;
            isGrounded = true;
        }
    } // TAKES FROM UYPDATEVERTICALMOVEMENT AND TRANSFORMS IT INTO UPWARDS/JUMP MOVEMENT

}