using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDropItem", menuName = "ScriptableObject/DropItem")]
public abstract class DropItem : ScriptableObject
{
    [SerializeField] private new string name;
    public string Name { get { return name; } }
    public GameObject prefab;
    public abstract void Use();
}
