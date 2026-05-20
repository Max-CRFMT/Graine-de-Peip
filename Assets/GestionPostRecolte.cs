using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GestionPostRecolte : MonoBehaviour
{
    public static GestionPostRecolte instance;

    public Carte carte_cible_recolte;
    public char PiocheOuBanque;

    public char PiocheToJardinOuPiocheToBanque; //Si pioche

    public char BanqueReloadOuBanqueToJardin; //Si banque
    
    public void Awake()
    {
        instance = this;
    }

    public void ReactivationPostRecolte()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(false);

        TurnHandler.instance.recolte_en_cours = false;

        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"BoutonUIJoueur"}, "CanvasGUI");
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"boutonFinTour"}, "UIJoueur");
            
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"SpeciesStack"}, "CanvasGUI");
    }

    public void ChoixBanqueOuJardinVenantDePioche()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        TurnHandler.instance.recolte_pioche_en_cours = true;

        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(false);
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecoltePioche").gameObject.SetActive(true);
        Debug.Log("Ici normalement");
        ButtonScreenMoverScript.instance.ScreenMoverTopButtonpressed();
        Debug.Log("Et la");
    }

    public void AjoutConstantesEctAuTurnHandlerEtCloture()
    {
        TurnHandler.instance.liste_carte_cible_recolte.Add(carte_cible_recolte);
        TurnHandler.instance.liste_PiocheOuBanque.Add(PiocheOuBanque);
        TurnHandler.instance.liste_BanqueReloadOuBanqueToJardin.Add(PiocheToJardinOuPiocheToBanque);
        TurnHandler.instance.liste_PiocheToJardinOuPiocheToBanque.Add(BanqueReloadOuBanqueToJardin);

        ReactivationPostRecolte();
    }

}
