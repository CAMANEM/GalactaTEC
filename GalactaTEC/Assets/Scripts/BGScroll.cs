using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{

    [SerializeField] private RawImage image;
    [SerializeField] private float xScroll, yScroll;


    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(xScroll, yScroll) * Time.deltaTime, image.uvRect.size);
    }

    
}
