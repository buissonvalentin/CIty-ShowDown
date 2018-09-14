using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    AudioSource audioSource;

    public AudioClip soundHover;
    public AudioClip soundClick;

    // Use this for initialization
    void Start () {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = soundClick;
        audioSource.Play();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(200, 200, 200, 0.6f);
        audioSource.clip = soundHover;
        audioSource.Play();
       
        // add sound
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 1f);
    }
}
