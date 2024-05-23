using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    public float speed = 0.4f;
    public GameObject shotDestruction;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(180f, 0, 0, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        transform.position = temp;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "HorizontalBoundary")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerShot")
        {
            Debug.Log("Collision entre tiros");
        }
    }

    void OnDestroy()
    {
        Instantiate(shotDestruction, transform.position, Quaternion.identity);
    }
}
