using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private int initialScore = 3;

    private int currentScore;

    private enum GameState
    {
        Playing,
        GameOver
    }
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = initialScore;
        gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Playing)
        {
            if (currentScore <= 0)
            {
                gameState = GameState.GameOver;
                Debug.Log("Gane Over");
            }
        }
    }

    public void losingScore(int s = 1)
    {
        currentScore -= s;
    }
}
