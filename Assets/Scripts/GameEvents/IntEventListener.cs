using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEvents
{
    [System.Serializable]
    public class UnityIntEvent : UnityEvent<int> { }


    public class IntEventListener : MonoBehaviour
    {
        public IntEvent Event;
        public UnityIntEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(int arg)
        {
            Response.Invoke(arg);
        }
    }
}
