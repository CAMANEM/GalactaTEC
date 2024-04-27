using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn1PlayerScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void _1PlayerButtonOnClick()
    {
        // ToDo: Updates quantity of players to 1
        SceneManager.LoadScene("LoginScene");
    }

    public void _2PlayersButtonOnClick()
    {
        // ToDo: Updates quantity of players to 2
        SceneManager.LoadScene("LoginScene");
    }
}
