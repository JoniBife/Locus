using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {

        public Transform LeftCharacter, RightCharacter;
        [SerializeField]
        private float _followDelay = 0.3f;
        [SerializeField]
        private Vector3 _followOffset = new Vector3(0f, 0f, 0.5f);
        private Vector3 _velocity = Vector3.zero;

        private void FixedUpdate()
        {
            // Obtaining position in between both characters
            Vector3 targetPosition = (LeftCharacter.position +  RightCharacter.position) * 0.5f;
            targetPosition.y = transform.position.y; // We don't want to move the camera in the Y axis

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _followDelay) + _followOffset;
        }
    }
}