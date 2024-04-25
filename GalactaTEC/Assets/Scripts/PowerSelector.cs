using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSelector : MonoBehaviour
{
    public int powerSelected = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NextPower();
    }

    void NextPower(){
        if(Input.GetKeyDown(KeyCode.N)) {
            
            Vector3 pos = GetComponent<RectTransform>().anchoredPosition;
            if (pos.x <= 600f)
            {
                pos.x += 116.6f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                powerSelected++;
            }
            else
            {
                pos.x = 306.4f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                powerSelected = 0;
            }
            
        }
    }
}