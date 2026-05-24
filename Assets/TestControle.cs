using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestControle : MonoBehaviour
{
    public Dictionary<string, string> Dico_NomCarte_Attache;
    int cmpt = 0;
    private Player joueur;
    public static TestControle instance;

    public void Awake()
    {
        instance = this;
    }
    public void ActivateBoutton()
    {
        for (int i = 0; i < GameLogic.instance.Liste_Joueurs.Count; i++)
        {
            GameObject continent = GameObject.Find(GameLogic.instance.liste_continent_active[i].name);
            Debug.Log(continent.name);
            for (int j = 1; j < continent.transform.childCount; j++)
            {
                GameObject enfant_continent = continent.transform.GetChild(j).gameObject;
                Debug.Log(enfant_continent.name);
                enfant_continent.GetComponent<Button>().enabled = true; //Active le bouton associé
            }
        }
    }
    public void DesactivateBoutton()
    {
        for (int i = 0; i < GameLogic.instance.Liste_Joueurs.Count; i++)
        {
            GameObject continent = GameObject.Find(GameLogic.instance.liste_continent_active[i].name);
            Debug.Log(continent.name);
            for (int j = 1; j < continent.transform.childCount; j++)
            {
                GameObject enfant_continent = continent.transform.GetChild(j).gameObject;
                Debug.Log(enfant_continent.name);
                enfant_continent.GetComponent<Button>().enabled = false; //Desactive le bouton associé
            }
        }
    }
    public GameObject PanelControle;
    public GameObject bouton_clique;
    
    public void Test_controle(string nom_carte)
    {
        joueur = TurnHandler.instance.PlayerActuel;
        bouton_clique = EventSystem.current.currentSelectedGameObject;
        nom_carte = bouton_clique.name;

        Debug.Log(nom_carte);
        
        GameObject carte_plante = GameObject.Find(nom_carte);

        GameObject enfant_plante = carte_plante.transform.GetChild(0).gameObject;
        TextMeshProUGUI compteur = enfant_plante.GetComponent<TextMeshProUGUI>();
        string nombre_affiche = compteur.text;
        string nom_carte_detache = GameLogic.instance.Dico_NomCarte_Attache.FirstOrDefault(x => x.Value == nom_carte).Key;
        Debug.Log(nom_carte_detache);
        if (int.Parse(nombre_affiche) > 0)
        {
            if (GameLogic.instance.Is_invasif(nom_carte_detache, joueur.continent.defausse))
            {
                MenuOptions.instance.ResearchCanvasSelonTag("TxtControleInvasif").gameObject.SetActive(true);
                Debug.Log("Invasif");
                carte_plante.transform.GetComponent<SpeciesStackScript>().SupprimerCarteControle(1, nom_carte_detache);
            }
            else
            {
                Debug.Log("Else");
                OpenClosePanel.instance.openPanelControl(PanelControle, nom_carte);
            }
        }
    }
    public void Test_boutton()
    {
        joueur = TurnHandler.instance.PlayerActuel;
        //int nombre = joueur.continent.defausse.Count;
        ////string bla = "";
        ////for (int j = 0; j < joueur.continent.defausse.Count; j++)
        ////{
        ////    bla += joueur.continent.defausse[j].nom + " ; ";
        ////}
        //Debug.Log(nombre);
    }
}
