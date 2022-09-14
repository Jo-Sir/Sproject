using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float moveSpeed;
    private Monster[] target;
    private Vector3 moveVec;
    private float moveH;
    private float moveV;
    private float moveY;
    private float jumpPower = 5;
    private bool jumpLimit = true;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        target = FindObjectsOfType<Monster>();
    }

    private void Update()
    {
        Jump();
        Move();
        if (Input.GetButtonDown("Fire1")) 
        {
            for (int i = 0; i < target.Length; i++)
            { 
                target[i].GetComponent<IDamagable>().TakeDamage(10f); 
            }
        }
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
