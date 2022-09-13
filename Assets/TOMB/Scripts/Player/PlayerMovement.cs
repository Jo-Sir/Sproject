using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject target;
    private Vector3 moveVec;
    private float moveH;
    private float moveV;
    private float moveY;
    private float jumpPower = 5;
    private bool jumpLimit = true;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        Jump();
        Move();
        if (Input.GetButtonDown("Fire1")) { target.GetComponent<IDamagable>().TakeDamage(10f);  }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpLimit)
        {
            moveY += jumpPower;
        }
    }
    private void Move()
    {

        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (characterController.isGrounded)
        {
            moveY = 0;
            jumpLimit = true;
        }
        else
        {
            moveY += Physics.gravity.y * Time.deltaTime;
            jumpLimit = false;
        }

        // wolrd ÁÂÇ¥
        // moveVec = new Vecter3(moveH, 0, moveY) * moveSpeed;
        // local ÁÂÇ¥
        moveVec = (transform.right * moveH + transform.forward * moveV) * moveSpeed;
        moveVec.y = moveY;
        characterController.Move(moveVec * Time.deltaTime);
    }
}
