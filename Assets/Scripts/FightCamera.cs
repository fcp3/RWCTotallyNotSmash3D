using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCamera : MonoBehaviour
{

    [SerializeField]
    Player3D p1;

    [SerializeField]
    Player3D p2;

    [SerializeField]
    float distanceScale;

    [SerializeField]
    float speed;

    [SerializeField]
    int MAX_SIZE;

    [SerializeField]
    int MIN_SIZE;

    Camera c;
    Vector3 currentPosition;
    Vector3 positionGoal;
    float currentSize, destSize;
    float distance; //distance between characters
    float delta = (1f / 60f); //to control for time

    // Use this for initialization
    void Start()
    {
        c = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos1 = p1.transform.position;
        Vector3 pos2 = p2.transform.position;

        positionGoal = new Vector3((pos1.x + pos2.x) / 2f, ((pos1.y + pos2.y) + 5) / 2f, 0);
        currentPosition += (positionGoal - currentPosition) * delta;

        distance = Vector2.Distance(new Vector2(pos1.x, pos1.y), new Vector2(pos2.x, pos2.y));

        destSize = MAX_SIZE * distance * distanceScale;

        currentSize += (destSize - currentSize) * delta;

        if (currentSize > MAX_SIZE) { currentSize = MAX_SIZE; Debug.Log("Am I here"); }
        if (currentSize < MIN_SIZE) { currentSize = MIN_SIZE; }

        //Camera.main.orthographicSize = currentSize;
        Camera.main.fieldOfView = currentSize;
        currentPosition.z = -25f;
        c.transform.position = currentPosition;
    }

    private void FixedUpdate()
    {

    }
}
