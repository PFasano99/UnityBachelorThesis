using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [Header("Pannello del menù")]
    [Tooltip("Il Panel che contiene il bottone del menù ed il panel insideMenuPanel")]
    public Image menuPanel;

    [Header("Bottone del menù")]
    [Tooltip("Il bottone del menù che richiama il panel insideMenuPanel")]
    public Button menuButton;

    [Header("Pannello interno del menù")]
    [Tooltip("Il Panel che contiene la ui delle impostazioni del menù")]
    public Image insideMenuPanel;

    [Space]

    [Header("I componenti della UI per la sensibilità")]
    public TextMeshProUGUI sensitivityText;
    public Slider sensitivitySlide;
    public TextMeshProUGUI sensitivityValueText;
    public string sensitivityIta, sensitivityEng;

    [Space]

    [Header("I componenti della UI per la rotazione")]
    public TextMeshProUGUI rotationAmountText;
    public Slider rotationAmountSlide;
    public TextMeshProUGUI rotationAmountValueText;
    public string rotationAmountIta, rotationAmountEng;

    [Space]

    [Header("I componenti della UI per il maxZoomIn")]
    public TextMeshProUGUI maxZoomInText;
    public Slider maxZoomInSlide;
    public TextMeshProUGUI maxZoomInValueText;
    public string maxZoomInIta, maxZoomInEng;

    [Space]

    [Header("I componenti della UI per il maxZoomOut")]
    public TextMeshProUGUI maxZoomOutText;
    public Slider maxZoomOutSlide;
    public TextMeshProUGUI maxZoomOutValueText;
    public string maxZoomOutIta, maxZoomOutEng;

    [Space]

    [Header("I componenti della UI per i secondi di visibilità del decal")]
    public TextMeshProUGUI secondsVisibilityText;
    public Slider secondsVisibilitySlide;
    public TextMeshProUGUI secondsVisibilityValueText;
    public string secondsVisibilityIta, secondsVisibilityEng;
    [Header("secondi di visibilità")]
    [Tooltip("I secondi di visibilità del panel per la spiegazione dei decal")]
    [Range(1f, 100.0f)]
    public float secondsVisibility;

    [Space]

    [Header("I componenti della UI per il maxZoomOut")]
    public TextMeshProUGUI maxUpText;
    public Slider maxUpSlide;
    public TextMeshProUGUI maxUpValueText;
    public string maxUpIta, maxUpEng;

    [Space]

    [Header("I componenti della UI per il maxZoomOut")]
    public TextMeshProUGUI maxDownText;
    public Slider maxDownSlide;
    public TextMeshProUGUI maxDownValueText;
    public string maxDownIta, maxDownEng;

    [Space]

    [Header("I componenti della UI per scegliere la lingua")]
    public Button itaButton;
    public Button engButton;

    public string language;

    [Space]

    
    [Header("Velocità di apertura del menù")]
    [Tooltip("indica ogni quanti secondi/1000 il menù si sposta verso destra o sinistra, più picolo il valore, piuù veloce si aprirà il menù")]
    [Range(0.1f, 1f)]
    public float menuSpeed;

    

    private Coroutine menuOpenCorutine;

    // Start is called before the first frame update
    void Start()
    {       
        getAllStats();
        setLanguage();

        menuButton.onClick.AddListener(delegate {
            menuSlide();                         
        });

        itaButton.onClick.AddListener(delegate { language = "ITA"; setLanguage(); });
        engButton.onClick.AddListener(delegate { language = "ENG"; setLanguage(); });

        sensitivitySlide.onValueChanged.AddListener(delegate {
            changeSlider(sensitivitySlide, sensitivityValueText);
            GetComponent<CameraController>().sensitivity = sensitivitySlide.value;
        });

        rotationAmountSlide.onValueChanged.AddListener(delegate {
            changeSlider(rotationAmountSlide, rotationAmountValueText);
            GetComponent<CameraController>().rotateAmount = rotationAmountSlide.value;
        });

        maxZoomInSlide.onValueChanged.AddListener(delegate {
            changeSlider(maxZoomInSlide, maxZoomInValueText);
            GetComponent<CameraController>().maxZoomIn = (int) maxZoomInSlide.value;
        });

        maxZoomOutSlide.onValueChanged.AddListener(delegate {
            changeSlider(maxZoomOutSlide, maxZoomOutValueText);
            GetComponent<CameraController>().maxZoomOut = (int)maxZoomOutSlide.value;
        });

        secondsVisibilitySlide.onValueChanged.AddListener(delegate {
            changeSlider(secondsVisibilitySlide, secondsVisibilityValueText);
            secondsVisibility = secondsVisibilitySlide.value;
        });

        maxUpSlide.onValueChanged.AddListener(delegate {
            changeSlider(maxUpSlide, maxUpValueText);
            GetComponent<CameraController>().maxMoveUp = maxUpSlide.value;
        });

        maxDownSlide.onValueChanged.AddListener(delegate {
            changeSlider(maxDownSlide, maxDownValueText);
            GetComponent<CameraController>().maxMoveDown = maxDownSlide.value;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setLanguage()
    {
        if (language == "ITA")
        {
            sensitivityText.text = sensitivityIta;
            rotationAmountText.text = rotationAmountIta;
            maxZoomInText.text = maxZoomInIta;
            maxZoomOutText.text = maxZoomOutIta;
            secondsVisibilityText.text = secondsVisibilityIta;
            maxDownText.text = maxDownIta;
            maxUpText.text = maxUpIta;
        }
        else if (language == "ENG")
        {
            sensitivityText.text = sensitivityEng;
            rotationAmountText.text = rotationAmountEng;
            maxZoomInText.text = maxZoomInEng;
            maxZoomOutText.text = maxZoomOutEng;
            secondsVisibilityText.text = secondsVisibilityEng;
            maxDownText.text = maxDownEng;
            maxUpText.text = maxUpEng;
        }
    }

    public void changeSlider(Slider s, TextMeshProUGUI display)
    {
        s.SetValueWithoutNotify(s.value);
        display.text = s.value.ToString();
    }

    void getAllStats()
    {
        sensitivitySlide.value = GetComponent<CameraController>().sensitivity;
        sensitivityValueText.text = GetComponent<CameraController>().sensitivity.ToString();

        rotationAmountSlide.value = GetComponent<CameraController>().rotateAmount;
        rotationAmountValueText.text = GetComponent<CameraController>().rotateAmount.ToString();

        maxZoomInSlide.value = GetComponent<CameraController>().maxZoomIn;
        maxZoomInValueText.text = GetComponent<CameraController>().maxZoomIn.ToString();

        maxZoomOutSlide.value = GetComponent<CameraController>().maxZoomOut;
        maxZoomOutValueText.text = GetComponent<CameraController>().maxZoomOut.ToString();

        secondsVisibilitySlide.value = secondsVisibility;
        secondsVisibilityValueText.text = secondsVisibility.ToString();

        maxDownSlide.value = GetComponent<CameraController>().maxMoveDown;
        maxDownValueText.text = GetComponent<CameraController>().maxMoveDown.ToString();

        maxUpSlide.value = GetComponent<CameraController>().maxMoveUp;
        maxUpValueText.text = GetComponent<CameraController>().maxMoveUp.ToString();
    }

    
    void menuSlide()
    {
        RectTransform menuTransform = menuPanel.GetComponent<RectTransform>();

        if (menuTransform.position.x < 333)
        {
            menuOpenCorutine = StartCoroutine(openMenuCounter(menuTransform, menuSpeed, 1));
        }
        else if(menuTransform.position.x > 127)
        {          
            menuOpenCorutine = StartCoroutine(openMenuCounter(menuTransform, menuSpeed, -1));
        }
        
    }

    IEnumerator openMenuCounter(RectTransform rect, float second, float xValue)
    {
        while (true)
        {
            yield return new WaitForSeconds(second/1000);
            rect.anchoredPosition += new Vector2(xValue, 0f);
            if (rect.position.x >= 333)
            {
                StopCoroutine(menuOpenCorutine);
            }
            else if (rect.position.x <= 127)
            {
                StopCoroutine(menuOpenCorutine);
            }
        }
    }
}
