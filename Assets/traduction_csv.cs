using System.IO;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using System.Collections.Generic;


public class traduction_csv : MonoBehaviour
{
    public List<string> nom_evenement = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string tableau_evenement =  "Assets/data/tableau_evenement.csv";
        using (StreamReader reader = new StreamReader(tableau_evenement))
        {
            reader.ReadLine(); //Là on lit et on affiche la première ligne où y a les titres
            
            string line;
            while ((line = reader.ReadLine()) != null) //Là on va lire chaque ligne du fichier
            {
                string[] caracteristique_evenement = line.Split('§');
                //print(caracteristique_evenement);
                print($"Nom: {caracteristique_evenement[0]}, Type: {caracteristique_evenement[1]}" +
                    $", Description: {caracteristique_evenement[2]}, Tas de couleur: {caracteristique_evenement[3]}");
                nom_evenement.Add(caracteristique_evenement[0]);
                print(nom_evenement.Count);
            }
            for (int i = 0; i < nom_evenement.Count; i++)
            {
                print(nom_evenement[i]);
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}