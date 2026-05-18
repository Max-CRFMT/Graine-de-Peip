using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Timers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public static TurnHandler instance;
    public bool FinTour;
    public bool FinDiscution;
    public Dictionary<Player, List<PlayerAction>> Dico_JoueurActions;
    public Player PlayerActuel;
    public GameObject TxtCountdown;
    public GameObject ButtonCountdown;
    //Action don
    public List<Player> liste_player_cible_don;
    public int indice_player_cible_don;
    public List<int> liste_montant_don;
    public int indice_liste_montant_don;
    //Action restauration
    public List<char> liste_JB_restauration;
    public int indice_JB_restauration;
    public List<Carte> liste_carte_selected_restauration;
    public int indice_carte_selected_restauration;
    //Action recensement
    public List<string> liste_continent_cible_recensement;
    public int indice_liste_continent_cible_recensement;
    //Action recolte 
    public List<Carte> liste_carte_cible_recolte;
    public int indice_liste_carte_cible_recolte;
    public List<Char> liste_PiocheOuBanque;
    public int indice_liste_PiocheOuBanque;
    public List<Char> liste_PiocheToJardinOuPiocheToBanque;
    public int indice_liste_PiocheToJardinOuPiocheToBanque;
    public List<Char> liste_BanqueReloadOuBanqueToJardin;
    public int indice_liste_BanqueReloadOuBanqueToJardin;
    //Action controle
    public List<Player> liste_player_cible_controle;
    public int indice_liste_player_cible_controle;
    public List<Carte> liste_liste_carte_controle;
    public int indice_liste_liste_cartes_controle;
    public List<char> liste_JP_controle;
    public int indice_liste_JP_controle;
    public List<int> liste_nb_cartes_controle;
    public int indice_liste_nb_cartes_controle;

    public bool resencement_en_cours;


    public TurnHandler() { }

    public enum PlayerAction
    {
        Subventions,
        Eduquer,
        Recruter,
        Recenser,
        Recolter,
        Ameliorer,
        Don,
        Restauration,
        Controle
    }

    public List<PlayerAction> Liste_PlayerActions = new List<PlayerAction>()
    {
        PlayerAction.Don,
        PlayerAction.Recenser,
        PlayerAction.Recolter,
        PlayerAction.Restauration,
        PlayerAction.Controle,
        PlayerAction.Ameliorer,
        PlayerAction.Recruter,
        PlayerAction.Eduquer,
        PlayerAction.Subventions
    };

    public Dictionary<PlayerAction, Action<Player>> Dico_Actions = new()
    {
        {PlayerAction.Subventions, player => player.DemandeSubventions()},
        {PlayerAction.Eduquer, player => player.Eduquer()},
        {PlayerAction.Recruter, player => player.Recruter_Ouvrier()},
        {PlayerAction.Recenser, player => player.RecencerGraines(instance.liste_continent_cible_recensement[instance.indice_liste_continent_cible_recensement])},
        {PlayerAction.Recolter, player => player.RecolterGraines(instance.liste_carte_cible_recolte[instance.indice_liste_carte_cible_recolte],
                                                                instance.liste_PiocheOuBanque[instance.indice_liste_PiocheOuBanque],
                                                                instance.liste_PiocheToJardinOuPiocheToBanque[instance.indice_liste_PiocheToJardinOuPiocheToBanque],
                                                                instance.liste_BanqueReloadOuBanqueToJardin[instance.indice_liste_BanqueReloadOuBanqueToJardin])},
        {PlayerAction.Ameliorer, player => player.AmeliorerJardin()},
        {PlayerAction.Don, player => player.Don(instance.liste_montant_don[instance.indice_liste_montant_don], 
                                            player,
                                            instance.liste_player_cible_don[instance.indice_player_cible_don])},
        {PlayerAction.Restauration, player => player.Restauration(instance.liste_JB_restauration[instance.indice_JB_restauration],
                                                    instance.liste_carte_selected_restauration[instance.indice_carte_selected_restauration])},
        {PlayerAction.Controle, player => player.Controle(instance.liste_liste_carte_controle[instance.indice_liste_liste_cartes_controle],
                                                        instance.liste_JP_controle[instance.indice_liste_JP_controle],
                                                        instance.liste_nb_cartes_controle[instance.indice_liste_nb_cartes_controle])}
    };

    private void Awake()
    {
        instance = this;
        instance.Dico_JoueurActions = new Dictionary<Player, List<PlayerAction>>();
        instance.FinTour = false;
        instance.FinDiscution = false;
        instance.PlayerActuel = new Player("John", 0, "Amerique du Sud");

        //Action don
        instance.liste_player_cible_don = new List<Player>();
        instance.indice_player_cible_don = 0;
        instance.liste_montant_don = new List<int>();
        instance.indice_liste_montant_don = 0;

        //Action restauration
        instance.liste_JB_restauration = new List<char>();
        instance.indice_JB_restauration = 0;
        instance.liste_carte_selected_restauration = new List<Carte>();
        instance.indice_carte_selected_restauration = 0;

        //Action recensement
        instance.liste_continent_cible_recensement = new List<string>();
        instance.indice_liste_continent_cible_recensement = 0;

        //Action controle
        instance.liste_liste_carte_controle = new List<Carte>();
        instance.indice_liste_liste_cartes_controle = 0;
        instance.liste_JP_controle = new List<char>();
        instance.indice_liste_JP_controle = 0;
        instance.liste_nb_cartes_controle = new List<int>();
        instance.indice_liste_nb_cartes_controle = 0;

        //Action recolte 
        instance.liste_carte_cible_recolte = new List<Carte>();
        instance.indice_liste_carte_cible_recolte = 0;
        instance.liste_PiocheOuBanque = new List<char>();
        instance.indice_liste_PiocheOuBanque = 0;
        instance.liste_PiocheToJardinOuPiocheToBanque = new List<char>();
        instance.indice_liste_PiocheToJardinOuPiocheToBanque = 0;
        instance.liste_BanqueReloadOuBanqueToJardin = new List<char>();
        instance.indice_liste_BanqueReloadOuBanqueToJardin = 0;

        instance.resencement_en_cours = false;
}

    public void EffectuerActions()
    {
        Dictionary<Player, List<PlayerAction>>.KeyCollection keys = Dico_JoueurActions.Keys; 
        //Parcours sur le nombre d'actions
        for (int i = 0; i < Liste_PlayerActions.Count; i++)
        {
            //Parcours sur les joueurs
            foreach (Player key in keys)
            {
                //Parcours sur les actions des joueurs
                foreach (PlayerAction Action in Dico_JoueurActions[key])
                {
                    //Si l'action du joueur est la même que l'action alors on effectue l'action
                    if (Liste_PlayerActions[i] == Action)
                    {
                        Dico_Actions[Action](key);
                    }
                }
            }
        }
        
        //Une fois que les actions sont effectuées on supprime la liste pour en créer une nouvelle et on réinitialise tout
        instance.Dico_JoueurActions = new Dictionary<Player, List<PlayerAction>>();
        //Action don
        instance.liste_player_cible_don = new List<Player>();
        instance.indice_player_cible_don = 0;
        instance.liste_montant_don = new List<int>();
        instance.indice_liste_montant_don = 0;

        //Action restauration
        instance.liste_JB_restauration = new List<char>();
        instance.indice_JB_restauration = 0;
        instance.liste_carte_selected_restauration = new List<Carte>();
        instance.indice_carte_selected_restauration = 0;

        //Action recensement
        instance.liste_continent_cible_recensement = new List<string>();
        instance.indice_liste_continent_cible_recensement = 0;

        //Action controle
        instance.liste_liste_carte_controle = new List<Carte>();
        instance.indice_liste_liste_cartes_controle = 0;
        instance.liste_JP_controle = new List<char>();
        instance.indice_liste_JP_controle = 0;
        instance.liste_nb_cartes_controle = new List<int>();
        instance.indice_liste_nb_cartes_controle = 0;

        //Action recolte 
        instance.liste_carte_cible_recolte = new List<Carte>();
        instance.indice_liste_carte_cible_recolte = 0;
        instance.liste_PiocheOuBanque = new List<char>();
        instance.indice_liste_PiocheOuBanque = 0;
        instance.liste_PiocheToJardinOuPiocheToBanque = new List<char>();
        instance.indice_liste_PiocheToJardinOuPiocheToBanque = 0;
        instance.liste_BanqueReloadOuBanqueToJardin = new List<char>();
        instance.indice_liste_BanqueReloadOuBanqueToJardin = 0;
    }

    public void AjouterActionDansDicoJoueursAction(PlayerAction action)
    {
        //Faut que al fonction soit appellée par une autre fonction 
        //Si le nom du joueur est dans déjà dans les clées du dico, on rajoute l'action
        if (PresenceKeyJoueur(PlayerActuel))
        {
            instance.Dico_JoueurActions[PlayerActuel].Add(action);
        }
        //Si le nom du joueur n'est pas déjà dans les clées du dico, on ajoute le joueur et l'action
        else
        {
            instance.Dico_JoueurActions.Add(PlayerActuel, new List<PlayerAction>(){action});
        }
    }
    
    public void Creationlisteevenement()
    {
        string filePath = Application.streamingAssetsPath + "Assets/data/tableau_event.csv";
        using (StreamReader reader = new StreamReader(filePath))
        {
            reader.ReadLine();
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
        Canvas[] canvas_UI_liste = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        Canvas canvas_UI = canvas_UI_liste[0];
        foreach (Canvas canvas in canvas_UI_liste)
        {
            if (canvas.tag == "UIJoueur")
            {
                canvas_UI = canvas;
            }
        }

        GameObject TexteCountDown = Instantiate(TxtCountdown, canvas_UI.transform);
        GameObject BoutonCountdown = Instantiate(ButtonCountdown, canvas_UI.transform);

        yield return new WaitUntil(() => instance.FinDiscution);
    }


    public void MasquerUIJoueur()
    {
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "UIJoueur", "boutonFinTour" }, "UIJoueur", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "UIJoueur", "boutonFinTour" }, "CanvasGUI", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "UIJoueur" }, "UIJoueur", 2);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(false, new List<string>() { "UIJoueur" }, "CanvasGUI", 2);
    }


    public void ReafficherUI()
    {
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "UIJoueur", "boutonFinTour" }, "UIJoueur", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "UIJoueur", "boutonFinTour" }, "CanvasGUI", 0);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "UIJoueur" }, "UIJoueur", 2);
        MenuInGame.instance.ChangementActiveBoutonRawImageOuTexteSelonTags(true, new List<string>() { "UIJoueur" }, "CanvasGUI", 2);
    }

    public void ChangementTourJoueur(Player joueur_suivant)
    {
        //TODO - Doit s'occuper de tout ce qui est changement du tour d'un joueur � l'autre, n�c�ssitera beaucoup de fonction sous-jacentes (Faire apparaitre de l'UI et bouger la cam�ra)
    }
    
    public void FinDeTour()
    {
        //TODO - Doit s'occuper de tout ce qui precede le changement de tour, necessitera aussi des fonctions sous-jacentes (suppression de l'UI)
        instance.FinTour = true;
    }

    public bool PresenceKeyJoueur(Player player)
    {
        return instance.Dico_JoueurActions.ContainsKey(player);
    }

    public void AnnulerDerniereAction()
    {
        if (PresenceKeyJoueur(PlayerActuel) && (instance.Dico_JoueurActions[PlayerActuel].Count != 0))
        {
            Debug.Log("Action Removed");
            instance.PlayerActuel.Points_Action += 1 ;

            if (instance.Dico_JoueurActions[instance.PlayerActuel][instance.Dico_JoueurActions[instance.PlayerActuel].Count() - 1] == PlayerAction.Don)
            {
                if (instance.liste_player_cible_don.Count() * instance.liste_montant_don.Count() > 0)
                {
                    instance.liste_player_cible_don.RemoveAt(instance.liste_player_cible_don.Count() - 1);
                    instance.liste_montant_don.RemoveAt(instance.liste_montant_don.Count() - 1);
                }
            }

            else if (instance.Dico_JoueurActions[instance.PlayerActuel][instance.Dico_JoueurActions[instance.PlayerActuel].Count() - 1] == PlayerAction.Subventions)
            {
                instance.PlayerActuel.SubventionDemandee = false;
            }
            else if (instance.Dico_JoueurActions[instance.PlayerActuel][instance.Dico_JoueurActions[instance.PlayerActuel].Count() - 1] == PlayerAction.Recenser)
            {
                GestionRecensement.instance.ReactivationPostRecensement();
            }
            instance.Dico_JoueurActions[instance.PlayerActuel].RemoveAt(instance.Dico_JoueurActions[instance.PlayerActuel].Count() - 1);
        } 
        else
        {
            Debug.Log("Aucune action demandee, il n'y a rien à retirer");
        }
    }
    
    public void AfficherActionsJoueurActuel()
    {
        if (instance.Dico_JoueurActions.ContainsKey(PlayerActuel) && (instance.Dico_JoueurActions[PlayerActuel].Count != 0))
        {
            foreach (PlayerAction actions in instance.Dico_JoueurActions[PlayerActuel])
            {
                Debug.Log(actions);
            }
        }
        else
        {
            Debug.Log("Il n'y a pas d'actions a enlever, je joueur n'a aucune action enregistre");
        }
    }

    public IEnumerator TourJoueur()
    {
        //On veut que le tour se bloque tant que je joueur n'a pas appuyé sur le bouton qui passe son tour
        yield return new WaitUntil(() => instance.FinTour);
    }

    public void RemplirPtActionsChaqueJoueur()
    {
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            player.RemplirPointAction();
        }
    }

    public void EvolutionPhaseVegetale()
    {
        //Faire évoluer la phase végétale : 
        //Si il y a plus de X cartes de même variété dans un biome ou environnement, ajouter % cartes graines dans cette zone.
        //Si il y a X cartes ou moins d’une même variété, l’espèce disparaît dans cet environnement.Retirer toutes les cartes présentes dans cette zone.
    }

    
    public IEnumerator RoundComplet()
    {
        RajouterAToutLesJoueursPiecesMissionEct();
        //Evenement();
        MasquerUIJoueur();
        yield return StartCoroutine(TempsDeDiscussion());
        ReafficherUI();
        RemplirPtActionsChaqueJoueur();

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            instance.FinTour = false;
            instance.PlayerActuel = joueur;
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
            

            Debug.Log("Le joueur actuel est :" + PlayerActuel.pseudo);

            //ChangementTourJoueur(PlayerActuel);

            yield return StartCoroutine(TourJoueur());
        }
        EffectuerActions();
        instance.FinDiscution = false;
    }
}
