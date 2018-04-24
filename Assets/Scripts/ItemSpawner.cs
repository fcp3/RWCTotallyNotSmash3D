using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    int timer;
    int nextSpawn;

    Vector3 nextPos;

    enum Items { HAMMER, PIZZA, STONE, STAR, BULLET, SHANK}
    Items nextItem;

    public GameObject hammer, pizza, stone, star, bullet, shank;

    // Use this for initialization
    void Start () {
        chooseNextItem();
        chooseNextLocation();
        chooseNextSpawn();
    }

    private void FixedUpdate()
    {
        timer += (int)(Time.deltaTime * 60);
        if(timer > nextSpawn)
        {
            //spawn the item
            instatiateItem(nextItem);

            chooseNextItem();
            chooseNextSpawn();
            chooseNextLocation();

            timer = 0;
        }
    }

    void instatiateItem(Items spawn)
    {
        //spawn this
        switch (spawn)
        {
            case Items.BULLET:
                Instantiate(bullet, nextPos, Quaternion.identity);
                break;
            case Items.STAR:
                Debug.Log(star);
                Instantiate(star, nextPos, Quaternion.identity);
                break;
            case Items.STONE:
                Debug.Log(stone);
                Instantiate(stone, nextPos, Quaternion.identity);
                break;
            default:
                Debug.Log(spawn);
                Debug.Log(nextPos);
                break;
        }
        
    }

    void chooseNextItem()
    {
        int num = Random.Range(3, 6);
        switch (num)
        {
            case 1:
                nextItem = Items.HAMMER;
                break;
            case 2:
                nextItem = Items.PIZZA;
                break;
            case 3:
                nextItem = Items.STONE;
                break;
            case 4:
                nextItem = Items.STAR;
                break;
            case 5:
                nextItem = Items.BULLET;
                break;
            case 6:
                nextItem = Items.SHANK;
                break;
            default:
                nextItem = Items.BULLET;
                break;
        }
    }

    void chooseNextSpawn()
    {
        nextSpawn = Random.Range(75, 150);
    }

    void chooseNextLocation()
    {
        nextPos = new Vector3(Random.Range(-12, 12), Random.Range(5, 10), 0);
        Debug.Log(nextPos);
    }
}
