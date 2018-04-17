using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour {

	public AudioClip[] audioClip;
	public Scene currentScene;

    [SerializeField]
    Light inGameLight;

    [SerializeField]
    HealthBar p1Health;

    [SerializeField]
    HealthBar p2Health;

	public bool endGameMusic;

    static bool loadLevel = false;

    public Text gameOverText;
    public Text winnerText;
    public Text pauseText;

    //public GameObject bullet;
    //bool bulletSpawn = false;

    enum Gamestate {PLAYING, GAME_OVER, PAUSE}

    Gamestate currentGameState, prevGameState;

    public int timer = 600;
    public int ticker = 0;

	void Start () {
        winnerText.enabled = false;
        gameOverText.enabled = false;

        currentGameState = Gamestate.PLAYING;
        prevGameState = Gamestate.PAUSE;
        //bullet = Resources.Load("bullet") as GameObject;
        //bullet = Resources.Load("Assets/Prefab/Bullet") as GameObject;

        //if (!loadLevel)
        //{
            //SceneManager.LoadScene("2dfighter");
            //loadLevel = true;
        //}

		// Below this comment is the audio management segment for START
		Scene currentScene = SceneManager.GetActiveScene ();
		int buildIndex = currentScene.buildIndex;

		endGameMusic = false;

		if (buildIndex == 0)
		{
			PlayMusic (0);
		}

		if (buildIndex == 1)
		{
			PlayMusic (0);
		}

		if (buildIndex == 2)
		{
			PlayMusic (Random.Range(2, 5));
		}

		if (buildIndex == 3)
		{
			PlayMusic (Random.Range(2, 5));
		}
    }

	public void PlayMusic(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = audioClip [clip];
		audio.Play ();
	}

	public void StopMusic()
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Stop ();
	}

	/* public void EndGameBGM()
	{
		endGameMusic = Random.Range(5, 7);
	}
    */ 
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
            pauseText.enabled = true;
            //Show pause on screen
            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene("StageSelect");
            } else if(Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if (currentGameState == Gamestate.PLAYING)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
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

            Time.timeScale = 1;
            inGameLight.intensity = 1f;
            pauseText.enabled = false;

            if(p1Health.CurrentHealth <= 0)
            {
                changeGameState(Gamestate.GAME_OVER);
            }
            else if (p2Health.CurrentHealth <= 0)
            {
                changeGameState(Gamestate.GAME_OVER);
            }
        }
        else if (currentGameState == Gamestate.GAME_OVER)
        {
			if (endGameMusic) {
				PlayMusic (5);
				endGameMusic = false;
			}
			winnerText.enabled = true;
            gameOverText.enabled = true;
            Time.timeScale = 0;
            SetWinnerText();

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene (0);
			}
        }
	}

    private void SetWinnerText()
    {
        if (p1Health.CurrentHealth <= 0)
        {
            winnerText.text = "Player 2 has won!";
        }
        else if (p2Health.CurrentHealth <= 0)
        {
            winnerText.text = "Player 1 has won!";
        }
    }

    private void changeGameState(Gamestate state)
    {
        prevGameState = currentGameState;
        currentGameState = state;
		if(state == Gamestate.GAME_OVER)
			endGameMusic = true;
    }
}
