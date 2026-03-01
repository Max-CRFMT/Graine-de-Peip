using UnityEngine;
using UnityEngine.UI;

public class SpeciesStackScript : MonoBehaviour
{

    public int CardAmount = 0;
    public bool IsDiscovered = true;

    
    public Text CardAmountText;
    public SpriteRenderer SpeciesStackSprite;
    public Material originalMat;

    [ContextMenu("Increase Card Amount")]
    public void IncreaseCardAmount()
    {
        CardAmount += 1;
        CardAmountText.text = CardAmount.ToString();
    }

    [ContextMenu("Decrease Card Amount")]
    public void DecreaseCardAmount()
    {
        if (CardAmount <= 0)
            Debug.Log("Cannot decrease card amount, already at 0");
        else
            CardAmount -= 1;
        CardAmountText.text = CardAmount.ToString();

    }

    [ContextMenu("Card is drawn")]
    public void SpeciesStackCardIsDiscovered() // FONCTION FAIS POUR *SI* LA CARTE EST DECOUVERTE
    {
        //SpeciesStack.SetActive(true); 
        IsDiscovered = true;

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
        if (CardAmount <= 0)
        {
            GetComponentInParent<Renderer>().material = Resources.Load<Material>("GreyscaleMaterial"); //On lit le parent de l'objet, ici SpeciesStack et on charge le matériau "Greyscale"
        }
        else
        {
            GetComponentInParent<Renderer>().material = originalMat;
        }
    }
    void Start()
    {
        //SpeciesStack.SetActive(false);
        SpeciesStackSprite = GetComponent<SpriteRenderer>();
        SpeciesStackSprite.enabled = false;
        CardAmountText.enabled = false;
        originalMat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        CardStackColor();
        IsSpeciesStackDiscovered();
    }
}
