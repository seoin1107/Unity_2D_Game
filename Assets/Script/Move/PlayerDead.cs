using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerDead : MonoBehaviour
{
    public CharacterStatus characterStatus; // CharacterStatus ����
    public GameObject blackBackground;     // ���� ��� �̹���
    public GameObject tryAgainText;              // "Try Again?" �ؽ�Ʈ UI
    public GameObject pressAnyKeyText;           // "Press Any Key" �ؽ�Ʈ UI
    private bool isDead = false;           // ��� ó�� ���� �÷���

    void Start()
    {
        if (characterStatus == null)
        {
            // ���� ������Ʈ���� CharacterStatus ������Ʈ�� �����ɴϴ�.
            characterStatus = GetComponent<CharacterStatus>();
        }

        // �ؽ�Ʈ �ʱ�ȭ (��Ȱ��ȭ ����)
        if (tryAgainText != null)
        {
            tryAgainText.gameObject.SetActive(false);
        }
        if (pressAnyKeyText != null)
        {
            pressAnyKeyText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ��� ó���� �� ���� ����
        if (!isDead && characterStatus != null && characterStatus.characterStat.curHP <= 0)
        {
            isDead = true; // ��� ó�� ����
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        // �÷��̾� ���̾��� ���
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("�÷��̾� ��� ó��");

            // ���� ��� Ȱ��ȭ
            if (blackBackground != null)
            {
                blackBackground.SetActive(true);
            }

            // �ٽ� ���� �޽��� �� �Է� ���
            StartCoroutine(WaitForRetry());
        }
    }

    IEnumerator WaitForRetry()
    {
        yield return new WaitForSeconds(1f); // 1�� ���
        // "Try Again?" Ȱ��ȭ
        if (tryAgainText != null)
        {
            tryAgainText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f); // "Try Again?" �� 1�� ���

        // "Press Any Key" Ȱ��ȭ
        if (pressAnyKeyText != null)
        {
            pressAnyKeyText.gameObject.SetActive(true);
        }

        Debug.Log("�ٽ� �����Ϸ��� �ƹ� Ű�� ��������.");

        // �ƹ� Ű�� ���� ������ ���
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // �ε� �� ȣ�� �� ���� �̵�
        Loading.nScene = 2;
        SceneManager.LoadScene(1);
    }
}
