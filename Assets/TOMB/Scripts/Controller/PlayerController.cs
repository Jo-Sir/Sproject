using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IDamagable
{
    [Header("stat")]
    [SerializeField, Range(1f, 10f)] private float moveSpeed = 5f;
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    private Animator animator;
    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public Animator Animator { set { animator = value; } get { return animator; } }
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        hp = MaxHp;
    }
    public void HpHeal(float heal)
    {
        if (Hp == MaxHp) { return; }
        hp += heal;
        if (Hp > MaxHp) { hp = MaxHp; }
        GameManager.Instance.uI.changeHpBar?.Invoke(Hp);
    }
    
    public void TakeDamage(float damage)
    {
        hp -= damage;
        GameManager.Instance.uI.changeHpBar?.Invoke(Hp);
    }
}
