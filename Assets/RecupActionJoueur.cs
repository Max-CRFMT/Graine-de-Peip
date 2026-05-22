using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RecupActionJoueur : MonoBehaviour
{
    public void RecupSubventions()
    {
        TurnHandler.instance.PlayerActuel.DemandeSubventions();
    }

    public void RecupEduquer()
    {
        TurnHandler.instance.PlayerActuel.Eduquer();
    }
    
    public void RecupRecruter()
    {
        TurnHandler.instance.PlayerActuel.Recruter_Ouvrier();
    }

    public void RecupRecenser()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {

            TurnHandler.instance.resencement_en_cours = true;
            GameObject.FindGameObjectWithTag("bottomButtonToGameAtlas").GetComponent<ButtonScreenMoverScript>().ScreenMoverBottomButtonPressed();

            GestionRecensement.instance.Glow();

            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"topButtonToPlayerBoard"}, "CanvasGUI");
            MenuOptions.instance.ResearchCanvasSelonTag("TxtRecensement").gameObject.SetActive(true);
            MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"DrawPile"}, "CanvasGUI");
        }
    }

    public void RecupRecolter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            Debug.Log("Recolter appel");

            TurnHandler.instance.recolte_en_cours = true;

            GameObject.FindGameObjectWithTag("bottomButtonToGameAtlas").GetComponent<ButtonScreenMoverScript>().ScreenMoverBottomButtonPressed();

            MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(true);

            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"BoutonUIJoueur", "boutonFinTour"}, "CanvasGUI");
            
            MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        }
    }

    public void RecupAmeliorer()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            Debug.Log("Ameliorer appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
            TurnHandler.instance.PlayerActuel.AmeliorerJardin();
        }
    }

    public void RecupRestauration()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            Debug.Log("Restauration appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);

        }
    }

    public void RecupControle()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            Debug.Log("Controle appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }


    public void AnnulerRecoltePioche()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"Jardins"}, "CanvasGUI");
        TurnHandler.instance.recolte_pioche_en_cours = false;
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecoltePioche").gameObject.SetActive(false);
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(true);

        ButtonScreenMoverScript.instance.ScreenMoverBottomButtonPressed();
    }

    public void AnnulerRecolte()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        GestionPostRecolte.instance.ReactivationPostRecolte();
        TurnHandler.instance.recolte_en_cours= false;
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecoltePioche").gameObject.SetActive(false);
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(false);
    }

    public void AnnulerRecensement()
    {
        GestionRecensement.instance.ReactivationPostRecensement();
    }


    public void AnnulerInterfaceBanque()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("BanqueList").gameObject.SetActive(false);
    }



    public void UpdateUIActionJoueur()
    {
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }
}
