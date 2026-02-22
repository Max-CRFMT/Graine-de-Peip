using UnityEngine;

public class RecupActionJoueur : MonoBehaviour
{
    public void RecupSubventions()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Subventions);
        Debug.Log("Subvention appelé");
    }

    public void RecupEduquer()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Eduquer);
    }
    public void RecupRecruter()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recruter);
    }
    public void RecupRecenser()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recenser);
    }
    public void RecupRecolter()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Recolter);
    }
    public void RecupAmeliorer()
    {
        TurnHandler.instance.AjouterActionDansDicoJoueursAction(TurnHandler.PlayerAction.Ameliorer);
    }
}
