using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SpeciesStackScript : MonoBehaviour
{
    public GameObject SpeciesStack;
    public int CardAmount = 0;
    public bool IsDiscovered = false;

    // Visual components
    public TextMeshProUGUI CardAmountText;
    public Image SpeciesStackSprite;
    public Material coloredMat;
    public Material greyscaleMat;

    public List<Carte> Liste_Carte_Recensee = new List<Carte>();

    public string nom_espece;
    public int nb_control = SliderControl.instance.montantSelectionneControl;

    [ContextMenu("Increase Card Amount")]
    public void IncreaseCardAmount()
    {
        CardAmount += 1;
        CardAmountText.text = CardAmount.ToString();
        CardStackColor();
    }

    [ContextMenu("Decrease Card Amount")]
    public void DecreaseCardAmount()
    {
        if (CardAmount <= 0)
            Debug.Log("Cannot decrease card amount, already at 0");
        else
            CardAmount -= 1;
        CardAmountText.text = CardAmount.ToString();
        CardStackColor();
    }

    [ContextMenu("Card is drawn")]
    public void SpeciesStackCardIsDiscovered() // FONCTION FAIS POUR *SI* LA CARTE EST DECOUVERTE
    {
        IsDiscovered = true;
        return;
    }

    public void IsSpeciesStackDiscovered() // FONCTION UTILISE POUR AFFICHER LE STACK SI CETTE CARTE EST DECOUVERTE
    {
        if (IsDiscovered)
        {
            CardAmountText.enabled = true;
            SpeciesStackSprite.enabled = true;
        }
    }

    public void Ajouter(Carte carte_a_ajouter)
    {
        Liste_Carte_Recensee.Add(carte_a_ajouter);
    }

    public void CardStackColor()
    {
        if (SpeciesStackSprite != null)
            if (CardAmount <= 0)
            {
                SpeciesStackSprite.material = greyscaleMat; //On lit le parent de l'objet, ici SpeciesStack et on charge le matériau "Greyscale"
            }
            else
            {
                SpeciesStackSprite.material = coloredMat;
            }
    }

    void Start()
    {
        if (SpeciesStackSprite != null)
        {
            SpeciesStackSprite.enabled = false;
        }
        CardAmountText.enabled = false;
        // SpeciesStackCardIsDiscovered();
    }

    public void RenvoyerCartePioche()
    {
        if (TurnHandler.instance.recolte_en_cours)
        {
            if (CardAmount > 0)
            {
                DecreaseCardAmount();
                GestionPostRecolte.instance.carte_cible_recolte = Liste_Carte_Recensee[0];
                GestionPostRecolte.instance.PiocheOuBanque = 'P';
                GestionPostRecolte.instance.ChoixBanqueOuJardinVenantDePioche();
            } else 
            {
                Debug.Log("Bah y'en a pas.");
            }
        }
    }

    public void RenvoyerCarteControle()
    {
        if (TurnHandler.instance.controle_en_cours)
        {
            if (CardAmount > 0)
            {
                nom_espece = gameObject.name;
                Debug.Log(nom_espece);
            }
            else { Debug.Log("Aucune carte de cette espèce !"); }
        }
    }

    public void SupprimerCarteControle()
    {
        if (TurnHandler.instance.controle_en_cours)
        {
            if (CardAmount > 0)
            {
                for (int i = 0; i < nb_control; i++)
                {
                    DecreaseCardAmount();
                }
            }
            else { Debug.Log("Aucune carte de cette espèce !"); }
        }
    }

    void Update()
    {
        IsSpeciesStackDiscovered();
    }
}
