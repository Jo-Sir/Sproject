using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GunController : MonoBehaviour
{
    #region Field
    [SerializeField] private Gun[] guns;
    private Gun curGun;
    private int curGunNum;
    private Animator playerAnimator;
    private bool isAction = true; //false면행동불가 true면 행동가능
    #endregion Field

    #region Property
    public bool IsAction { set { isAction = value; } get { return isAction; } }
    public int CurGunNum { get { return curGunNum; } }
    #endregion Property

    #region Unity
    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        guns = GetComponentsInChildren<Gun>(true);
        curGun = guns[0];
        curGunNum = 0;
        curGun.gameObject.SetActive(true);
    }
    private void Start()
    {
        SwapGun(curGunNum + 1);
    }
    private void Update()
    {
        if ((IsAction && !PlayerManager.Instance.player.IsDie))
        { 
            ShotDivision(curGunNum);
            if ((Input.inputString.Equals("1") || Input.inputString.Equals("2") || Input.inputString.Equals("3") /*|| Input.inputString.Equals("4")*/))
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
    #endregion Unity

    #region privateFunc
    private void SwapGun(int value)
    { 
        value -= 1;
        curGun.gameObject.SetActive(false);
        curGun = guns[value];
        curGun.gameObject.SetActive(true);
        curGunNum = value;
        PlayerManager.Instance.gunsAudioController.audioClipsChange(curGunNum);
        ChangeGunsImage();
        ChangeAmmoText();
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
        if (curGun.CurBullet == curGun.MaxBullet) return;
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
    #endregion privateFunc

    #region publicFunc
    public void SkillReload()
    {
        if (curGun.CurBullet == curGun.MaxBullet) return;
        if (curGun.TotalBullet <= 0) return;
        curGun.Reload(curGunNum);
        ChangeAmmoText();
    }
    public void ShotGunReload()
    {
        curGun.ReloadShotGun();
    }
    public void TotalAmmoUp(float addAmmo)
    {
        curGun.AddTotalAmmo(addAmmo);
        ChangeAmmoText();
    }
    public void ChangeAmmoText()
    {
        PlayerManager.Instance.playerUI.changeUIText?.Invoke(curGun.CurBullet, curGun.TotalBullet);
    }
    public void ChangeGunsImage()
    {
        PlayerManager.Instance.playerUI.changeGunImage?.Invoke(curGunNum);
    }
    #endregion publicFunc
}
