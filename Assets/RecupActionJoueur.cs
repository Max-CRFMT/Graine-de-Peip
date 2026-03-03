using UnityEngine;

public class RecupActionJoueur : MonoBehaviour
{
    public void RecupSubventions()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.PlayerActuel.Points_Action -= 1;
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Subventions);
            Debug.Log("Subvention appel, nb action du joueur actuel restant : " + TurnHandler.instance.PlayerActuel.Points_Action);
        }
    }

    public void RecupEduquer()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1)) //Ajouter verification du montant (VerifMontant(50) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Eduquer);
            TurnHandler.instance.PlayerActuel.Points_Action -= 1;
            Debug.Log("Eduquer appel");
        }
    }
    public void RecupRecruter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1) && (!TurnHandler.instance.PlayerActuel.OuvrierAchete)) //VerifMontant(100) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recruter);
            Debug.Log("Recrute appel");
            TurnHandler.instance.PlayerActuel.Points_Action -= 1;
        }
    }
    public void RecupRecenser()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1)) //(VerifMontant(10) && VerifPointAction(1)) + sûrement d'autres conditions sur la pioche
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recenser);
            Debug.Log("Recenser appel");
            TurnHandler.instance.PlayerActuel.Points_Action -= 1;
        }

    }
    public void RecupRecolter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1)) //VerifMontant(20) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recolter);
            Debug.Log("Recolter appel");
            TurnHandler.instance.PlayerActuel.Points_Action -= 1;
        }
    }
    public void RecupAmeliorer()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Ameliorer);
            Debug.Log("Ameliorer appel");
            TurnHandler.instance.PlayerActuel.Points_Action -= 1;
        }

    }

    public void RecupAnnulerAction()
    {
        TurnHandler.instance.AnnulerDerniereAction();
    }

    public void AfficherAction()
    {
        TurnHandler.instance.AfficherActionsJoueurActuel();
    }
}
