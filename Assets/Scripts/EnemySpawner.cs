using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private NodeState node1;
    [SerializeField] private NodeState node2;
    
    private GameObject enemy;
    private bool isSpawnerOn = true;
    private bool isBeginning = true;

    public Material defaultMaterialPath;
    public Material clearedMaterialPath;
    public GameObject pathGO;
    private Renderer pathRend;

    public int edgeCost = -1;

    void Start() {
        pathRend = pathGO.GetComponent<Renderer>();
        pathRend.enabled = true;
        pathRend.sharedMaterial = defaultMaterialPath;
    }

    public bool getSpawnerState() {
        return isSpawnerOn;
    }

    public int getEdgeCost() {
        int tempCost = 0;
        if (!isSpawnerOn) {
            tempCost = edgeCost;
        }
        return tempCost;
    }

    void flipPathState() {
        if ((node1.getNodeState()) && (node2.getNodeState())) { // if both nodes are on a cleared state, 
            isSpawnerOn = false; // turn off spawner
            pathRend.sharedMaterial = clearedMaterialPath; // path is blue
        } else {
            isSpawnerOn = true; // otherwise turn on spawner
            pathRend.sharedMaterial = defaultMaterialPath; // path is red
        }
    }

    // Update is called once per frame
    void Update()
    {   
        flipPathState();

        // only spawn enemies when Switch is off/red
        if (isSpawnerOn) {
            if (enemy == null) {
                enemy = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
                if (!isBeginning) {
                    enemy.SetActive(false);
                    StartCoroutine("respawnWithDelay");
                }
            }
            isBeginning = false;
        } else {
            Destroy(enemy);
            enemy = null;
        }
    }

    private IEnumerator respawnWithDelay() {
        yield return new WaitForSeconds(10f);
        if (enemy != null)
            enemy.SetActive(true);
    }
}
