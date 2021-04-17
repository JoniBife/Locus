using Assets.Scripts.Common;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    [RequireComponent(typeof(Outline))]
    public class CorridorDoor : MonoBehaviour
    {
        private bool _opening = false;
        [SerializeField]
        private float _openDuration = 3.0f;

        private AudioSource _audioSource;

        private Outline _outline;

        private float _openDistance = 3.0f;
        private Vector3 _targetPosition;
        private Vector3 _velocity = Vector3.zero;

        private Vector3 _startPosition;

        // Use this for initialization
        void Start()
        {
            _startPosition = transform.position;
            _targetPosition = transform.position;
            _targetPosition.y -= _openDistance;
            _outline = GetComponent<Outline>();
            _audioSource = GetComponent<AudioSource>();
        }

        private bool _playedSound = false;
        // Update is called once per frame
        void Update()
        {
            if (_opening)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _openDuration);
                if (!_playedSound)
                {
                    _audioSource.Play();
                    _playedSound = true;
                }
                if (transform.position == _targetPosition)
                {
                    _opening = false;
                    _outline.OutlineWidth = 0f;
                }
            } 
        }

        public void Open()
        {
            Color successColor = Color.white;
            ColorUtility.TryParseHtmlString(GlobalParameters.SuccessColor, out successColor);
            _opening = true;
            _outline.OutlineColor = successColor; 
        }

        public void Reset()
        {
            _opening = false;
            _outline.OutlineColor = Color.red;
            transform.position = _startPosition;
            _playedSound = false;
        }
    }
}