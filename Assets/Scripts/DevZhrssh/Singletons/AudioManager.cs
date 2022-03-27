using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevZhrssh.Singletons
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        private static AudioManager _instance;
        public static AudioManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<AudioManager>();
                return _instance;
            }
        }

        #endregion

        [System.Serializable]
        public class Audio
        {
            public string name;
            public AudioClip clip;
        }

        private Dictionary<string, GameObject> _sounds = new Dictionary<string, GameObject>();
        public Audio[] audios;

        private void Start()
        {
            // Creates a new audio source for each audio clip.
            foreach (Audio audio in audios)
            {
                GameObject obj = new GameObject(audio.name + " Audio Source");
                obj.transform.parent = transform;
                AudioSource source = obj.AddComponent<AudioSource>();
                source.clip = audio.clip;
                source.playOnAwake = false;

                if (!_sounds.ContainsKey(audio.name))
                    _sounds.Add(audio.name, obj);
            }
        }

        public void OnEnable()
        {
            this.enabled = true;
        }

        public void OnDisable()
        {
            this.enabled = false;
        }

        public void Play(string name)
        {
            if (_sounds.ContainsKey(name))
                _sounds[name].GetComponent<AudioSource>()?.Play();
        }
    }

}
