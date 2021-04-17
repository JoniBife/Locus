using Assets.Scripts.Common;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class SpacePull : MonoBehaviour
    {
        [SerializeField]
        private float _pullForce;
        [SerializeField]
        private Vector3 _pullDirection;
        private Rigidbody _characterToPull;
        private bool _pulling = false;

        private void FixedUpdate()
        {
            if (_pulling)
            {
                _characterToPull.ApplyForceToReachVelocity(_pullDirection * _pullForce, 1f);
            } 
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerRight") || other.CompareTag("PlayerLeft"))
            {
                _pulling = true;
                _characterToPull = other.gameObject.GetComponent<Rigidbody>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerRight") || other.CompareTag("PlayerLeft"))
            {
                _pulling = false;
                _characterToPull = null;
            }
        }
    }
}