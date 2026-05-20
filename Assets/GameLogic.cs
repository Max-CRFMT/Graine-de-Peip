using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameLogic : MonoBehaviour
{

    public int nb_joueurs ;
    public string difficulte;
    public List<Player> Liste_Joueurs;
    public static GameLogic instance;
    public int ToursRestants;
    public string texte;
    public bool partiefinie;
    public List<Carte_event> liste_event;

    public Dictionary<string, string> Dico_NomCarte_Attache;
    public Dictionary<string, string> Dico_traduction_useless;

    public Dictionary<string, int> DicoTourEnFctDeDifficulte = new Dictionary<string, int>()
    {
        {"Facile",7},
        {"Normal", 14},
        {"Difficile", 21}
    };

public GameLogic() { }

    public void Awake()
    {
        instance = this;
        instance.nb_joueurs = 2;
        instance.difficulte = "Facile";
        instance.Liste_Joueurs = new List<Player>();
        instance.ToursRestants = DicoTourEnFctDeDifficulte[difficulte];
        instance.partiefinie = false;

        instance.Dico_traduction_useless = new Dictionary<string, string>(){
                    {"Asie", "Asia"},
                    {"Europe", "Europe"},
                    {"Amerique du Nord", "NorthAmerica"},
                    {"Oceanie", "Oceania"},
                    {"Afrique", "Africa"},
                    {"Amerique du Sud", "SouthAmerica"},
        };

        instance.Dico_NomCarte_Attache =  new Dictionary<string, string>(){
            {" Ambroisie  à  feuilles  d'armoise ","AmbroisieAFeuillesDarmoise"},
            {" Pavot  Polaire ","PavotPolaire"},
            {" Épicéa  de  Serbie   ","EpiceaDeSerbie"},
            {" Croc  de  sorcière ","CrocDeSorcière"},
            {" Cocotier  de  mer ","CocotierDeMer"},
            {"Plantes-cailloux","PlantesCailloux"},
            {" Arbre  tabatiére ","ArbreTabatiere"},
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
            {" Arum  Titan ","Kokio"}, //Non fonctionnelle
            {" Herbe  de  la  Pampa ","HerbeDeLaPampa"},
            {" Chapeau  de  Turc ","ChapeauDeTurc"},
            {" Luzerne  tropicale ","LuzerneTropicale"},
            {" Nénuphar  géant   ","NenupharGeant"},
            {" Plantes  à  bisous   ","PlantesABisous"},
        };


    }

    public void SetNbJoueurs(int nombre)
    {
        nb_joueurs = nombre;
        Debug.Log("Le nombre de joueur selectionne est : " + nb_joueurs);

    }

    public void SetDifficulte(string difficult)
    {
        difficulte = difficult;
        Debug.Log("La difficulte selectionnee est : " + difficulte);
    }

    public List<string> SelectionMaps = new List<string>() {"Europe", "Afrique", "Asie", "Océanie", "Amérique du Nord", "Amérique du Sud"};

    public static string RemoveAccents(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        string normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    public void SetListeJoueurs()
    {
        System.Random random = new System.Random();
        List<(string, string)> ListeJoueursAttente = new List<(string, string)>();

        for (int i = 0; i < instance.nb_joueurs; i++)
        {
            string nom_a_trouver = "Joueur" + (i+1).ToString();
            GameObject[] couple_nom_map = GameObject.FindGameObjectsWithTag(nom_a_trouver);
            string nom_joueur = couple_nom_map[0].GetComponent<TMP_InputField>().text;
            string map_joueur_accent = couple_nom_map[1].GetComponent<TMP_Dropdown>().options[couple_nom_map[1].GetComponent<TMP_Dropdown>().value].text;
            string map_joueur = RemoveAccents(map_joueur_accent);
            if (map_joueur == "Aleatoire")
            {
                ListeJoueursAttente.Add((nom_joueur, map_joueur));
            }
            else
            {
                instance.Liste_Joueurs.Add(new Player(nom_joueur, 0, map_joueur));
                SelectionMaps.Remove(map_joueur_accent);
            }
        }

        foreach ((string, string) couple in ListeJoueursAttente)
        {
            string map_joueur_accent = SelectionMaps[random.Next(SelectionMaps.Count)];
            string map_joueur = RemoveAccents(map_joueur_accent);
            SelectionMaps.Remove(map_joueur_accent);
            instance.Liste_Joueurs.Add(new Player(couple.Item1, 1, map_joueur));
        }
    }

    public bool VerifUniteMap(TMP_Dropdown DropdownSource)
    {
        TMP_Dropdown[] listeDeroulantesMaps = GameObject.FindObjectsByType<TMP_Dropdown>(FindObjectsSortMode.None);
        foreach (TMP_Dropdown maps in listeDeroulantesMaps)
        {
            if ((DropdownSource.value != 0) && (maps.value == DropdownSource.value) && (maps.tag != DropdownSource.tag))
            {
                DropdownSource.value = 0;
                Debug.Log("Vous ne pouvez pas avoir deux continents identiques, map aléatoire");
                return false;
            }
        }
        return true;
    }

    public void DemarrerJeu()
    {
        Debug.Log("Skibidi ça lance");
        StartCoroutine(Jeu());
    }

    public void SupprimerGameObjectSelonTag(string tag)
    {
        foreach (var objects in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(objects);
        }
    }

    public GameObject continent_joueur;
    public List<GameObject> liste_continent_active;
    public void ActivateContinents()
    {
        liste_continent_active = new List<GameObject>();
        foreach (Player joueur in GameLogic.instance.Liste_Joueurs)
        {
            var continents = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var continent in continents)
            {
                if (continent.tag == Dico_traduction_useless[joueur.map_choisie])
                {
                    continent_joueur = continent;
                    liste_continent_active.Add(continent);
                }
            }
            continent_joueur.gameObject.SetActive(true);
        }
    }
    public IEnumerator Jeu()
    {
        AsyncOperation ChargenementScene = SceneManager.LoadSceneAsync("Game");

        yield return ChargenementScene;

        ActivateContinents();

        while (instance.ToursRestants > 0)
        {
            Debug.Log("Tours restants : " + instance.ToursRestants);

            
            yield return TurnHandler.instance.StartCoroutine(TurnHandler.instance.RoundComplet());

            instance.ToursRestants--;
        }
        FindePartie();
    }

    public void SpawnVoileEtTextFindePartie(string texte)
    {
        MenuOptions.instance.ResearchCanvasSelonTag("CanvasFin").gameObject.SetActive(true);
        MenuOptions.instance.ChangerTexteDansCanvas(MenuOptions.instance.ResearchCanvasSelonTag("CanvasFin"), texte, "CanvasFin");
    }

    public void FindePartie()
    {
        instance.partiefinie = true;
        bool placeholder = true;
        if (placeholder) //partie gagnée
        {
            texte = "Partie gagnée ! Vous avez sauvé la terre !";
        }
        else //partie perdue 
        {
            texte = "Partie perdue... Try again, Save the Earth";
        }
        SpawnVoileEtTextFindePartie(texte);
    }

    public void RebootGame()
    {
        foreach (var objects in GameObject.FindGameObjectsWithTag("LogiqueJeu"))
        {
            Destroy(objects);
        }
        Debug.Log("Fonction FinDePartie() executée");
        SceneManager.LoadScene("Lobby");
    }

    public List<List<string>> Traduction_csv(string fichier_csv, int nombre_de_caracteristique, List<List<string>> liste_carte)
    {
        string tableau_evenement = fichier_csv;
        //ça c'est le pointeur qui va lire ligne par ligne notre csv
        using (StreamReader reader = new(tableau_evenement))
        {
            //on lit la première ligne où y a les titres pour pouvoir l'ignorer 
            reader.ReadLine();
            int indice = 0;
            //initialisation d'une autre variable qui va prendre pour chaque boucle la chaine de caractère d'une ligne
            string lecteur_de_ligne;
            //Là on va lire chaque ligne du fichier jusqu'à qu'il y en ait plus
            while ((lecteur_de_ligne = reader.ReadLine()) != null)
            {
                //ici on va découper la ligne sur la quel on est en fonction du caractère qu'on aura choisi comme séparateur lors de la création du csv 
                string[] ligne_decouper = lecteur_de_ligne.Split('§');
                List<string> ligne = new List<string>();
                //on transforme la ligne_découper qui est un string[] en une liste pour pouvoir la manipuler
                ligne.AddRange(ligne_decouper);
                //ici on va trié les élément en trop si il y en a, c'est pour ça qu'on a définie la variable nombre_de_caractéristique qui va définir le nombre délément qu'on veut pour une carte
                if (ligne.Count > nombre_de_caracteristique)
                {
                    //Sa c'est la fonction qui va enlever tout les élément de la liste qui on un indice supérieur au nombre que l'on veut
                    ligne.RemoveRange(nombre_de_caracteristique, ligne.Count - nombre_de_caracteristique);
                    liste_carte.Add(ligne);
                }
                else
                {
                    liste_carte.Add(ligne);
                }
                indice += 1;
            }
        }
        return liste_carte;
    }

    public List<Carte> Creation_carte_plante(List<List<string>> liste_de_caracteristique, string nom_continent)
    {
        List<Carte> liste_instance = new();
        bool conservable;
        for (int i = 0; i < liste_de_caracteristique.Count; i++)
        {
            if (liste_de_caracteristique[i][6] == "Orthodoxe (Oui)")
            {
                conservable = true;
            }
            else
            {
                conservable = false;
            }
            for (int j = 0; j < int.Parse(liste_de_caracteristique[i][7]); j++)
            {
                Carte une_carte = new Carte(
                liste_de_caracteristique[i][0],
                liste_de_caracteristique[i][2],
                conservable, 
                int.Parse(liste_de_caracteristique[i][7]),
                int.Parse(liste_de_caracteristique[i][8]),
                liste_de_caracteristique[i][9],
                nom_continent);
                liste_instance.Add(une_carte);

            }

        }
        return liste_instance;
    }

    public List<Carte> Creation_carte_defausse(List<List<string>> liste_de_caracteristique, string nom_continent)
    {
        List<Carte> liste_instance = new();
        bool conservable;
        for (int i = 0; i < liste_de_caracteristique.Count; i++)
        {
            if (liste_de_caracteristique[i][6] == "Orthodoxe (Oui)")
            {
                conservable = true;
            }
            else
            {
                conservable = false;
            }
            for (int j = 0; j < 9-int.Parse(liste_de_caracteristique[i][7]); j++)
            {
                Carte une_carte = new Carte(
                liste_de_caracteristique[i][0],
                liste_de_caracteristique[i][2],
                conservable,
                int.Parse(liste_de_caracteristique[i][7]),
                int.Parse(liste_de_caracteristique[i][8]),
                liste_de_caracteristique[i][9],
                nom_continent);
                liste_instance.Add(une_carte);

            }

        }
        return liste_instance;
    }

    public List<Carte_event> Creation_carte_event(List<List<string>> liste_de_caracteristique)
    {
        Debug.Log("Exec");
        Debug.Log(liste_de_caracteristique.Count);
        List<Carte_event> liste_instance = new();
        for (int i = 0; i < liste_de_caracteristique.Count; i++)
        {
            Carte_event une_carte_event = new Carte_event(
                liste_de_caracteristique[i][0],
                liste_de_caracteristique[i][1],
                liste_de_caracteristique[i][2],
                liste_de_caracteristique[i][3],
                liste_de_caracteristique[i][4], 
                liste_de_caracteristique[i][5],
                int.Parse(liste_de_caracteristique[i][7])
                );
            liste_instance.Add(une_carte_event);
        }
        return liste_instance;
    }
    public bool Is_invasif(string carte_a_tester, List<Carte> list_carte_défausse)
    {
        if (list_carte_défausse.Exists(carte => carte.nom == carte_a_tester))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

