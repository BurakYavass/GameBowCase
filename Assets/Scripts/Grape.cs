using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour
{
    [SerializeField] private ParticleSystem grapeParticle;

    public void ParticlePlay()
    {
        grapeParticle.Play();
    }
}
