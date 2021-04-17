using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public List<Audio> audios;
        public static AudioManager Instance;

        // Use this for initialization
        void Awake()
        {
            audios.ForEach(audio =>
            {
                audio.AddSource(gameObject.AddComponent<AudioSource>());
            });
            Instance = this;
        }

        public void PlayAudio(string name)
        {
            Audio audio = audios.Find(a => a.AudioName == name);
            if (audio == null)
            {
                throw new ArgumentException("Cannot play audio due to wrong name");
            } else
            {
                if (!audio.Source.isPlaying)
                    audio.Source.Play();
            }
        }

        public void PlayAudioDelayed(string name, float delay)
        {
            Audio audio = audios.Find(a => a.AudioName == name);
            if (audio == null)
            {
                throw new ArgumentException("Cannot play audio due to wrong name");
            }
            else
            {
                if (!audio.Source.isPlaying)
                    audio.Source.PlayDelayed(delay);
            }
        }

        public void StopAudio(string name)
        {
            Audio audio = audios.Find(a => a.AudioName == name);
            if (audio == null)
            {
                throw new ArgumentException("Cannot play audio due to wrong name");
            }
            else
            {
                if (!audio.Source.isPlaying)
                    audio.Source.Stop();
            }
        }
    }
}