using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Collections;
using System.Timers;
using TMPro;
using System.Linq;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler instance;
    public bool FinTour;
    public bool FinDiscution;
    public Dictionary<Player, List<PlayerAction>> Dico_JoueurActions;
    public Player PlayerActuel;
    public GameObject TxtCountdown;
    public GameObject ButtonCountdown;
    public TurnHandler() { }


    public enum PlayerAction
    {
        Subventions,
        Eduquer,
        Recruter,
        Recenser,
        Recolter,
        Ameliorer
    }

    public Dictionary<PlayerAction, Action<Player>> Dico_Actions = new()
    {
        {PlayerAction.Subventions, player => player.DemandeSubventions()},
        {PlayerAction.Eduquer, player => player.Eduquer()},
        {PlayerAction.Recruter, player => player.Recruter_Ouvrier()},
        {PlayerAction.Recenser, player => player.RecencerGraines()},
        {PlayerAction.Recolter, player => player.RecolterGraines()},
        {PlayerAction.Ameliorer, player => player.AmeliorerJardin()}
    };

    private void Awake()
    {
        instance = this;
        instance.Dico_JoueurActions = new Dictionary<Player, List<PlayerAction>>();
        instance.FinTour = false;
        instance.FinDiscution = false;
    }
    public void Traduction_csv(string fichier_csv, int nombre_de_caracteristique, List<List<string>> carte_evenement)
    {
        string tableau_evenement = fichier_csv; //ici on va assigné à notre fichier csv (exemple:"Assets/data/tableau_evenement.csv")un nom de variable, actuellement la variable est un énorme string 
        using (StreamReader reader = new StreamReader(tableau_evenement)) //ça c'est le pointeur qui va lire ligne par ligne notre csv
        {
            reader.ReadLine(); //Là on lit la première ligne où y a les titres pour pouvoir l'ignorer 
            int indice = 0; //ici c'est optionnel mais on peut initier un compteur qui nous dira sur quel ligne on est
            string lecteur_de_ligne; //initialisation d'une autre variable qui va prendre pour chaque boucle la chaine de caractère d'une ligne
            while ((lecteur_de_ligne = reader.ReadLine()) != null) //Là on va lire chaque ligne du fichier jusqu'à qu'il y en ait plus
            {
                string[] ligne_decouper = lecteur_de_ligne.Split('|'); //ici on va découper la ligne sur la quel on est en fonction du caractère qu'on aura choisi comme séparateur lors de la création du csv 
                List<string> ligne = new List<string>(); //initialisation d'une liste à une dimension 
                ligne.AddRange(ligne_decouper); //on transforme la ligne_découper qui est un string[] en une liste pour pouvoir la manipuler
                if (ligne.Count > nombre_de_caracteristique) //ici on commence la partie où on va trié les élément en trop si il y en a, c'es pour ça qu'on a définie la variable nombre_de_caractéristique qui va définir le nombre délément on veut pour une carte
                {
                    ligne.RemoveRange(nombre_de_caracteristique, ligne.Count - nombre_de_caracteristique); //ici sa va enlever tout les élément de la liste qui on un indice supérieur au nombre que l'on veut
                    carte_evenement.Add(ligne); //et enfin ici on met la ligne qui correspond a une carte dans une liste de liste où chaque ligne sera tout les caractèristique d'une carte et chaque colonne une caractéristique en particulier
                }
                else
                {
                    carte_evenement.Add(ligne);
                }
                indice += 1;
            }
        }
    }

    public void RajouterAToutLesJoueursPiecesMissionEct()
    {
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            player.MiseAJourDebutTourPieces();
        }
    }

    public void Evenement()
    {
        //TODO - Doit piocher une carte �v�nement et appliquer ce dernier
        //PiocherCarteEvenement();
        //AppliquerEvenement();
    }

    public IEnumerator TempsDeDiscussion()
    {
        //TODO - Doit bloquer les commandes pendant 5min et afficher un tableau r�capitulatif des stats/missions des joueurs (faut que le tableau récapitulatif ait le tag TimerDiscution)
        Canvas canvas = FindAnyObjectByType<Canvas>();
        GameObject TexteCountDown = Instantiate(TxtCountdown, canvas.transform);
        GameObject BoutonCountdown = Instantiate(ButtonCountdown, canvas.transform);

        yield return new WaitUntil(() => instance.FinDiscution);
    }

    public void MasquerUIJoueur()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        UnityEngine.UI.Button[] gameobjects = canvas.GetComponentsInChildren<UnityEngine.UI.Button>(true);
        foreach (UnityEngine.UI.Button go in gameobjects)
        {
            if (go.gameObject.tag == "UIJoueur")
            {
                go.gameObject.SetActive(false);
            }

        }
    }

    public void ReafficherUI()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        UnityEngine.UI.Button[] gameobjects = canvas.GetComponentsInChildren<UnityEngine.UI.Button>(true);
        foreach (UnityEngine.UI.Button go in gameobjects)
        {
            if (go.gameObject.tag == "UIJoueur")
            {
                go.gameObject.SetActive(true);
            }
            
        }
    }

    public void ChangementTourJoueur(Player joueur_suivant)
    {
        //TODO - Doit s'occuper de tout ce qui est changement du tour d'un joueur � l'autre, n�c�ssitera beaucoup de fonction sous-jacentes (Faire apparaitre de l'UI et bouger la cam�ra)
    }
    
    public void FinDeTour()
    {
        //TODO - Doit s'occuper de tout ce qui pr�c�de le changement de tour, n�c�ssitera aussi des fonctions sous-jacentes (suppression de l'UI)
        instance.FinTour = true;
    }

    public void EffectuerActions()
    {
        Dictionary<Player, List<PlayerAction>>.KeyCollection keys = Dico_JoueurActions.Keys; 
        for (int i = 1; i < GameLogic.instance.Liste_Joueurs.Count + 1; i++)
        {
            foreach (Player key in keys)
            {
                foreach (PlayerAction Action in Dico_JoueurActions[key])
                {
                    Dico_Actions[Action](key);
                }
            }
        }
        //Une fois que les actions sont effectuées on supprime la liste pour en créer une nouvelle
        instance.Dico_JoueurActions = new Dictionary<Player, List<PlayerAction>>();
    }
    public void AjouterActionDansDicoJoueursAction(PlayerAction action)
    {
        //Faut que al fonction soit appellée par une autre fonction 
        //Si le nom du joueur est dans déjà dans les clées du dico, on rajoute l'action
        if (instance.Dico_JoueurActions.ContainsKey(PlayerActuel))
        {
            instance.Dico_JoueurActions[PlayerActuel].Add(action);
        }
        //Si le nom du joueur n'est pas déjà dans les clées du dico, on ajoute le joueur et l'action
        else
        {
            instance.Dico_JoueurActions.Add(PlayerActuel, new List<PlayerAction>(){action});
        }
    }
    
    public IEnumerator TourJoueur()
    {
        //On veut que le tour se bloque tant que je joueur n'a pas appuyé sur le bouton qui passe son tour
        yield return new WaitUntil(() => instance.FinTour);

    }
    
    public IEnumerator RoundComplet()
    {
        //RajouterAToutLesJoueursPiecesMissionEct();
        //Evenement();
        MasquerUIJoueur();
        yield return StartCoroutine(TempsDeDiscussion());
        instance.FinDiscution = false;
        ReafficherUI();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            instance.FinTour = false;
            PlayerActuel = joueur;

            Debug.Log("Le joueur actuel est :" + PlayerActuel.pseudo);

            //ChangementTourJoueur(PlayerActuel);

            yield return StartCoroutine(TourJoueur());
        }
        //EffectuerActions();
    }
}
