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
    public Text volume;
    int musiqueVolume;


    // Noise Settings
    public Slider noiseSlider;
    public Text noise;
    int noiseVolume;


    // Vsync Settings
    public Toggle vsyncToggle;
    int vsyncValue;

    // Return option
    public Button returnButton;
    public Transform mainMenu;

    // Save option
    public Button apply;

    //Shortcut option
    public Image rotateShortcutZone;
    public Text rotateShortcutText;
    KeyCode rotateShortcut;
    bool isMouseOverRotateZone;

    public Image destroyShortcutZone;
    public Text destroyShortcutText;
    KeyCode destroyShortcut;
    bool isMouseOverDestroyZone;


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
       // musiqueVolume = 20;
        volume.text = musiqueVolume.ToString();

        soundSlider.minValue = 0;
        soundSlider.maxValue = 100;
        soundSlider.value = musiqueVolume;
        soundSlider.wholeNumbers = true;

        soundSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.AddListener(delegate 
        {
            //musiqueVolume = (int)soundSlider.value;
            volume.text = soundSlider.value.ToString();
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
        noise.text = noiseVolume.ToString();

        noiseSlider.minValue = 0;
        noiseSlider.maxValue = 100;
        noiseSlider.value = noiseVolume;
        noiseSlider.wholeNumbers = true;

        noiseSlider.onValueChanged.RemoveAllListeners();
        noiseSlider.onValueChanged.AddListener(delegate
        {
            //noiseVolume = (int)noiseSlider.value;
            noise.text = noiseSlider.value.ToString();
        });
    }

    void SetUpShortcutManagement()
    {
        isMouseOverDestroyZone = false;
        isMouseOverRotateZone = false;

        //rotateShortcut = KeyCode.G;
        //destroyShortcut = KeyCode.X;

        rotateShortcutText.text = rotateShortcut.ToString();
        destroyShortcutText.text = destroyShortcut.ToString();

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

    }



}
