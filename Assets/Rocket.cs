using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour

{

    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 1000f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Friendly":
                print("ok");
                break;
            case "Fuel":
                print("fuel");
                break;
            default:
                print("dead");
                    break;

        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //take manual control of rotation

    }

    private void Thrust(){

        float mainThrustThisFrame = mainThrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrustThisFrame);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }




}