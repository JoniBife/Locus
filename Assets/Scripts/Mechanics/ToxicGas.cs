using Assets.Scripts.Audio;
using Assets.Scripts.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ToxicGas : MonoBehaviour
    {
        private ParticleSystem _toxicParticles;

        private AudioSource _audioSource;

        [SerializeField]
        private float _onDuration;
        [SerializeField]
        private float _offDuration;
        [SerializeField]
        private int _playerId;
        [SerializeField]
        private float _startDelay;
        [SerializeField]
        private bool _startManually = false;

        public IntEvent PlayerDeath;

        private void Start()
        {
            _toxicParticles = GetComponent<ParticleSystem>();
            _audioSource = GetComponent<AudioSource>();
            _toxicParticles.Pause();
            if (!_startManually)
                StartCoroutine(ToxicGasRoutine());
        }
        private void OnParticleTrigger()
        {
            List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
            int numEnter = _toxicParticles.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            if (numEnter > 0)
                PlayerDeath.Raise(_playerId);
        }

        private IEnumerator ToxicGasRoutine()
        {
            yield return new WaitForSeconds(_startDelay);
            while (true)
            {
                _toxicParticles.Play();
                _audioSource.Play();
                yield return new WaitForSeconds(_onDuration);
                _toxicParticles.Stop();
                _audioSource.Stop();
                yield return new WaitForSeconds(_offDuration);
            }
        }

        public void StartToxicGas()
        {
            if (_startManually)
                StartCoroutine(ToxicGasRoutine());
        }

        public void Reset()
        {
            if (_startManually)
            {
                StopAllCoroutines();
                _toxicParticles.Stop();
            }
        }
    }
}