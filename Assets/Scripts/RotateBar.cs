using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBar : MonoBehaviour
{
    public Image rotateBarLeft;
    public Image rotateBarRight;

    // Start is called before the first frame update
    void Start()
    {
        rotateBarLeft.fillAmount = 0;
        rotateBarRight.fillAmount = 0;
    }

    public void setRotateBar(float degree) {
        if (degree > 180f) {
            degree = degree - 360f;
        }

        float tempPercent = 0;
        if (degree > 20) {
            if (degree >= 25) {
                tempPercent = 5;
            } else {
                tempPercent = degree - 20; 
            }
        } else if (degree < -20) {
            if (degree <= -25) {
                tempPercent = -5;
            } else {
                tempPercent = degree + 20;
            }
        } else {
            tempPercent = 0;
        }
        
        if (tempPercent < 0) {
            rotateBarLeft.fillAmount = -(tempPercent/5);
            rotateBarRight.fillAmount = 0;
        } else if (tempPercent > 0) {
            rotateBarLeft.fillAmount = 0;
            rotateBarRight.fillAmount = (tempPercent/5);
        } else {
            tempPercent = 0;
            rotateBarLeft.fillAmount = 0;
            rotateBarRight.fillAmount = 0;
        }
    }
}
