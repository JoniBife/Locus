using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {

        private void Start()
        {
            Cursor.visible = true;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}