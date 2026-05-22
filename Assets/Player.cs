using JetBrains.Annotations;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player 
{
    public bool SubventionDemandee = false;
    public string pseudo;
    public int pieces;
    public string map_choisie;
    public int Points_Action_Max;
    public int Points_Action;

    public int ThuneARecolterDebutTourProchain_base;

    public Continent continent;

    public bool OuvrierAchete;
    public Carte carte_drawn;

    public List<int> Liste_prix_ouvrier = new List<int>(){12, 8, 6, 4};

    public Dictionary<string, string> Dico_ContinentUI_ContinentBack;
    public Player joueur_cible;
    public GameObject carte_script;
    public Dictionary<string, string> Dico_NomCarte_Attache;



    public Player(string name, int coins, string name_map)
    {
        pseudo = name;
        pieces = coins;
        map_choisie = name_map;
        Points_Action_Max = 3;
        
        Points_Action = Points_Action_Max;
        ThuneARecolterDebutTourProchain_base = 2;
        
        OuvrierAchete = false;
        //Liaison de classe
        continent = new Continent(map_choisie);
        Debug.Log("Pseudo du joueur : " + pseudo + "\nPieces du joueur : " + pieces + "\nMap que le joueur à choisi :" + continent.name);
    }

    public bool VerifMontant(int nb)
    {
        return pieces >= nb;
    }

    public bool VerifPointAction(int nb)
    {
        return Points_Action >= nb;
    }

    public void RajouterPointAction(int nb)
    {
        Points_Action += nb;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RemplirPointAction()
    {
        Points_Action = Points_Action_Max;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RetirerPointAction(int nb)
    {
        Points_Action -= nb;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RajouterPointActionMax()
    {
        RetirerPieces(Liste_prix_ouvrier[continent.EducationLevel]);
        Points_Action_Max++;
        ChangementUITextJoueur.instance.ChangePointsActionJoueur();
    }

    public void RajouterPieces(int nb)
    {
        pieces += nb;
    }

    public void RetirerPieces(int nb)
    {
        pieces -= nb;
    }

    public void DemandeSubventions()
    {
        int montantSubvention = 1;
        if (continent.name == "Afrique")
        {
            montantSubvention = 0;
        }

        if (VerifPointAction(1) && !SubventionDemandee && VerifMontant(montantSubvention))
        {

            Debug.Log("Function demandeSubventions exec");
            pieces += 2;
            RetirerPointAction(1);
            SubventionDemandee = true;
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
        }
        else
        {
            Debug.Log("Pas assez de pts d'actions ou de thune ou subventions déjà demandée ce tour-ci");
        }

    }

    public void Don(int montant_don, Player player_cible)
    {
        int prix_don = 1;
        if (VerifMontant(prix_don))
        {
            Debug.Log("Function Don exec");
            player_cible.RajouterPieces(montant_don);
            RetirerPieces(montant_don + prix_don);
            Debug.Log(pieces);
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void RestaurationBanque(Carte carte_selectionnee)
    {
    //    Debug.Log("Function restauration Banque exec");
    //    foreach (Player player in GameLogic.instance.Liste_Joueurs)
    //    {
    //        if (player.continent.name == carte_selectionnee.continent_name)
    //        {
                //Ajouter dans la pile face pas cachée la carte
                //foreach (Carte carte in continent.banque.FileDeCartes)
                //{
                    //if (carte_selectionnee.nom == carte.nom)
                    //{
                       // continent.banque.RemoveCard(continent.banque.FileDeCartes, carte_selectionnee);
                    //}
    //            }
    //        }
    //    }
    }
    public void Restauration(char name, Carte carte_selected)
    {
        int prix_restauration = 2;
        if (continent.name != carte_selected.continent_name)
        {
            prix_restauration = 3;
        }
        if (VerifMontant(prix_restauration))
        {
            if (name == 'B')
            {
                RestaurationBanque(carte_selected);
            }
            else if (name == 'J')
            {
                RestaurationJardin(carte_selected);
            }
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }
    public void RestaurationJardin(Carte carte_selectionnee)
    {
        Debug.Log("Function restauration Jardin exec");
        //Compost.retirer(Carte)
        GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_selectionnee.nom]).GetComponent<SpeciesStackScript>().IncreaseCardAmount();
        GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_selectionnee.nom]).GetComponent<SpeciesStackScript>().Ajouter(carte_selectionnee);

    }

    public void Controle(Carte carte_controlee, char JardinOuPiohe, int Nb_carte_Controlee)
    {
        int prix_controle = 1;
        if (carte_controlee.continent_name != map_choisie)   
        {
            prix_controle = 2;
        }
        if (map_choisie == "Océanie" || map_choisie == "Oceanie")
        {
            prix_controle--;
        }


        if (VerifMontant(prix_controle))
        {
            Debug.Log("Function controle exec");
            
            if (JardinOuPiohe == 'J')
            {
                foreach (Carte carte_jardin in continent.jardin.Liste_Carte)
                {
                    if (carte_jardin == carte_controlee)
                    {
                        //continent.jardin.Liste_Carte.Remove(carte_jardin);
                        //defausse.add(carte_jardin);
                    }
                }
            }
            else if (JardinOuPiohe == 'P')
            {
                //Parcourir la pioche ça attendra que nico ait fini
            }
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void Eduquer()
    {

        int prix_education = 4;
        if (VerifMontant(prix_education) && continent.EducationLevel <=3 && VerifPointAction(1))
        {
            Debug.Log("Function eduquer exec");
            continent.EducationLevel += 1;
            RetirerPieces(prix_education);
            RetirerPointAction(1);
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
        } else if (continent.EducationLevel == 3)
        {
            Debug.Log("Votre continent est déjà au niveau max");
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }

    }

    public void Recruter_Ouvrier()
    {

        int prix_ouvrier = Liste_prix_ouvrier[continent.EducationLevel];
        if (map_choisie == "Europe")
        {
            prix_ouvrier--;
        }

        if (VerifMontant(prix_ouvrier) && VerifPointAction(1) && !OuvrierAchete)
        {
            Debug.Log("Function recruter ouvrier exec");
            RajouterPointActionMax();
            OuvrierAchete = true;
            Debug.Log("Maintenant, pt actions max = " + Points_Action_Max);
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }
    public void RecencerGraines(string nom_continent)
    {
        Dico_ContinentUI_ContinentBack = new Dictionary<string, string>(){
            {"DrawPilesAsia","Asie"},
            {"DrawPileEurope","Europe"},
            {"DrawPileNorthAmerica","Amerique du Nord"},
            {"DrawPileOceania","Oceanie"},
            {"DrawPileAfrica","Afrique"},
            {"DrawPile","Amerique du Sud"},
        };

        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            if (joueur.map_choisie == Dico_ContinentUI_ContinentBack[nom_continent])
            {
                Debug.Log(joueur.map_choisie);
                joueur_cible = joueur;
            }
        }

        int prix_recensement = 1;
        if (joueur_cible.map_choisie != map_choisie)
        {
            prix_recensement = 2;
            Debug.Log(prix_recensement);
        }

        if (map_choisie == "Amerique du Sud" || map_choisie == "Amérique du Sud")
        {
            prix_recensement -= 1;
        }

        if (VerifMontant(prix_recensement))
        {
            Debug.Log("Function recenser exec");
            RetirerPieces(prix_recensement);
            RetirerPointAction(1);

            for (int i = 0; i != 3; i++)
            {
                carte_drawn = joueur_cible.continent.pileFaceCachee[0];
                joueur_cible.continent.pileFaceCachee.RemoveAt(0);

                GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_drawn.nom]).GetComponent<SpeciesStackScript>().SpeciesStackCardIsDiscovered();
                GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_drawn.nom]).GetComponent<SpeciesStackScript>().IncreaseCardAmount();   
                GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_drawn.nom]).GetComponent<SpeciesStackScript>().Ajouter(carte_drawn);   
            }
            ChangementUITextJoueur.instance.ChangerChangementJoueur();
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void RecolterGraines(Carte carte_cible, char PiocheOuBanque, char PiocheToJardinOuPiocheToBanque, char BanqueReloadOuBanqueToJardin)
    {
        int prix_recolte = 2;
        if (carte_cible.continent_name != map_choisie)
        {
            prix_recolte = 3;
        }
        if (map_choisie == "Amérique du Nord" || map_choisie == "Amerique du Nord")
        {
            prix_recolte--;
        }

        if (VerifMontant(prix_recolte)) 
        {
            if ((!continent.banque.VerificationPresenceCarteDansBanque(carte_cible) && !continent.jardin.VerifierPresenceDansJardin(carte_cible) || BanqueReloadOuBanqueToJardin == 'J'))
            {
                RetirerPointAction(1);
                RetirerPieces(prix_recolte);
                ChangementUITextJoueur.instance.ChangerChangementJoueur();

                Debug.Log("Function recolter exec");
                Debug.Log(PiocheOuBanque);
                Debug.Log(PiocheToJardinOuPiocheToBanque);
                Debug.Log(BanqueReloadOuBanqueToJardin);

                if (PiocheOuBanque == 'P') //Carte prise de la pioche
                {
                    if (PiocheToJardinOuPiocheToBanque == 'J') //Carte va dans le jardin du joueur
                    {
                        GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_cible.nom]).GetComponent<SpeciesStackScript>().DecreaseCardAmount();
                        continent.jardin.Liste_Carte.Add(carte_cible);

                        GestionPostRecolte.instance.jardin_cible.GetComponent<GestionInteractionJardin>().carte_contenue = carte_cible;

                        continent.jardin.UpdateSpriteJardin();
                    }
                    else if (PiocheToJardinOuPiocheToBanque == 'B') //Carte va dans la banque du joueur
                    {
                        GameObject.Find(GameLogic.instance.Dico_NomCarte_Attache[carte_cible.nom]).GetComponent<SpeciesStackScript>().DecreaseCardAmount();
                        Debug.Log("Ajoute");
                        continent.banque.listeDeListes[0].Add(carte_cible);
                    }
                }

                else if (PiocheOuBanque == 'B') // Carte prise de la banque
                {
                    if (BanqueReloadOuBanqueToJardin == 'R') //Remettre la carte à un autre endroit de la banque
                    {
                        Debug.Log("Reload");
                        continent.banque.Remontage1Carte(carte_cible.nom);
                        continent.banque.UpdateUIJoueur();
                    }
                    else if (BanqueReloadOuBanqueToJardin == 'J') //Envoyer la carte dans le jardin
                    {
                        GestionPostRecolte.instance.jardin_cible.GetComponent<GestionInteractionJardin>().carte_contenue = carte_cible;
                        continent.jardin.Liste_Carte.Add(carte_cible);
                        continent.banque.EnleverCartesBanqueSelonCarte(carte_cible);
                        continent.jardin.UpdateSpriteJardin();
                    }
                }
            }
        }
        else    
        {
            Debug.Log("Montant non acquis, action annulée, cheh. ");
        }
    }

    public void AmeliorerJardin()
    {
        int prix_jardin = 4;
        if (map_choisie == "Asie")
        {
            prix_jardin--;
        }



        if (VerifMontant(prix_jardin))
        {
            RetirerPieces(prix_jardin);
            Debug.Log("Function ameliorer exec");
            //TODO - ajouter un biome ou ajouter un espace dans le jardin
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void MiseAJourDebutTourPieces()
    {
        pieces += ThuneARecolterDebutTourProchain_base + continent.EducationLevel+2;
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
}
