using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewHealItem", menuName = "ScriptableObject/DropItem/HealItem")]
public class HealItem : DropItem
{
    [SerializeField] private float heal;
    public override void Use()
    {
        PlayerManager.Instance.player.HpHeal(heal); 
    }
}
