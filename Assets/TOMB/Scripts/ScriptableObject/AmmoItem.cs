using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAmmoItem", menuName = "ScriptableObject/DropItem/AmmoItem")]
public class AmmoItem : DropItem
{
    [SerializeField] private int ammo;
    public override void Use()
    {
        GameManager.Instance.gunController.TotalAmmoUp(ammo);
    }
}
