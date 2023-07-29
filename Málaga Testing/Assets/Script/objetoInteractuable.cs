using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objetoInteractuable : MonoBehaviour
{
    [SerializeField] GameObject interactBubble;

    [HideInInspector] public bool interactuable = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision)
        {
            interactBubble.SetActive(true);
            interactuable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision)
        {
            interactBubble.SetActive(false);
            interactuable = false;
        }
    }
    public void desactivarObjeto()
    {
        gameObject.SetActive(false);
    }
}
