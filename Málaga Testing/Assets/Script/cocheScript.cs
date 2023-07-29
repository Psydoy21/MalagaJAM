using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cocheScript : MonoBehaviour
{

    [SerializeField] float velocidadCoche;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Vector3.right * Time.deltaTime * velocidadCoche;
        }
    }
}
