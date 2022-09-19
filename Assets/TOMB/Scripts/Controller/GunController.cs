using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun[] guns;
    private Gun curGun;
    private int curGunNum;
    private Animator playerAnimator;
    private void Awake()
    {
        playerAnimator = GameManager.Instance.player.GetComponentInChildren<Animator>();
        guns = GetComponentsInChildren<Gun>(true);
        curGun = guns[0];
        curGunNum = 0;
        curGun.gameObject.SetActive(true);

    }
    private void Update()
    {
        
        ShotDivision(curGunNum);
        if (Input.inputString.Equals("1") || Input.inputString.Equals("2") || Input.inputString.Equals("3"))
        {
            if (int.Parse(Input.inputString)!= (curGunNum + 1))
            { 
                playerAnimator.SetTrigger("Swap");
                SwapGun(int.Parse(Input.inputString));
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReLord();
        }
    }
    private void SwapGun(int value)
    { 
        value -= 1;
        curGun.gameObject.SetActive(false);
        curGun = guns[value];
        curGun.gameObject.SetActive(true);
        curGunNum = value;
        playerAnimator.SetInteger("CurGun", curGunNum);
    }
    private void Shoot()
    {
        curGun.Shoot(curGunNum);
    }
    private void ReLord()
    {
        curGun.ReLord();
    }
    private void ShotDivision(int value)
    {
        if (curGunNum == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }
}
