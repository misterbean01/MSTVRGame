using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapParticles : MonoBehaviour
{
    public ParticleSystem myParticles;
    private bool isParticleEnabled = false;

    void Awake() {
        myParticles.Stop();
    }

    public void switchParticleState() {
        isParticleEnabled = !isParticleEnabled;
        if (isParticleEnabled) {
            myParticles.Play();
        } else {
            myParticles.Stop();
        }
    }
}
