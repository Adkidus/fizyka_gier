using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public bool PrefabShooting = false;
    public Rigidbody theBullet;
    public float Speed = 20;
    public bool RaycastShooting = false;
    public Transform Effect;
    public float TheDammage = 100;

    void Update()
    {
        if (PrefabShooting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var clone = Instantiate(theBullet, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z + 0.5f), transform.rotation);
                clone.velocity = transform.TransformDirection(new Vector3(0, 0, Speed));
                Destroy(clone.gameObject, 3);
            }
        }
        if (RaycastShooting)
        {
            RaycastHit rhit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out rhit, 100))
                {
                    var particleClone = Instantiate(Effect, rhit.point, Quaternion.LookRotation(rhit.normal));
                    Destroy(particleClone.gameObject, 2);
                }
            }
        }
    }
}
