using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoguelikeProto.Scripts.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private GameObject _restartButton;
        [SerializeField] private GameObject _player;
        void Update()
        {
            _restartButton.SetActive(_player == null);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
