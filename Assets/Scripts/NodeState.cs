using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeState : MonoBehaviour
{
    public Material defaultMaterialNode;
    public Material clearedMaterialNode;
    private Renderer nodeRend;

    public bool isNodeCleared = false;

    public void flipNodeState() {
        isNodeCleared = !isNodeCleared;
    }

    public bool getNodeState() {
        return isNodeCleared;
    }

    public void Start() {
        nodeRend = GetComponent<Renderer>();
        nodeRend.enabled = true;
        nodeRend.sharedMaterial = defaultMaterialNode;
    }

    public void Update() {
        if (isNodeCleared) {
            nodeRend.sharedMaterial = clearedMaterialNode;
        } else {
            nodeRend.sharedMaterial = defaultMaterialNode;
        }
    }
}
