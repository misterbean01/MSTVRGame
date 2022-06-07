using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyManagement : MonoBehaviour
{
    private int minEnergyMST = 18;
    private int currentEnergyAvailable;
    private int currentEnergyCost = 0;
    public EnemySpawner[] spawners;

    public Text textUIEnergyAvailable;

    void Start() {
        currentEnergyAvailable = minEnergyMST;
        // Text textUIEnergyAvailable = GameObject.Find("CanvasUI/Text").GetComponent<Text>();
    }

    public int getMinEnergyMST() {
        return minEnergyMST;
    }

    public int getCurrentEnergyAvailable() {
        return currentEnergyAvailable;
    }

    public void editEnergy(int en) {
        minEnergyMST += en;
    }

    void Update() {
        Debug.Log(getCurrentEnergyAvailable());
        int tempTotalEnergyCost = 0;
        foreach(EnemySpawner spawner in spawners) {
            tempTotalEnergyCost += spawner.getEdgeCost();
        }
        currentEnergyCost = tempTotalEnergyCost;
        currentEnergyAvailable = minEnergyMST - currentEnergyCost;
        textUIEnergyAvailable.text = "Energy Supply: " + currentEnergyAvailable;
    }
}
