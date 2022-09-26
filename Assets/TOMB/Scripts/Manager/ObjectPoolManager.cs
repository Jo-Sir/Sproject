using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectData
{
    public KeyType key;
    public GameObject prefab;
    public int initCount;
    public int maxCount;
}
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] private List<ObjectData> objectDatas = new List<ObjectData>();
    private Dictionary<KeyType, Stack<GameObject>> poolDict;
    private Dictionary<KeyType, ObjectData> dataDict;
    public UnityAction returnObjectAll;
    private new void Awake()
    {
        base.Awake();
        Init();
    }
    private void Init()
    {
        // �ν�����â���� �Էµȵ����Ͱ� ������ ����
        if (objectDatas.Count == 0) { return; }
        // ������ ��ųʸ� �ʱ�ȭ
        poolDict = new Dictionary<KeyType, Stack<GameObject>>();
        dataDict = new Dictionary<KeyType, ObjectData>();
        // �����͸� var���� ������ �ְ� Ǯ ������ֱ�
        foreach (var data in objectDatas)
        {
            CreatePool(data);
        }
    }
    private void CreatePool(ObjectData data)
    {
        // poolDict ��ųʸ��� ���� Ű�� ������ ����
        if (poolDict.ContainsKey(data.key)) { return; }
        // ŰŸ�Կ� �����Ϳ� ���� Ű�� ������ ����
        if (!Enum.TryParse<KeyType>(data.key.ToString(), out KeyType key)) { return; }
        GameObject poolobj = null;
        // ������ �� Ű�� ������ �����̳� �����
        if (!GameObject.Find(data.key.ToString()))
        {
            poolobj = new GameObject(data.key.ToString());
            poolobj.transform.SetParent(transform);
        }
        // ������� �����̳� ä���
        CreateObject(data);
    }
    private void CreateObject(ObjectData data)
    {
        
        Stack<GameObject> pool = new Stack<GameObject>(data.maxCount);
        // ���� �����̳� ���� ��������
        GameObject poolobj = GameObject.Find(data.key.ToString()); //�̰͵� ���ε� ���ϰ�
        // ���� �����̳ʿ� ������Ʈ ���� �ֱ� �ֱ�
        for (int i = 0; i < data.initCount; i++)
        {
            GameObject obj = Instantiate(data.prefab);
            obj.transform.SetParent(poolobj.transform);
            obj.SetActive(false);
            pool.Push(obj);
        }
        // Ű���� ������
        if (!poolDict.TryGetValue(data.key, out var poolkey))
        { 
            // Ǯ��ųʸ��� ������ ��ųʸ��� Ű�� �� �߰�
            poolDict.Add(data.key, pool);
            dataDict.Add(data.key, data);
        }
        // Ű���� ������ ���� �߰�
        poolDict[data.key] = pool;
        dataDict[data.key] = data;
    }
    public GameObject GetObject(KeyType key)
    {
        // Ǯ ��ųʸ��� Ű�� �������� �ʴ� ��� null ����
        if (!poolDict.TryGetValue(key, out var pool)) { return null; }
        GameObject obj = null;
        // ��ųʸ� Ǯ���� ������
        if (pool.Count > 0)
        {
            obj = pool.Pop();
        }
        // Ǯ�� �����Ͱ� ������ ������ ������ ��ųʸ����� Ű������ ������ ������ �ٽ� ����
        else
        {
            if (dataDict.TryGetValue(key, out var data))
            {
                CreateObject(data);
                // ����� Ǯ �ٽ� Ȯ��
                poolDict.TryGetValue(key, out var newpool);
                obj = newpool.Pop();
            }
        }
        // ������ ������Ʈ �θ� ������ Ȱ��ȭ
        obj.transform.SetParent(null);
        obj.gameObject.SetActive(true);
        return obj;
    }
    public void ReturnObject(GameObject obj, KeyType key)
    {
        // Ǯ ��ųʸ��� Ű ������ ������ �μ�
        if (!poolDict.TryGetValue(key , out var pool))
        {
            Destroy(obj);
            return;
        }
        // ������ �ڱⰡ ������ �����̳ʿ� �ٽ� ����
        GameObject poolobj = GameObject.Find(key.ToString());//�̰� ���ε� ã���ʰ�
        obj.transform.SetParent(poolobj.transform);
        obj.SetActive(false);
        pool.Push(obj);
    }
}
