using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{

    public GameObject[] lifes;
    public GameObject[] halfLifes;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject life4;
    public GameObject halfLife1;
    public GameObject halfLife2;
    public GameObject halfLife3;
    public GameObject halfLife4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLifes(int playerLife, bool damaged){
        playerLife--;
        for (int i = 0; i <= 3; i++) {
            Debug.Log(i);
            if(i < playerLife)
            {
                lifes[i].SetActive(true);
                halfLifes[i].SetActive(false);
            }
            else if (i == playerLife)
            {
                if (damaged)
                {
                    lifes[i].SetActive(false);
                    halfLifes[i].SetActive(true);
                }
                else{
                    lifes[i].SetActive(true);
                    halfLifes[i].SetActive(false);
                }
            }
            else if(i > playerLife)
            {
                lifes[i].SetActive(false);
                halfLifes[i].SetActive(false);
            }
        }
    }
}
