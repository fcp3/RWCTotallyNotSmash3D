using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageSelect : MonoBehaviour {

	public void LoadEnvy()
    {
        SceneManager.LoadScene("StageEnvy");
    }

    public void LoadGreed()
    {
        SceneManager.LoadScene("StageGreed");
    }

	public void LoadSloth()
	{
		
	}


}
