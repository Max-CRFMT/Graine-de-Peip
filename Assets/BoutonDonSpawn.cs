using UnityEngine;
using System.Collections;

public class BoutonDonSpawn : MonoBehaviour
{
    public GameObject bouton;
    public Transform CanvasTransform;
    public RectTransform SpawnBoutonDON;
    public int espace;
    [ContextMenu("Apparaition")]
    public void AparaitionBouton()
    {
        int nb_bouton = 5;
        int temp = espace;
        for (int i = 0; i < nb_bouton; i++) 
        {
            GameObject nouveauBouton = Instantiate(bouton, CanvasTransform);
            RectTransform rt = nouveauBouton.GetComponent<RectTransform>();
            rt.anchoredPosition = SpawnBoutonDON.anchoredPosition;

            rt.anchoredPosition = new Vector2(SpawnBoutonDON.anchoredPosition.x,espace);
            
            espace = espace - temp;
        }
    }
}
