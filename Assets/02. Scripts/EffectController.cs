using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] skills;

    public void EnableEffect(int idx)
    {
        skills[idx].Play();
    }

}
