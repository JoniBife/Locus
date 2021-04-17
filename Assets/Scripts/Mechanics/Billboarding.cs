using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Billboarding : MonoBehaviour
    {

        private Camera _camera;

        // Use this for initialization
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
    }
}