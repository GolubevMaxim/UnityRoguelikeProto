using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoguelikeProto.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            Debug.Log("starting game");
            SceneManager.LoadScene("SampleScene 1");
        }

        public void QuitGame()
        {
            Debug.Log("quitting");
            Application.Quit();
        }
    }
}
