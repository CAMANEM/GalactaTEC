using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserShot : MonoBehaviour
{

    public float speedY = 500f;
    public float speedX = 300f;
    public float deactivateTimer = 5f;
    private PointManager pointManager;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBonusEffect();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.y += speedY * Time.deltaTime;
        temp.x -= speedX * Time.deltaTime;
        transform.position = temp;
    }
}
