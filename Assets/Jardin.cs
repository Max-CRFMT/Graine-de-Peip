using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Jardin 
{
    public string name;
    public int limite_max_jardin = 8;
    public int niveau_jardin = 1;
    public static int fibo1 = 1;
    public static int fibo2 = 1;

    public List<string> liste_biome_jardin;

    public List<Carte> Liste_Carte;

    public bool PresentDansJardin;

    Dictionary<string, List<string>> Dict_Continent_Biomes = new Dictionary<string, List<string>>()
    {
        {"Europe", new List<string>(){"Forêt tempérée"} },
        {"Afrique", new List<string>(){"Brousse", "Désert"} },
        {"Asie", new List<string>(){"Forêt pluvieuse"} },
        {"Oceanie", new List<string>(){"Désert", "Prairies"} },
        {"Amerique du Sud", new List<string>(){"Forêt pluvieuse"} },
        {"Amerique du Nord", new List<string>(){"Forêt de conifères"} },
    };

    public Jardin(string nom_continent)
    {
        name = nom_continent;
        if (liste_biome_jardin == null)  //Pour pas r�initialiser la liste_biome_jardin
        {
            liste_biome_jardin = new List<string>(Dict_Continent_Biomes[nom_continent]);
        }
        Liste_Carte = new List<Carte>();
    }

    public void AjouterCarteDansJardin(Carte carte_a_ajoute)
    {
        Liste_Carte.Add(carte_a_ajoute);
    }

    public void EnableButton()
    {
        for (int i = 1; i != niveau_jardin + 1; i++)
        {
            string obj = "Jardin" + i.ToString();
            var go = GameObject.Find(obj).gameObject;
            go.GetComponent<UnityEngine.UI.Button>().enabled = true;
        }
    }

    public void DisableButton()
    {
        for (int i = 1; i != niveau_jardin + 1; i++)
        {
            string obj = "Jardin" + i.ToString();
            var go = GameObject.Find(obj).gameObject;
            go.GetComponent<UnityEngine.UI.Button>().enabled = false;
        }
    }

    public void UpdateSpriteJardin()
    {
        for (int i = 1; i != 9; i++)
        {
            string obj = "Jardin" + i.ToString();
            if (Liste_Carte.Count() >= i)
            {
                GameObject.Find(obj).gameObject.GetComponent<GestionInteractionJardin>().carte_contenue = Liste_Carte[i - 1];
            } else
            {
                GameObject.Find(obj).gameObject.GetComponent<GestionInteractionJardin>().carte_contenue = null;
            }

            GameObject.Find(obj).gameObject.GetComponent<GestionInteractionJardin>().MiseAJourUI();
            var go = GameObject.Find(obj).gameObject;
            go.GetComponent<UnityEngine.UI.Button>().enabled = true;
        }
    }

    public bool VerifierPresenceDansJardin(Carte carte_a_verifier)
    {
        PresentDansJardin = false;
        foreach (Carte carte in Liste_Carte)
        {
            if (carte.nom == carte_a_verifier.nom)
            {
                PresentDansJardin = true;
            }
        }
        return PresentDansJardin;
    }

    
    public void Amelioration_niveau_jardin()
    {
        if (niveau_jardin < limite_max_jardin)
            {
                niveau_jardin = fibo1 + fibo2;
                fibo1 = fibo2;
                fibo2 = niveau_jardin;
                Debug.Log("Amélioration effectuée, le jardin peut maitenant accueillir " + niveau_jardin + " plantes.");
                TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
                ChangementUITextJoueur.instance.ChangerChangementJoueur();
            }
        else
            {
                Debug.Log("Niveau maximal atteint");
            }
    }

    public void Ajout_biome_jardin(string biome)
    {
        Debug.Log("Le biome " + biome + " a été ajouté à la liste des biomes du jardin.");
        liste_biome_jardin.Add(biome);
        TurnHandler.instance.PlayerActuel.RetirerPointAction(1);
        ChangementUITextJoueur.instance.ChangerChangementJoueur();
    }
}
