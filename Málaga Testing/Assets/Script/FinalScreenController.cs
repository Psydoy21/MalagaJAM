using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScreenController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            animator.SetTrigger("Start Long Transition");
        }
    }

}
