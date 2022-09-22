using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class GunSpriteData
{
    public Sprite gunsBodySprite;
    public Sprite magazineSprite;
    public Sprite scopeSprite;
}
public class UIController : MonoBehaviour
{
    public UnityAction<float, float> changeUIText;
    public UnityAction<int> changeGunImage;
    public UnityAction<float> changeHpBar;
    [SerializeField] private List<GunSpriteData> gunSpriteDatas = new List<GunSpriteData>();
    [SerializeField] private TextMeshProUGUI curAmmo;
    [SerializeField] private TextMeshProUGUI totalAmmo;
    [SerializeField] private Image gunsBody;
    [SerializeField] private Image magazine;
    [SerializeField] private Image scope;
    [SerializeField] private Image hpbar;
    [SerializeField] private Image damageBar;
    [SerializeField] private float playerMaxHp;

    private void Awake()
    {
        playerMaxHp = GameManager.Instance.player.MaxHp;
        changeUIText = (float curAmmo, float totalAmmo) => TextAmmoChange(curAmmo, totalAmmo);
        changeGunImage = (int curGunNum) => GunsImageChange(curGunNum);
        changeHpBar = (float curHp) => UpdateHp(curHp);
        hpbar.fillAmount = 1f; 
        damageBar.fillAmount = 1f;
    }

    public void TextAmmoChange(float curAmmo, float totalAmmo)
    {
        this.curAmmo.text = curAmmo.ToString();
        this.totalAmmo.text = totalAmmo.ToString();
    }
    public void GunsImageChange(int curGunNum)
    {
        gunsBody.sprite = gunSpriteDatas[curGunNum].gunsBodySprite;
        magazine.sprite = gunSpriteDatas[curGunNum].magazineSprite;
        scope.sprite = gunSpriteDatas[curGunNum].scopeSprite;
    }
    private void UpdateHp(float curHp)
    {
        float cent = curHp / playerMaxHp;// 목적지
        if (hpbar.fillAmount < cent) 
        { // 회복
            StartCoroutine(UpdateGreenHp(cent));
        }
        else 
        { // 맞았을때
            hpbar.fillAmount = cent;
            StartCoroutine(UpdateRedHp(cent));
        }
    }
    private IEnumerator UpdateRedHp(float cent)
    {
        for (float i = damageBar.fillAmount; i > cent; i-=0.007f)
        {
            damageBar.fillAmount = i;
            yield return null;
        }
        damageBar.fillAmount -= 0.005f;
    }

    private IEnumerator UpdateGreenHp(float cent)
    {
        for (float i = hpbar.fillAmount; i < cent; i += 0.007f)
        {
            hpbar.fillAmount = i;
            yield return null;
        }
    }
}
