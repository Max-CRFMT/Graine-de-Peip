using Unity.VisualScripting;
using UnityEngine;

public class PileEspece //Classe qui gère la pile d'une espèce. Avec une quantité de carte et une image
{
    public PileEspece(string nom)
    {
        int quantity = 0;
        string ImagePath = Application.persistentDataPath + "\assets ou un truc du genre" + nom + ".png"; 
        bool isDiscovered = false;
    }

    //TODO - Carte visible mais grisée si quantity = 0. Socket invisible si carte pas découverte.
}
