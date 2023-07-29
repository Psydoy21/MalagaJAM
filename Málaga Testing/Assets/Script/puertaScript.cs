using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puertaScript : MonoBehaviour
{
    //---------------acceso al script del jugador---------------
    [SerializeField] PlayerPlataformaScript PlayerPlataformaScript;
    //-----------indice de escena-----------
    private int nextSceneIndex;
    //----------Acceso al animator de fade----------
    [SerializeField] GameObject fade;
    private Animator animator;
    private void Start()
    {
        animator = fade.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision && PlayerPlataformaScript.objetoCogido == true)
        {
            StartCoroutine(SceneLoad());
            PlayerPlataformaScript.bloquearElPersonaje();
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
