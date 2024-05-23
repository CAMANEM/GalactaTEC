using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(180f, 0, 0, Space.Self);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
