using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    private static DontDestroyOnLoadObject instance;

    void Awake()
    {
        // 이미 인스턴스가 존재한다면, 중복 방지를 위해 이 객체를 파괴합니다.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스 저장 및 씬 전환 시 삭제되지 않도록 설정
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
