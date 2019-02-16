using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    GameObject ebullet;
    public Transform target;
    public Transform mytransform;
    public GameObject enemy;
    public float elife = 100f;

    void Start()
    {
        ebullet = Resources.Load("bullets") as GameObject;
    }

    void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.forward * 5 * Time.deltaTime);

        GameObject bullets = Instantiate(ebullet) as GameObject;
        bullets.transform.position = transform.position * 1;

        Rigidbody rb = bullets.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 20;
        Destroy(bullets, 9f);

        if (elife <= 0)
        {
            enemy.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bulleter"))
        {
            elife -= 50;
        }
    }
}
