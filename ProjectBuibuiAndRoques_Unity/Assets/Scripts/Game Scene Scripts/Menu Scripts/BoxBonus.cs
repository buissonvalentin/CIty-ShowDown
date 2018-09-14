using ProjectRoquesAndBuiBui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxBonus : MonoBehaviour {

    public Transform bonusModel;

    // Use this for initialization
    void Start()
    {
        // Creation des box lois


        Ville v = FindObjectOfType<GameManager>().ville;

        foreach (Bonus b in v.BonusExistant)
        {
            Transform temp = Instantiate(bonusModel, transform.Find("Container"));
            temp.Find("Title").GetComponent<Text>().text = b.Nom;
            temp.Find("Description").GetComponent<Text>().text = b.Description;
            temp.gameObject.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {  
        transform.Find("Points").GetComponent<Text>().text = "Points : " + FindObjectOfType<GameManager>().ville.QuotaPointBonus.ToString();
    }
}
