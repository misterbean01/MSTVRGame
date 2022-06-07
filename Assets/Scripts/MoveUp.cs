using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    Rigidbody rigidBody;
    public GameObject upperRayCast;
    public GameObject lowerRayCast;
    public float stepSmooth = 20f;

    void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        stepClimb();
    }
    
    private void stepClimb()
    {
        Ray rayLower = new Ray(lowerRayCast.transform.position, transform.forward);
        Ray rayUpper = new Ray(upperRayCast.transform.position, transform.forward);
        RaycastHit hitLower, hitUpper;

        float bodyWidth = 2f;

        if (Physics.Raycast(rayLower, out hitLower, bodyWidth + 0.05f)) // Bottom Raycast check if there is an obstacle
        {
            if (!Physics.Raycast(rayUpper, out hitUpper, bodyWidth + 0.2f)) // Top Raycast check if rigid body can climb it
            {
                rigidBody.velocity = new Vector3(0, Time.fixedDeltaTime * stepSmooth, 0);
            }
        }
    }

    // void stepClimb()
    // {
    //      RaycastHit hitLower;
    //     if (Physics.Raycast(lowerRayCast.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
    //     {
    //         RaycastHit hitUpper;
    //         if (!Physics.Raycast(upperRayCast.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
    //         {
    //             rigidBody.velocity = new Vector3(0, Time.fixedDeltaTime * stepSmooth, 0);
    //         }
    //     }

    //      RaycastHit hitLower45;
    //     if (Physics.Raycast(lowerRayCast.transform.position, transform.TransformDirection(1.5f,0,1), out hitLower45, 0.1f))
    //     {

    //         RaycastHit hitUpper45;
    //         if (!Physics.Raycast(upperRayCast.transform.position, transform.TransformDirection(1.5f,0,1), out hitUpper45, 0.2f))
    //         {
    //             rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
    //         }
    //     }

    //     RaycastHit hitLowerMinus45;
    //     if (Physics.Raycast(lowerRayCast.transform.position, transform.TransformDirection(-1.5f,0,1), out hitLowerMinus45, 0.1f))
    //     {

    //         RaycastHit hitUpperMinus45;
    //         if (!Physics.Raycast(upperRayCast.transform.position, transform.TransformDirection(-1.5f,0,1), out hitUpperMinus45, 0.2f))
    //         {
    //             rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
    //         }
    //     }
    // }
}
