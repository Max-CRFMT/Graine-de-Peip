using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptDemarrer : MonoBehaviour
{


    public void InitierPartie()
    {
        GameLogic.instance.SetListeJoueurs();
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            List<List<string>> tableau = new();
            tableau = GameLogic.instance.Traduction_csv("Assets/data/tableau/tableau_oceanie.csv", 10, tableau);
            joueur.continent.pileFaceCachee = GameLogic.instance.Creation_carte_plante(tableau, joueur.continent.name);
            Debug.Log(joueur.pseudo + " " + joueur.continent.pileFaceCachee.Count);
        }
        GameLogic.instance.SupprimerGameObjectSelonTag("SupprB");
        GameLogic.instance.ShuffleListeJoueur(GameLogic.instance.Liste_Joueurs);
        TurnHandler.instance.PlayerActuel = GameLogic.instance.Liste_Joueurs[0];
        //TODO - Shuffle tout les paquets de cartes
        GameLogic.instance.DemarrerJeu();
    }
}
