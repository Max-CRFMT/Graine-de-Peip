using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RecupActionJoueur : MonoBehaviour
{
    public void RecupSubventions()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1) && !TurnHandler.instance.PlayerActuel.SubventionDemandee)
        {
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Subventions);
            TurnHandler.instance.PlayerActuel.SubventionDemandee = true;
            Debug.Log("Subvention appel");
        }
        else
        {
            Debug.Log("Pas assez de pts d'actions ou subventions déjà demandée ce tour-ci");
        }
    }

    public void RecupEduquer()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Eduquer);
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
            Debug.Log("Eduquer appel");
        }
    }
    
    public void RecupRecruter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1) && (!TurnHandler.instance.PlayerActuel.OuvrierAchete)) 
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recruter);
            Debug.Log("Recrute appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }

    public void RecupRecenser()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recenser);
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);

            TurnHandler.instance.resencement_en_cours = true;
            GameObject.FindGameObjectWithTag("bottomButtonToGameAtlas").GetComponent<ButtonScreenMoverScript>().ScreenMoverBottomButtonPressed();



            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"bottomButtonToGameAtlas", "topButtonToPlayerBoard"}, "CanvasGUI");
            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"boutonFinTour"}, "UIJoueur");
            MenuOptions.instance.ResearchCanvasSelonTag("TxtRecensement").gameObject.SetActive(true);
            MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"DrawPile"}, "CanvasGUI");
        }

    }

    public void RecupRecolter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recolter);
            Debug.Log("Recolter appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);

            TurnHandler.instance.recolte_en_cours = true;

            GameObject.FindGameObjectWithTag("bottomButtonToGameAtlas").GetComponent<ButtonScreenMoverScript>().ScreenMoverBottomButtonPressed();

            MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(true);

            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"BoutonUIJoueur"}, "CanvasGUI");
            MenuInGame.instance.ChangementClicableBoutonSelonTags(false, new List<string>(){"boutonFinTour"}, "UIJoueur");
            
            MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        }
    }

    public void RecupAmeliorer()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Ameliorer);
            Debug.Log("Ameliorer appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }

    public void RecupDon()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Don);
            Debug.Log("Don appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }

    public void RecupRestauration()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Restauration);
            Debug.Log("Restauration appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }

    public void RecupControle()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Controle);
            Debug.Log("Controle appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }


    public void AnnulerRecoltePioche()
    {
        MenuInGame.instance.ChangementClicableBoutonSelonTags(true, new List<string>(){"SpeciesStack"}, "CanvasGUI");
        TurnHandler.instance.recolte_pioche_en_cours = false;
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecoltePioche").gameObject.SetActive(false);
        MenuOptions.instance.ResearchCanvasSelonTag("TxtRecolte").gameObject.SetActive(true);

        ButtonScreenMoverScript.instance.ScreenMoverBottomButtonPressed();
    }

    public void AnnulerInterfaceBanque()
    {
        MenuOptions.instance.ResearchCanvasSelonTag("BanqueList").gameObject.SetActive(false);
    }
    public void RecupAnnulerAction()
    {
        TurnHandler.instance.AnnulerDerniereAction();
        UpdateUIActionJoueur();
    }

    public void AfficherAction()
    {
        TurnHandler.instance.AfficherActionsJoueurActuel();
    }

    public void UpdateUIActionJoueur()
    {
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }
}
