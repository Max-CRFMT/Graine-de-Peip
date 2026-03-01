using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class script_valider : MonoBehaviour
{
    // Définition des préfabs
    public Canvas canvas;
    public GameObject txt_prefab;
    public GameObject SelectionCarte_prefab;
    public GameObject Bouton_Retour;
    public GameObject Bouton_Demarrer;
    //Définition des distances constantes
    public int xmin;
    public int ymax;

    public int intervalle_x;
    public int intervalle_y;

    public void Valider()
    {
        //Change cette "boite de dialogue" en une interface pour rentrer le nom des diff�rents joueurs

        intervalle_x = 600;
        intervalle_y = 250;
        xmin = -600;
        ymax = 100;

        GameLogic.instance.SupprimerGameObjectSelonTag("Suppr");

        List<Vector3> Liste_Position = new List<Vector3>();

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 Position = new Vector3(xmin+(intervalle_x*i), ymax-(intervalle_y*j) , 0);

                Liste_Position.Add(Position);
            }
        }

        
        //Création tableau noms et séléction carte
        for (int joueur = 0; joueur < GameLogic.instance.nb_joueurs; joueur++)
        {
            //Création des endroits pour rentrer les prénoms en mettant le nom du tag comme nom par défaut
            GameObject prefab = Instantiate(txt_prefab, canvas.transform);
            RectTransform rt = prefab.GetComponent<RectTransform>();
            rt.anchoredPosition = Liste_Position[joueur];
            prefab.tag = "Joueur" + (joueur + 1).ToString();
            prefab.GetComponent<TMP_InputField>().text = prefab.tag.ToString();

            //Création des listes déroulantes pour sélectionner les maps
            GameObject prefab_select = Instantiate(SelectionCarte_prefab, canvas.transform);
            RectTransform rt3 = prefab_select.GetComponent<RectTransform>();
            Vector3 posistion_supplementaire2 = new Vector3(0, -60, 0);
            rt3.anchoredPosition = Liste_Position[joueur]+posistion_supplementaire2;
            prefab_select.tag = "Joueur" + (joueur+1).ToString();

        }
        GameObject prefab_retour = Instantiate(Bouton_Retour, canvas.transform);

        GameObject prefab_demarrer = Instantiate(Bouton_Demarrer, canvas.transform);

    }
}