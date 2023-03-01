using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _isGameOver = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1); // Main Scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
