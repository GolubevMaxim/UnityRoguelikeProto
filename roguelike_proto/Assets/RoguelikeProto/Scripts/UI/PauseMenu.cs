using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoguelikeProto.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool OnPause = false;

        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject player;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && player != null)
            {
                if (OnPause) Resume();
                else Pause();
            }
        }

        public void Resume()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            OnPause = false;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            OnPause = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Debug.Log("quitting");
            Application.Quit();
        }

        public void QuitToMenu()
        {
            Time.timeScale = 1;
            OnPause = false;
            SceneManager.LoadScene("MenuScene");
        }

        void Pause()
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            OnPause = true;
        }
    }
}
