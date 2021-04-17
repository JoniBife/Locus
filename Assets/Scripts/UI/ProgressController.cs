using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ProgressController : MonoBehaviour
    {
        public Transform EndPoint;
        public Transform LeftCharacter, RightCharacter;
        private TextMeshProUGUI _progressText;
        private uint _progress = 0;
        private float _maxDistance;

        private void Start()
        {
            _maxDistance = EndPoint.position.z;
            _progressText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            // Obtaining position in between both characters
            Vector3 positionBetween = (LeftCharacter.position + RightCharacter.position) * 0.5f;
            float distanceToEnd = EndPoint.position.z - positionBetween.z;
            _progress = (uint)(Mathf.Clamp(((_maxDistance - distanceToEnd) / _maxDistance) * 100f, 0f, 100f));
            _progressText.text = _progress.ToString() + "%";
        }
    }
}