using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(GroundChecker))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVec;
    private float moveH;
    private float moveV;
    private float moveY;
    private float moveSpeed;
    private bool jumpLimit = true;
    private GroundChecker groundChecker;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {

        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (groundChecker.IsGrounded)
        {
            moveY = 0;
            jumpLimit = true;
            Jump();
        }
        else
        {
            moveY += Physics.gravity.y * Time.deltaTime;
            jumpLimit = false;
        }
        moveVec = (transform.right * moveH + transform.forward * moveV) * GameManager.Instance.player.MoveSpeed;
        moveVec.y = moveY;
        characterController.Move(moveVec * Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpLimit)
        {
            moveY += GameManager.Instance.player.JumpPower;
        }
    }
}
