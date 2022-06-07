using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayshooter : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    private int buttonClickNum;
    private float resetTimer = 0.7f ;
    private bool isButtonHeld;

    private bool isDoubleClick = false;
    private bool isTripleClick = false;

    public GameObject minimap;
    private bool isMinimapEnabled = false;

    private bool gazeOn = false;
    private float gazeOnTimer = 0f;
    public GazeBar gazeBar;

    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        minimap.SetActive(isMinimapEnabled);
    }

    void shootUsingRayCaster() {
        Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            //StartCoroutine(SphereIndicator(hit.point));

            GameObject hitObject = hit.transform.gameObject;

            ReactiveTarget enemyTarget = hitObject.GetComponent<ReactiveTarget>();
            if (enemyTarget != null) {
                StartCoroutine(SphereIndicator(hit.point));
                enemyTarget.ReactToHit();
            } 
        }
    }

    void gazeUsingRayCasterInteractable() {
        Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
        Ray ray = _camera.ScreenPointToRay(point);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("Interactable")) {
                GameObject hitObject = hit.transform.gameObject;

                // check if hit object is a Switch
                SwitchState switchTarget = hitObject.GetComponent<SwitchState>();
                // check if hit object is for map particles
                MapParticles minimapTarget = go.GetComponent<MapParticles>();
                //Debug.Log(minimapTarget);
                if (switchTarget != null) {
                    gazeOn = true;
                    gazeOnTimer += Time.deltaTime;
                    gazeBar.setGazeBar(gazeOnTimer / 3); // 3 is max gaze time

                    if (gazeOnTimer >= 3 && gazeOn) { // 3 is max gaze time
                        StartCoroutine(SphereIndicator(hit.point));
                        switchTarget.SwitchMaterials();
                        gazeOn = false;
                        gazeOnTimer = 0f;
                        gazeBar.setGazeBar(gazeOnTimer);
                    }
                } else if (minimapTarget != null) {
                    //Debug.Log("Hit!");
                    gazeOn = true;
                    gazeOnTimer += Time.deltaTime;
                    gazeBar.setGazeBar(gazeOnTimer / 3); // 3 is max gaze time

                    if (gazeOnTimer >= 3 && gazeOn) { // 3 is max gaze time
                        StartCoroutine(SphereIndicatorSmall(hit.point));
                        minimapTarget.switchParticleState();
                        gazeOn = false;
                        gazeOnTimer = 0f;
                        gazeBar.setGazeBar(gazeOnTimer);
                    }
                } else {
                    gazeOn = false;
                    gazeOnTimer = 0f;
                    gazeBar.setGazeBar(gazeOnTimer);
                }
            } else {
                gazeOn = false;
                gazeOnTimer = 0f;
                gazeBar.setGazeBar(gazeOnTimer);
            }
        } else {
            gazeOn = false;
            gazeOnTimer = 0f;
            gazeBar.setGazeBar(gazeOnTimer);
        }
    }

    public void updateButtonHeldState(bool state) {
        isButtonHeld = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isButtonHeld) {
            gazeUsingRayCasterInteractable();
        } else {
            gazeOn = false;
            gazeOnTimer = 0f;
            gazeBar.setGazeBar(0f);
        }

        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine("resetButtonClick");
            buttonClickNum++;
        }

        if (isDoubleClick) {
            shootUsingRayCaster();
            buttonClickNum = 0;
            isDoubleClick = false;
        }

        if (isTripleClick) {
            // trigger Minimap on triple click
            isMinimapEnabled = !isMinimapEnabled; // flip the bool when triple clicked
            minimap.SetActive(isMinimapEnabled);
            buttonClickNum = 0;
            isTripleClick = false;
        }
    }

    private IEnumerator resetButtonClick() {
        yield return new WaitForSeconds(resetTimer);
        
        if (buttonClickNum == 2) {
            isDoubleClick = true;
        }
        
        if (buttonClickNum >= 3) {
            isTripleClick = true;
        }

        buttonClickNum = 0; // for the single click
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(180,0,0));
        sphere.transform.position = pos;
        yield return new WaitForSeconds(0.2f);
        Destroy(sphere);
    }

    private IEnumerator SphereIndicatorSmall(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(180,0,0));
        float size = 0.2f;
        sphere.transform.localScale = new Vector3(size, size, size);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(0.1f);
        Destroy(sphere);
    }
}
