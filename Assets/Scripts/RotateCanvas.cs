using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCanvas : MonoBehaviour
{   
    private float rotationSpeedDef = 3f;
    private float moveSpeedDef = 0.3f;
    private float newRotationSpeed = 0f;

    void Start()
    {
        InvokeRepeating("randomRotationSpeed", 0f, 5f);   
    }
    
    void FixedUpdate()
    {   
        transform.Rotate(new Vector3(0f, newRotationSpeed, 0f));
        float y = transform.position.y + Mathf.PingPong(Time.time * moveSpeedDef, 0.4f) - 0.2f; 
        //Debug.Log(y);  
        transform.position = new Vector3(transform.position.x, y, transform.position.z);        
    }

    void randomRotationSpeed()
    {
        newRotationSpeed = Random.Range(-rotationSpeedDef, rotationSpeedDef);
        
    }
}
