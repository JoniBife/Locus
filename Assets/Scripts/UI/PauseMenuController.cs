using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        private GameObject _pauseMenu;
        [SerializeField]
        private GameEvent _restartEvent;

        private void Start()
        {
            Cursor.visible = false;
            _pauseMenu = this.gameObject;
            _pauseMenu.SetActive(false);
        }

        public bool ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_pauseMenu.activeSelf)
                {
                    OpenMenu();
                }
                else
                {
                    CloseMenu();
                }
            }

            return _pauseMenu.activeSelf;
        }
        
        private void OpenMenu()
        {
            Cursor.visible = true;
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        private void CloseMenu()
        {
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Restart()
        {
            _restartEvent.Raise();
            CloseMenu();
        }

        public void Resume()
        {
            CloseMenu();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}