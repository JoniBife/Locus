using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    [RequireComponent(typeof(ParticleSystem))]
    public class BloodParticles : MonoBehaviour
    {

        private ParticleSystem _particleSystem;

        // Use this for initialization
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_particleSystem.IsAlive())
            {
                //Destroy(gameObject);
            }    
                
        }
    }
}