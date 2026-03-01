using UnityEngine;

public class RecupFinTourJoueur : MonoBehaviour
{
   public void FinTourPressed()
    {
        TurnHandler.instance.FinDeTour();
    }
}
