using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [Header("stat")]
    [SerializeField, Range(1f, 10f)] private float moveSpeed = 5f;
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private float jumpPower;
    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float JumpPower { get { return jumpPower; } }
    private Monster[] monsters;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hp = MaxHp;
        
    }

    private void Update()
    {
        if (monsters is null)
        {
            monsters = FindObjectsOfType<Monster>();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i].GetComponent<IDamagable>().TakeDamage(10f);
            }
        }
    }
    public void HpHeal(float heal)
    {
        if (Hp == MaxHp) { return; }
        hp += heal;
        if (Hp > MaxHp) { hp = MaxHp; }
    }
}
