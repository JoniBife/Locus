using System.Collections;
using UnityEngine;
using static Assets.CharactersController;

namespace Assets.Scripts.powerups
{
    public class PermanentIndependency : PowerUp
    {
        public override void OnPowerUpCaught(CharInfo character)
        {
            character.IndependentMovement = true;
            GameObject.Destroy(this.gameObject);
        }
    }
}