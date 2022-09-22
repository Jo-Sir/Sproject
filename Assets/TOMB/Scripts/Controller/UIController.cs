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
    [SerializeField] private float playerCurHp;
    [SerializeField] private float playerMaxHp;
    private float preHp;


    private void Awake()
    {
        playerMaxHp = GameManager.Instance.player.MaxHp;
        playerCurHp = GameManager.Instance.player.Hp;
        changeUIText = (float curAmmo, float totalAmmo) => TextAmmoChange(curAmmo, totalAmmo);
        changeGunImage = (int curGunNum) => GunsImageChange(curGunNum);
        changeHpBar = (float curHp) => UpdateGreenHp(curHp);
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
    public void UpdateGreenHp(float curHp)
    {
        float cent = curHp / playerMaxHp;// ¸ñÀûÁö
        if (curHp == playerMaxHp) { hpbar.fillAmount = cent; damageBar.fillAmount = cent; }
        hpbar.fillAmount = cent;
        StartCoroutine(UpdateRedHp(cent));
    }
    public IEnumerator UpdateRedHp(float cent)
    {
        for (float i = damageBar.fillAmount; i > cent; i-=0.005f)
        {
            damageBar.fillAmount = i;
            yield return null;
        }
        damageBar.fillAmount = hpbar.fillAmount - 0.001f;
    }
}
