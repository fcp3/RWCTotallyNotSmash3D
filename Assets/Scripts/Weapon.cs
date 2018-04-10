using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int Damage
    {
        get { return damage; }
    }

    public int PlayerNum
    {
        get { return playerNum; }
        set { playerNum = value; }
    }

    public bool PickedUp
    {
        get { return pickedUp; }
        set { pickedUp = value; }
    }

    protected Rigidbody rBody;
    protected BoxCollider hitBox;

    protected int playerNum;
    protected int damage;
    protected int numUses = 1;
    protected bool pickedUp;

    protected virtual void Start()
    {
        rBody = GetComponent<Rigidbody>();
        hitBox = GetComponent<BoxCollider>();
        damage = 1;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "player")
        {
            if (pickedUp)
            {
                rBody.useGravity = false;
                rBody.velocity = new Vector3(0, 0, 0);
                hitBox.isTrigger = true;
            }
                
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "player")
        {
            if (!pickedUp && this.playerNum != col.gameObject.GetComponent<Player3D>().PlayerNum)
            {
                numUses--;
                if (numUses == 0)
                    Destroy(this.gameObject);
            }
        }
    }
}
