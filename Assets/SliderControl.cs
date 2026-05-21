using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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

    public void InitialisationSliderControle()
    {
        GameObject carte_plante = GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[" Paw  Paw "]);
        GameObject enfant_plante = carte_plante.transform.GetChild(0).gameObject;
        TextMeshProUGUI compteur = enfant_plante.GetComponent<TextMeshProUGUI>();
        int nb_espece_recense = int.Parse(compteur.text);
        amountSlider.minValue = 1;
        amountSlider.maxValue = Mathf.Min(3,nb_espece_recense);
    }

    public void JoueurDonCliqued(string nom_a_trouver)
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
        if (TurnHandler.instance.PlayerActuel.VerifPointAction(1))
        {
            Debug.Log("Don appel");
            TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
            int montantSelectionne = (int)amountSlider.value;
            Debug.Log("Montant : " + montantSelectionne + " pour " + joueur_cible.pseudo);

            TurnHandler.instance.PlayerActuel.Don(montantSelectionne, joueur_cible);
        }
        else
        {
            Debug.Log("Pas de pts d'action rip");
        }

    }

    public void OnValidateClickedControle()
    {   
        int montantSelectionne = (int)amountSlider.value;
        Debug.Log("Eradiquer" + montantSelectionne + "plantes");
    }
}
