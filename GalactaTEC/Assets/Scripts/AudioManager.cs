using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

namespace audio_manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer myMixer;
        public AudioSource source;
        public AudioClip bgMusic;
        float volume = 1f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            changeVolume();
        }

        private static AudioManager instance;

        public static AudioManager getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("audio_manager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void changeVolume()
        {

            if (Input.GetKeyDown(KeyCode.W))
            {

                volume += 0.1f;
                myMixer.SetFloat("Music", (float)Math.Log10(volume) * 20f);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                volume -= 0.1f;
                myMixer.SetFloat("Music", (float)Math.Log10(volume) * 20f);
            }
        }

        public void playTitleSoundtrack()
        {
            source.clip = bgMusic;
            source.Play();
        }
    }
}
