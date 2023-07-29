using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuDeInicioScript : MonoBehaviour
{
    private int nextSceneIndex;




    public void cargarSiguienteScene()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void salirDelJue0go()
    {
        Application.Quit();
    }
}
