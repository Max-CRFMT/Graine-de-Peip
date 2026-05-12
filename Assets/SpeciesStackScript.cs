using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void Update()
    {
        IsSpeciesStackDiscovered();
    }
}
