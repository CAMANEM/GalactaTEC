using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnCollision(){

        Debug.Log("Enemy: Collision");
    }

    void OnTriggerEnter2D(){

        Debug.Log("Enemy: Trigger");
    }
}
