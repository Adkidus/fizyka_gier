using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AnimControl :  MonoBehaviour
{
    private Animator _animator;
    public Rigidbody _rigibody;
    public Rigidbody[] _rigidbodies;

    public float rotationSpeed = 1f;
    public bool onGround;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _rigibody = GetComponent<Rigidbody>();

        onGround = true;

        ToggleKinematics(true);
    }

    void Update()
    {
        bool walking = Input.GetKey(KeyCode.W);
        _animator.SetBool("walking", walking);

        bool backward = Input.GetKey(KeyCode.S);
        _animator.SetBool("backward", backward);

        bool running = Input.GetKey(KeyCode.LeftShift);
        _animator.SetBool("running", running);

        bool sneak = Input.GetKey(KeyCode.F);
        _animator.SetBool("sneak", sneak);

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 300.0f * rotationSpeed;
        transform.Rotate(0, x, 0);

        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rigibody.velocity = new Vector3(0f, 5f, 0f);
                onGround = false;
            }
        }
    }

    void ToggleKinematics(bool toggle)
    {
        foreach (var rb in _rigidbodies)
        {
            if (rb == _rigibody)
                continue;
            rb.isKinematic = toggle;
            rb.detectCollisions = !toggle;
            _animator.enabled = toggle;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            onGround = true;

        if (other.gameObject.tag == "Enemy")
            Ragdol();
    }

    void Ragdol()
    {
        ToggleKinematics(false);
        _animator.enabled = false;

        Invoke("Restart", 5f);
    }

    void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}