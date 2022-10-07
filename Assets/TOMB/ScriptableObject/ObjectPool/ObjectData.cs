using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewObjectPoolData", menuName = "ScriptableObject/ObjectPoolData")]
public class ObjectData : ScriptableObject
{
    public KeyType key;
    public GameObject prefab;
    public int initCount;
    public int maxCount;
}
