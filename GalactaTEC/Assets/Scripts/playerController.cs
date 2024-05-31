using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour

{
    //private PlayerLives playerLives;

    public float maxSpeed = 0.2f;

    [SerializeField]
    private GameObject normalShot;

    [SerializeField]
    private GameObject chaserBullet;

    [SerializeField]
    private GameObject shieldPower;

    [SerializeField]
    private GameObject shieldMidPower;

    [SerializeField]
    private GameObject shieldMinPower;

    public int shieldLevel = 0;

    [SerializeField]
    private GameObject bonusAura;

    [SerializeField]
    private Transform attack_Point;
    public GameObject ExpansiveShot_0;

    public GameObject explosion;

    public GameObject bonus;


    public bool expansiveShot = false;
    public bool chaserShot = false;
    public bool x2Pts = false;
    public bool shield = false;

    int chaserShotsCounter = 0;

    public int lifes = 3;        // Vidas actuales del jugador
    public bool damaged = false;        // Vidas actuales del jugador
    public int maxLifes = 4;     // M�ximo de vidas que el jugador puede tener


    // Start is called before the first frame update
    void Start()
    {
        shieldPower.SetActive(false);
        shieldMidPower.SetActive(false);
        shieldMinPower.SetActive(false);
        instanciateBonus();
        //playerLives = GetComponent<PlayerLives>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                //playerLives.AddLife();        // Llamar a AddLife() en el script PlayerLives
            }

            Move();
            Attack();
        }
    }

    void Move()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.A)) {
            
            MoveAux();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveAux();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            MoveAux();
        }
        else if (Input.GetKeyDown(KeyCode.S))
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
        else if (Input.GetKeyDown(KeyCode.E))
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
                    bonusAura.SetActive(true);
                    Invoke("desactivateBonusAura", 15f);
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
                    shieldLevel = 3;
                    Debug.Log("Shield Shot used and deactivated");
                }
                break;
        }
    }

    private void desactivateBonusAura(){
        bonusAura.SetActive(false);
    }

    public void instanciateBonus(){
        // x 959.4363
        // x 962.55
        // y 608.34
        // y 608.38
        Vector3 pos =  new Vector3(959f, 608.10f, 10f); 
        int randX = Random.Range(960, 962);
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



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            getHighDamage();
        }
        else if (collision.gameObject.tag == "EnemyShot")
        {
            getLowDamage();
        }
        else if (collision.gameObject.tag == "ChargedShot")
        {
            getHighDamage();
        }

    }

    private void destroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Absorbs the damage if it is active
    private void getLowDamageShield(){

        if (shieldLevel == 1)
        {
            shieldLevel--;
            shieldMinPower.SetActive(false);
        }
        else if (shieldLevel == 2)
        {
            shieldLevel--;
            shieldMidPower.SetActive(false);;
            shieldMinPower.SetActive(true);
        }
        else if (shieldLevel == 3)
        {
            shieldLevel--;
            shieldPower.SetActive(false);;
            shieldMidPower.SetActive(true);
        }
    }

    // Absorbs the damage if it is active
    private void getHighDamageShield(){

        if (shieldLevel == 1)
        {
            shieldLevel--;
            shieldMinPower.SetActive(false);
            getLowDamage();
        }
        else if (shieldLevel == 2)
        {
            shieldLevel -= 2;
            shieldMidPower.SetActive(false);
        }
        else if (shieldLevel == 3)
        {
            shieldLevel -= 2;
            shieldPower.SetActive(false);;
            shieldMinPower.SetActive(true);
        }
    }


    // Controls life when receives low damage
    private void getLowDamage(){

        if (0 < shieldLevel)
        {
            getLowDamageShield();
        }
        else if(damaged)
        {
            lifes--;
            damaged = false;
            // change player
            destroy();
        }
        else{
            damaged = true;
        }
        updateUiLifes();
    }

    // Controls life whe receives high damage
    private void getHighDamage(){
        if (0 < shieldLevel)
        {
            getHighDamageShield();
        }
        else
        {
            damaged = false;
            lifes--;
            updateUiLifes();
            // change player
            destroy();    
        }
    }

    private void updateUiLifes(){
        GameObject.Find("txtLifes").GetComponent<LifeScript>().updateLifes(lifes, damaged);
    }

    public void increaseLifes(){
        lifes++;
        updateUiLifes();
    }

}