using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ProjectRoquesAndBuiBui;

public class Menu : MonoBehaviour {


    Transform amenagementSelectionMenu;
    Transform infoBoxAmenagement;
    Transform infoBoxAmenagement1Para;
    Transform infoBoxAmenagementMultiPara;
    Transform infoBoxVille;
    Transform newsBox;
    Transform endBox;

    //Barre de navigation
    Transform argent;
    Transform populationNav;
    Transform revenuNav;

    //InfoBoxVille
    public Text impotAisee;
    public Text impotMoyenne;
    public Text impotOuvriere;
    public Text impotEntreprise;
    public Text bonheur;
    public Text populationText;
    public Text chomage;
    public Text eau;
    public Text electricite;
    public Text logements;
    public Text revenuText;
    public Text nourriture;
    public Text nbrBatiments;

    Transform currentMenuSelection;
    Transform lastBtn;

    public Transform news;



	// Use this for initialization
	void Start () {
        amenagementSelectionMenu = transform.Find("AmenagementSelectionMenu");
        infoBoxAmenagement = transform.Find("InfoBoxAmenagement");
        infoBoxVille = transform.Find("InfoBoxVille");
        newsBox = transform.Find("NewsBox");
        infoBoxAmenagement1Para = transform.Find("InfoBoxAmenagement1Para");
        infoBoxAmenagementMultiPara = transform.Find("InfoBoxAmenagementMultiPara");
        endBox = transform.Find("EndBox");

        argent = GameObject.Find("Portefeuille").transform;
        populationNav = GameObject.Find("Population").transform;
        revenuNav = GameObject.Find("Revenu").transform;
    }
	
    /// <summary>
    /// Active la popup qui donne les informations sur l'amenagement choisi
    /// </summary>
    /// <param name="t"></param>
    public void AfficheInfoAmenagement(Transform t) 
    {
        Text infoText;
        RectTransform rectTransform;
        Amenagement a = t.GetComponent<AmenagementPrefab>().Amenagement;
        CacherInfoAmenagement();
        //SignalBatimentDeconnecte

        if (a is Commercant || a is CompagnieEau || a is CompagnieElectricite || a is Primaire)
        {
            infoText = infoBoxAmenagement1Para.Find("Container").Find("InfoText").GetComponent<Text>();
            rectTransform = (RectTransform)infoBoxAmenagement1Para.transform;
            
            infoBoxAmenagement1Para.Find("SignalBatimentDeconnecte").gameObject.SetActive(!(a as Batiment).EstConnecte);
            
            // Modification des informations de la pop up en fonction du batiment choisi
            #region Creation pop up gestion batiment
           
            if (a is Commercant)
            {
                infoBoxAmenagement1Para.Find("Container").Find("TextPara").GetComponent<Text>().text = "Prix de vente : " + (a as Commercant).Prix;
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMin").GetComponent<Text>().text = "0";
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMax").GetComponent<Text>().text = "100";

                Slider slider = infoBoxAmenagement1Para.Find("Container").Find("Container").Find("Slider").GetComponent<Slider>();
                slider.onValueChanged.RemoveAllListeners();
                slider.minValue = 0;
                slider.maxValue = 100;
                slider.wholeNumbers = true;

                Ville v = GameObject.FindObjectOfType<GameManager>().ville;
                Button btn = infoBoxAmenagement1Para.Find("Container").Find("Button").GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (Amenagement amen in v.Amenagements)
                    {
                        if (amen is Commercant && amen.Niveau == a.Niveau)
                        {
                            (amen as Commercant).PrixVente = slider.value;
                        }
                    }
                });

                UnityAction<float> callback = new UnityAction<float>((float value) =>
                {
                    (a as Commercant).PrixVente = slider.value;
                });

                slider.onValueChanged.AddListener(callback);
                slider.value = (a as Commercant).PrixVente;
            } 
            if (a is CompagnieEau)
            {
                infoBoxAmenagement1Para.Find("Container").Find("TextPara").GetComponent<Text>().text = "Production d'eau : " + (a as CompagnieEau).EauProduite;
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMin").GetComponent<Text>().text = (a as CompagnieEau).ProductionMin.ToString();
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMax").GetComponent<Text>().text = (a as CompagnieEau).ProductionMax.ToString();

                Slider slider = infoBoxAmenagement1Para.Find("Container").Find("Container").Find("Slider").GetComponent<Slider>();
                slider.onValueChanged.RemoveAllListeners();
                slider.minValue = (a as CompagnieEau).ProductionMin;
                slider.maxValue = (a as CompagnieEau).ProductionMax;
                slider.wholeNumbers = true;

                Ville v = GameObject.FindObjectOfType<GameManager>().ville;
                Button btn = infoBoxAmenagement1Para.Find("Container").Find("Button").GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (Amenagement amen in v.Amenagements)
                    {
                        if (amen is CompagnieEau && amen.Niveau == a.Niveau)
                        {
                            (amen as CompagnieEau).EauProduite = (int)slider.value;
                        }
                    }
                });

                UnityAction<float> callback = new UnityAction<float>((float value) =>
                {
                    (a as CompagnieEau).EauProduite = (int)value;
                });

                slider.onValueChanged.AddListener(callback);
                slider.value = (a as CompagnieEau).EauProduite;
            }
            if (a is CompagnieElectricite)
            {
                infoBoxAmenagement1Para.Find("Container").Find("TextPara").GetComponent<Text>().text = "Energie produite : " + (a as CompagnieElectricite).EnergieProduite;
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMin").GetComponent<Text>().text = (a as CompagnieElectricite).ProductionMax.ToString();
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMax").GetComponent<Text>().text = (a as CompagnieElectricite).ProductionMax.ToString();

                Slider slider = infoBoxAmenagement1Para.Find("Container").Find("Container").Find("Slider").GetComponent<Slider>();
                slider.onValueChanged.RemoveAllListeners();
                slider.minValue = (a as CompagnieElectricite).ProductionMax;
                slider.maxValue = (a as CompagnieElectricite).ProductionMax;
                slider.wholeNumbers = true;

                Ville v = GameObject.FindObjectOfType<GameManager>().ville;
                Button btn = infoBoxAmenagement1Para.Find("Container").Find("Button").GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (Amenagement amen in v.Amenagements)
                    {
                        if (amen is CompagnieElectricite && amen.Niveau == a.Niveau)
                        {
                            (amen as CompagnieElectricite).EnergieProduite = (int)slider.value;
                        }
                    }
                });

                UnityAction<float> callback = new UnityAction<float>((float value) =>
                {
                    (a as CompagnieElectricite).EnergieProduite = (int)value;
                });

                slider.onValueChanged.AddListener(callback);
                slider.value = (a as CompagnieElectricite).EnergieProduite;
            }
            if (a is Primaire)
            {
                infoBoxAmenagement1Para.Find("Container").Find("TextPara").GetComponent<Text>().text = "Produvtion nourriture : " + (a as Primaire).NourritureProduite;
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMin").GetComponent<Text>().text = (a as Primaire).ProductionMin.ToString();
                infoBoxAmenagement1Para.Find("Container").Find("Container").Find("ValMax").GetComponent<Text>().text = (a as Primaire).ProductionMax.ToString();

                Slider slider = infoBoxAmenagement1Para.Find("Container").Find("Container").Find("Slider").GetComponent<Slider>();
                slider.onValueChanged.RemoveAllListeners();
                slider.minValue = (a as Primaire).ProductionMin;
                slider.maxValue = (a as Primaire).ProductionMax;
                slider.wholeNumbers = true;

                Ville v = GameObject.FindObjectOfType<GameManager>().ville;
                Button btn = infoBoxAmenagement1Para.Find("Container").Find("Button").GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (Amenagement amen in v.Amenagements)
                    {
                        if (amen is Primaire && amen.Niveau == a.Niveau)
                        {
                            (amen as Primaire).NourritureProduite = (int)slider.value;
                        }
                    }
                });

                UnityAction<float> callback = new UnityAction<float>((float value) =>
                {
                    (a as Primaire).NourritureProduite = (int)value;
                });

                slider.onValueChanged.AddListener(callback);
                slider.value = (a as Primaire).NourritureProduite;
            }
            #endregion

            infoBoxAmenagement1Para.gameObject.SetActive(true);
        }
        else if (a is CompagnieTransport)
        {
            infoText = infoBoxAmenagementMultiPara.Find("InfoText").GetComponent<Text>();
            rectTransform = (RectTransform)infoBoxAmenagement1Para.transform;

            //para 1
            infoBoxAmenagementMultiPara.Find("Container").Find("TextPara").GetComponent<Text>().text = "Nombre de Transport : " + (a as CompagnieTransport).NombreTransport;
            infoBoxAmenagementMultiPara.Find("Container").Find("Container").Find("ValMin").GetComponent<Text>().text = "0";
            infoBoxAmenagementMultiPara.Find("Container").Find("Container").Find("ValMax").GetComponent<Text>().text = "100";

            Slider slider = infoBoxAmenagementMultiPara.Find("Container").Find("Container").Find("Slider").GetComponent<Slider>();
            slider.onValueChanged.RemoveAllListeners();
            slider.minValue = 0;
            slider.maxValue = 100;
            slider.wholeNumbers = true;

            Ville v = GameObject.FindObjectOfType<GameManager>().ville;
           

            UnityAction<float> callback = new UnityAction<float>((float value) =>
            {
                (a as CompagnieTransport).NombreTransport = (int)value;
            });

            slider.onValueChanged.AddListener(callback);
            slider.value = (a as CompagnieTransport).NombreTransport;

            //para 2
            infoBoxAmenagementMultiPara.Find("Container").Find("TextPara2").GetComponent<Text>().text = "Prix Tiquet : " + (a as CompagnieTransport).PrixTransport;
            infoBoxAmenagementMultiPara.Find("Container").Find("Container2").Find("ValMin").GetComponent<Text>().text = "0";
            infoBoxAmenagementMultiPara.Find("Container").Find("Container2").Find("ValMax").GetComponent<Text>().text = "10";

            Slider slider2 = infoBoxAmenagementMultiPara.Find("Container").Find("Container2").Find("Slider").GetComponent<Slider>();
            slider2.onValueChanged.RemoveAllListeners();
            slider2.minValue = 0;
            slider2.maxValue = 10;
            slider2.wholeNumbers = false;
            
            
            UnityAction<float> callback2 = new UnityAction<float>((float value) =>
            {
                (a as CompagnieTransport).PrixTransport = value;
            });

            slider2.onValueChanged.AddListener(callback2);
            slider2.value = (a as CompagnieTransport).PrixTransport;

            //para 3
            infoBoxAmenagementMultiPara.Find("TextPara3").Find("Container").GetComponent<Text>().text = "Capacite Transport : " + (a as CompagnieTransport).CapaciteTransport;
            infoBoxAmenagementMultiPara.Find("Container3").Find("Container").Find("ValMin").GetComponent<Text>().text = "0";
            infoBoxAmenagementMultiPara.Find("Container3").Find("Container").Find("ValMax").GetComponent<Text>().text = "40";

            Slider slider3 = infoBoxAmenagementMultiPara.Find("Container").Find("Container3").Find("Slider").GetComponent<Slider>();
            slider3.onValueChanged.RemoveAllListeners();
            slider3.minValue = 0;
            slider3.maxValue = 40;
            slider3.wholeNumbers = true;
            
           

            UnityAction<float> callback3 = new UnityAction<float>((float value) =>
            {
                (a as CompagnieTransport).CapaciteTransport = (int)value;
            });

            slider3.onValueChanged.AddListener(callback3);
            slider3.value = (a as CompagnieTransport).CapaciteTransport;


            Button btn = infoBoxAmenagement1Para.Find("Container").Find("Button").GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                foreach (Amenagement amen in v.Amenagements)
                {
                    if (amen is CompagnieTransport && amen.Niveau == a.Niveau)
                    {
                        (amen as CompagnieTransport).NombreTransport = (int)slider.value;
                        (amen as CompagnieTransport).PrixTransport = slider2.value;
                        (amen as CompagnieTransport).CapaciteTransport = (int)slider3.value;

                    }
                }
            });
            infoBoxAmenagementMultiPara.gameObject.SetActive(true);
        }
        else
        {
            infoText = infoBoxAmenagement.Find("InfoText").GetComponent<Text>();
            rectTransform = (RectTransform)infoBoxAmenagement.transform;
            infoBoxAmenagement.gameObject.SetActive(true);
            if(a is Batiment)
                infoBoxAmenagement.Find("SignalBatimentDeconnecte").gameObject.SetActive(!(a as Batiment).EstConnecte);
        }
        
        infoText.text = a.ToString(); 
        Vector2 viewportPoint = Camera.main.WorldToScreenPoint(t.position);  
        rectTransform.position = viewportPoint;
    }

    /// <summary>
    /// Cache la pop up d'information
    /// </summary>
    public void CacherInfoAmenagement()
    {
        infoBoxAmenagement.gameObject.SetActive(false);
        infoBoxAmenagement1Para.gameObject.SetActive(false);
        infoBoxAmenagementMultiPara.gameObject.SetActive(false);
    }

    /// <summary>
    /// Met a jour toutes les valeurs de statistiques de la ville
    /// </summary>
    /// <param name="v"></param>
    public void AfficheInfoVille(Ville v)
    {
        impotAisee.GetComponent<Text>().text = v.CoefImpotAisee.ToString() + "%";
        impotMoyenne.GetComponent<Text>().text = v.CoefImpotMoyenne.ToString() + "%";
        impotOuvriere.GetComponent<Text>().text = v.CoefImpotOuvriere.ToString() + "%";
        impotEntreprise.GetComponent<Text>().text = v.CoefImpotEntreprise.ToString() + "%";

        bonheur.GetComponent<Text>().text = v.Bonheur.ToString() + "%";
        populationText.GetComponent<Text>().text = v.Population.ToString();
        chomage.GetComponent<Text>().text = v.PersonneChomage.ToString();
        eau.GetComponent<Text>().text = v.EauProduite.ToString();
        electricite.GetComponent<Text>().text = v.EnergieProduite.ToString();
        logements.GetComponent<Text>().text = v.CapaciteLogement.ToString();
        revenuText.GetComponent<Text>().text = v.Revenu.ToString();
        nourriture.GetComponent<Text>().text = v.NourritureProduite.ToString();
        nbrBatiments.GetComponent<Text>().text = v.Amenagements.Count.ToString();
    }

    /// <summary>
    /// Met a jour les informations de la barre de navigation
    /// </summary>
    /// <param name="v"></param>
    public void AfficheInfoPrincipales(Ville v)
    {
        argent.Find("Text").GetComponent<Text>().text = v.Argent.ToString();
        populationNav.Find("Text").GetComponent<Text>().text = v.Population.ToString();
        revenuNav.Find("Text").GetComponent<Text>().text = v.Revenu.ToString();
    }

    /// <summary>
    /// Affiche le menu de selection passe en parametre et cache celui ouvert précédement
    /// </summary>
    /// <param name="menuSelection"></param>
    public void AfficheMenuSelection(Transform menuSelection, Transform btn)
    {
        if(currentMenuSelection != null)
        {
            currentMenuSelection.gameObject.SetActive(false);
            lastBtn.GetComponent<AfficheMenuSelectionButton>().Close();
        }
        lastBtn = btn;
        menuSelection.gameObject.SetActive(true);
        currentMenuSelection = menuSelection;
    }

    public void FermeMenuSelection()
    {
        if (currentMenuSelection != null)
        {
            currentMenuSelection.gameObject.SetActive(false);
            lastBtn.GetComponent<AfficheMenuSelectionButton>().Close();
        }
    }

    /// <summary>
    /// Ajoute une news dans la news box
    /// </summary>
    /// <param name="texte"></param>
    public void AjouterNews(string texte)
    {
        Transform temp = Instantiate(news, newsBox.Find("Container"));
    }

    public void AfficheBoxFinPartie(Ville v)
    {
        endBox.gameObject.SetActive(true);
        endBox.Find("Container").Find("Pop").Find("Valeur").GetComponent<Text>().text = v.Population.ToString();
        endBox.Find("Container").Find("Arg").Find("Valeur").GetComponent<Text>().text = v.Argent.ToString();
        endBox.Find("Container").Find("Rev").Find("Valeur").GetComponent<Text>().text = v.Revenu.ToString();
        endBox.Find("Container").Find("Def").Find("Valeur").GetComponent<Text>().text = "0";
        endBox.Find("Container").Find("Bon").Find("Valeur").GetComponent<Text>().text = v.Bonheur.ToString();
        endBox.Find("Container2").Find("Score").GetComponent<Text>().text = CalculScoreFinale(v).ToString();

    }

    float CalculScoreFinale(Ville ville)
    {
        return  0.2f * (float)ville.Population + 0.1f * (float)ville.Argent + 0.3f * (float)ville.Revenu + 0.2f * (float)ville.Bonheur; // 0.2 * défi gnagné
    }
}
