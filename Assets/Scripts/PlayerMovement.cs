using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // The speed at which the player moves (Standard speed)
    private CharacterController charcontroller;
    private float horizontalInput; // The horizontal input from the player
    private float verticalInput;
    private Vector3 moveDirection; // The direction in which the player is moving

    private Vector3 movement; // The movement vector that will be applied to the player (Combining movement with orientation)

    public float runSpeed; // The speed at which the player runs (Updated speed)
    private float currentMoveSpeed; // The current speed of the player (can be walk or run speed)
 




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charcontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput * moveSpeed, 0f, verticalInput * moveSpeed);
        movement = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized * currentMoveSpeed; // Calculate the movement vector based on the player's input and speed (Combining movement with orientation)

    if (Input.GetKey(KeyCode.LeftShift)) // Check if player is holding down Left Shift to run and then use runSpeed Property in Unity to update player's speed
        {
            currentMoveSpeed = runSpeed;

        }
        else
        {
            currentMoveSpeed = moveSpeed; // If not holding down Left Shift, use normal moveSpeed Property in Unity to update player's speed
        }

        movement = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized * currentMoveSpeed; // Calculate the movement vector based on the player's input and speed (Combining movement with orientation)
        charcontroller.Move(movement *Time.deltaTime); 
        

    }
}
