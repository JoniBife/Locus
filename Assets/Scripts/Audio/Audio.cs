using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    [Serializable]
    public class Audio
    {
        public string AudioName;

        public AudioClip Clip;
        [Range(0f, 1f)]
        public float Volume;
        public float Pitch;

        public float MaxDistance;
        public float MinDistance;

        public AudioRolloffMode RolloffMode;

        [HideInInspector]
        public AudioSource Source;

        public void AddSource(AudioSource source)
        {
            this.Source = source;
            this.Source.clip = Clip;
            this.Source.volume = Volume;
            this.Source.pitch = Pitch;
            this.Source.rolloffMode = RolloffMode;
            this.Source.maxDistance = MaxDistance;
            this.Source.minDistance = MinDistance;
            this.Source.spatialBlend = 1f;
        }
    }
}