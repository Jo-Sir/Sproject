using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIController : MonoBehaviour
{
    public UnityAction<float, float> changeUIText;
    [SerializeField] private TextMeshProUGUI curAmmo;
    [SerializeField] private TextMeshProUGUI totalAmmo;
    private Sprite[] gunsBodySprite;
    private Sprite[] magazineSprite;
    private Sprite[] ScopeSprite;
    private void Awake()
    {
        changeUIText = (float curAmmo, float totalAmmo) => TextAmmoChange(curAmmo, totalAmmo);
    }
    public void TextAmmoChange(float curAmmo, float totalAmmo)
    {
        this.curAmmo.text = curAmmo.ToString();
        this.totalAmmo.text = totalAmmo.ToString();
    }
}
