using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalBullet : MonoBehaviour
{

    public float speed = 5f;
    public float deactivateTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
    }
}
