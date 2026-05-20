using UnityEngine;
using System.Collections.Generic;

public class GestionRecensement : MonoBehaviour
{
    public static GestionRecensement instance; 
    
    public void Awake()
    {
        instance = this;
    }

    public void Glow()
    {
        var PileContients = GameObject.FindGameObjectsWithTag("DrawPile");
        foreach (var continentactif in PileContients)
        {
            continentactif.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Unglow()
    {
        var PileContients = GameObject.FindGameObjectsWithTag("DrawPile");
        foreach (var continentactif in PileContients)
        {
            continentactif.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ReactivationPostRecensement()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecensement").gameObject.SetActive(false);
        
        TurnHandler.instance.resencement_en_cours = false;
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"topButtonToPlayerBoard"}, "CanvasGUI");
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"DrawPile"}, "CanvasGUI");

        Unglow();
    }

    public void AjouterTurnHandlerNomContinent()
    {
        string string_nom = GetComponentInParent<UnityEngine.UI.Button>().name;
        Debug.Log(string_nom);
        TurnHandler.instance.liste_continent_cible_recensement.Add(string_nom);

        ReactivationPostRecensement();
    }
}
