using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider amountSlider;
    public TMP_Text amountText;
    public Player joueur_cible;
    public int montantSelectionneControl;
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

    public void InitialisationSliderControle(string nom_carte)
    {
        GameObject carte_plante = GameObject.Find(nom_carte);
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
        int montantSelectionne = (int)amountSlider.value;
        Debug.Log("Montant : " + montantSelectionne + " pour " + joueur_cible.pseudo);
        //Faudrait maintenant que ça actionne 
        TurnHandler.instance.liste_player_cible_don.Add(joueur_cible);
        TurnHandler.instance.liste_montant_don.Add(montantSelectionne);
    }

    public void OnValidateClickedControle()
    {   
        montantSelectionneControl = (int)amountSlider.value;
        Debug.Log("Eradiquer" + montantSelectionneControl + "plantes");
        string nom_carte = ControleDesPopulations.instance.bouton_clique.name;
        GameObject carte_plante = GameObject.Find(nom_carte);
        string nom_carte_detache = GameLogic.instance.Dico_NomCarte_Attache.FirstOrDefault(x => x.Value == nom_carte).Key;
        carte_plante.transform.GetComponent<SpeciesStackScript>().SupprimerCarteControle(montantSelectionneControl, nom_carte_detache);
    }
}
