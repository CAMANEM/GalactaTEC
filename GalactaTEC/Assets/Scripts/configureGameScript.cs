using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using audio_manager;

public class configureGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.getInstance().playBackgroundSoundtrack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
