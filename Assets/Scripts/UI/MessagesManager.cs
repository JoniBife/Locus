using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MessagesManager : MonoBehaviour
    {
        [SerializeField]
        private float _timeBetweenCharacters = 0.5f;
        [SerializeField]
        private float _messageShowDelay = 2f;
        private static string _animatorParameter = "SlideUp";
        private GameObject _slidingMessage;
        private Animator _animator;
        private TextMeshProUGUI _text;
        private Queue<string> _phrases = new Queue<string>();
        private bool _showingMessage = false, _advancePhrase = true, _skipWriting = false, _finishedPhrase = true;
        public CharactersController CharactersController;
        public List<AudioClip> KeySounds;
        private AudioSource _audioSource;

        void Start()
        {
            _slidingMessage = GameObject.Find("SlidingMessage");
            _animator = _slidingMessage.GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _text = _slidingMessage.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ProcessInput()
        {
            if (_showingMessage)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                { 
                    if (_finishedPhrase)
                    {
                        if (_advancePhrase)
                            SlideDownText();
                        else
                            _advancePhrase = true;
                    }
                    else
                    {
                        _skipWriting = true;
                    }
                }
            } 
        }

        public void SlideUpText(List<string> phrases)
        {
            if (phrases.Count > 0)
            {
                CharactersController.LeftCharInfo.MovementEnabled = false;
                CharactersController.RightCharInfo.MovementEnabled = false;
                _animator.SetBool(_animatorParameter, true);
                _showingMessage = true;
                foreach (string phrase in phrases)
                {
                    _phrases.Enqueue(phrase);
                }
                StartCoroutine(WriteTextRoutine());
            }
        }

        private IEnumerator WriteTextRoutine()
        {
            yield return new WaitForSeconds(_messageShowDelay);
            foreach (string phrase in _phrases)
            {
                while (!_advancePhrase) { yield return null;  }
                
                _finishedPhrase = false;
                _advancePhrase = false;
                string currPhrase = "_";
                _text.text = currPhrase;
                foreach (char c in phrase)
                {
                    _audioSource.clip = KeySounds[Random.Range(0, KeySounds.Count - 1)];
                    _audioSource.Play();
                    if (_skipWriting)
                    {
                        currPhrase = phrase + "_";
                        _text.text = currPhrase;
                        _skipWriting = false;
                        break;
                    }

                    currPhrase = currPhrase.Remove(currPhrase.Length - 1);
                    currPhrase += c;
                    currPhrase += "_";
                    _text.text = currPhrase;
                    yield return new WaitForSeconds(_timeBetweenCharacters);
                }
                _finishedPhrase = true;
                
            }
            _advancePhrase = true;
        }

        public void SlideDownText()
        {
            CharactersController.LeftCharInfo.MovementEnabled = true;
            CharactersController.RightCharInfo.MovementEnabled = true;
            _showingMessage = false;
            _text.text = "_";
            _phrases.Clear();
            _animator.SetBool(_animatorParameter, false);
        }
    }
}