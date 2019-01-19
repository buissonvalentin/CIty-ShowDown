using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Others;

public class SettingsBox : MonoBehaviour
{
    // QUality Settings
    public Text quality;
    public Button upQuality;
    public Button lowerQuality;

    int qualityLevel;
    int maxQuality;
    int minQuality;


    // Musique Settings
    public Slider soundSlider;
    public Text volumeText;
    int musiqueVolume;


    // Noise Settings
    public Slider noiseSlider;
    public Text noiseText;
    int noiseVolume;

    //Move Sensibility Settings
    public Slider moveSensitivitySlider;
    public Text moveSensitivityText;
    float moveSensitivity;


    //Move Sensibility Settings
    public Slider orientationSensitivitySlider;
    public Text orientationSensitivityText;
    float orientationSensitivity;



    // Vsync Settings
    public Toggle vsyncToggle;
    int vsyncValue;

    // Return option
    public Button returnButton;
    public Transform mainMenu;

    // Save option
    public Button apply;

    ///Shortcut option
    //rotation
    public Image rotateShortcutZone;
    public Text rotateShortcutText;
    KeyCode rotateShortcut;
    bool isMouseOverRotateZone;

    //destroy
    public Image destroyShortcutZone;
    public Text destroyShortcutText;
    KeyCode destroyShortcut;
    bool isMouseOverDestroyZone;

    //forward
    public Image forwardShortcutZone;
    public Text forwardShortcutText;
    KeyCode forwardShortcut;
    bool isMouseOverForwardZone;

    //backward
    public Image backwardShortcutZone;
    public Text backwardShortcutText;
    KeyCode backwardShortcut;
    bool isMouseOverBackwardZone;

    //left
    public Image leftShortcutZone;
    public Text leftShortcutText;
    KeyCode leftShortcut;
    bool isMouseOverLeftZone;

    //right
    public Image rightShortcutZone;
    public Text rightShortcutText;
    KeyCode rightShortcut;
    bool isMouseOverRightZone;


    // Start is called before the first frame update
    void Start()
    {
        SetUpValues();
        SetUpQualityManagement();
        SetUpVolumeSettings();
        SetUpVSyncManagement();
        SetUpNoiseSettings();
        SetUpShortcutManagement();

        returnButton.onClick.AddListener(() =>
        {
            mainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });

        apply.onClick.AddListener(() =>
        {
            UpdateSettings();
            SavesManager.SaveSettings();
        });

        

    }

    private void OnEnable()
    {
        SetUpValues();
        SetUpQualityManagement();
        SetUpVolumeSettings();
        SetUpVSyncManagement();
        SetUpNoiseSettings();
        SetUpShortcutManagement();
        SetUpMoveSensitivity();
        SetUpOrientationSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.inputString != "" && Input.inputString != null) // we check if user pressed a key tp update shortcut
        {
            string key = Input.inputString.ToUpper();
            KeyCode keyC = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);

            if (isMouseOverRotateZone)
            {
                rotateShortcut = keyC;
                rotateShortcutText.text = keyC.ToString();
            }
            else if (isMouseOverDestroyZone)
            {
                destroyShortcut = keyC;
                destroyShortcutText.text = keyC.ToString();
            }
            else if (isMouseOverForwardZone)
            {
                forwardShortcut = keyC;
                forwardShortcutText.text = keyC.ToString();
            }
            else if (isMouseOverBackwardZone)
            {
                backwardShortcut = keyC;
                backwardShortcutText.text = keyC.ToString();
            }
            else if (isMouseOverRightZone)
            {
                rightShortcut = keyC;
                rightShortcutText.text = keyC.ToString();
            }
            else if (isMouseOverLeftZone)
            {
                leftShortcut = keyC;
                leftShortcutText.text = keyC.ToString();
            }
        }
    }

    void SetUpQualityManagement()
    {
        string[] qualities = QualitySettings.names;
        minQuality = 0;
        maxQuality = qualities.Length - 1;


        QualitySettings.SetQualityLevel(qualityLevel, true);
        quality.text = qualities[qualityLevel];

        upQuality.onClick.RemoveAllListeners();
        upQuality.onClick.AddListener(() =>
        {
            qualityLevel++;
            if (qualityLevel > maxQuality) qualityLevel = maxQuality;
            quality.text = qualities[qualityLevel];

        });

        lowerQuality.onClick.RemoveAllListeners();
        lowerQuality.onClick.AddListener(() =>
        {
            qualityLevel--;
            if (qualityLevel < minQuality) qualityLevel = minQuality;
            quality.text = qualities[qualityLevel];
        });
    }

    void SetUpVolumeSettings()
    {
        volumeText.text = musiqueVolume.ToString();

        soundSlider.minValue = 0;
        soundSlider.maxValue = 100;
        soundSlider.value = musiqueVolume;
        soundSlider.wholeNumbers = true;

        soundSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.AddListener(delegate 
        {
            volumeText.text = soundSlider.value.ToString();
        });
    }

    void SetUpMoveSensitivity()
    {
        moveSensitivityText.text = moveSensitivity.ToString();

        moveSensitivitySlider.minValue = 0;
        moveSensitivitySlider.maxValue = 10;
        moveSensitivitySlider.value = moveSensitivity;
        moveSensitivitySlider.wholeNumbers = false;

        moveSensitivitySlider.onValueChanged.RemoveAllListeners();
        moveSensitivitySlider.onValueChanged.AddListener(delegate
        {
            moveSensitivityText.text = moveSensitivitySlider.value.ToString();
        });
    }

    void SetUpOrientationSensitivity()
    {
        orientationSensitivityText.text = orientationSensitivity.ToString();

        orientationSensitivitySlider.minValue = 0;
        orientationSensitivitySlider.maxValue = 10;
        orientationSensitivitySlider.value = orientationSensitivity;
        orientationSensitivitySlider.wholeNumbers = false;

        orientationSensitivitySlider.onValueChanged.RemoveAllListeners();
        orientationSensitivitySlider.onValueChanged.AddListener(delegate
        {
            orientationSensitivityText.text = orientationSensitivitySlider.value.ToString();
        });
    }

    void SetUpVSyncManagement()
    {
        //vsyncValue = 0;
        QualitySettings.vSyncCount = vsyncValue;
        vsyncToggle.isOn = vsyncValue == 1;

        vsyncToggle.onValueChanged.RemoveAllListeners();
        vsyncToggle.onValueChanged.AddListener(delegate
        {
            vsyncValue = (vsyncToggle.isOn ? 1 : 0);
            //QualitySettings.vSyncCount = vsyncValue;
        });
    }

    void SetUpNoiseSettings()
    {
        //noiseVolume = 20;
        noiseText.text = noiseVolume.ToString();

        noiseSlider.minValue = 0;
        noiseSlider.maxValue = 100;
        noiseSlider.value = noiseVolume;
        noiseSlider.wholeNumbers = true;

        noiseSlider.onValueChanged.RemoveAllListeners();
        noiseSlider.onValueChanged.AddListener(delegate
        {
            //noiseVolume = (int)noiseSlider.value;
            noiseText.text = noiseSlider.value.ToString();
        });
    }

    void SetUpShortcutManagement()
    {
        isMouseOverDestroyZone = false;
        isMouseOverRotateZone = false;
        isMouseOverBackwardZone = false;
        isMouseOverForwardZone = false;
        isMouseOverRightZone = false;
        isMouseOverLeftZone = false;
        

        rotateShortcutText.text = rotateShortcut.ToString();
        destroyShortcutText.text = destroyShortcut.ToString();
        forwardShortcutText.text = forwardShortcut.ToString();
        backwardShortcutText.text = backwardShortcut.ToString();
        rightShortcutText.text = rightShortcut.ToString();
        leftShortcutText.text = leftShortcut.ToString();

        // rotation
        rotateShortcutZone.GetComponent<OnHover>().OnEnter += () =>
        {
            isMouseOverRotateZone = true;
            Image imgTemp = rotateShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0.1f);
        };

        rotateShortcutZone.GetComponent<OnHover>().OnLeave += () =>
        {
            isMouseOverRotateZone = false;
            Image imgTemp = rotateShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0f);
        };

        //destroy
        destroyShortcutZone.GetComponent<OnHover>().OnEnter += () =>
        {
            isMouseOverDestroyZone = true;
            Image imgTemp = destroyShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0.1f);
        };

        destroyShortcutZone.GetComponent<OnHover>().OnLeave += () =>
        {
            isMouseOverDestroyZone = false;
            Image imgTemp = destroyShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0f);
        };

        //forward
        forwardShortcutZone.GetComponent<OnHover>().OnEnter += () =>
        {
            isMouseOverForwardZone = true;
            Image imgTemp = forwardShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0.1f);
        };

        forwardShortcutZone.GetComponent<OnHover>().OnLeave += () =>
        {
            isMouseOverForwardZone = false;
            Image imgTemp = forwardShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0f);
        };

        //backward
        backwardShortcutZone.GetComponent<OnHover>().OnEnter += () =>
        {
            isMouseOverBackwardZone = true;
            Image imgTemp = backwardShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0.1f);
        };

        backwardShortcutZone.GetComponent<OnHover>().OnLeave += () =>
        {
            isMouseOverBackwardZone = false;
            Image imgTemp = backwardShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0f);
        };

        //right
        rightShortcutZone.GetComponent<OnHover>().OnEnter += () =>
        {
            isMouseOverRightZone = true;
            Image imgTemp = rightShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0.1f);
        };

        rightShortcutZone.GetComponent<OnHover>().OnLeave += () =>
        {
            isMouseOverRightZone = false;
            Image imgTemp = rightShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0f);
        };

        //left
        leftShortcutZone.GetComponent<OnHover>().OnEnter += () =>
        {
            isMouseOverLeftZone = true;
            Image imgTemp = leftShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0.1f);
        };

        leftShortcutZone.GetComponent<OnHover>().OnLeave += () =>
        {
            isMouseOverLeftZone = false;
            Image imgTemp = leftShortcutZone.GetComponent<Image>();
            imgTemp.color = new Color(imgTemp.color.r, imgTemp.color.g, imgTemp.color.b, 0f);
        };


    }

    void UpdateSettings()
    {
        Settings set = FindObjectOfType<Manager>().Settings;

        set.quality = qualityLevel;
        set.vsync = (vsyncToggle.isOn ? 1 : 0);
        set.musiqueVolume = (int)soundSlider.value;
        set.noiseVolume = (int)noiseSlider.value;
        set.shortcutKeyDelete = destroyShortcut;
        set.shortcutKeyRotate = rotateShortcut;
        set.shortcutKeyForward = forwardShortcut;
        set.shortcutKeyBackward = backwardShortcut;
        set.shortcutKeyRight = rightShortcut;
        set.shortcutKeyLeft = leftShortcut;
        set.moveSensibility = moveSensitivitySlider.value;
        set.orientationSensibility = orientationSensitivitySlider.value;


        if (FindObjectOfType<MouseRayCast>() != null)
            FindObjectOfType<MouseRayCast>().UpdateSettings();
    }

    void SetUpValues()
    {
        Settings set = SavesManager.LoadSettings();
        qualityLevel = set.quality;
        vsyncValue = set.vsync;
        musiqueVolume = set.musiqueVolume;
        noiseVolume = set.noiseVolume;
        destroyShortcut = set.shortcutKeyDelete;
        rotateShortcut = set.shortcutKeyRotate;
        forwardShortcut = set.shortcutKeyForward;
        backwardShortcut = set.shortcutKeyBackward;
        rightShortcut = set.shortcutKeyRight;
        leftShortcut = set.shortcutKeyLeft;
        moveSensitivity = set.moveSensibility;
        orientationSensitivity = set.orientationSensibility;

    }



}
