using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ProjectRoquesAndBuiBui;

public class ImageSelectionAmenagementHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoBox;
    public GameObject filtre;
    GameObject tempFiltre;

    void Start()
    {
    }

    private void Update()
    {
         
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        infoBox.SetActive(true);

        infoBox.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        Transform ctn = infoBox.transform.Find("filtre").Find("Container");
        Amenagement a = GetComponent<SelectionneAmenagementBouton>().amenagement.GetComponent<AmenagementPrefab>().Amenagement;
        ctn.Find("Nom").GetComponent<Text>().text = a.Nom;
        ctn.Find("Prix").GetComponent<Text>().text = a.Prix.ToString();
        ctn.Find("Taille").GetComponent<Text>().text = a.Taille.ToString();
        ctn.Find("Info").GetComponent<Text>().text = a.AffichageAchat();
        /*
        ctn.Find("Nom").GetComponent<Text>().text = a.Nom;
        ctn.Find("Prix").GetComponent<Text>().text = a.Prix.ToString();
        ctn.Find("Taille").GetComponent<Text>().text = a.Taille.ToString();
        ctn.Find("Info").GetComponent<Text>().text = "info supp";
        */
        (infoBox.transform as RectTransform).position = new Vector2((transform as RectTransform).position.x, (infoBox.transform as RectTransform).position.y);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        infoBox.SetActive(false);
    }
}
