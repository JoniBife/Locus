using System.Collections;
using UnityEngine;
using static Assets.CharactersController;

namespace Assets.Scripts.Mechanics
{
    public class ShuttleController : MonoBehaviour
    {
        private string _animatorParameter = "TakeOff";
        private Animator _animator;
        public ParticleSystem LeftThurster, RightThurster;
        public GameObject Door;
        public Transform LeaveShuttlePosition;
        private AudioSource _audioSource;
        public bool CharacterAttached { get; private set; } = false;
        private bool _finishedTrip = false;

        private void Start()
        {
            TurnOffThrusters();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            //Door.SetActive(false);
        }

        public void TakeOff()
        {
            _animator.SetBool(_animatorParameter, true);
        }

        public void TurnOnThrusters()
        {   
            if (_audioSource != null)
                _audioSource.Play();
            LeftThurster.Play();
            RightThurster.Play();
        }

        public void TurnOffThrusters()
        {
            if (_audioSource != null)
                _audioSource.Stop();
            LeftThurster.Pause();
            RightThurster.Pause();
        }

        public void Return()
        {
            _animator.SetBool(_animatorParameter, false);
        }

        private CharInfo _attachedCharacter;
        public void AttachCharacter(CharInfo charInfo)
        {
            //Door.SetActive(true);
            _attachedCharacter = charInfo;
            charInfo.Go.transform.parent = transform;
            charInfo.MovementEnabled = false;
            CharacterAttached = true;
        }

        public void Update()
        {
            if (_attachedCharacter != null)
            {
                if (!IsCharacterWithinShuttle(_attachedCharacter.Go.transform))
                {
                    DetachCharacter();
                }
            }
        }

        private void DetachCharacter()
        {
            CharacterAttached = false;
            _attachedCharacter.MovementEnabled = true;
            _attachedCharacter.Go.transform.parent = null;
            _attachedCharacter = null;
        }

        public void EndTrip()
        {
            CharacterAttached = false;
            _attachedCharacter.Go.transform.parent = null;
            _attachedCharacter.CharacterRevive(LeaveShuttlePosition.position);
            _attachedCharacter = null;
            _finishedTrip = true;
            Return();
           
        }

        [SerializeField]
        private float _detectRadius = 2.0f;
        public bool IsCharacterWithinShuttle(Transform character)
        {
            if (_finishedTrip)
                return false;

            float distToCharacter = (transform.position - character.position).magnitude;

            if (distToCharacter < _detectRadius)
            {
                return true;
            }
            return false;
        }
    }
}