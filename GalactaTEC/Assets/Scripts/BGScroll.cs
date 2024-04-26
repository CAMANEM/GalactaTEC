using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{

    public float scrollSpeed = 0.1f;
    private MeshRenderer meshRenderer;
    private float yScroll;

    
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Scroll(){

        yScroll = Time.time * scrollSpeed;

        Vector2 offset = new Vector2(yScroll, 0f);
        //meshRenderer.sharedMaterial.setTextureOffset("_MainTex", offset);
    }
}
