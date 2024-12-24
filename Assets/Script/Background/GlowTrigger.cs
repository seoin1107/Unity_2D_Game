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
            // SpriteRenderer 컴포넌트를 가져와 색 변경
            SpriteRenderer spriteRenderer = Glow.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red; // 원하는 색으로 변경
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
