using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Player의 Transform
    public Transform playerTransform;

    // 이동 속도 조정
    public float speed = 0.125f;

    // Start는 첫 프레임 전에 한 번 실행
    void Start()
    {
        if (playerTransform != null)
        {
            // 초기 카메라 위치를 플레이어 위치로 설정
            transform.position = new Vector3(
                playerTransform.position.x,
                playerTransform.position.y,
                transform.position.z
            );
        }
    }

    // Update는 매 프레임 실행
    void Update()
    {
        if (playerTransform != null)
        {
            // 목표 위치 계산
            Vector3 targetPosition = new Vector3(
                playerTransform.position.x,
                playerTransform.position.y,
                transform.position.z
            );

            // 현재 위치에서 목표 위치로 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
