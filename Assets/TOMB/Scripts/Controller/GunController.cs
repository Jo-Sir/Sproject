using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun[] guns;
    private Gun curGun;
    private int curGunNum;
    private Animator playerAnimator;
    private UnityAction<float, float> changeUI;
    private bool isAction = true; //false면행동불가 true면 행동가능
    public bool IsAction { set { isAction = value; } get { return isAction; } }
    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        guns = GetComponentsInChildren<Gun>(true);
        curGun = guns[0];
        curGunNum = 0;
        curGun.gameObject.SetActive(true);
        SwapGun(curGunNum + 1);
        changeUI = GameManager.Instance.uI.changeUIText;// 왜안됌?
    }
    private void Update()
    {
        if (IsAction)
        { 
            ShotDivision(curGunNum);
            if ((Input.inputString.Equals("1") || Input.inputString.Equals("2") || Input.inputString.Equals("3")))
            {
                if (int.Parse(Input.inputString)!= (curGunNum + 1))
                { 
                    SwapGun(int.Parse(Input.inputString));
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
    }
    private void SwapGun(int value)
    { 
        value -= 1;
        curGun.gameObject.SetActive(false);
        curGun = guns[value];
        curGun.gameObject.SetActive(true);
        curGunNum = value;
        playerAnimator.Play(curGunNum.ToString() + "_Swap");
    }
    private void Shoot()
    {
        // Fire Empty 사운드
        if (curGun.CurBullet <= 0) { return; }
        playerAnimator.Play(curGunNum.ToString() + "_Fire");
        curGun.Shoot(curGunNum);
        
    }
    private void Reload()
    {
        if (curGun.TotalBullet <= 0) return;
        playerAnimator.Play(curGunNum.ToString() + "_Reload");
        playerAnimator.SetInteger("ShotGunReloadNum", (int)curGun.CurBullet);
        if (curGunNum != 2)
        { 
            curGun.Reload(curGunNum);
        }
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
    public void ShotGunReload()
    {
        curGun.ReloadShotGun();
    }
    public void ChangeAmmoText()
    {
        // changeUI?.Invoke(curGun.CurBullet, curGun.TotalBullet);
        GameManager.Instance.uI.changeUIText?.Invoke(curGun.CurBullet, curGun.TotalBullet);
    }
}
