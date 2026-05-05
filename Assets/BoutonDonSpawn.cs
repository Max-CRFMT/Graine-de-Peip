using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;

public class BoutonDonSpawn : MonoBehaviour
{
    public GameObject bouton;
    public Transform CanvasTransform;
    public RectTransform SpawnBoutonDON;
    public int espace = 40;
    [ContextMenu("Apparaition")]
    public void AparaitionBouton()
    {
        int nb_bouton = GameLogic.instance.Liste_Joueurs.Count;
        int temp = espace;
        for (int i = 0; i < nb_bouton; i++) 
        {
            bouton.tag = "Joueur" + (i + 1).ToString();
            GameObject nouveauBouton = Instantiate(bouton, CanvasTransform);
            GameObject.FindWithTag(bouton.tag);
            TMP_Text nom_joueur = nouveauBouton.GetComponentInChildren<TMP_Text>();
            nom_joueur.text = GameLogic.instance.Liste_Joueurs[i].pseudo;
            RectTransform rt = nouveauBouton.GetComponent<RectTransform>();
            rt.anchoredPosition = SpawnBoutonDON.anchoredPosition;

            rt.anchoredPosition = new Vector2(SpawnBoutonDON.anchoredPosition.x,espace);
            
            espace = espace - temp;
        }
    }
}
