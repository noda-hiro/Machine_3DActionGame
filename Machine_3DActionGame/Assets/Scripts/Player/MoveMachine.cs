using UnityEngine;

public class MoveMachine
{
    private const float moveSpeed = 20f;
    private const float dashSpeedd = 10f;
    private const float gravity = 3f;
    private bool isDash;
    private Vector3 moveDire = Vector3.zero;

    public void MovementCalculation(CharacterController characterController, Camera camera, Transform transform)
    {
        var keyValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // if (characterController.isGrounded == false) return;
        moveDire = keyValue;
        moveDire = transform.TransformDirection(moveDire);
        moveDire *= isDash ? dashSpeedd : moveSpeed;

        moveDire.y -= gravity * Time.deltaTime;
        characterController.Move(moveDire * Time.deltaTime);
    }
}
