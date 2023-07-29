using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;
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
    public TextAsset textito;

    // Start is called before the first frame update
    void Start()
    {
        if(textito!=null)
        {
            lineas = (textito.text.Split("\n"));
        }
        longi = lineas.Length;
        inrange = false;
        panel.SetActive(false);
        exclamacion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(inrange&&!isopen) {
                StartDialogue();
            }
            else if(isopen&& texto.text == lineas[linea])
            {
                closedialogue();
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
        exclamacion.SetActive(true);
        Time.timeScale = 1f;
        isopen = false;
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
        isopen = true;
        panel.SetActive(true);
        exclamacion.SetActive(false);
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
}
