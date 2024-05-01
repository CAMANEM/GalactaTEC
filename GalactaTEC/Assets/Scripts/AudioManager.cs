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
        [SerializeField] public AudioSource musicSource;
        [SerializeField] public AudioSource sfxSource;
        [SerializeField] public AudioClip clip;
        [SerializeField] public AudioClip movementClip;
        [SerializeField] public AudioClip bonusClip;
        [SerializeField] public AudioClip shotClip;
        [SerializeField] public AudioClip shipDestructionClip;
        [SerializeField] float volume = 1f;

        private string[] backgroundSoundtracksPaths = { "Audio/Soundtracks/Menu/Menu - Anarchic Starport", "Audio/Soundtracks/Menu/Menu - Blaze Your Own Trail",
                                                        "Audio/Soundtracks/Menu/Menu - Coma Berenices", "Audio/Soundtracks/Menu/Menu - Delta Phoenicis",
                                                        "Audio/Soundtracks/Menu/Menu - Draco", "Audio/Soundtracks/Menu/Menu - Fawoal",
                                                        "Audio/Soundtracks/Menu/Menu - Imperial Starport", "Audio/Soundtracks/Menu/Menu - Neutral Blue Giant",
                                                        "Audio/Soundtracks/Menu/Menu - Neutral Magnetar", "Audio/Soundtracks/Menu/Menu - New Alliance",
                                                        "Audio/Soundtracks/Menu/Menu - Quator", "Audio/Soundtracks/Menu/Menu - Tarach Tor",
                                                        "Audio/Soundtracks/Menu/Menu - Ursa Major", "Audio/Soundtracks/Menu/Menu - Virgo",
                                                        "Audio/Soundtracks/Menu/Menu - Zelada" };

        private string[] level1SoundtracksPaths = { "Audio/Soundtracks/Level/Level - Section 100", "Audio/Soundtracks/Level/Level - Section 101",
                                                    "Audio/Soundtracks/Level/Level - Section 102", "Audio/Soundtracks/Level/Level - Section 103",
                                                    "Audio/Soundtracks/Level/Level - Section 104" };

        private string[] level2SoundtracksPaths = { "Audio/Soundtracks/Level/Level - Section 200", "Audio/Soundtracks/Level/Level - Section 201",
                                                    "Audio/Soundtracks/Level/Level - Section 202", "Audio/Soundtracks/Level/Level - Section 203",
                                                    "Audio/Soundtracks/Level/Level - Section 204" };

        private string[] level3SoundtracksPaths = { "Audio/Soundtracks/Level/Level - Section 300", "Audio/Soundtracks/Level/Level - Section 301",
                                                    "Audio/Soundtracks/Level/Level - Section 302", "Audio/Soundtracks/Level/Level - Section 303",
                                                    "Audio/Soundtracks/Level/Level - Section 304", "Audio/Soundtracks/Level/Level - Section 305" };

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
            if(this.currentPlaylist != this.backgroundSoundtracksPaths)
            {
                this.currentPlaylist = this.backgroundSoundtracksPaths;

                this.musicSource.Stop();
            }
        }

        public void playLevel1Soundtrack()
        {
            if (this.currentPlaylist != this.level1SoundtracksPaths)
            {
                this.currentPlaylist = this.level1SoundtracksPaths;

                this.musicSource.Stop();
            }
        }

        public void playLevel2Soundtrack()
        {
            if (this.currentPlaylist != this.level2SoundtracksPaths)
            {
                this.currentPlaylist = this.level2SoundtracksPaths;

                this.musicSource.Stop();
            }
        }

        public void playLevel3Soundtrack()
        {
            if (this.currentPlaylist != this.level3SoundtracksPaths)
            {
                this.currentPlaylist = this.level3SoundtracksPaths;

                this.musicSource.Stop();
            }
        }


        // Verifies if there are soundtracks playing, if there are, does nothing, if not, play a random soundtrack of currentPlaylist array
        private void checkSoundtrackActivity()
        {
            if (!musicSource.isPlaying && !this.isAudioPaused)
            {
                int index = UnityEngine.Random.Range(0, this.currentPlaylist.Length);

                this.clipPath = this.currentPlaylist[index];

                this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                musicSource.clip = this.clip;

                musicSource.Play();
            }
        }

        public void playShotEffect()
        {
            this.sfxSource.clip = this.shotClip;
            this.sfxSource.Play();
        }

        public void playMovementEffect()
        {
            this.sfxSource.clip = this.movementClip;
            this.sfxSource.Play();
        }

        public void playBonusEffect()
        {
            this.sfxSource.clip = this.bonusClip;
            this.sfxSource.Play();
        }

        public void playShipDestructionEffect()
        {
            this.sfxSource.clip = this.shipDestructionClip;
            this.sfxSource.Play();
        }
    }
}