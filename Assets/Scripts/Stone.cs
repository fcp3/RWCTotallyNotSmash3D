using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : BulletController {

	[SerializeField]
	float jumpForce;

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
				horizMove = 2f;
				rBody.useGravity = true;
			}
			else
			{
				horizMove = -2f;
				rBody.useGravity = true;
			}
		}

		HandleMovement(horizMove);
	}

	public override void Shoot(Transform t)
	{
		base.Shoot(t);
		transform.localScale = new Vector3(20, 20, 20);
		rBody.AddForce(new Vector2(0, jumpForce));
	}

	public override void HandleMovement(float horizontal)
	{
		if (isShooting)
		{
			rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
		}
		else
		{
			rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
		}
	}
}
