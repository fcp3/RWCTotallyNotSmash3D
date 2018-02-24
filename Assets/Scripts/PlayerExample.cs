using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExample : MonoBehaviour
{

    public float FrictionModifier
    {
        get { return frictionModifier; }
        set
        {
            if (value > 0)
            {
                frictionModifier = value;
            }
        }
    }

    public int PlayerNum
    {
        get { return playerNum; }
    }

    public Rigidbody2D PlayerRigid
    {
        get { return myRigidBody; }
    }

    [SerializeField]
    protected int playerNum;

    [SerializeField]
    protected int health;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    protected float movementSpeed;

    //added variables for movement, acceleration for ramped up speed
    int horizMove = 0;
    protected int accelerator = 50;
    protected int MAX_MOVE = 1000;

    KeyCode left, right, jumpMove, down;
    KeyCode attk1, attk2;

    float frictionModifier = 5f; //This is for if the floor is ice
    protected float speedModifier = 1000f; //This is to control the model's speed within a reasonable speed

    private Rigidbody2D myRigidBody;

    private Animator myAnimator;

    private BulletController currentBullet;

    private bool attack;

    private bool attack2;

    protected bool facingRight;

    private bool isGrounded;

    private bool jump;

    protected bool hasBullet;

    protected virtual void Start()
    {
        facingRight = true;
        hasBullet = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        if (playerNum == 1)
        {
            left = KeyCode.A;
            right = KeyCode.D;
            jumpMove = KeyCode.Space;
            down = KeyCode.S;

            attk1 = KeyCode.LeftShift;
            attk2 = KeyCode.LeftControl;
        }
        else if (playerNum == 2)
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            jumpMove = KeyCode.UpArrow;
            down = KeyCode.DownArrow;

            attk1 = KeyCode.RightShift;
            attk2 = KeyCode.RightControl;
        }
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void FixedUpdate()
    {
        bool keyDown = false;

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
    private void HandleMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetKey(down))
        {
            if (!isGrounded)
            {
                myRigidBody.AddForce(new Vector2(0, -jumpForce / 5));
            }
        }

        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if (attack)
        {
            if (hasBullet)
            {
                hasBullet = false;
                currentBullet.FacingRight = this.facingRight;
                currentBullet.Shoot(this.transform);
                currentBullet = null;
            }
            else
            {
                myAnimator.SetTrigger("attack");
            }
        }
        else if (attack2)
        {
            myAnimator.SetTrigger("attack2");
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
            attack = true;
        }

        if (Input.GetKeyDown(attk2))
        {
            attack2 = true;
        }
    }

    private void ResetValues()
    {
        attack = false;

        attack2 = false;

        jump = false;
    }

    protected void damage(int d, Vector2 k)
    {
        float modifier = 100f;
        myRigidBody.AddForce(k * modifier);
        health -= d;
        Debug.Log("Hit the player " + health);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
            frictionModifier = 5f;
        }
        else if (col.gameObject.tag == "ice")
        {
            frictionModifier = .5f;
            isGrounded = true;
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
                    if (bc.FacingRight) { knock = new Vector2(5, 1); }
                    else { knock = new Vector2(-5, 1); }
                    damage(bc.Damage, knock);
                }
            }
        }
    }
}
