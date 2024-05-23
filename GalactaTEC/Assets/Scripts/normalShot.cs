using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using audio_manager;

public class NormalShot : MonoBehaviour
{

    public float speed = 0.4f;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playShotEffect();
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
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy"){
            GameSceneScript gameSceneScriptScript = GameObject.Find("Canvas").GetComponent<GameSceneScript>();
            gameSceneScriptScript.updateScore(200);
            destroy();
        }
        else if (collision.gameObject.tag == "EnemyShot")
        {
            destroy();
        }
    }


    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += speed * Time.deltaTime;
        transform.position = temp;
        
    }

    private void destroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}