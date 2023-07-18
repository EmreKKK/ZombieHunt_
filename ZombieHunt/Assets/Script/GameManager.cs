using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject player;
    public GameObject enemySpawner;
    public GameObject GameOverGO;

    public GameObject scoreTextUIGO;
    public GameObject TimeCounter;
    public GameObject GameTitleGO;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                GameOverGO.SetActive(false);

                playButton.SetActive(true);

                GameTitleGO.SetActive(true);

                break;


            case GameManagerState.Gameplay:

                playButton.SetActive(false);

                player.GetComponent<PlayerController>();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                scoreTextUIGO.GetComponent<GameScore>().Score = 0;

                TimeCounter.GetComponent<TimeCounter>().StartTimeCounter();

                GameTitleGO.SetActive(false);

                break;



            case GameManagerState.GameOver:

                enemySpawner.GetComponent<EnemySpawner>();        
                GameOverGO.SetActive(true);

                Invoke("ChangeToOpeningState", 8f);

                TimeCounter.GetComponent<TimeCounter>().StopTimeCounter();

                break;
        }
    }
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}

