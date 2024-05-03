using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using audio_manager;

public class NormalShot : MonoBehaviour
{

    public float speed = 500f;
    public float deactivateTimer = 5f;

    private PointManager pointManager;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playShotEffect();

        //vea esto mae XD es para el puntaje
        //pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnCollision(){

        Debug.Log("Shot: Collision");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy"){
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            GameSceneScript gameSceneScriptScript = GameObject.Find("Canvas").GetComponent<GameSceneScript>();
            gameSceneScriptScript.updateScore(50);
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    // Destroy(collision.gameObject);
    // pointManager.UpdateScore(50)
    // Destroy(gameObject);
    //}

    //if (collision.gameObject.tag == "Boundary")
    //{
    //Destroy(gameObject);
    //        }
    //   }




    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
        
    }

}