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
    public AudioClip bonusSound;
    public float volume=0.5f;
    public float minY = 350f;
    public float maxY = 910f;
    public float minX = 300f;
    public float maxX = 1600f;


    public bool expansiveShot = false;
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
            usePower();
        }
    }

    public void ActivateChaser(){
        source.PlayOneShot(bonusSound, volume);
        chaserShot = true;
        ChaserItem chaserScript = GameObject.Find("ChaserShot").GetComponent<ChaserItem>();
        chaserScript.Activate();
    }

    public void ActivateExpansive(){
        source.PlayOneShot(bonusSound, volume);
        expansiveShot = true;
        ExpansiveItem expansiveScript = GameObject.Find("ExpansiveShot").GetComponent<ExpansiveItem>();
        expansiveScript.Activate();
    }

    public void ActivateShield(){
        source.PlayOneShot(bonusSound, volume);
        shield = true;
        ShieldItem shieldScript = GameObject.Find("Shield").GetComponent<ShieldItem>();
        shieldScript.Activate();
    }

    public void ActivateX2Pts(){
        source.PlayOneShot(bonusSound, volume);
        x2Pts = true;
        x2PtItem x2PtsScript = GameObject.Find("x2ptShot").GetComponent<x2PtItem>();
        x2PtsScript.Activate();
    }

    void usePower(){

        PowerSelector powerScript = GameObject.Find("PowerSelector").GetComponent<PowerSelector>();
        switch (powerScript.powerSelected)
        {
            case 0:
                if (chaserShot)
                {
                    chaserShot = false;
                    ChaserItem chaserScript = GameObject.Find("ChaserShot").GetComponent<ChaserItem>();
                    chaserScript.Desactivate();
                    Instantiate(explosiveShot, attack_Point.position, Quaternion.identity);
                    Debug.Log("Chaser used and deactivated");
                }
                break;
            case 1:
                if (expansiveShot)
                {
                    expansiveShot = false;
                    ExpansiveItem expansiveScript = GameObject.Find("ExpansiveShot").GetComponent<ExpansiveItem>();
                    expansiveScript.Desactivate();
                    Instantiate(explosiveShot, attack_Point.position, Quaternion.identity);
                    Debug.Log("Expansive Shot used and deactivated");
                }
                break;
            case 2:
                if (x2Pts)
                {
                    x2Pts = false;
                    x2PtItem x2PtsScript = GameObject.Find("x2ptShot").GetComponent<x2PtItem>();
                    x2PtsScript.Desactivate();
                    Instantiate(explosiveShot, attack_Point.position, Quaternion.identity);
                    Debug.Log("x2Pts Shot used and deactivated");
                }
                break;
            case 3:
                if (shield)
                {
                    shield = false;
                    ShieldItem shieldScript = GameObject.Find("Shield").GetComponent<ShieldItem>();
                    shieldScript.Desactivate();
                    Instantiate(explosiveShot, attack_Point.position, Quaternion.identity);
                    Debug.Log("Shield Shot used and deactivated");
                }
                break;
        }
    }
}