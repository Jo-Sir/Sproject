using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAmmoItem", menuName = "ScriptableObject/DropItem/AmmoItem")]
public class AmmoItem : DropItem
{
    [SerializeField] private int ammo;
    public override void Use()
    {
        if (PlayerManager.Instance.gunController.CurGunNum == 1) { ammo = 30; }
        else if (PlayerManager.Instance.gunController.CurGunNum == 2) { ammo = 4; }
        PlayerManager.Instance.gunController.TotalAmmoUp(ammo);
    }
}
