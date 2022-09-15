using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public KeyType key;
    public GameObject prefab;
    public int initCount;
    public int maxCount;
}
public enum KeyType{  ItemHeal, ItemAmmo }
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] private List<ObjectData> objectDatas = new List<ObjectData>();
    private Dictionary<string, Stack<GameObject>> poolDict;
    private Dictionary<KeyType, ObjectData> dataDict;
    private new void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        if (objectDatas.Count == 0) { return; }
        poolDict = new Dictionary<string, Stack<GameObject>>();
        dataDict = new Dictionary<KeyType, ObjectData>();
        foreach (var data in objectDatas)
        {
            Debug.Log(data.prefab.name);
            CreatePool(data);
        }
    }
    private void CreatePool(ObjectData data)
    {

        if (poolDict.ContainsKey(data.key.ToString())) { return; }
        if (!Enum.TryParse<KeyType>(data.key.ToString(), out KeyType key)) { return; }
        GameObject poolobj = null;
        // 풀 만들기
        if (!GameObject.Find(data.key.ToString()))
        {
            poolobj = new GameObject(data.key.ToString());
            poolobj.transform.SetParent(transform);
        }
        CreateObject(data);
    }
    private void CreateObject(ObjectData data)
    {
        Stack<GameObject> pool = new Stack<GameObject>(data.maxCount);
        GameObject poolobj = GameObject.Find(data.key.ToString());
        //만든 풀에 오브젝트 넣기
        for (int i = 0; i < data.initCount; i++)
        {
            GameObject obj = Instantiate(data.prefab);
            obj.transform.SetParent(poolobj.transform);
            obj.SetActive(false);
            pool.Push(obj);
        }
        poolDict.Add(data.key.ToString(), pool);
        dataDict.Add(data.key, data);
    }
    public GameObject GetObject(KeyType key)
    {
        // 키가 존재하지 않는 경우 null 리턴
        if (!poolDict.TryGetValue(key.ToString(), out var pool)) { return null; }
        GameObject obj = null;
        if (pool.Count > 0)
        {
            obj = pool.Pop();
        }
        // 2. 재고가 없는 경우 샘플로부터 복제
        else
        {
            if (dataDict.TryGetValue(key, out var data))
            {
                CreateObject(data);
            }
        }
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        return obj;
    }
    public void ReturnObject(GameObject obj, KeyType key)
    {
        // 딕셔너리에 있나 확인후 없으면 부숨
        if (!poolDict.TryGetValue(key.ToString() , out var pool))
        {
            Destroy(obj);
            return;
        }
        // 있으면 풀에 넣음
        GameObject poolobj = GameObject.Find(key.ToString());
        obj.transform.SetParent(poolobj.transform);
        obj.SetActive(false);
        pool.Push(obj);
    }
}
