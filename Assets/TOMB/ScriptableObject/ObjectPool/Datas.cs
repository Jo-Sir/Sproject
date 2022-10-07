using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas : MonoBehaviour
{
    [SerializeField] private List<ObjectData> objectDatas = new List<ObjectData>();

    public List<ObjectData> ObjectDatas { get { return objectDatas; } }
}
