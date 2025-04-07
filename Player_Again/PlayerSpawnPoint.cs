using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour 
{
    void Start()
    {
        // 저장된 위치로 플레이어 이동
        float x = PlayerPrefs.GetFloat("SpawnX", 2f);
        float y = PlayerPrefs.GetFloat("SpawnY", 23f);
        float z = PlayerPrefs.GetFloat("SpawnZ", 0f);

        transform.position = new Vector3(x, y, z);
    }
}
