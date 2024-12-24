using UnityEngine;

public class BossRoomManager : RoomManager
{
    public GameObject boss; // ���� ������Ʈ
    public float doorMoveDistance = 5.0f; // ���� �̵��� �Ÿ�
    public float doorMoveSpeed = 5.0f; // ���� �̵��ϴ� �ӵ�

    protected override void Start()
    {
        
        SetCameraPriority(defaultCamera, HighPriority); // �⺻ ī�޶� Ȱ��ȭ
        SetCameraPriority(virtualCamera, LowPriority); // �� ī�޶�� ��Ȱ��ȭ
        if (boss != null)
        {
            boss.SetActive(false); // ������ �ʱ� ���¿��� ��Ȱ��ȭ
        }
    }

    public override void EnterRoom()
    {
        base.EnterRoom();
        if (boss != null && !isRoomCleared)
        {
            StartBossFight(); // ������ ����
        }
        LockDoors();
    }

    public override void ExitRoom()
    {
        //if (!isRoomCleared) return; // ������ óġ���� �ʾҴٸ� ���� �� ����
        base.ExitRoom(); // �θ� Ŭ������ ExitRoom ȣ��
    }

    public override void ClearRoom()
    {
        isRoomCleared = true; // ���� óġ ���
        if (boss != null)
        {
            boss.SetActive(false); // ���� ��Ȱ��ȭ
        }
        UnlockDoors();
    }

    protected override void LockDoors()
    {
        foreach (var door in doors)
        {
            StartCoroutine(MoveDoor(door.transform, Vector3.down * doorMoveDistance)); // ���� �Ʒ��� �̵�
        }
    }

    protected override void UnlockDoors()
    {
        foreach (var door in doors)
        {
            StartCoroutine(MoveDoor(door.transform, Vector3.up * doorMoveDistance)); // ���� ���� �̵�
        }
    }

    private void StartBossFight()
    {
        if (boss != null)
        {
            boss.SetActive(true); // ���� Ȱ��ȭ
        }
    }

    private System.Collections.IEnumerator MoveDoor(Transform doorTransform, Vector3 targetOffset)
    {
        Vector3 startPos = doorTransform.position;
        Vector3 targetPos = startPos + targetOffset;
        float elapsedTime = 0f;

        while (elapsedTime < doorMoveDistance / doorMoveSpeed)
        {
            doorTransform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime * doorMoveSpeed) / doorMoveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.position = targetPos; // ���� ��ġ ����
    }
}
