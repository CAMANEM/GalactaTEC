using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    public float volume=0.5f;
    public float minY = 350f;
    public float maxY = 910f;
    public float minX = 300f;
    public float maxX = 1600f;


    public bool expasiveShot = false;
    public bool chaserShot = false;
    public bool x2Pts = false;
    public bool shield = false;

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

        if(Input.GetKeyDown(KeyCode.LeftArrow) && (pos.x > minX)) {
            
            MoveAux();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && (pos.x < maxX))
        {
            MoveAux();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (pos.y < maxY))
        {
            MoveAux();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)  && (pos.y > minY))
        {
            MoveAux();
        }
    }

    void MoveAux(){
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

    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(normalShot, attack_Point.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(explosiveShot, attack_Point.position, Quaternion.identity); 
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateChaser();
        }
    }

    public void ActivateChaser(){
        chaserShot = true;
        ChaserItem chaserScript = GameObject.Find("ChaserShot").GetComponent<ChaserItem>();
        chaserScript.Activate();
    }

    public void ActivateExpansive(){
        expasiveShot = true;
        ExpansiveItem expansiveScript = GameObject.Find("ExpansiveShot").GetComponent<ExpansiveItem>();
        expansiveScript.Activate();
    }

    public void ActivateShield(){
        shield = true;
        ShieldItem shieldScript = GameObject.Find("Shield").GetComponent<ShieldItem>();
        shieldScript.Activate();
    }

    public void ActivateX2Pts(){
        x2Pts = true;
        x2PtItem x2PtsScript = GameObject.Find("x2ptShot").GetComponent<x2PtItem>();
        x2PtsScript.Activate();
    }
}