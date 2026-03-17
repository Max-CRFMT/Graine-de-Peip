using UnityEngine;

public class RecupActionJoueur : MonoBehaviour
{

    public void RecupSubventions()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Subventions);
            Debug.Log("Subvention appel");
        }
    }

    public void RecupEduquer()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1)) //Ajouter verification du montant (VerifMontant(50) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Eduquer);
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
            Debug.Log("Eduquer appel");
        }
    }
    public void RecupRecruter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1) && (!TurnHandler.instance.PlayerActuel.OuvrierAchete)) //VerifMontant(100) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recruter);
            Debug.Log("Recrute appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }
    }
    public void RecupRecenser()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1)) //(VerifMontant(10) && VerifPointAction(1)) + s�rement d'autres conditions sur la pioche
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recenser);
            Debug.Log("Recenser appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        }

    }
    public void RecupRecolter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1)) //VerifMontant(20) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recolter);
            Debug.Log("Recolter appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
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
