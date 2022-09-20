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
        // ���콺 �Ⱥ��̰� ���
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
        // (����, �ּҰ�, �ִ밪)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Unity ���� Quaternion�� ���ȸ��, ���� ��ġ������
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);


        // transform.LookAt(new Vector3(1, 1, 1));
        // transform.Rotate(Vector3.left * mouseY);
    }
}
