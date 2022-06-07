using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    private int buttonClickNum;
    private float resetTimer = 0.2f ;
    private bool isButtonHeld;

    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void shootUsingRayCaster() {
        Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            StartCoroutine(SphereIndicator(hit.point));

            GameObject hitObject = hit.transform.gameObject;
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

            if (target != null) {
                target.ReactToHit();
            } else {
                StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    public void updateButtonHeldState(bool state) {
        isButtonHeld = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isButtonHeld) {
            // RaycastHit hit;
            // GameObject go = hit.collider.gameObject;
            // if (go.CompareTag("Interactable")) {
                shootUsingRayCaster();
            
        }


        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine("resetButtonClick");
            buttonClickNum++;
        }

        if (buttonClickNum == 2) {
            shootUsingRayCaster();
            buttonClickNum = 0;
        }
    }

    private IEnumerator resetButtonClick() {
        yield return new WaitForSeconds(resetTimer);
        buttonClickNum = 0;
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(250,0,20));
        sphere.transform.position = pos;
        yield return new WaitForSeconds(0.4f);
        Destroy(sphere);
    }
}