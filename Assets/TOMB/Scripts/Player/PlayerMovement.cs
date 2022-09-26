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
    private bool ableStep = true;
    private GroundChecker groundChecker;
    private void Awake()
    {
        moveSpeed = PlayerManager.Instance.player.MoveSpeed;
        characterController = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        if (!PlayerManager.Instance.player.IsDie)
        { 
            Move();
        }
    }
    private void Move()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        float distance = (characterController.velocity - Vector3.zero).sqrMagnitude;
        if (groundChecker.IsGrounded) { moveY = 0; }
        else { moveY += Physics.gravity.y * Time.deltaTime; }
        if ((Input.GetButtonDown("Jump") && distance != 0 && ableStep)) { StartCoroutine(Evasion()); }
        moveVec = (transform.right * moveH + transform.forward * moveV) * moveSpeed;
        moveVec.y = moveY;
        characterController.Move(moveVec * Time.deltaTime);
    }
    private IEnumerator Evasion()
    {
        ableStep = false;
        for (int i = 0; i < 100; i+=10)
        {
            moveVec = (transform.right * moveH + transform.forward * moveV) * i;
            characterController.Move(moveVec * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        ableStep = true;
    }
}
