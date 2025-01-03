using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWall : MonoBehaviour
{
    public GameObject deletGround;
    public GameObject targetObj;

    private bool Inpotal = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetObj = collision.gameObject;
            Inpotal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Inpotal = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Inpotal && Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(movingWall());
        }
    }
    IEnumerator movingWall()
    {
        float duration = 3.0f; // 3초 동안 이동
        float elapsedTime = 0f; // 경과 시간 초기화
        float moveSpeed = 1.0f; // 초당 이동 속도
        while (elapsedTime < duration)
        {
            if (deletGround != null)
            {
                Vector3 position = deletGround.transform.position;
                position.y += moveSpeed * Time.deltaTime; // y축으로 이동
                deletGround.transform.position = position;
            }

            elapsedTime += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
    }
}
