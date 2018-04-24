using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : BulletController {

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
            if (facingRight)
            {
                horizMove = 7f;
            }
            else
            {
                horizMove = -7f;
            }
        }

        HandleMovement(horizMove);
    }

    public override void Shoot(Transform t)
    {
        base.Shoot(t);
        transform.localScale = new Vector3(20, 20, 20);
    }
}
