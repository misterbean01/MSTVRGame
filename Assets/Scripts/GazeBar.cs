using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeBar : MonoBehaviour
{
    private Image gazeBar;

    void Start() {
        gazeBar = GetComponent<Image>();
        gazeBar.fillAmount = 0;
    }

    public void setGazeBar(float timePercent) {
        gazeBar.fillAmount = timePercent;
    }
}
