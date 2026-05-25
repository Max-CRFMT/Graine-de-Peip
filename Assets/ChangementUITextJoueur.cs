using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TurnHandler;
using UnityEngine.UI;

public class ChangementUITextJoueur : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMPseudoJoueur;
    [SerializeField] TextMeshProUGUI TMPThuneJoueur;
    
    [SerializeField] TextMeshProUGUI TMPContinentJoueur;
    [SerializeField] TextMeshProUGUI TMPCompteurPointsAction;
    [SerializeField] TextMeshProUGUI TMPThunePrevisionnelle;

    [SerializeField] TextMeshProUGUI TMPniveauEducationJoueur;
    [SerializeField] TextMeshProUGUI TMPniveauJardinJoueur;

    public Sprite plateauJoueurSprite;
    public GameObject plateauJoueur;
    public SpriteRenderer plateauJoueurSpriteRenderer;

    public static ChangementUITextJoueur instance;

    private void Awake()
    {
        instance = this;
        plateauJoueur = GameObject.FindGameObjectWithTag("playerBoard");
    }

    public void ChangePseudoJoueur()
    {
        TMPseudoJoueur.text = TurnHandler.instance.PlayerActuel.pseudo;
    }

    public void ChangeThuneJoueur()
    {
        TMPThuneJoueur.text =  TurnHandler.instance.PlayerActuel.pieces.ToString();
    }

    public void ChangePointsActionJoueur()
    {
        TMPCompteurPointsAction.text = TurnHandler.instance.PlayerActuel.Points_Action.ToString();
    }


    public void ChangePlateauJoueur()
    {
        plateauJoueur.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("PlayerBoard/" + TurnHandler.instance.PlayerActuel.map_choisie.ToString());
        if (plateauJoueur.GetComponent<SpriteRenderer>().sprite == null)
        {
            Debug.Log("Marche pas" + " PlayerBoard/" + TurnHandler.instance.PlayerActuel.map_choisie.ToString());
        }
        
    }

    public void ChangerEducationJoueur()
    {
        TMPniveauEducationJoueur.text = TurnHandler.instance.PlayerActuel.continent.EducationLevel.ToString();
    }

    public void ChangerBiomesJoueur()
    {
        var gos = GameObject.FindGameObjectsWithTag("Biomes");
        foreach (var go in gos)
        {
            if (TurnHandler.instance.PlayerActuel.continent.jardin.liste_biome_jardin.Contains(go.name))
            {
                Image image = go.GetComponent<Image>();
                image.color = TurnHandler.instance.PlayerActuel.continent.banque.Visible;
            } else
            {
                Debug.Log(go.name);
                Image image = go.GetComponent<Image>();
                image.color = TurnHandler.instance.PlayerActuel.continent.banque.Invisible;
            }
        }
    }


    public void ChangerNiveauJardinJoueur()
    {
        TMPniveauJardinJoueur.text = TurnHandler.instance.PlayerActuel.continent.jardin.niveau_jardin.ToString();
    }
    public void ChangerChangementJoueur()
    {
        ChangePseudoJoueur();
        ChangeThuneJoueur();
        ChangePointsActionJoueur();
        ChangePlateauJoueur();        
        ChangerNiveauJardinJoueur();
        ChangerEducationJoueur();
        ChangerBiomesJoueur();
    }
}
