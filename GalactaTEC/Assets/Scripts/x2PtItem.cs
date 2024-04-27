using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class x2PtItem : MonoBehaviour
{

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(){
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
    }

    public void Desactivate(){
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }
}
