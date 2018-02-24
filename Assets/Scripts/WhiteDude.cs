using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteDude : Player {

	// Use this for initialization
	protected override void Start () {
        facingRight = false;

        base.Start();
		
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
}
