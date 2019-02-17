using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject moveplatform;
    public Collider player;

    float maxY = 7.84f;
    float minY = 0.36f;

    bool stay;

    void Start()
    {
        player = GetComponent<Collider>();
        stay = false;
    }

    void Update()
    {
        if (stay && moveplatform.transform.position.y <= maxY)
            ElevatorMoveTop();
        else if(!stay && moveplatform.transform.position.y >= minY)
            ElevatorMoveBottom();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        stay = true;
        player = other;
    }

    private void OnTriggerExit(Collider other)
    {
        stay = false;
    }

    void ElevatorMoveTop()
    {
        player.transform.position += moveplatform.transform.up * Time.deltaTime;
        moveplatform.transform.position += moveplatform.transform.up * Time.deltaTime;
    }

    void ElevatorMoveBottom()
    {
        moveplatform.transform.position -= moveplatform.transform.up * Time.deltaTime;
    }
}
