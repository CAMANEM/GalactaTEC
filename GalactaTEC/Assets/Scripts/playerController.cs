using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using audio_manager;

public class PlayerController : MonoBehaviour

{
    private PlayerLives playerLives;

    public float maxSpeed = 0.2f;

    [SerializeField]
    private GameObject normalShot;

    [SerializeField]
    private GameObject expansiveBullet;

    [SerializeField]
    private GameObject chaserBullet;

    [SerializeField]
    private GameObject shieldPower;

    [SerializeField]
    private GameObject shieldMidPower;

    [SerializeField]
    private GameObject shieldMinPower;

    [SerializeField]
    private Transform attack_Point;
    public GameObject ExpansiveShot_0;

    public GameObject explosion;


    public GameObject bonus;


    //public AudioSource source;
    //public AudioClip audioClip;
    //public AudioClip bonusSound;
    public float volume=0.5f;
    public float minY = 350f;
    public float maxY = 910f;
    public float minX = 300f;
    public float maxX = 1600f;


    public bool expansiveShot = false;
    public bool chaserShot = false;
    public bool x2Pts = false;
    public bool shield = false;

    int chaserShotsCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        shieldPower.SetActive(false);
        shieldMidPower.SetActive(false);
        shieldMinPower.SetActive(false);
        playerLives = GetComponent<PlayerLives>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            playerLives.AddLife();        // Llamar a AddLife() en el script PlayerLives
        }

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
        //AudioManager.getInstance().playMovementEffect();
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
        //source.PlayOneShot(bonusSound, volume);
        chaserShot = true;
        ChaserItem chaserScript = GameObject.Find("ChaserShot").GetComponent<ChaserItem>();
        chaserScript.Activate();
    }

    public void ActivateExpansive(){
        //source.PlayOneShot(bonusSound, volume);
        expansiveShot = true;
        ExpansiveItem expansiveScript = GameObject.Find("ExpansiveShot").GetComponent<ExpansiveItem>();
        expansiveScript.Activate();
    }

    public void ActivateShield(){
        //source.PlayOneShot(bonusSound, volume);
        shield = true;
        ShieldItem shieldScript = GameObject.Find("Shield").GetComponent<ShieldItem>();
        shieldScript.Activate();
    }

    public void ActivateX2Pts(){
        //source.PlayOneShot(bonusSound, volume);
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
                    if (chaserShotsCounter == 2)
                    {
                        chaserShot = false;
                        ChaserItem chaserScript = GameObject.Find("ChaserShot").GetComponent<ChaserItem>();
                        chaserScript.Desactivate();
                        chaserShotsCounter = 0;
                    }
                    Instantiate(chaserBullet, attack_Point.position, Quaternion.identity);
                    Debug.Log("Chaser used and deactivated");
                    chaserShotsCounter++;
                }
                break;
            case 1:
                if (expansiveShot)
                {
                    expansiveShot = false;
                    ExpansiveItem expansiveScript = GameObject.Find("ExpansiveShot").GetComponent<ExpansiveItem>();
                    expansiveScript.Desactivate();
                    Instantiate(ExpansiveShot_0, attack_Point.position, Quaternion.identity);
                    Debug.Log("Expansive Shot used and deactivated");
                }
                break;
            case 2:
                if (x2Pts)
                {
                    x2Pts = false;
                    x2PtItem x2PtsScript = GameObject.Find("x2ptShot").GetComponent<x2PtItem>();
                    x2PtsScript.Desactivate();
                    GameSceneScript gameSceneScript = GameObject.Find("Canvas").GetComponent<GameSceneScript>();
                    gameSceneScript.activateX2Pts();
                    Debug.Log("x2Pts Shot used and deactivated");
                }
                break;
            case 3:
                if (shield)
                {
                    shield = false;
                    ShieldItem shieldScript = GameObject.Find("Shield").GetComponent<ShieldItem>();
                    shieldScript.Desactivate();
                    shieldPower.SetActive(true);
                    Debug.Log("Shield Shot used and deactivated");
                }
                break;
        }
    }

    public void instanciateBonus(){
        Vector3 pos =  new Vector3(300f, 1100f, 10f); 
        int randX = Random.Range(300, 1600);
        pos.x = (float)randX;
        Instantiate(bonus, pos, Quaternion.identity);

    }

    /*
        randomizes if a bonus should generate. And invoke its generation
    */
    public void generateBonus(){
        int randNum = Random.Range(0, 2);
        if (randNum == 1)
        {
            Invoke("instanciateBonus", 2f);
        }
    }

}