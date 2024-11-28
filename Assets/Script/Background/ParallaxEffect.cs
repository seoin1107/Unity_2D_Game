using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public string playerLayerName = "Player"; // Player가 속한 Layer 이름
    public Vector2 parallaxFactor = new Vector2(1.0f, 1.0f); // X, Y축에 대한 Parallax 비율

    private Transform player;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        // Player Layer에 속한 오브젝트를 찾아 설정
        int playerLayer = LayerMask.NameToLayer(playerLayerName);

        if (playerLayer >= 0)
        {
            GameObject playerObject = FindObjectOfType<GameObject>();
            foreach (var obj in FindObjectsOfType<GameObject>())
            {
                if (obj.layer == playerLayer)
                {
                    player = obj.transform;
                    break;
                }
            }
        }

        if (player != null)
        {
            lastPlayerPosition = player.position;
        }
        else
        {
            Debug.LogWarning($"Player Layer '{playerLayerName}'에 속한 오브젝트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // 플레이어 이동량 계산
        Vector3 deltaMovement = player.position - lastPlayerPosition;

        // 배경을 플레이어 이동량에 따라 조절 (X, Y축에 각각 Parallax Factor 적용)
        transform.position += new Vector3(0, deltaMovement.y * parallaxFactor.y, 0);

        // 플레이어의 현재 위치 업데이트
        lastPlayerPosition = player.position;
    }
}
