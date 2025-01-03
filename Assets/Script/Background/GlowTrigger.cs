using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GlowTrigger : MonoBehaviour
{
    public GameObject Glow;
    float time = 0.0f;
    float F_time = 0.7f;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Glow != null)
        {
            // SpriteRenderer ������Ʈ�� ������ �� ����
            SpriteRenderer spriteRenderer = Glow.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red; // ���ϴ� ������ ����
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
