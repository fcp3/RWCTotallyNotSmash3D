using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour {

    public int PlayerNum
    {
        get { return playerNum; }
    }

    public bool Attacking
    {
        get { return anim.GetCurrentAnimatorStateInfo(0).IsName("attack"); }
    }
    
    public bool FacingRight
    {
        get { return facingRight; }
    }

    public int Damage
    {
        get {
            if(Attacking)
            {
                return attackDamage1;
            }
            return 0;
        }
    }

    [SerializeField]
    int speed;

    [SerializeField]
    protected int playerNum;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    protected int maxHealth;
    protected int health;

    protected HealthBar healthBar;

    Rigidbody rBody;

    Animator anim;

    private GameObject rightArm;

    private BulletController currentBullet;

    protected bool hasBullet;
    protected bool hit;
    protected bool recovered;
    protected bool recovering;
    protected bool recentAttack;

    protected int attackDamage1 = 10, attackDamage2 = 10;

    protected int recoveryTime = 100;
    protected int recoveryTimer = 0;

    /*protected int attackTime1 = 100;
    protected int attackTime2 = 500;
    protected int attackTimer = 0;
    */

    //added variables for movement, acceleration for ramped up speed
    int horizMove = 0;
    protected int accelerator = 50;
    protected int MAX_MOVE = 1000;

    KeyCode left, right, jumpMove, down;
    KeyCode attk1, attk2;

    float frictionModifier = 5f; //This is for if the floor is ice
    protected float speedModifier = 1000f; //This is to control the model's speed within a reasonable speed

    private bool attack1;

    private bool attack2;

    protected bool facingRight;

    private bool isGrounded;

    private bool jump;


    // Use this for initialization
    void Start () {
        facingRight = true;
        isGrounded = true;
        hit = false;
        recentAttack = false;
        rBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        health = maxHealth;

        GameObject[] g = GameObject.FindGameObjectsWithTag("rightArm");
        foreach(GameObject t in g)
        {
            if(t.GetComponentInParent<Player3D>().PlayerNum == this.playerNum 
                && t.GetComponent<SphereCollider>() != null)
            {
                rightArm = t;
            }
        }

        if (playerNum == 1)
        {
            GameObject healthObject = GameObject.FindGameObjectWithTag("player1health");
            healthBar = healthObject.GetComponent<HealthBar>();
            healthBar.MaxHealth = maxHealth;
            healthBar.CurrentHealth = health;

            left = KeyCode.A;
            right = KeyCode.D;
            jumpMove = KeyCode.Space;
            down = KeyCode.S;

            attk1 = KeyCode.LeftShift;
            attk2 = KeyCode.LeftControl;
        }
        else if (playerNum == 2)
        {
            GameObject healthObject = GameObject.FindGameObjectWithTag("player2health");
            healthBar = healthObject.GetComponent<HealthBar>();
            healthBar.MaxHealth = maxHealth;
            healthBar.CurrentHealth = health;

            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            jumpMove = KeyCode.UpArrow;
            down = KeyCode.DownArrow;

            attk1 = KeyCode.RightShift;
            attk2 = KeyCode.RightControl;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(recovered)
        {
            if(Input.GetKeyDown(jumpMove))
            {
                hit = false;
            }
        }
        else
        {
            recoveryTimer++;
            if(recoveryTimer >= recoveryTime)
            {
                recovered = true;
                recoveryTimer = 0;
            }
        }

        HandleInput();
        healthBar.CurrentHealth = health;
	}

    private void FixedUpdate()
    {
        bool keyDown = false;

        if(Attacking)
        {
            rightArm.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            rightArm.gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (Input.GetKey(left))
        {
            keyDown = true;
            facingRight = false;
            if (horizMove > -MAX_MOVE) { horizMove -= (int)(accelerator * frictionModifier); }
        }
        if (Input.GetKey(right))
        {
            keyDown = true;
            facingRight = true;
            if (horizMove < MAX_MOVE) { horizMove += (int)(accelerator * frictionModifier); }
        }

        if (!keyDown)
        {
            if (horizMove != 0)
            {
                if (horizMove < 0) { horizMove += (int)(accelerator * frictionModifier); }
                else { horizMove -= (int)(accelerator * frictionModifier); }
            }
        }

        HandleMovement(horizMove / speedModifier);

        HandleAttacks();

        ResetValues();
    }

    private void HandleAttacks()
    {
        if (attack1)
        {
            if (hasBullet)
            {
                hasBullet = false;
                currentBullet.FacingRight = this.facingRight;
                currentBullet.Shoot(this.transform);
                currentBullet = null;
                attack1 = false;
            }
            else 
            {
                anim.SetTrigger("attackTrigger");
                attack1 = false;
                anim.SetTrigger("attackTrigger");
            }
            /*
            else
            {
                anim.SetTrigger("attack");
            }
        }
        */ 
        }
        else if (attack2)
        {
            // anim.SetTrigger("attack2");
            attack2 = false;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(jumpMove))
        {
            jump = true;
        }

        if (Input.GetKeyDown(attk1))
        {
            attack1 = true;
            rBody.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(attk2))
        {
            attack2 = true;
            
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            rBody.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetKey(down))
        {
            if (!isGrounded)
            {
                rBody.AddForce(new Vector2(0, -jumpForce / 5));
            }
        }

        if (hit || Attacking)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
        }
        else
        {
            rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
        }
        //myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void ResetValues()
    {
        jump = false;
    }

    protected void damage(int d, Vector2 k)
    {
        float modifier = 25f;
        rBody.AddForce(k * (modifier * d));
        health -= d;
        Debug.Log("Hit the player " + health);
        hit = true;
        recovered = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "rightArm")
        {
            //damage me here
            Debug.Log("right arm colliding");
            Player3D p = col.gameObject.GetComponentInParent<Player3D>();
            Debug.Log("On trigger enter: " + p.Attacking);
            Debug.Log("rightArm player is attacking");
            if (recovered)
            {
                Debug.Log("player is recovered");
                int x = 0, y = 0;

                if (p.FacingRight) { x = 1; }
                else { x = -1; }

                if (p.transform.position.y > this.transform.position.y) { y = -1; }
                else { y = 1; }

                int d = p.Damage;

                damage(d, new Vector2(x, y));
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "ground")
        {
            isGrounded = true;
            recovered = true;
            hit = false;
            frictionModifier = 5f;
        }

        if(col.gameObject.tag == "player")
        {
            GameObject p1 = col.gameObject;
            Physics.IgnoreCollision(p1.GetComponent<CapsuleCollider>(), GetComponent<CapsuleCollider>());
            /*Player3D p = col.gameObject.GetComponentInParent<Player3D>();
            if (p.Attacking)
            {
                if (recovered)
                {
                    int x = 0, y = 0;

                    if (p.FacingRight) { x = 1; }
                    else { x = -1; }

                    if (p.transform.position.y > this.transform.position.y) { y = -1; }
                    else { y = 1; }

                    int d = p.Damage;

                    damage(d, new Vector2(x, y));
                }
            }
            */
        }

        if (col.gameObject.tag == "bullet")
        {
            GameObject b = col.gameObject;
            BulletController bc = b.GetComponent<BulletController>();
            if (bc.PlayerNum != this.playerNum)
            {
                if (bc.PlayerNum == 0)
                {
                    // pick up
                    hasBullet = true;
                    bc.PlayerNum = this.playerNum;
                    this.currentBullet = bc;
                }
                else
                {
                    Vector2 knock = new Vector2(0, 0);
                    if (bc.FacingRight) { knock = new Vector2(1, 1); }
                    else { knock = new Vector2(-1, 1); }
                    damage(bc.Damage, knock);
                }
            }
        }
    }
}
