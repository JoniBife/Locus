using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class ButtonController : MonoBehaviour
    {
        private bool _isButtonPressed = false;
        public GameEvent onButtonClickEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!_isButtonPressed && other.CompareTag("Player"))
            {
                _isButtonPressed = true;
                onButtonClickEvent.Raise();
            }
        }
    }
}