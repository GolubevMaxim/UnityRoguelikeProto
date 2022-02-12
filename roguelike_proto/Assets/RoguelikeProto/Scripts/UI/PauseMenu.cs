using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoguelikeProto.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private static bool _onPause = false;

        [SerializeField] private GameObject pauseScreen;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_onPause) Resume();
                else Pause();
            }
        }

        public void Resume()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            _onPause = false;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            _onPause = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Debug.Log("quitting");
            Application.Quit();
        }

        void Pause()
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            _onPause = true;
        }
    }
}
