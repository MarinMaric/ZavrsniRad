using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimator : MonoBehaviour
{
    Animator anim;

    public void Test()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        
        anim.SetBool("Fire", false);
    }
}
