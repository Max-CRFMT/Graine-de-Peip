using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class script_valider : MonoBehaviour
{
    // Définition des préfabs
    public Canvas canvas;
    public GameObject txt_prefab;
    public GameObject BoutonAleatoire_prefab;
    public GameObject SelectionCarte_prefab;

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

        foreach (var objects in GameObject.FindGameObjectsWithTag("Suppr"))
        {
            Destroy(objects);
        }

        List<Vector3> Liste_Position = new List<Vector3>();

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 Position = new Vector3(xmin+(intervalle_x*i), ymax-(intervalle_y*j) , 0);

                Liste_Position.Add(Position);
                Debug.Log(Position);
            }
        }

        

        for (int joueur = 0; joueur < GameLogic.instance.nb_joueurs; joueur++)
        {
            GameObject prefab = Instantiate(txt_prefab, canvas.transform);
            RectTransform rt = prefab.GetComponent<RectTransform>();
            rt.anchoredPosition = Liste_Position[joueur];

        }
    }
}
