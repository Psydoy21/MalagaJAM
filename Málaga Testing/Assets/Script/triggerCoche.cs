using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerCoche : MonoBehaviour
{
    int nextSceneIndex;
    [SerializeField] GameObject fade;
    Animator animator;
    private void Start()
    {
        animator = fade.GetComponent<Animator>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player") 
        {
            Debug.Log("Trigger");
            StartCoroutine(SceneLoad());
        }
    }
    public IEnumerator SceneLoad()
    {
        animator.SetTrigger("Start Transition");
        yield return new WaitForSeconds(1f);
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
