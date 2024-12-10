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


    //public bool isLocked = true; // 문 잠금 상태

    //public void Lock()
    //{
    //    isLocked = true;
    //    // 잠금 처리 (예: 애니메이션, 물리적 상태 등)
    //}

    //public void Unlock()
    //{
    //    isLocked = false;
    //    // 잠금 해제 처리 (예: 애니메이션, 물리적 상태 등)
    //}
}
