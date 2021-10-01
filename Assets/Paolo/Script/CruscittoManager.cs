using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CruscittoManager : MonoBehaviour
{
    [Header("Immagini spie")]
    [Tooltip("Le immagini delle spie mostrate sul cruscotto")]
    public Image[] spie;

    [Header("Testo spiegazione")]
    [Tooltip("Testo della spiegazione delle che verrà mostrato sul display del cruscotto")]
    [TextArea]
    public string[] spieTextIta, spieTextEng;

    [Header("Testo sul cruscotto")]
    [Tooltip("Testo nel panel del cruscotto in cui verrà caricato il testo associato al decal")]
    public TextMeshProUGUI dispalyText;

    [Space]

    [Header("Posizone di destinazione della camera")]
    [Tooltip("Posizone di destinazione della camera quando ci si vuole avvicinare al cruscotto")]
    public Transform cruscottoCamera;
    [Header("Posizone di destinazione della camera in terza persona")]
    [Tooltip("Posizone di destinazione della camera in terza persona ")]
    public Transform thirdCamera;
    [Header("Posizone originale della camera")]
    [Tooltip("Posizone originale della camera quando ci si vuole allontanare dal cruscotto")]
    public Vector3 originalCamera;

    [Space]

    [Header("Testo del mini display quadrato")]
    [Tooltip("Testo del mini display quadrato che si attiva sull'errore")]
    public TextMeshProUGUI miniDisplayText;
    [Header("Testo del mini display ovale")]
    [Tooltip("Testo del mini display ovale che mostra data ed ora")]
    public TextMeshProUGUI miniDisplaySmallText;
    [Header("Immagine del mini display quadrato")]
    [Tooltip("Immagine del mini display quadrato che si attiva sull'errore")]
    public Image miniDisplayImg;

    [Space]

    
    public float warningFlashSpeed;

    private GameObject tractor;
    private Coroutine warningVisible;

    int spiaDaAccendere;
    int km,km2;
    public bool positionBool = true;
    public bool positionThree = false;

    // Start is called before the first frame update
    void Start()
    {
        tractor = GameObject.FindGameObjectWithTag("Machine");
        originalCamera = Camera.main.transform.position;
        km = (int)Random.Range(1, 999);
        km2 = (int)Random.Range(0, 999);
        resetSpie();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        miniDisplaySmallText.text = "  " + System.DateTime.Now.ToString("dd/MM/yyyy") + " " + System.DateTime.Now.ToString("hh:mm:ss") + "  " + "\n" +"  KM: " + km+"."+km2 ;      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            spiaDaAccendere = (int)Random.Range(0, spie.Length);
            accendiSpia(spiaDaAccendere);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            accendiSpia(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            accendiSpia(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            accendiSpia(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            accendiSpia(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            accendiSpia(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            accendiSpia(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            accendiSpia(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            accendiSpia(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            accendiSpia(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            accendiSpia(9);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            accendiSpia(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            accendiSpia(11);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            accendiSpia(12);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            accendiSpia(13);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            resetSpie();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            moveCamera();
        }
    }

    void accendiSpia(int spiaId)
    {
        
        resetSpie();

        warningVisible = StartCoroutine(warningCounter(warningFlashSpeed));

        if (spiaId <= spie.Length - 1)
        {
            spie[spiaId].color = new Color(1f, 0f, 0f, 1f);
            if (Camera.main.GetComponent<MenuHandler>().language == "ITA")
            {
                dispalyText.text = spieTextIta[spiaId];
            }
            else if (Camera.main.GetComponent<MenuHandler>().language == "ENG")
            {
                dispalyText.text = spieTextEng[spiaId];
            }                
        }
        
    }

    void resetSpie()
    {
        if(warningVisible != null)
        StopCoroutine(warningVisible);

        miniDisplayImg.gameObject.SetActive(false);
        foreach (Image spia in spie)
        {
            spia.color = new Color(0f, 0f, 0f, 0f);
        }

        dispalyText.text = "";

        if (Camera.main.GetComponent<MenuHandler>().language == "ITA")
            miniDisplayText.text = "Situazione regolare";
        else if (Camera.main.GetComponent<MenuHandler>().language == "ENG")
            miniDisplayText.text = "Machine working properly";
    }

    IEnumerator warningCounter(float sec)
    {
        while (true)
        {
            miniDisplayImg.gameObject.SetActive(true);
            yield return new WaitForSeconds(sec*2);

            if (Camera.main.GetComponent<MenuHandler>().language == "ITA")
                miniDisplayText.text = "Errore, controllare spie e diplay";
            else if (Camera.main.GetComponent<MenuHandler>().language == "ENG")
                miniDisplayText.text = "Error, check indicators and display ";
            miniDisplayImg.gameObject.SetActive(false);

            yield return new WaitForSeconds(sec);

            StopCoroutine(warningVisible);
            warningVisible = StartCoroutine(warningCounter(warningFlashSpeed));
        }
    }
    
    void moveCamera()
    {
        if (positionBool && !positionThree)
        {
            positionBool = false;
            positionThree = false;
            Camera.main.transform.position = cruscottoCamera.position;
            tractor.GetComponent<MeshRenderer>().enabled = false;
            Camera.main.GetComponent<CameraController>().isThirdPerson = false;
        }
        else if(!positionBool && !positionThree)
        {
            tractor.GetComponent<MeshRenderer>().enabled = true;
            positionThree = true;
            positionBool = true;
            Camera.main.transform.position = thirdCamera.position;
            Camera.main.transform.rotation = thirdCamera.rotation;
            Camera.main.GetComponent<CameraController>().isThirdPerson = true;
        }
        else if (positionBool && positionThree)
        {
            tractor.GetComponent<MeshRenderer>().enabled = true;
            positionThree = false;
            positionBool = true;
            Camera.main.transform.position = originalCamera;
            Camera.main.GetComponent<CameraController>().isThirdPerson = false;
        }
    }
}
