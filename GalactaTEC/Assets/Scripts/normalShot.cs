using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShot : MonoBehaviour
{

    public float speed = 500f;
    public float deactivateTimer = 5f;

    //public AudioSource source;
    //public AudioClip audioClip;

    //public float volume=0.5f;
    private PointManager pointManager;


    // Start is called before the first frame update
    void Start()
    {
        // Invoke("DeactivateGameObject", deactivateTimer);
        // source.PlayOneShot(audioClip, volume);

        //vea esto mae XD es para el puntaje
        //  pointManager = GameObject.Find("PointMananger").GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    void DeactivateGameObject(){

        gameObject.SetActive(false);
    }
}