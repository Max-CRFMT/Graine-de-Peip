using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GestionPostRecolte : MonoBehaviour
{
    public static GestionPostRecolte instance;

    public Carte carte_cible_recolte;
    public char PiocheOuBanque = 'D'; // D par defaut

    public char PiocheToJardinOuPiocheToBanque = 'D'; //Si pioche

    public char BanqueReloadOuBanqueToJardin = 'D'; //Si banque

    public GameObject jardin_cible;

    public int banque2ou3;
    
    public void Awake()
    {
        instance = this;
    }

    public void ReactivationPostRecolte()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(false);
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecoltePioche").gameObject.SetActive(false);


        TurnHandler.instance.recolte_en_cours = false;
        TurnHandler.instance.recolte_pioche_en_cours = false;


        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"BoutonUIJoueur", "boutonFinTour", "bottomButtonToGameAtlas"}, "CanvasGUI");
            
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        TurnHandler.instance.PlayerActuel.continent.jardin.DisableButton();


        PiocheOuBanque = 'D';
        PiocheToJardinOuPiocheToBanque = 'D';
        BanqueReloadOuBanqueToJardin = 'D';


    }

    public void ChoixBanqueOuJardinVenantDePioche()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        TurnHandler.instance.PlayerActuel.continent.jardin.EnableButton();

        TurnHandler.instance.recolte_pioche_en_cours = true;
        GameObject.FindGameObjectWithTag("topButtonToPlayerBoard").GetComponent<ButtonScreenMoverScript>().ScreenMoverTopButtonpressed();

        TurnHandler.instance.PlayerActuel.continent.banque.Banque1.transform.GetChild(0).gameObject.SetActive(true);


        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(false);
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecoltePioche").gameObject.SetActive(true);
    }


    public void AjoutConstantesEctAuTurnHandlerEtCloture()
    {
        TurnHandler.instance.PlayerActuel.RecolterGraines(carte_cible_recolte, PiocheOuBanque, PiocheToJardinOuPiocheToBanque, BanqueReloadOuBanqueToJardin);

        ReactivationPostRecolte();

        TurnHandler.instance.PlayerActuel.continent.banque.UpdateUIJoueur();
    }

}
