using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptDemarrer : MonoBehaviour
{
    public static void ShuffleListeJoueur(List<Player> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public void InitierPartie()
    {
        GameLogic.instance.SetListeJoueurs();
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            List<List<string>> tableau = new();
            tableau = GameLogic.instance.Traduction_csv("Assets/data/tableau/tableau_oceanie.csv", 10, tableau);
            joueur.continent.PileFaceCachee = GameLogic.instance.Creation_carte_plante(tableau);
        }
        GameLogic.instance.SupprimerGameObjectSelonTag("SupprB");
        ShuffleListeJoueur(GameLogic.instance.Liste_Joueurs);
        //TODO - Shuffle tout les paquets de cartes
        GameLogic.instance.DemarrerJeu();
    }
}
