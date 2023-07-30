using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class objeto : MonoBehaviour
{
    [SerializeField] public GameObject panel;
    [SerializeField] public Text texto;
    public bool inrange;
    public bool objetoc;
    private bool isopen;
    [SerializeField] public GameObject exclamacion;
    [SerializeField] public int linea;
    public int longi;
    [SerializeField, TextArea(4, 6)] private string[] lineas;
    public float type;
    public int indTexto;
    public TextAsset [] textitos;
    private int nextSceneIndex;
    public Collider2D gatillo;
    public int contador;
    //animator del fade out/in
    [SerializeField] private GameObject fade;
    [HideInInspector] Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        indTexto = 0;
        contador = 0;
        animator = fade.GetComponent<Animator>();
        if(textitos!=null)
        {
            lineas = (textitos[indTexto].text.Split("\n"));
        }
        longi = lineas.Length;
        inrange = false;

        exclamacion.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            StartDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(inrange&&!isopen) {
                StartDialogue();
                inrange = false;
            }
            else if(isopen&& texto.text == lineas[linea])
            {
                closedialogue();
                if((indTexto >= textitos.Length) || (contador==2))
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            exclamacion.SetActive(true);
            inrange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exclamacion.SetActive(false);
            inrange = false;
        }
    }
    private void StartDialogue()
    {
        if(indTexto<1)
        {
            indTexto++;
        }
        
        Debug.Log("aumenta el indice");
        isopen = true;
        panel.SetActive(true);
        exclamacion.SetActive(false);
        linea = 0;
        Time.timeScale = 0f;
        StartCoroutine(Showline());
        contador++;
    }
    void cambioDeTexto()
    {
        if (textitos != null)
        {
            lineas = (textitos[indTexto].text.Split("\n"));
        }
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
    public IEnumerator SceneLoad()
    {
        animator.SetTrigger("Start Transition");
        yield return new WaitForSeconds(1f);
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
