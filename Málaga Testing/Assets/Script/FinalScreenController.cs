using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenController : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(LongTransition());
        }
    }
    IEnumerator LongTransition()
    {
        animator.SetTrigger("Start Long Transition");
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("Menu de inicio");
    }

}
