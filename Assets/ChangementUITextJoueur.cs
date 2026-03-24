using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TurnHandler;

public class ChangementUITextJoueur : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMPseudoJoueur;
    [SerializeField] TextMeshProUGUI TMPThuneJoueur;
    
    [SerializeField] TextMeshProUGUI TMPContinentJoueur;
    [SerializeField] TextMeshProUGUI TMPCompteurPointsAction;
    [SerializeField] TextMeshProUGUI TMPThunePrevisionnelle;




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
        TMPContinentJoueur.text = TurnHandler.instance.PlayerActuel.map_choisie;
    }
        public void ChangePointsActionJoueur()
    {
        TMPCompteurPointsAction.text = TurnHandler.instance.PlayerActuel.Points_Action.ToString();
    }
    public void ChangerChangementJoueur()
    {
        ChangeContinentJoueur();
        ChangePseudoJoueur();
        ChangeThuneJoueur();
        ChangePointsActionJoueur();
    }

    public void UpdateThunePrevisionnelle()
    {
        // Doit changer la caractéristique de la thune qui devrait arriver au prochain tour normalement

    }

}
