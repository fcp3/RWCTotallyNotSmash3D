using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public int Damage
    {
        get { return damage; }
    }

    public int PlayerNum
    {
        get { return playerNum; }
        set { playerNum = value; }
    }

    public bool FacingRight
    {
        get { return facingRight; }
        set { facingRight = value; }
    }

    [SerializeField]
    float speed;
    protected float horizMove;

    [SerializeField]
    int playerNum;

    [SerializeField]
    int damage;

    protected Rigidbody rBody;

    protected bool hasGravity;
    protected bool isShooting;
    protected bool facingRight;
    protected bool pickedUp;

	// Use this for initialization
	protected virtual void Start () {
        hasGravity = true;
        isShooting = false;
        facingRight = false;
        pickedUp = false;
        playerNum = 0;
        rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void FixedUpdate()
    {
        if(!hasGravity)
        {
            rBody.useGravity = false;
        }
        else
        {
            rBody.useGravity = true;
        }

        if(isShooting)
        {
            if(facingRight)
            {
                horizMove = 5;
            }
            else
            {
                horizMove = -5;
            }
        }

        HandleMovement(horizMove);
    }

    public void HandleMovement(float horizontal)
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

    public virtual void Shoot (Transform t)
    {
        isShooting = true;
        // need to adjust position based on players direction

        Vector3 playerPos = t.position;
        Vector3 playerDirection = new Vector3(0,0,0);
        if(facingRight)
        {
            playerDirection.x = .5f;
        }
        else
        {
            playerDirection.x = -.5f;
        }
        float spawnDistance = 3;

        Vector3 spawnPos = playerPos + (playerDirection * spawnDistance);

        transform.position = spawnPos;
        transform.position = new Vector3(transform.position.x, transform.position.y + 1,
            transform.position.z);
        transform.localScale = new Vector3(1, 1, 1);
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "player")
        {
            if (!pickedUp && (this.playerNum == 0 || this.playerNum == col.gameObject.GetComponent<Player3D>().PlayerNum))
            {
                transform.localScale = new Vector3(0, 0, 0);
                transform.position = new Vector3(1000, 30000, 0);
                pickedUp = true;
                hasGravity = false;
            }
            else if(isShooting && this.playerNum != col.gameObject.GetComponent<Player3D>().PlayerNum)
            {
                isShooting = false;
                Destroy(this.gameObject);
            }
        }
        
        if(col.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }
    }
}
