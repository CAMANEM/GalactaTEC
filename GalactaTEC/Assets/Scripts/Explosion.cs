using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Enemy"){
            Destroy(collision.gameObject);
            GameSceneScript gameSceneScriptScript = GameObject.Find("Canvas").GetComponent<GameSceneScript>();
            gameSceneScriptScript.updateScore(200);
        }
    }

    
}
