using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;
    private float mouseX;
    private float mouseY;
    private float mouseSensitivity;
    private void Start()
    {
        mouseSensitivity = GameManager.Instance.mouseSensitivity;
        // 마우스 안보이게 잠금
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Look();
    }

    private void Look()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        // (방향, 최소값, 최대값)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Unity 에서 Quaternion은 상대회전, 축이 겹치지않음
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);


        // transform.LookAt(new Vector3(1, 1, 1));
        // transform.Rotate(Vector3.left * mouseY);
    }
}
