using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cocheScript : MonoBehaviour
{
    [SerializeField] public GameObject panel;
    [SerializeField] public Text texto;
 

    private bool isopen;

    [SerializeField] public int linea;
    
    [SerializeField, TextArea(4, 6)] private string[] lineas;
    public float type;
    public int indTexto;
    public TextAsset[] textitos;
    private int nextSceneIndex;
    public int longi;

    [SerializeField] float velocidadCoche;
    // Update is called once per frame
    void Start()
    {
        indTexto = 0;
        
        //animator = fade.GetComponent<Animator>();
        if (textitos != null)
        {
            lineas = (textitos[indTexto].text.Split("\n"));
        }
        longi = lineas.Length;
        

        
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            StartDialogue();
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Vector3.right * Time.deltaTime * velocidadCoche;
        }
        //texto
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isopen)
            {
                StartDialogue();
            }
            else if (isopen && texto.text == lineas[linea])
            {
                closedialogue();
                if (indTexto >= textitos.Length)
                {
                    Debug.Log("cerrardialogo");
                    StartCoroutine(SceneLoad());
                }
            }
            else
            {
                completedialogue();
            }
        }
    }
    private void StartDialogue()
    {
        if (indTexto < 1)
        {
            indTexto++;
        }

        Debug.Log("aumenta el indice");
        isopen = true;
        panel.SetActive(true);
        
        linea = 0;
        Time.timeScale = 0f;
        StartCoroutine(Showline());
       
    }
    private IEnumerator Showline()
    {
        int charindex;
        charindex = 0;
        texto.text = string.Empty;
        foreach (char ch in lineas[linea])
        {
            texto.text += ch;
            charindex++;
            yield return new WaitForSecondsRealtime(type);
        }
    }
    private void completedialogue()
    {
        //si añado otra corutina se va a liar
        StopAllCoroutines();
        texto.text = lineas[linea];
    }
    private void closedialogue()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        isopen = false;
        cambiartext();
    }
    private void cambiartext()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            lineas = (textitos[indTexto].text.Split("\n"));
        }

    }
    public IEnumerator SceneLoad()
    {
        //animator.SetTrigger("Start Transition");
        yield return new WaitForSeconds(1f);
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
