using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : Player2D
{
    private MeshRenderer render;

    private float offset;
    public float speed;

    void Start()
    {
        render = GetComponent<MeshRenderer>();   
    }

    void Update()
    {
        if(moveDir.x == Input.GetAxisRaw("Horizontal"))
        {
            //offset += CharacterStatus.moveSpeed * speed;
        }
        
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
