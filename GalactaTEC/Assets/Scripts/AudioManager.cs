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
        [SerializeField] public AudioClip enemyExplosion;
        [SerializeField] public AudioClip enemyShot1;
        [SerializeField] public AudioClip enemyShot2;
        [SerializeField] public AudioClip enemyShot3;
        [SerializeField] public AudioClip enemyChargedShot;
        [SerializeField] public AudioClip shipDestructionClip;
        [SerializeField] float volume = 1f;

        [SerializeField] public AudioSource pauseMusicSource;

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

        public string[] podiumSoudtracksPaths = { "Audio/Soundtracks/Podium/Podium - Victory Battle", "Audio/Soundtracks/Podium/Podium - Winds of Victory" };

        private string[] currentPlaylist;

        private string[] nextPlaylist;

        public string[] loggedUserFavoriteSoundtracks;

        public string clipPath;

        public bool isAudioPaused = false;
        public float audioVolumeBeforeStopOrPause;

        public int playlistIndex = 0;

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
                volume += 0.05f;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                volume -= 0.05f;
            }

            this.musicSource.volume = volume;
            this.pauseMusicSource.volume = volume;
            this.sfxSource.volume = volume;
        }

        public void playBackgroundSoundtrack()
        {
            if(this.currentPlaylist != this.backgroundSoundtracksPaths)
            {
                this.nextPlaylist = this.backgroundSoundtracksPaths;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playLevel1Soundtrack()
        {
            if (this.currentPlaylist != this.level1SoundtracksPaths)
            {
                this.nextPlaylist = this.level1SoundtracksPaths;

                this.audioVolumeBeforeStopOrPause = this.volume;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playLevel2Soundtrack()
        {
            if (this.currentPlaylist != this.level2SoundtracksPaths)
            {
                this.nextPlaylist = this.level2SoundtracksPaths;

                this.audioVolumeBeforeStopOrPause = this.volume;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playLevel3Soundtrack()
        {
            if (this.currentPlaylist != this.level3SoundtracksPaths)
            {
                this.nextPlaylist = this.level3SoundtracksPaths;

                this.audioVolumeBeforeStopOrPause = this.volume;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }

        public void playPodiumSoundtrack()
        {
            if (this.currentPlaylist != this.podiumSoudtracksPaths)
            {
                this.nextPlaylist = this.podiumSoudtracksPaths;

                this.audioVolumeBeforeStopOrPause = this.volume;

                InvokeRepeating("fadeStopMusic", 0f, 0.1f);
            }
        }


        // Verifies if there are soundtracks playing, if there are, does nothing, if not, play a random soundtrack of currentPlaylist array
        private void checkSoundtrackActivity()
        {
            if (!musicSource.isPlaying && !this.isAudioPaused && this.currentPlaylist == this.podiumSoudtracksPaths)
            {
                if(this.playlistIndex == 0)
                {
                    this.playlistIndex = 1;
                } else if(this.playlistIndex == 1)
                {
                    this.playlistIndex = 0;
                } else
                {
                    this.playlistIndex = UnityEngine.Random.Range(0, this.currentPlaylist.Length);
                }

                this.clipPath = this.currentPlaylist[this.playlistIndex];

                this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                musicSource.clip = this.clip;

                InvokeRepeating("fadePlayMusic", 0f, 0.01f);
            }
            else if (!musicSource.isPlaying && !this.isAudioPaused && gameManager.getInstance().player1Email != "")
            {
                this.playlistIndex = UnityEngine.Random.Range(0, this.currentPlaylist.Length);

                if (isSounditrackInUserFavorites(this.currentPlaylist[this.playlistIndex]))
                {
                    this.clipPath = this.currentPlaylist[this.playlistIndex];

                    this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                    this.musicSource.clip = this.clip;

                    this.pauseMusicSource.clip = this.clip;

                    InvokeRepeating("fadePlayMusic", 0f, 0.01f);
                }
            } else if (!musicSource.isPlaying && !this.isAudioPaused && this.currentPlaylist != null)
            {
                this.playlistIndex = UnityEngine.Random.Range(0, this.currentPlaylist.Length);

                this.clipPath = this.currentPlaylist[this.playlistIndex];

                this.clip = (AudioClip)Resources.Load(this.clipPath, typeof(AudioClip));

                musicSource.clip = this.clip;

                InvokeRepeating("fadePlayMusic", 0f, 0.01f);
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

        public void playEnemyExplosionEffect()
        {
            this.sfxSource.clip = this.enemyShot1;
            this.sfxSource.Play();
        }

        public void playEnemyShot1Effect()
        {
            this.sfxSource.clip = this.enemyShot1;
            this.sfxSource.Play();
        }

        public void playEnemyShot2Effect()
        {
            this.sfxSource.clip = this.enemyShot2;
            this.sfxSource.Play();
        }

        public void playEnemyShot3Effect()
        {
            this.sfxSource.clip = this.enemyShot3;
            this.sfxSource.Play();
        }

        public void playEnemyChargedShotEffect()
        {
            this.sfxSource.clip = this.enemyChargedShot;
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
            this.audioVolumeBeforeStopOrPause = this.musicSource.volume;
            InvokeRepeating("fadePauseMusic", 0f, 0.1f);
        }

        public void unPauseSoundtrack()
        {
            InvokeRepeating("fadeUnPauseMusic", 0f, 0.1f);
        }

        private void fadeStopMusic()
        {
            this.volume -= 0.1f;

            if (this.volume <= 0f)
            {
                this.musicSource.Stop();
                this.currentPlaylist = this.nextPlaylist;
                CancelInvoke("fadeStopMusic");
            }
        }

        private void fadePauseMusic()
        {
            this.volume -= 0.1f;

            if (this.volume <= 0f)
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
                this.volume = 0f;
                this.musicSource.Play();
            }

            this.volume += 0.1f;

            if (this.volume >= this.audioVolumeBeforeStopOrPause)
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

            this.volume += 0.1f;

            if (this.volume >= this.audioVolumeBeforeStopOrPause)
            {
                CancelInvoke("fadeUnPauseMusic");
            }
        }
    }
}
