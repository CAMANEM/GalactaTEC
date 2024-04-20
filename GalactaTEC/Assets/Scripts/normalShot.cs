using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalShot : MonoBehaviour
{

    public float speed = 500f;
    public float deactivateTimer = 5f;

    public AudioSource source;
    public AudioClip audioClip;

    public float volume=0.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        // Invoke("DeactivateGameObject", deactivateTimer);
        source.PlayOneShot(audioClip, volume);
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

    void DeactivateGameObject(){

        gameObject.SetActive(false);
    }
}