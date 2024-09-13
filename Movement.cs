using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Codes have been refactored,but logic is still the same.

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            
            StartThrusting();

        } else {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        // if and else if only apply to the below
        if (Input.GetKey(KeyCode.A))
        {
            RotatingLeft();
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            RotatingRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        // AddRelativeForce(Vector3): Vector3 parameter; 3 values, for both direction and magnitude
            // 1 (on X), 1 (on Y), 1 (on Z)
            // 0 (on X), 1 (on Y), 0 (on Z) -> Up in the air
            // rb.AddRelativeForce(0, 1, 0);
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying) 
            {
                // audioSource.Play();
                audioSource.PlayOneShot(mainEngine);
            }

            if (!mainBooster.isPlaying)
            {
                 mainBooster.Play();
            }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void RotatingLeft()
    {
        if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }

            // Debug.Log("Rotating Left");
            // transform.Rotate(0, 0, 1 * rotateLeftFactor * Time.deltaTime);
            ApplyRotation(rotationThrust);
    }

    void RotatingRight()
    {
        if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
        // Debug.Log("Rotating Right");
        // transform.Rotate(0, 0, -1 * rotateRightFactor * Time.deltaTime);

        // transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        ApplyRotation(-rotationThrust);
    }

    void StopRotating()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {   
        // Have to freeze the Physics here because any collison between ...
        // ...Rocket and obstacle will mess up the rotation. Manually rotate.
        rb.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        // However, after finishing applying rotation, I have to unfreeze the rotation...
        // ...to let the Physics engine take over.
        rb.freezeRotation = false;
    }
}
