using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            door.SetActive(true);
        }
    }


    //public bool isLocked = true; // �� ��� ����

    //public void Lock()
    //{
    //    isLocked = true;
    //    // ��� ó�� (��: �ִϸ��̼�, ������ ���� ��)
    //}

    //public void Unlock()
    //{
    //    isLocked = false;
    //    // ��� ���� ó�� (��: �ִϸ��̼�, ������ ���� ��)
    //}
}
