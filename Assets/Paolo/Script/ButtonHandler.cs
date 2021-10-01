using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [Header("Bottone decal")]
    [Tooltip("Bottone associato al decal")]
    public Button button;

    [Header("Testo nella schermata principale")]
    [Tooltip("Testo nel panel della schermata principale in cui verrà caricato il testo associato al decal")]
    public TextMeshProUGUI daScivere;

    [Header("Testo spiegazione")]
    [Tooltip("Testo della spiegazione del decal che verrà mostrato quando si preme sul decal")]
    [TextArea]
    public string testoIta, testoEng;

    [Header("Immagine nella schermata principale")]
    [Tooltip("Immagine nel panel della schermata principale in cui verrà caricato lo sprite associato al decal")]
    public Image decalImage;

    [Header("Panel della schermata principale")]
    [Tooltip("Panel della schermata principale in cui verrà caricato lo sprite associato al decal")]
    public Image decalPanel;

    [Header("Sprite del decal")]
    [Tooltip("Lo sprite associato al decal")]
    public Sprite decalSprite;
    
    
    private float second;
    private Coroutine panelVisible;
    
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(delegate { showText(); });
        decalPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            decalPanel.gameObject.SetActive(false);
        }
    }

    void showText()
    {
        second = Camera.main.GetComponent<MenuHandler>().secondsVisibility;

        if (Camera.main.GetComponent<MenuHandler>().language == "ITA")
        {
            daScivere.text = testoIta;
        }
        else if (Camera.main.GetComponent<MenuHandler>().language == "ENG")
        {
            daScivere.text = testoEng;
        }

        decalImage.sprite = decalSprite;
        decalPanel.gameObject.SetActive(true);
        panelVisible = StartCoroutine(visibilityCounter(second)) ;
    }

    IEnumerator visibilityCounter(float sec)
    {
        while (true) { 
              
            yield return new WaitForSeconds(sec);
            decalPanel.gameObject.SetActive(false);
            StopCoroutine(panelVisible);
        }
    }

}
