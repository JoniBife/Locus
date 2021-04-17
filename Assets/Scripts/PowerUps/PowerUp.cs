using System.Collections;
using UnityEngine;
using static Assets.CharactersController;

namespace Assets.Scripts
{
    public abstract class PowerUp : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerLeft"))
                OnPowerUpCaught(GameObject.FindObjectOfType<CharactersController>().LeftCharInfo);
            else if (other.CompareTag("PlayerRight"))
                OnPowerUpCaught(GameObject.FindObjectOfType<CharactersController>().RightCharInfo);
        }

        public abstract void OnPowerUpCaught(CharInfo characterInfo);
    }
}