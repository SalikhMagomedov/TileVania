﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    
    private void Awake()
    {
        var numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadSceneAsync(0);
        Destroy(gameObject);
    }
}