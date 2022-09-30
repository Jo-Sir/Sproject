using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject spawnPosition;
    public UIController playerUI;
    public PlayerController player;
    public GunController gunController;
    public GunsAudioController gunsAudioController;
    public AudioController audioController;
    private Collider mapcollider;
    public int spwanCount;
    
    private new void Awake()
    {
        mapcollider = spawnPosition.GetComponent<Collider>();
    }
    private void Start()
    {
        StageStart();
    }
    public void StageStart()
    {
        for (int i = 0; i < spwanCount; i++)
        {
            GameObject obj;
            obj = ObjectPoolManager.Instance.GetObject((KeyType)(Random.Range(2, 5)));
            obj.transform.position = GetRandomPosion();
        }
    }
    public Vector3 GetRandomPosion()
    {
        Vector3 mapPosition = spawnPosition.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = mapcollider.bounds.size.x;
        float range_Z = mapcollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X - 10f, mapPosition.y, range_Z - 10f);
        Vector3 respawnPosition = mapPosition + RandomPostion;
        return respawnPosition;
    }
}
