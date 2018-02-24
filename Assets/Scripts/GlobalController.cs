using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour {

    static bool loadLevel = false;


    //public GameObject bullet;
    //bool bulletSpawn = false;

    public int timer = 600;
    public int ticker = 0;

	void Start () {
        //bullet = Resources.Load("bullet") as GameObject;
        //bullet = Resources.Load("Assets/Prefab/Bullet") as GameObject;

        if (!loadLevel)
        {
            SceneManager.LoadScene("2dfighter");
            loadLevel = true;
        }
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

		if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("dev");
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            SceneManager.LoadScene("2dfighter");
        }
	}
}
