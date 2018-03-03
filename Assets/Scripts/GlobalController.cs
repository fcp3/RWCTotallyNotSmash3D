using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour {

    [SerializeField]
    Light inGameLight;

    [SerializeField]
    HealthBar p1Health;

    [SerializeField]
    HealthBar p2Health;

    static bool loadLevel = false;


    //public GameObject bullet;
    //bool bulletSpawn = false;

    enum Gamestate {PLAYING, GAME_OVER, PAUSE}

    Gamestate currentGameState, prevGameState;

    public int timer = 600;
    public int ticker = 0;

	void Start () {

        currentGameState = Gamestate.PLAYING;
        prevGameState = Gamestate.PAUSE;
        //bullet = Resources.Load("bullet") as GameObject;
        //bullet = Resources.Load("Assets/Prefab/Bullet") as GameObject;

        //if (!loadLevel)
        //{
            //SceneManager.LoadScene("2dfighter");
            //loadLevel = true;
        //}
    }
	
	void Update () {
        /*
        if(ticker < timer)
        {
            ticker++;
        }
        else
        {
            if (!bulletSpawn)
            {
                Debug.Log("Bullet Spawn");
                Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
                bulletSpawn = true;
            }
        }
        */
        if(currentGameState == Gamestate.PAUSE)
        {
            Time.timeScale = 0;
            inGameLight.intensity = .01f;
            //Show pause on screen
        }
        else if (currentGameState == Gamestate.PLAYING)
        {
            Time.timeScale = 1;
            inGameLight.intensity = 1f;

            if(p1Health.CurrentHealth <= 0)
            {
                changeGameState(Gamestate.GAME_OVER);
            }
            else if (p2Health.CurrentHealth <= 0)
            {
                changeGameState(Gamestate.GAME_OVER);
            }
        }

		if(Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            if (currentGameState == Gamestate.PAUSE)
            {
                changeGameState(Gamestate.PLAYING);
            }
            else
            {
                changeGameState(Gamestate.PAUSE);
            }
        }

	}

    private void changeGameState(Gamestate state)
    {
        prevGameState = currentGameState;
        currentGameState = state;
    }
}
