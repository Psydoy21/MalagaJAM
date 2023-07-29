using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlane : MonoBehaviour
{
    [SerializeField]PlayerPlataformaScript PlayerPlataformaScript;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            PlayerPlataformaScript.deathPlane();
        }
    }
}
