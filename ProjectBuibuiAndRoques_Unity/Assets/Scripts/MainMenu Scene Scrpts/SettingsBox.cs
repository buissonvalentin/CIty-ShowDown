using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBox : MonoBehaviour
{
    // QUality Settings
    public Text quality;
    public Button upQuality;
    public Button lowerQuality;

    int qualityLevel;
    int maxQuality;
    int minQuality;


    // Sound Settings
    public Slider soundSlider;
    public Text volume;
    int volumeValue;

    // Vsync Settings
    public Toggle vsyncToggle;
    int vsyncValue;

    // Return option
    public Button returnButton;
    public Transform mainMenu;

    // Save option
    public Button save;


    // Start is called before the first frame update
    void Start()
    {
        SetUpQualityManagement();
        SetUpVolumeSettings();
        SetUpVSync();

        returnButton.onClick.AddListener(() =>
        {
            mainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });

        save.onClick.AddListener(() =>
        {

        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpQualityManagement()
    {
        string[] qualities = QualitySettings.names;
        minQuality = 0;
        maxQuality = qualities.Length - 1;
        qualityLevel = qualities.Length / 2;


        QualitySettings.SetQualityLevel(qualityLevel, true);
        quality.text = qualities[qualityLevel];

        upQuality.onClick.AddListener(() =>
        {
            qualityLevel++;
            if (qualityLevel > maxQuality) qualityLevel = maxQuality;
            QualitySettings.SetQualityLevel(qualityLevel, true);
            quality.text = qualities[qualityLevel];

        });

        lowerQuality.onClick.AddListener(() =>
        {
            qualityLevel--;
            if (qualityLevel < minQuality) qualityLevel = minQuality;
            QualitySettings.SetQualityLevel(qualityLevel, true);
            quality.text = qualities[qualityLevel];
        });
    }

    void SetUpVolumeSettings()
    {
        volumeValue = 20;
        volume.text = volumeValue.ToString();

        soundSlider.minValue = 0;
        soundSlider.maxValue = 100;
        soundSlider.value = volumeValue;
        soundSlider.wholeNumbers = true;

        soundSlider.onValueChanged.AddListener(delegate 
        {
            volumeValue = (int)soundSlider.value;
            volume.text = volumeValue.ToString();
        });
    }

    public int GetVolumeValue()
    {
        return volumeValue;
    }

    void SetUpVSync()
    {
        vsyncValue = 0;
        QualitySettings.vSyncCount = vsyncValue;
        vsyncToggle.isOn = false;

        vsyncToggle.onValueChanged.AddListener(delegate
        {
            vsyncValue = (vsyncToggle.isOn ? 1 : 0);
            QualitySettings.vSyncCount = vsyncValue;
        });
    }



}
