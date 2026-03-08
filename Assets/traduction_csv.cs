using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;


public class traduction_csv : MonoBehaviour
{
    public List<List<string>> carte_evenement = new List<List<string>>();
    public int nombre_de_caractéristique_de_la_carte = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string tableau_evenement =  "Assets/data/tableau_evenement.csv";
        using (StreamReader reader = new StreamReader(tableau_evenement))
        {
            reader.ReadLine(); //Là on lit la première ligne où y a les titres pour pouvoir l'ignorer 
            int indice = 0;
            string lecteur_de_ligne;
            while ((lecteur_de_ligne = reader.ReadLine()) != null) //Là on va lire chaque ligne du fichier
            {
                string[] ligne_découper = lecteur_de_ligne.Split('|');
                List<string> ligne = new List<string>();
                ligne.AddRange(ligne_découper);
                if (ligne.Count > nombre_de_caractéristique_de_la_carte)
                {
                    ligne.RemoveRange(nombre_de_caractéristique_de_la_carte,ligne.Count - nombre_de_caractéristique_de_la_carte);
                    carte_evenement.Add(ligne);
                }
                else
                {
                    carte_evenement.Add(ligne);
                }
                print(carte_evenement[indice][3]);
                indice += 1;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}