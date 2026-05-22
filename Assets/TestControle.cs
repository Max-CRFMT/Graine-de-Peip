using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestControle : MonoBehaviour
{
    public Dictionary<string, string> Dico_NomCarte_Attache;
    int cmpt = 0;
    private Player joueur;
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
    public void Test_controle(string nom_carte)//,int nombre_a_supprimer)
    {
        joueur = TurnHandler.instance.PlayerActuel;
        nom_carte = " Paw  Paw ";
        int nombre_a_supprimer = SliderControl.instance.montantSelectionneControl; //Valeur du slider

        GameObject carte_plante = GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[nom_carte]);
        //Debug.Log(GameLogic.instance.Dico_NomCarte_Attache[nom_carte]);
        Debug.Log(carte_plante);
        GameObject enfant_plante = carte_plante.transform.GetChild(0).gameObject;
        Debug.Log(enfant_plante);
        TextMeshProUGUI compteur = enfant_plante.GetComponent<TextMeshProUGUI>();
        Debug.Log(compteur);
        string nombre_affiche = compteur.text;
        if (GameLogic.instance.Is_invasif(nom_carte, joueur.continent.defausse))
        {
            if (joueur.continent.defausse.Count == 0) { Debug.Log("La liste est vide !"); }
            Debug.Log("Invasif");
            int nombre_version_int = int.Parse(nombre_affiche);
            nombre_version_int--;
            nombre_version_int.ToString();
            Debug.Log(nombre_version_int);
        }
        else
        {
            Debug.Log("Else");
            int nombre_version_int = int.Parse(nombre_affiche);
            nombre_version_int = nombre_version_int - nombre_a_supprimer;
            string nouveau_compteur = nombre_version_int.ToString();
            Debug.Log(nouveau_compteur);
            compteur.text = nouveau_compteur;
        }
    }
    public void Test_boutton()
    {
        Debug.Log("Let's go");
    }
}
