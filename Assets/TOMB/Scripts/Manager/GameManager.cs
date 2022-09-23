using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject map;
    public UIController uI;
    public PlayerController player;
    public GunController gunController;
    private Collider mapcollider;
    [Range(0f, 2000f)] public float mouseSensitivity;
    public int spwanCount;
    public int stageCount;
    private new void Awake()
    {
        base.Awake();
        mapcollider = map.GetComponent<Collider>();
    }
    private void Start()
    {
        StageStart();
    }
    public void StageStart()
    {
        
        if (stageCount >=4) { stageCount = 4; }
        for (int i = 0; i< spwanCount; i++)
        {   
            GameObject obj;
            obj = ObjectPoolManager.Instance.GetObject((KeyType)(Random.Range(2, stageCount + 2)));
            obj.transform.position = GetRandomPosion();
        }
    }
    public Vector3 GetRandomPosion()
    {
        Vector3 mapPosition = map.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = mapcollider.bounds.size.x;
        float range_Z = mapcollider.bounds.size.z;
        
        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, mapPosition.y, range_Z);
        Vector3 respawnPosition = mapPosition + RandomPostion;
        return respawnPosition;
    }
}
