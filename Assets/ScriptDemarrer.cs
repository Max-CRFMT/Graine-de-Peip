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
            tableau = GameLogic.instance.Traduction_csv($"Assets/data/tableau/tableau_{joueur.map_choisie}.csv", 10, tableau);
            joueur.continent.pileFaceCachee = GameLogic.instance.Creation_carte_plante(tableau);
            Debug.Log(joueur.pseudo + " " + joueur.continent.pileFaceCachee.Count);
        }
        GameLogic.instance.SupprimerGameObjectSelonTag("SupprB");
        ShuffleListeJoueur(GameLogic.instance.Liste_Joueurs);
        TurnHandler.instance.PlayerActuel = GameLogic.instance.Liste_Joueurs[0];
        //TODO - Shuffle tout les paquets de cartes
        GameLogic.instance.DemarrerJeu();
    }
}
