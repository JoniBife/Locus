using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class ToxicGasTrigger : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _toxicGasTriggerEvent;
        private bool _raisedEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!_raisedEvent)
            {
                _toxicGasTriggerEvent.Raise();
                _raisedEvent = true;
            }
        }

        public void Reset()
        {
            _raisedEvent = false;
        }
    }
}