using JetBrains.Annotations;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

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
        if (VerifMontant(montantSubvention))
        {
            Debug.Log("Function demandeSubventions exec");
            pieces += 2;
        }
        else
        {
            Debug.Log("Pas assez de thune rip bozo");
        }
    }

    public void Don(int montant_don, Player player_source, Player player_cible)
    {
        int prix_don = 1;
        if (VerifMontant(prix_don))
        {
            Debug.Log("Function Don exec");
            player_cible.RajouterPieces(montant_don);
            player_source.RetirerPieces(montant_don + prix_don);
            TurnHandler.instance.indice_player_cible_don++;
            TurnHandler.instance.indice_liste_montant_don++;
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
    }

    public void RestaurationBanque(Carte carte_selectionnee)
    {
        Debug.Log("Function restauration Banque exec");
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            if (player.continent.name == carte_selectionnee.continent_name)
            {
                //Ajouter dans la pile face pas cachée la carte
                foreach (Carte carte in continent.banque.FileDeCartes)
                {
                    if (carte_selectionnee.nom == carte.nom)
                    {
                        continent.banque.RemoveCard(continent.banque.FileDeCartes, carte_selectionnee);
                    }
                }
            }
        }
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
            TurnHandler.instance.indice_JB_restauration++;
            TurnHandler.instance.indice_carte_selected_restauration++;
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
        TurnHandler.instance.indice_JB_restauration++;
        TurnHandler.instance.indice_carte_selected_restauration++;
    }
    public void RestaurationJardin(Carte carte_selectionnee)
    {
        Debug.Log("Function restauration Jardin exec");
        //Compost.retirer(Carte)
        foreach (Player player in GameLogic.instance.Liste_Joueurs)
        {
            if (player.continent.name == carte_selectionnee.continent_name)
            {
                player.continent.pileFaceCachee.Add(carte_selectionnee);
                ShuffleListeCartes(player.continent.pileFaceCachee);
            }
        }
    }

    public void Controle(Carte carte_controlee, char JardinOuPiohe, int Nb_carte_Controlee)
    {
        int prix_controle = 1;
        if (carte_controlee.continent_name != map_choisie)   
        {
            prix_controle = 2;
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
        TurnHandler.instance.indice_liste_liste_cartes_controle++;
        TurnHandler.instance.indice_liste_JP_controle++;
        TurnHandler.instance.indice_liste_nb_cartes_controle++;
    }

    public void Eduquer()
    {
        int prix_education = 4;
        if (VerifMontant(prix_education) && continent.EducationLevel <=3)
        {
            Debug.Log("Function eduquer exec");
            continent.EducationLevel += 1;
            RetirerPieces(prix_education);
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }

    }

    public void Recruter_Ouvrier()
    {
        if (VerifMontant(Liste_prix_ouvrier[continent.EducationLevel]))
        {
            Debug.Log("Function recruter ouvrier exec");
            RajouterPointActionMax();
            OuvrierAchete = true;
            Debug.Log("Maintenant, pt actions max = " + Points_Action_Max);
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

        Dico_NomCarte_Attache = new Dictionary<string, string>(){
            {" Ambroisie  à  feuilles  d'armoise ","AmbroisieAFeuillesDarmoise"},
            {" Pavot  Polaire ","PavotPolaire"},
            {" Épicéa  de  Serbie   ","EpiceaDeSerbie"},
            {" Croc  de  sorcière ","CrocDeSorcière"},
            {" Cocotier  de  mer ","CocotierDeMer"},
            {"Plantes-cailloux","PlantesCailloux"},
            {" Impatiente  de  l'Himalaya ","ImpatianteDeLhimalaya"},
            {" Adonis  de  printemps ","AdonisDuPrintemps"},
            {" Rose  du  désert ","RoseDuDesert"},
            {"Rafflesia","Rafflesia"},
            {"Saxaoul","Saxaoul"},
            {" Dompte-Venin  noir ","DompteVeninNoir"},
            {" Reine  de  la  nuit ","ReineDeLaNuit"},
            {" Paw  Paw ","PawPaw"},
            {" Sapin  de  Fraser ","SapinDeFraser"},
            {" Baîe  du  faisan   ","BaieDuFaisan"},
            {" Marguerite  de  l'île  Campbell ","MargueriteDeLileCampbell"},
            {" Pois  du  désert  de  Sturt ","PoisDuDesertDeSturt"},
            {"Arum  Titan","Kokio"}, //Non fonctionnelle
            {" Herbe  de  la  Pampa ","HerbeDeLaPampa"},
            {" Chapeau  de  Turc ","ChapeauDeTurc"},
            {" Luzerne  tropicale ","LuzerneTropicale"},
            {" Nénuphar  géant   ","NenupharGeant"},
            {" Plantes  à  bisous   ","PlantesABisous"},
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

        if (VerifMontant(prix_recensement))
        {
            Debug.Log("Function recenser exec");
            RetirerPieces(prix_recensement);
            carte_drawn = joueur_cible.continent.pileFaceCachee[0];
            joueur_cible.continent.pileFaceCachee.RemoveAt(0);

            GameObject.Find(Dico_NomCarte_Attache[carte_drawn.nom]).GetComponent<SpeciesStackScript>().SpeciesStackCardIsDiscovered();
            GameObject.Find(Dico_NomCarte_Attache[carte_drawn.nom]).GetComponent<SpeciesStackScript>().IncreaseCardAmount();
        }
        else
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
        TurnHandler.instance.indice_liste_continent_cible_recensement++;
    }

    public void RecolterGraines(Carte carte_cible, char PiocheOuBanque, char PiocheToJardinOuPiocheToBanque, char BanqueReloadOuBanqueToJardin)
    {
        int prix_recolte = 1;
        if (carte_cible.continent_name != map_choisie)
        {
            prix_recolte = 2;
        }
        if (VerifMontant(prix_recolte))
        {
            Debug.Log("Function recolter exec");

            if (PiocheOuBanque == 'P') //Carte prise de la pioche
            {
                if (PiocheToJardinOuPiocheToBanque == 'J') //Carte va dans le jardin du joueur
                {
                    
                }
                else if (PiocheToJardinOuPiocheToBanque == 'B') //Carte va dans la banque du joueur
                {
                    
                }
            }
            else if (PiocheOuBanque == 'B') // Carte prise de la banque
            {
                if (BanqueReloadOuBanqueToJardin == 'R') //Remettre la carte à un autre endroit de la banque
                {
                    
                }
                else if (BanqueReloadOuBanqueToJardin == 'J') //Envoyer la carte dans le jardfin
                {
                    
                }
            }
        }
        else    
        {
            Debug.Log("Montant non acquis, action annulée, cheh");
        }
        TurnHandler.instance.indice_liste_carte_cible_recolte++;
        TurnHandler.instance.indice_liste_PiocheOuBanque++;
        TurnHandler.instance.indice_liste_BanqueReloadOuBanqueToJardin++;
        TurnHandler.instance.indice_liste_PiocheToJardinOuPiocheToBanque++;
    }

    public void AmeliorerJardin()
    {
        int prix_jardin = 4;
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
