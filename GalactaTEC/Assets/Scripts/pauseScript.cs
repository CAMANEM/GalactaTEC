using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using audio_manager;

public class pauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void puaseButtonOnClick()
    {
        AudioManager.getInstance().pauseSoundtrack();
        Time.timeScale = 0f;
    }
}
