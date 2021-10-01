using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Sensibiità rotazione")]
    [Tooltip("La sensibilità della rotazione assieme alla quantità di rotazione serve a far girare la telecamera")]
    [Range(0.5f, 100f)]
    public float sensitivity;

    [Header("Quantità di rotazione")]
    [Tooltip("La sensibilità della rotazione assieme alla quantità di rotazione serve a far girare la telecamera")]
    [Range(0.5f, 100f)]
    public float rotateAmount;

    [Space]

    [Header("Massimo zoom in")]
    [Tooltip("Lo zoom funziona al contrario, più è basso il valore più può zoommare in avanti")]
    [Range(20f, 69f)]
    public int maxZoomIn;
    [Header("Massimo zoom out")]
    [Tooltip("Lo zoom funziona al contrario, più è alto il valore più può zoommare indietro")]
    [Range(69f, 100f)]
    public int maxZoomOut;

    [Space]

    [Header("Visuale in terza persona")]
    [Tooltip("indica se la visuale è in terza persona")]
    public bool isThirdPerson = false;

    [Header("Massimo movimento su")]
    [Tooltip("il massimo movimento che la telecamera può fare verso l'alto quando siamo in terza persona")]
    [Range(1f, 10f)]
    public float maxMoveUp;
    [Header("Massimo movimento giù")]
    [Tooltip("il massimo movimento che la telecamera può fare verso il basso quando siamo in terza persona")]
    [Range(0f, 10f)]
    public float maxMoveDown;


    private float zoom;
    // Start is called before the first frame update
    void Start()
    {
        zoom = Camera.main.fieldOfView;
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void FixedUpdate()
    {    
        if(Input.GetKey(KeyCode.Mouse1))
        RotateCamera();
        
        rotateUsingButtons();

        zoomCamera();

        if (isThirdPerson)
        {
            rotateInThird();
        }
    }

    

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        destination.x -= Input.GetAxis("Mouse Y") * sensitivity * rotateAmount;
        destination.y += Input.GetAxis("Mouse X") * sensitivity * rotateAmount;
    
        
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * sensitivity);
        }
        
    }

    void rotateUsingButtons()
    {
        Vector3 origin = this.transform.eulerAngles;
        Vector3 destination = origin;
        Vector3 originCamera = Camera.main.transform.eulerAngles;
        Vector3 destinationCamera = originCamera;
        
        if (Input.GetKey(KeyCode.A))
        {
            destination.y -= sensitivity * rotateAmount;
        }
        if (Input.GetKey(KeyCode.D))
        {
            destination.y += sensitivity * rotateAmount;
        }
        if (Input.GetKey(KeyCode.W))
        {
            destinationCamera.x -= sensitivity * rotateAmount;
        }
        if (Input.GetKey(KeyCode.S))
        {
            destinationCamera.x += sensitivity * rotateAmount;
        }
        if (destination != origin)
        {
            this.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * sensitivity);
        }
        if (destinationCamera != originCamera)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(originCamera, destinationCamera, Time.deltaTime * sensitivity);
        }
    }

    void zoomCamera()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        if (zoom <= maxZoomIn)
        {
            zoom = maxZoomIn;
        }
        if (zoom >= maxZoomOut)
        {
            zoom = maxZoomOut;
        }
        Camera.main.fieldOfView = zoom;
    }

    void rotateInThird()
    {

        Vector3 originCameraUp = Camera.main.transform.position;
        Vector3 destinationCameraUp = Camera.main.transform.eulerAngles;

        if (Input.GetKey(KeyCode.Q))
            transform.RotateAround(GameObject.FindGameObjectWithTag("Machine").transform.position, Vector3.up, Time.deltaTime * rotateAmount);
        if (Input.GetKey(KeyCode.E))
            transform.RotateAround(GameObject.FindGameObjectWithTag("Machine").transform.position, Vector3.up, Time.deltaTime * rotateAmount * -1f);

        if (Input.GetKey(KeyCode.Space))
        {
            destinationCameraUp.y += sensitivity * rotateAmount;
            Camera.main.transform.position = Vector3.MoveTowards(originCameraUp, destinationCameraUp, Time.deltaTime * sensitivity / 10);

            if (Camera.main.transform.localPosition.y >= maxMoveUp)
            {
                Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, maxMoveUp, Camera.main.transform.localPosition.z);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            destinationCameraUp.y -= sensitivity * rotateAmount;
            Camera.main.transform.position = Vector3.MoveTowards(originCameraUp, destinationCameraUp, Time.deltaTime * sensitivity / 10);            
            if (Camera.main.transform.localPosition.y <= maxMoveDown/10)
            {
                Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, maxMoveDown/10, Camera.main.transform.localPosition.z);
            }
        }
    }

}
