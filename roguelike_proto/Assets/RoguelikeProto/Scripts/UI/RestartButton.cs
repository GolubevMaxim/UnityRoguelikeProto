using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoguelikeProto.Scripts.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private GameObject deathScreen;
        [SerializeField] private GameObject _player;
        void Update()
        {
            deathScreen.SetActive(_player == null);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
