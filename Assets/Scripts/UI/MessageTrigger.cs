using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField]
        [TextArea(3,10)]
        private List<string> _phrases;
        private MessagesManager _messagesManager;

        private bool _leftTriggerReached = false, _rightTriggerReached = false;
        private Transform[] _triggers; // 1 is left, 2 is right
        [SerializeField]
        private float _detectRadius = 3f;
        public Transform LeftPlayer, RightPlayer;
        private bool _triggered = false;

        // Use this for initialization
        void Start()
        {
            _messagesManager = FindObjectOfType<MessagesManager>();
            if (_messagesManager == null)
            {
                throw new MissingComponentException("Cannot have a MessageTrigger without a MessaesManager");
            }
            _triggers = GetComponentsInChildren<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            float leftDist = (_triggers[1].position - LeftPlayer.position).magnitude;
            float rightDist = (_triggers[2].position - RightPlayer.position).magnitude;

            if (leftDist < _detectRadius)
            {
                _leftTriggerReached = true;
            }

            if (rightDist < _detectRadius)
            {
                _rightTriggerReached= true;
            }

            if (_leftTriggerReached && _rightTriggerReached && !_triggered)
            {
                _triggered = true;
                _messagesManager.SlideUpText(_phrases);
            }
        }

        public void Reset()
        {
            _triggered = false;
            _leftTriggerReached = false;
            _rightTriggerReached = false;
            _messagesManager.SlideDownText();
        }
    }
}