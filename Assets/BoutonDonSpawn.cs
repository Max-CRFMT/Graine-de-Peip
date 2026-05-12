using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BoutonDonSpawn : MonoBehaviour
{
    public GameObject bouton;
    public Transform CanvasTransform;
    public RectTransform SpawnBoutonDON;
    public int espace = 40;
    private List<GameObject> boutonsCrees = new List<GameObject>();
    [ContextMenu("Apparaition")]
    public void AparaitionBouton()
    {
        int nb_bouton = GameLogic.instance.Liste_Joueurs.Count;
        int temp = espace;
        for (int i = 0; i < nb_bouton; i++) 
        {
            bouton.tag = "Joueur" + (i + 1).ToString();
            GameObject nouveauBouton = Instantiate(bouton, CanvasTransform);
            boutonsCrees.Add(nouveauBouton);
            TMP_Text nom_joueur = nouveauBouton.GetComponentInChildren<TMP_Text>();
            nom_joueur.text = GameLogic.instance.Liste_Joueurs[i].pseudo;
            RectTransform rt = nouveauBouton.GetComponent<RectTransform>();
            rt.anchoredPosition = SpawnBoutonDON.anchoredPosition;

            rt.anchoredPosition = new Vector2(SpawnBoutonDON.anchoredPosition.x,espace);
            
            espace = espace - temp;
        }
    }
    [ContextMenu("Destroy")]
    public void DestroyBouton()
    {
        foreach (GameObject boutons in boutonsCrees)
        {
            Destroy(boutons);
        }
        boutonsCrees.Clear();
        espace = 40;
    }
    
    [ContextMenu("AjouterThunes")]
    public void Test()
    {
        TurnHandler.instance.PlayerActuel.pieces += 10;
        Debug.Log(TurnHandler.instance.PlayerActuel.pieces);
        Debug.Log(TurnHandler.instance.PlayerActuel);
    }
}
