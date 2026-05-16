using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider amountSlider;
    public TMP_Text amountText;
    public Player joueur_cible;
    public string nom_a_trouver;
    public static SliderControl instance;

    public void Awake()
    {
        instance = this;
    }

    public void initialisationSlider()
    {
        int thunesdujoueur = TurnHandler.instance.PlayerActuel.pieces;
        amountSlider.minValue = 1;
        amountSlider.maxValue = thunesdujoueur-1;
        amountSlider.value = 1;
    }

    public void joueurdoncliqued(string nom_a_trouver)
    {
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            if (joueur.pseudo == nom_a_trouver)
            {
                joueur_cible = joueur;
            }
        }
    }

    public void OnSliderChange(float value)
    {
        amountText.text = ((int)value).ToString();
    }

    public void OnValidateClicked()
    {
        int montantSelectionne = (int)amountSlider.value;
        Debug.Log("Montant : " + montantSelectionne + " pour " + joueur_cible.pseudo);
        //Faudrait maintenant que ça actionne 
        TurnHandler.instance.liste_player_cible_don.Add(joueur_cible);
        TurnHandler.instance.liste_montant_don.Add(montantSelectionne);
    }
}
