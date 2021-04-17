using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class EndGameTrigger : MonoBehaviour
    {
        public Animator FadeOut;
        private bool _triggered = false;
        public CharactersController CharactersController;

        private void OnTriggerEnter(Collider other)
        {
            if (!_triggered && (other.CompareTag("PlayerLeft") || other.CompareTag("PlayerRight")))
            {
                CharactersController.LeftCharInfo.IndependentMovement = true;
                CharactersController.RightCharInfo.IndependentMovement = true;
                _triggered = true;
                FadeOut.SetTrigger("FadeOut");
            }
        }
    }
}