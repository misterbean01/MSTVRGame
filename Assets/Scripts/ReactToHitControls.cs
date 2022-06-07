using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToHitControls : MonoBehaviour
{
    public GameObject body;
    private float jumpforce = 25f;
    private float speed = 5f;

    public void ReactToHit()
    {
        string controlGOName = gameObject.name;
        Debug.Log(controlGOName);

        if (controlGOName == "CircleUp") {
            body.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        } else if (controlGOName == "CircleLeft") {
            body.GetComponent<Rigidbody>().AddForce(Vector3.up * speed, ForceMode.Impulse);
            body.GetComponent<Rigidbody>().AddForce(Vector3.right * -speed, ForceMode.Impulse);
        } else if (controlGOName == "CircleRight") {
            body.GetComponent<Rigidbody>().AddForce(Vector3.up * speed, ForceMode.Impulse);
            body.GetComponent<Rigidbody>().AddForce(Vector3.right * speed, ForceMode.Impulse);
        }
    }
}
