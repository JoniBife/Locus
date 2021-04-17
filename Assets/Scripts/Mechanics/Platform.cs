using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class Platform : MonoBehaviour
    {

        [SerializeField]
        private float _fallingDelay = 4f;
        [SerializeField]
        private float _fallingSpeed = 10f;
        [SerializeField]
        private Vector3 _fallingDirection = new Vector3(0,-1f,-1f);
        [SerializeField]
        private float _destroyHeight = -100;
        private bool _falling = false;

        private Vector3 _defaultPosition;

        private void Start()
        {
            _defaultPosition = transform.position;
        }

        void Update()
        {
            if (_falling)
            {
                transform.position += _fallingDirection.normalized * _fallingSpeed * Time.deltaTime;
            }

            if (transform.position.y < _destroyHeight)
                Destroy(this.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PlayerLeft") || collision.gameObject.CompareTag("PlayerRight"))
                StartCoroutine(FallingPlatformRoutine());
        }

        private IEnumerator FallingPlatformRoutine()
        {
            yield return new WaitForSeconds(_fallingDelay);
            _falling = true;
        }

        public void ResetPlatform()
        {
            StopAllCoroutines();
            _falling = false;
            transform.position = _defaultPosition;
        }
    }
}