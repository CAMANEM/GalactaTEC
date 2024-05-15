using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

using GameManager;

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

        public string[] backgroundSoundtracksPaths = { "Audio/Soundtracks/Menu/Menu - Anarchic Starport", "Audio/Soundtracks/Menu/Menu - Blaze Your Own Trail",
                                                        "Audio/Soundtracks/Menu/Menu - Coma Berenices", "Audio/Soundtracks/Menu/Menu - Delta Phoenicis",
                                                        "Audio/Soundtracks/Menu/Menu - Draco", "Audio/Soundtracks/Menu/Menu - Fawoal",
                                                        "Audio/Soundtracks/Menu/Menu - Imperial Starport", "Audio/Soundtracks/Menu/Menu - Neutral Blue Giant",
                                                        "Audio/Soundtracks/Menu/Menu - Neutral Magnetar", "Audio/Soundtracks/Menu/Menu - New Alliance",
                                                        "Audio/Soundtracks/Menu/Menu - Quator", "Audio/Soundtracks/Menu/Menu - Tarach Tor",
                                                        "Audio/Soundtracks/Menu/Menu - Ursa Major", "Audio/Soundtracks/Menu/Menu - Virgo",
                                                        "Audio/Soundtracks/Menu/Menu - Zelada" };

        public string[] level1SoundtracksPaths = { "Audio/Soundtracks/Level/Level - Section 100", "Audio/Soundtracks/Level/Level - Section 101",
                                                    "Audio/Soundtracks/Level/Level - Section 102", "Audio/Soundtracks/Level/Level - Section 103",
                                                    "Audio/Soundtracks/Level/Level - Section 104" };

        public string[] level2SoundtracksPaths = { "Audio/Soundtracks/Level/Level - Section 200", "Audio/Soundtracks/Level/Level - Section 201",
                                                    "Audio/Soundtracks/Level/Level - Section 202", "Audio/Soundtracks/Level/Level - Section 203",
                                                    "Audio/Soundtracks/Level/Level - Section 204" };

        public string[] level3SoundtracksPaths = { "Audio/Soundtracks/Level/Level - Section 300", "Audio/Soundtracks/Level/Level - Section 301",
                                                    "Audio/Soundtracks/Level/Level - Section 302", "Audio/Soundtracks/Level/Level - Section 303",
                                                    "Audio/Soundtracks/Level/Level - Section 304", "Audio/Soundtracks/Level/Level - Section 305" };

        private string[] currentPlaylist;

        public string[] loggedUserFavoriteSoundtracks;

        public string clipPath;

        public bool isAudioPaused = false;
        private float audioVolumeBeforePause;

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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                volume += 0.1f;
                myMixer.SetFloat("Music", (float)Math.Log10(volume) * 20f);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
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

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playLevel1Soundtrack()
        {
            if (this.currentPlaylist != this.level1SoundtracksPaths)
            {
                this.currentPlaylist = this.level1SoundtracksPaths;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playLevel2Soundtrack()
        {
            if (this.currentPlaylist != this.level2SoundtracksPaths)
            {
                this.currentPlaylist = this.level2SoundtracksPaths;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playLevel3Soundtrack()
        {
            if (this.currentPlaylist != this.level3SoundtracksPaths)
            {
                this.currentPlaylist = this.level3SoundtracksPaths;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }


        // Verifies if there are soundtracks playing, if there are, does nothing, if not, play a random soundtrack of currentPlaylist array
        private void checkSoundtrackActivity()
        {
            if (!musicSource.isPlaying && !this.isAudioPaused && gameManager.getInstance().player1Email != "")
            {
                int index = UnityEngine.Random.Range(0, this.currentPlaylist.Length);

                if (isSounditrackInUserFavorites(this.currentPlaylist[index]))
                {
                    this.clipPath = this.currentPlaylist[index];

                    this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                    musicSource.clip = this.clip;

                    InvokeRepeating("fadePlayMusic", 0f, 0.1f);
                }
            } else if (!musicSource.isPlaying && !this.isAudioPaused)
            {
                int index = UnityEngine.Random.Range(0, this.currentPlaylist.Length);

                this.clipPath = this.currentPlaylist[index];

                this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                musicSource.clip = this.clip;

                InvokeRepeating("fadePlayMusic", 0f, 0.1f);
            }
        }

        private bool isSounditrackInUserFavorites(string soundtrack)
        {
            foreach (string str in this.loggedUserFavoriteSoundtracks)
            {
                if (str == soundtrack)
                {
                    return true;
                }
            }
            return false;
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

        public void playSpecificSoundtrack(string soundtrack)
        {
            this.musicSource.Stop();

            this.clipPath = soundtrack;

            this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

            musicSource.clip = this.clip;

            InvokeRepeating("fadePlayMusic", 0f, 0.1f);
        }

        public void stopSoundtrack()
        {
            InvokeRepeating("fadeStopMusic", 0f, 0.1f);
        }

        public void pauseSoundtrack()
        {
            this.audioVolumeBeforePause = this.musicSource.volume;
            InvokeRepeating("fadePauseMusic", 0f, 0.1f);
        }

        public void unPauseSoundtrack()
        {
            InvokeRepeating("fadeUnPauseMusic", 0f, 0.1f);
        }

        private void fadeStopMusic()
        {
            musicSource.volume -= 0.1f;

            musicSource.volume = Mathf.Max(musicSource.volume, 0f);

            myMixer.SetFloat("Music", (float)Math.Log10(this.volume) * 20f);

            if (musicSource.volume <= 0f)
            {
                musicSource.Stop();
                CancelInvoke("fadeStopMusic");
            }
        }

        private void fadePauseMusic()
        {
            musicSource.volume -= 0.1f;

            musicSource.volume = Mathf.Max(musicSource.volume, 0f);

            myMixer.SetFloat("Music", (float)Math.Log10(this.volume) * 20f);

            if (musicSource.volume <= 0f)
            {
                this.musicSource.Pause();
                isAudioPaused = true;
                CancelInvoke("fadePauseMusic");
            }
        }

        private void fadePlayMusic()
        {
            if (!this.musicSource.isPlaying)
            {
                this.musicSource.Play();
            }

            musicSource.volume += 0.1f;

            musicSource.volume = Mathf.Max(musicSource.volume, 0f);

            myMixer.SetFloat("Music", (float)Math.Log10(this.volume) * 20f);

            if (musicSource.volume >= 1f)
            {
                CancelInvoke("fadePlayMusic");
            }
        }

        private void fadeUnPauseMusic()
        {
            if (this.isAudioPaused)
            {
                this.musicSource.UnPause();
                isAudioPaused = false;
            }

            musicSource.volume += 0.1f;

            musicSource.volume = Mathf.Max(musicSource.volume, 1f);

            myMixer.SetFloat("Music", (float)Math.Log10(this.volume) * 20f);

            if (musicSource.volume >= this.audioVolumeBeforePause)
            {
                CancelInvoke("fadeUnPauseMusic");
            }
        }
    }
}
