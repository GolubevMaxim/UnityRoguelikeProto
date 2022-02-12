using System.Collections;
using System.Collections.Generic;
using RoguelikeProto.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        Debug.Log("hi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
