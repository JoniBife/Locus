using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class ShuttlePairController : MonoBehaviour
    {
        public CharactersController CharactersController;
        public ShuttleController LeftShuttle, RightShuttle;

        void Update()
        {
            if (!LeftShuttle.CharacterAttached && LeftShuttle.IsCharacterWithinShuttle(CharactersController.LeftCharInfo.Go.transform))
            {
                CharactersController.RightCharInfo.IndependentMovement = true;
                LeftShuttle.AttachCharacter(CharactersController.LeftCharInfo);
            }
            else if (!RightShuttle.CharacterAttached && RightShuttle.IsCharacterWithinShuttle(CharactersController.RightCharInfo.Go.transform))
            {
                CharactersController.LeftCharInfo.IndependentMovement = true;
                RightShuttle.AttachCharacter(CharactersController.RightCharInfo);
            }

            if (RightShuttle.CharacterAttached && LeftShuttle.CharacterAttached)
            {
                CharactersController.RightCharInfo.IndependentMovement = false;
                CharactersController.LeftCharInfo.IndependentMovement = false;
                LeftShuttle.TakeOff();
                RightShuttle.TakeOff();
            }
        }
    }
}