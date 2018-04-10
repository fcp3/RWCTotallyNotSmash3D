using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : BulletController {

    protected bool hitGround = false;
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void FixedUpdate()
    {
        if (!hasGravity)
        {
            rBody.useGravity = false;
        }
        else
        {
            rBody.useGravity = true;
        }

        if (isShooting)
        {
            if (!hitGround)
            {
                if (facingRight)
                {
                    horizMove = 2.5f;
                }
                else
                {
                    horizMove = -2.5f;
                }
            }
            else
            {
                horizMove = 0;
            }
        }

        HandleMovement(horizMove);
    }

    public override void Shoot(Transform t)
    {
        rBody.AddForce(new Vector3(0, 15, 0));

        base.Shoot(t);

        transform.localScale = new Vector3(20, 20, 20);
        this.hasGravity = true;
    }

    protected override void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            hitGround = true;
        }

        base.OnCollisionEnter(col);
    }
}
