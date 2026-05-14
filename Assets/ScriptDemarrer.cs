using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    public static void ShuffleListeCartes(List<Carte> liste_carte)
    {
        var count = liste_carte.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = liste_carte[i];
            liste_carte[i] = liste_carte[r];
            liste_carte[r] = tmp;
        }
    }
    public static void ShuffleListeCartesevent(List<Carte_event> liste_carte_event)
    {
        var count = liste_carte_event.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = liste_carte_event[i];
            liste_carte_event[i] = liste_carte_event[r];
            liste_carte_event[r] = tmp;
        }
    }

    public void InitierPartie()
    {
        GameLogic.instance.SetListeJoueurs();
        
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            //g�n�ration des cartes non recens�es pour chaque continent
            List<List<string>> tableau = new();
            tableau = GameLogic.instance.Traduction_csv($"Assets/data/tableau/tableau_{joueur.map_choisie}.csv", 10, tableau);
            joueur.continent.pileFaceCachee = GameLogic.instance.Creation_carte_plante(tableau);
            Debug.Log(joueur.pseudo + " " + joueur.continent.pileFaceCachee.Count);
            ShuffleListeCartes(joueur.continent.pileFaceCachee);
            //g�n�ration des cartes de la d�fauce pour chaque continent
            joueur.continent.defausse = GameLogic.instance.Creation_carte_defausse(tableau);
        }

        GameLogic.instance.SupprimerGameObjectSelonTag("SupprB");
        GameLogic.instance.ShuffleListeJoueur(GameLogic.instance.Liste_Joueurs);
        TurnHandler.instance.PlayerActuel = GameLogic.instance.Liste_Joueurs[0];

        //g�n�ration des cartes �v�nement
        List<List<string>> tableau_event = new();
        tableau_event = GameLogic.instance.Traduction_csv($"Assets/data/tableau/tableau_event.csv", 8, tableau_event);
        GameLogic.instance.liste_event = GameLogic.instance.Creation_carte_event(tableau_event);
        ShuffleListeCartesevent(GameLogic.instance.liste_event);

        GameLogic.instance.DemarrerJeu();
        
    }
}
