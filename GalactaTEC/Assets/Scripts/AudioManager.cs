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
        public AudioClip clip;
        float volume = 1f;

        private string[] backgroundSoundtracksPaths = { "Audio/moveSound", "Audio/bonusSound" };
        private string[] gameSoundtracksPaths = { "Audio/BGMusic1" };

        private string[] currentPlaylist;

        private string clipPath;

        public bool isAudioPaused = false;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            changeVolume();

            checkSoundtrackActivity();
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

        public void playBackgroundSoundtrack()
        {
            this.currentPlaylist = this.backgroundSoundtracksPaths;

            this.source.Stop();
        }

        public void playGameSoundtrack()
        {
            this.currentPlaylist = this.gameSoundtracksPaths;

            this.source.Stop();
        }

        // Verifies if there are soundtracks playing, if there are, does nothing, if not, play a random soundtrack of currentPlaylist array
        private void checkSoundtrackActivity()
        {
            if (!source.isPlaying && !this.isAudioPaused)
            {
                int index = UnityEngine.Random.Range(0, this.currentPlaylist.Length);

                this.clipPath = this.currentPlaylist[index];

                this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                source.clip = this.clip;

                source.Play();
            }
        }
    }
}
