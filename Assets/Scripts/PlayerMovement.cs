using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // The speed at which the player moves
    private CharacterController charcontroller;
    private float horizontalInput; // The horizontal input from the player
    private float verticalInput;
    private Vector3 moveDirection; // The direction in which the player is moving

    private Vector3 movement; // The movement vector that will be applied to the player (Combining movement with orientation)




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
        movement = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized * moveSpeed; // Calculate the movement vector based on the player's input and speed (Combining movement with orientation)

        charcontroller.Move(movement *Time.deltaTime); 
    }
}