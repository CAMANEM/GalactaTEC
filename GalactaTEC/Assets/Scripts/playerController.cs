using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    float maxSpeed = 150f;

    [SerializeField]
    private GameObject normalShot;

    [SerializeField]
    private GameObject explosiveShot;

    [SerializeField]
    private Transform attack_Point;



    public AudioSource source;
    public AudioClip audioClip;
    public float volume = 0.5f;


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
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            source.PlayOneShot(audioClip, volume);
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            // Calculate the movement direction
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            // Calculate the new position based on the fixed movement distance
            Vector3 newPosition = transform.position + movement * maxSpeed;
            // Update the player's position
            transform.position = newPosition;
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(normalShot, attack_Point.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(explosiveShot, attack_Point.position, Quaternion.identity); 
        }
    }
}