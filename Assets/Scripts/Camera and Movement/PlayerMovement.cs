using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] CharacterController controller;
    float horizontal, vertical;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = Vector3.zero;

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= playerSpeed;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
