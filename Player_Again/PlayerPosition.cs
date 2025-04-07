using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private Transform playerTransform;
    public static PlayerPosition instance;
    void Start()
    {
        // 시작할 때 플레이어의 Transform 컴포넌트를 가져옵니다
        playerTransform = transform;
    }

    void Update()
    {
        // 현재 플레이어의 위치를 가져옵니다
        Vector3 currentPosition = playerTransform.position;
        
        
    }

    // 다른 스크립트에서 플레이어의 위치를 가져올 수 있는 메서드
    public Vector3 GetPlayerPosition()
    {
        return playerTransform.position;
    }
    public void repo(){
         playerTransform.position = new Vector3(-10f, -1.5f, 0f);
    }
}