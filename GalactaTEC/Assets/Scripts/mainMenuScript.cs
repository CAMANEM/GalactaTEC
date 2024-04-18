using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void _HallofFameButtonOnClick()
    {
        Debug.Log("Opening hall of fame window");
        SceneManager.LoadScene("HallofFameScene");
    }

    public void _ExitButtonOnClick()
    {
        Application.Quit();
    }
}
