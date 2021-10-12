using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner; //Enemy spawner reference
    public GameObject GameOverGO; //Game over image 
    public GameObject scoreUITextGO;  //Score text


    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState() //Update game manager state
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                GameOverGO.SetActive(false); //Hide game over

                playButton.SetActive(true); //Set play button visible

                break;
            case GameManagerState.Gameplay:

                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                playButton.SetActive(false); //Hide play button

                playerShip.GetComponent<PlayerControl>().Init(); //Set player visible

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner(); //Start ebeny spawner

                break;
            case GameManagerState.GameOver:

                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner(); //Stop enemy spawner

                GameOverGO.SetActive(true); //Display game over text

                Invoke("ChangeToOpeningState", 8f);

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