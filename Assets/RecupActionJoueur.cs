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
            Debug.Log("Eduquer appel");
            //RetirerPieces(50);
        }
    }
    public void RecupRecruter()
    {
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1) && (!TurnHandler.instance.PlayerActuel.OuvrierAchete)) //VerifMontant(100) && VerifPointAction(1)
        {
            TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recruter);
            Debug.Log("Recrute appel");
            //RetirerPieces(100);
        }
    }
    public void RecupRecenser()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recenser);
        Debug.Log("Recenser appel");
    }
    public void RecupRecolter()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recolter);
        Debug.Log("Recolter appel");
    }
    public void RecupAmeliorer()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Ameliorer);
        Debug.Log("Ameliorer appel");
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
