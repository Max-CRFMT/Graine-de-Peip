using UnityEngine;
using System.Collections;
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
        //Je trouve pas de paramètres à activer
    }

    public void Unglow()
    {
        //Du coup là non plus mdr
    }

    public void ReactivationPostRecensement()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecensement").gameObject.SetActive(false);
        
        TurnHandler.instance.resencement_en_cours = false;
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"bottomButtonToGameAtlas", "topButtonToPlayerBoard"}, "CanvasGUI");
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"boutonFinTour"}, "UIJoueur");
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"DrawPile", "SpeciesStack"}, "CanvasGUI");
    }

    public void AjouterTurnHandlerNomContinent()
    {
        string string_nom = GetComponentInParent<UnityEngine.UI.Button>().name;
        Debug.Log(string_nom);
        TurnHandler.instance.liste_continent_cible_recensement.Add(string_nom);
        //enlever interaction du bouton
        //Réactiver tout les boutons de la scène selon un certain 
        ReactivationPostRecensement();
    }
}
