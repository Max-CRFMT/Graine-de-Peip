using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    Dictionary<string, int> DicoTourEnFctDeDifficulte = new Dictionary<string, int>()
    {
        {"Facile",7},
        {"Normal", 14},
        {"Difficile", 21}
    };

    public GameLogic() { }

    private void Awake()
    {
        instance = this;
        instance.nb_joueurs = 2;
        instance.difficulte = "Facile";
        instance.Liste_Joueurs = new List<Player>();
        instance.ToursRestants = DicoTourEnFctDeDifficulte[difficulte];
        instance.partiefinie = false;
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

    public List<string> SelectionMaps = new List<string>() { "Europe", "Afrique", "Asie", "Oceanie", "Amerique du Nord", "Amerique du Sud" };
    public void SetListeJoueurs()
    {
        System.Random random = new System.Random();
        List<(string, string)> ListeJoueursAttente = new List<(string, string)>();

        for (int i = 0; i < instance.nb_joueurs; i++)
        {
            string nom_a_trouver = "Joueur" + (i+1).ToString();
            GameObject[] couple_nom_map = GameObject.FindGameObjectsWithTag(nom_a_trouver);
            string nom_joueur = couple_nom_map[0].GetComponent<TMP_InputField>().text;
            string map_joueur = couple_nom_map[1].GetComponent<TMP_Dropdown>().options[couple_nom_map[1].GetComponent<TMP_Dropdown>().value].text;

            if (map_joueur == "Aléatoire")
            {
                ListeJoueursAttente.Add((nom_joueur, map_joueur));
            }
            else
            {
                instance.Liste_Joueurs.Add(new Player(nom_joueur, 0, map_joueur));
                SelectionMaps.Remove(map_joueur);
            }
        }

        foreach ((string, string) couple in ListeJoueursAttente)
        {
            string map_joueur = SelectionMaps[random.Next(SelectionMaps.Count)];
            SelectionMaps.Remove(map_joueur);
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
        StartCoroutine(Jeu());
    }

    public void SupprimerGameObjectSelonTag(string tag)
    {
        foreach (var objects in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(objects);
        }
    }

    public IEnumerator Jeu()
    {
        AsyncOperation ChargenementScene = SceneManager.LoadSceneAsync("Game");

        yield return ChargenementScene;

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
                Carte une_carte = new Carte(liste_de_caracteristique[i][0], liste_de_caracteristique[i][2], conservable, int.Parse(liste_de_caracteristique[i][8]), nom_continent);
                liste_instance.Add(une_carte);

            }

        }
        return liste_instance;
    }
    public void ShuffleListeJoueur(List<Player> ts)
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

    public void ShuffleListeCartes(List<Carte> ts)
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
}

