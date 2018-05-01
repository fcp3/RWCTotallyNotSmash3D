using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour {

	public AudioClip[] audioClip;
	public Scene currentScene;

	// Use this for initialization
	void Start () {
		Scene currentScene = SceneManager.GetActiveScene ();
		int buildIndex = currentScene.buildIndex;

		if (buildIndex == 0)
		{
			PlayMusic (0);
		}

		if (buildIndex == 1)
		{
			PlayMusic (1);
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

	void Update ()
	{
		if(Input.GetKey(KeyCode.F1))
        {
            Application.Quit();
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

}
