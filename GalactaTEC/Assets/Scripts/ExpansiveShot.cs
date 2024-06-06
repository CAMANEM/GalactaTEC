using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using audio_manager;


public class ExpansiveShot : MonoBehaviour
{
    [SerializeField] public GameObject explosion;

    public float speed = 500f;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.getInstance().playShotEffect();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy"){
            Destroy(collision.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.kabum(pos);
    }
  */  
}
