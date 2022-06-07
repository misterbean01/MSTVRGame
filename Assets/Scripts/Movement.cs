using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpforce = 5;
    public Transform rotationController;
    public float maxRotationControl = 20f;
    public float minRotationControl = 10f;

    private float horizontalInput;
    private float forwardInput;
    private float sideInput;
    private Rigidbody playerRb;
    // Animator animator;

    private const float minDurationButtonHeld = 1.00f;
    private float timeButtonPressed = 0f;
    private bool isButtonHeld = false;

    public Rayshooter gaze;

    public RotateBar rotatebar;
    public Text moveText;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        moveText.enabled = isButtonHeld;
        // animator = GetComponent<Animator>();
    }

    // change degrees similar to the inspector (can do negative rotations) since eulerAngles uses 0 to 360
    float convertAnglestoInspectorSide(float degree) 
    {   
        // Debug.Log("deg = " + degree);
        float input = 0f;
        if (degree > 180f) {
            degree = degree - 360f;
        }
        float tempMinRot = 20f;
        float tempMaxRot = 25f;
        // the minimum degree where there is 0 movement
        if (degree <= tempMinRot && degree >= -tempMinRot)
            input = 0;
        else if (degree > tempMinRot)
            input = (degree - tempMinRot) / (tempMaxRot - tempMinRot);
        else if (degree < -tempMinRot)
            input = (degree + tempMinRot) / (tempMaxRot - tempMinRot);
        return input;
    }

    // change degrees similar to the inspector (can do negative rotations) since eulerAngles uses 0 to 360
    float convertAnglestoInspector(float degree) 
    {   
        float input = 0f;
        if (degree > 180f) {
            degree = degree - 360f;
        }

        // the minimum degree where there is 0 movement
        if (degree <= minRotationControl && degree >= -minRotationControl)
            input = 0;
        else if (degree > minRotationControl)
            input = (degree - minRotationControl) / (maxRotationControl - minRotationControl);
        else if (degree < -minRotationControl)
            input = (degree + minRotationControl) / (maxRotationControl - minRotationControl);
        return input;
    }

    float convertInputForTransform(float rot) {
        float input = rot;
        // use max input = +-1 when degree is above maxRotationControl
        if (input > 1)
            input = 1;
        if (input < -1)
            input = -1;
        return input;
    }

    void moveUsingRotation() {
        // movement via rotation input
        forwardInput = convertInputForTransform(convertAnglestoInspector(rotationController.localRotation.eulerAngles.x));
        horizontalInput = convertInputForTransform(convertAnglestoInspector(rotationController.localRotation.eulerAngles.z)) * -1; // reverse the controls
        sideInput = convertInputForTransform(convertAnglestoInspectorSide(rotationController.localRotation.eulerAngles.y));

        // move the player forward, backward, sideward, and rotate
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Rotate(Vector3.up * sideInput/2);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationController.localRotation.eulerAngles.y, transform.eulerAngles.z);
    }

    void OnCollisionEnter(Collision  col) {
        // If a GameObject has an "Enemy" tag, remove him. 
        if(col.gameObject.tag == "OutOfBounds") {
            //Debug.Log("Fall");
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            rotationController.transform.eulerAngles = Vector3.zero;
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            
        }
    }

    // Update is called once per frame
    void Update()
    {   
        rotatebar.setRotateBar(rotationController.localRotation.eulerAngles.y);
        moveText.enabled = isButtonHeld;

        if (!isButtonHeld) {
            // testing this out
            // Debug.Log("Trigger");                
            rotationController.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        } 

        //isButtonHeld = true;
        if (Input.GetMouseButtonDown(0)) {
            rotationController.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            timeButtonPressed = Time.time; // Keep track the time the button was pressed
            isButtonHeld = false;
            gaze.updateButtonHeldState(isButtonHeld);
            //Debug.Log("Start");
        }
        
        if (Input.GetMouseButtonUp(0)) {
            rotationController.transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            isButtonHeld = false;
            gaze.updateButtonHeldState(isButtonHeld);
            //Debug.Log("Release");
        } 

        if (Input.GetMouseButton(0)) {
            float _hold = Time.time; // keep track how much time the button is held
            // Debug.Log(isButtonHeld + ": " + _hold + " - " + timeButtonPressed + " > " + minDurationButtonHeld);
            if (_hold - timeButtonPressed > minDurationButtonHeld) {
                isButtonHeld = true;
                gaze.updateButtonHeldState(isButtonHeld);
                moveUsingRotation();
                //Debug.Log("Held");
            }             
        }
    }
}
