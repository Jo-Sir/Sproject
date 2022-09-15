using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAmmoItem", menuName = "ScriptableObject/DropItem/AmmoItem")]
public class AmmoItem : DropItem
{
    [SerializeField] private int ammo;
    public override void Use()
    {
        // 현제 보유중인 탄약 10증가
    }
}
