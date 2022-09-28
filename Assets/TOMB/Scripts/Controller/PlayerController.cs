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
    [SerializeField] private AudioClip[] audioClips;
    private bool isDie = false;
    private Animator animator;
    public float MaxHp { get { return maxHp; } }
    public AudioClip[] AudioClip { get { return audioClips; } }
    public float Hp 
    {
        set 
        {
            hp = value;
            if (hp <= 0)
            {
                isDie = true;
                PlayerManager.Instance.playerUI.IsDie();
                Invoke("Die", 2f);
            }
        }
        get { return hp; } 
    }
    public float MoveSpeed { get { return moveSpeed; } }
    public bool IsDie { set { isDie = value; } get { return isDie; } }
    public Animator Animator { set { animator = value; } get { return animator; } }
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        hp = MaxHp;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            GameManager.Instance.ReturnMain();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            PlayerManager.Instance.playerUI.GameClear();
        }
    }
    private void Die()
    {
        GameManager.Instance.ReturnMain();   
    }
    public void HpHeal(float heal)
    {
        if (Hp == MaxHp) { return; }
        Hp += heal;
        if (Hp > MaxHp) { hp = MaxHp; }
        PlayerManager.Instance.playerUI.changeHpBar?.Invoke(Hp);
    }
    public void TakeDamage(float damage)
    {
        if (Hp > 0)
        { 
            Hp -= damage;
            PlayerManager.Instance.playerUI.changeHpBar?.Invoke(Hp);
        }
    }
}
