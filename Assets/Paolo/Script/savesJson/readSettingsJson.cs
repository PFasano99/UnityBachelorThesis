using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class readSettingsJson : MonoBehaviour
{
    [Header("File di salvatggio delle impostazioni")]
    public TextAsset SettingsJson;

    [Header("Struttura del contenuto del file di salvatggio delle impostazioni")]
    public settingJsonArray settingsJsonArray;


    public settingJsonArray loadSettingsFile()
    {
        settingsJsonArray = JsonUtility.FromJson<settingJsonArray>(SettingsJson.text);
        return settingsJsonArray;
    }

    public void loadSettingSave()
    {
        settingJsonArray settings = loadSettingsFile();
        Camera.main.GetComponent<CameraController>().sensitivity = settings.Save[0].sensitivity;
        Camera.main.GetComponent<CameraController>().rotateAmount = settings.Save[0].rotationAmmount;
        Camera.main.GetComponent<CameraController>().maxZoomIn = (int) settings.Save[0].zoomIn;
        Camera.main.GetComponent<CameraController>().maxZoomOut = (int)settings.Save[0].zoomOut;
        Camera.main.GetComponent<CameraController>().maxMoveUp = settings.Save[0].maxUp;
        Camera.main.GetComponent<CameraController>().maxMoveDown = settings.Save[0].maxDown;
        Camera.main.GetComponent<MenuHandler>().secondsVisibility = settings.Save[0].decalTimer;
        Camera.main.GetComponent<MenuHandler>().language = settings.Save[0].language;

    }

    public void saveSettings()
    {
        string destination = Application.dataPath + "/Paolo/Script/savesJson/settingJson.json";
        settingJsonArray settings = loadSettingsFile();

        settings.Save[0].sensitivity = Camera.main.GetComponent<CameraController>().sensitivity;
        settings.Save[0].rotationAmmount = Camera.main.GetComponent<CameraController>().rotateAmount;
        settings.Save[0].zoomIn = Camera.main.GetComponent<CameraController>().maxZoomIn;
        settings.Save[0].zoomOut = Camera.main.GetComponent<CameraController>().maxZoomOut;
        settings.Save[0].maxUp = Camera.main.GetComponent<CameraController>().maxMoveUp;
        settings.Save[0].maxDown = Camera.main.GetComponent<CameraController>().maxMoveDown;
        settings.Save[0].decalTimer = Camera.main.GetComponent<MenuHandler>().secondsVisibility;
        settings.Save[0].language = Camera.main.GetComponent<MenuHandler>().language;

        string update = JsonUtility.ToJson(settings, true);
        File.WriteAllText(destination, update);
    }

    private void OnApplicationQuit()
    {
        saveSettings();
        AssetDatabase.Refresh();
    }

    private void Awake()
    {
        loadSettingSave();
    }

   

    
}
