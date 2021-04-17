using System.Collections;
using UnityEngine;
using static Assets.CharactersController;

namespace Assets.Scripts.powerups
{
    [RequireComponent(typeof(Renderer), typeof(Collider))]
    public class TemporaryIndependency : PowerUp
    {
        [SerializeField]
        private float _independencyDuration;
        private CharInfo _charInfo;
        private Renderer _renderer;
        private Collider _collider;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _collider = GetComponent<Collider>();
        }

        public override void OnPowerUpCaught(CharInfo character)
        {
            // When the power up is caught we hide the powerup, set the character controller to be independent
            // and start a coroutine that will reset everything after a certain duration has passed
            _renderer.enabled = false;
            _collider.enabled = false;;
            character.IndependentMovement = true;
            StartCoroutine(IndependencyStopRoutine());
        }

        private IEnumerator IndependencyStopRoutine()
        { 
            yield return new WaitForSeconds(_independencyDuration);
            _renderer.enabled = true;
            _collider.enabled = true;
            _charInfo.IndependentMovement = false;
        }
    }
}