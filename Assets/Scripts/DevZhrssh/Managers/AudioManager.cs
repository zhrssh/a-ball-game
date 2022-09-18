using System.Collections.Generic;
using UnityEngine;

namespace DevZhrssh.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [System.Serializable]
        public class Audio
        {
            public string name;
            public AudioClip clip;
            public bool isLooping;
            public bool playOnAwake;
            [Range(0 , 1)] public float volume;
        }

        private Dictionary<string, GameObject> _sounds = new Dictionary<string, GameObject>();
        public Audio[] audios;
        public bool isMuted;

        private void Start()
        {
            // Creates a new audio source for each audio clip.
            foreach (Audio audio in audios)
            {
                GameObject obj = new GameObject(audio.name + " Audio Source");
                obj.transform.parent = transform;
                AudioSource source = obj.AddComponent<AudioSource>();
                source.clip = audio.clip;
                source.loop = audio.isLooping;
                source.volume = audio.volume;

                if (audio.playOnAwake)
                    source.Play();

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
            if (isMuted) return;
            if (_sounds.ContainsKey(name) && _sounds[name] != null)
                _sounds[name].GetComponent<AudioSource>()?.Play();
        }

        public void Stop(string name)
        {
            if (isMuted) return;
            if (_sounds.ContainsKey(name) && _sounds[name] != null)
                _sounds[name].GetComponent<AudioSource>()?.Stop();
        }
    }

}
