using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private PauseMenuController _pauseMenuController;
        [SerializeField]
        private MessagesManager _messagesManager;

        void Update()
        {
            if (!_pauseMenuController.ProcessInput())
            {
                _messagesManager.ProcessInput();
            }
        }
    }
}