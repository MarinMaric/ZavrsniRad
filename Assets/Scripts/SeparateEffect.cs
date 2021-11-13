using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparateEffect : MonoBehaviour
{
    public delegate void Activate();
    public Activate activateFunction;
    public ParticleSystem effect;
    public AudioSource audioSrc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            if (effect != null)
            {
                effect.Play();
                GetComponent<MeshRenderer>().enabled = false;
            }
            audioSrc.Play();
            activateFunction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Robot")
        {
            if (effect != null)
            {
                Destroy(effect);
            }
            Destroy(gameObject);
        }
        
    }
}
