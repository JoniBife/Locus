using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class FadeOutController : MonoBehaviour
    {
        public void LoadEndGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}