using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Jardin
{
    public string name;
    public int limite_max_jardin = 8;
    public static int niveau_jardin = 1;
    public static int fibo1 = 0;
    public static int fibo2 = 1;
    public static List<string> liste_biome_jardin;

    GameObject Jardin1;
    GameObject Jardin2;
    GameObject Jardin3;
    GameObject Jardin4;
    GameObject Jardin5;
    GameObject Jardin6;
    GameObject Jardin7;
    GameObject Jardin8;

    public List<Carte> Liste_Carte;

    public bool PresentDansJardin;

    Dictionary<string, List<string>> Dict_Continent_Biomes = new Dictionary<string, List<string>>()
    {
        {"Europe", new List<string>(){"Bretagne", "Paris"} },
        {"Afrique", new List<string>(){"Mali", "Maroc"} },
        {"Asie", new List<string>(){"Japon", "Coree"} },
        {"Oceanie", new List<string>(){"Iles", "Australie" } },
        {"Amerique du Sud", new List<string>(){"Bresil", "Argentine"} },
        {"Amerique du Nord", new List<string>(){"Canada", "US" } },
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
        for (int i = 1; i != niveau_jardin+1; i++)
        {
            string obj = "Jardin" + i.ToString();
            Debug.Log(obj);
            if (Liste_Carte.Count() >= i)
            {
                Debug.Log("oui");
                GameObject.Find(obj).gameObject.GetComponent<GestionInteractionJardin>().carte_contenue = Liste_Carte[i - 1];
            } else
            {
                GameObject.Find(obj).gameObject.GetComponent<GestionInteractionJardin>().carte_contenue = null;
            }
            Debug.Log("non");

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

    public void Amelioration_du_jardin()
    {
        if (niveau_jardin < limite_max_jardin)
        {
            niveau_jardin = fibo1 + fibo2;
            fibo1 = fibo2;
            fibo2 = niveau_jardin;
            Debug.Log(niveau_jardin);
        }
        else
        {
            Debug.Log("Niveau maximal atteint");
        }
    }

    public void Ajout_un_biome_au_jardin(string nom_biome)
    {
        liste_biome_jardin.Add(nom_biome);
        //string message = string.Join(",", liste_biome_jardin);
        //Debug.Log(message);
    }
}
