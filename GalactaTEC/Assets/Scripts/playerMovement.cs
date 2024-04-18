using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    float maxSpeed = 3.5f;

    [SerializeField]
    private GameObject normalBullet;

    [SerializeField]
    private Transform attack_Point;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();

    }

    void Move()
    {
        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
        pos.y += Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;
        transform.position = pos;
    }

    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(normalBullet, attack_Point.position, Quaternion.identity);
        }
    }
}