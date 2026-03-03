using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TurnHandler;

public class ChangementUITextJoueur : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMPseudoJoueur;
    [SerializeField] TextMeshProUGUI TMPThuneJoueur;
    [SerializeField] TextMeshProUGUI TMPContinentJoueur;

    public static ChangementUITextJoueur instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangePseudoJoueur()
    {
        TMPseudoJoueur.text = TurnHandler.instance.PlayerActuel.pseudo;
    }

    public void ChangeThuneJoueur()
    {
        TMPThuneJoueur.text =  TurnHandler.instance.PlayerActuel.pieces.ToString();
    }

    public void ChangeContinentJoueur()
    {
        TMPContinentJoueur.text = TurnHandler.instance.PlayerActuel.continent.ToString();
    }

    public void ChangerChangementJoueur()
    {
        //ChangeContinentJoueur();
        ChangePseudoJoueur();
        ChangeThuneJoueur();
    }

}
