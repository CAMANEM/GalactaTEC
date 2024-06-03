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
        //SwitchPower();
    }

    void SwitchPower(){
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            
            Vector3 pos = GetComponent<RectTransform>().anchoredPosition;
            if (pos.x < 1550f)
            {
                pos.x += 100f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                powerSelected++;
            }
            else
            {
                pos.x = 1250f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                powerSelected = 0;
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Vector3 pos = GetComponent<RectTransform>().anchoredPosition;
            if (pos.x > 1250f)
            {
                pos.x -= 100f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                powerSelected--;
            }
            else
            {
                pos.x = 1550f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                powerSelected = 3;
            }
        }
    }
}
