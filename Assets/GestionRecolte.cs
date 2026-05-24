using UnityEngine;
using System.Collections.Generic;
public class GestionRecolte : MonoBehaviour
{
    public static GestionRecolte instance; 

    public void Awake()
    {
        instance = this;
    }
    public void Glow()
    {  
    var PileEspeces = GameObject.FindGameObjectsWithTag("SpeciesStackContour");
        foreach (var continentactif in PileEspeces)
        {
            Transform parentTransform = continentactif.transform.parent;
            SpeciesStackScript stackScript = parentTransform.GetComponent<SpeciesStackScript>();
            
            if (stackScript != null && stackScript.IsDiscovered)
            {
                    continentactif.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        
    }

    public void Unglow()
    {
    var PileEspeces = GameObject.FindGameObjectsWithTag("SpeciesStackContour");
        foreach (var continentactif in PileEspeces)
        {
            Transform parentTransform = continentactif.transform.parent;
            SpeciesStackScript stackScript = parentTransform.GetComponent<SpeciesStackScript>();

            if (stackScript != null && stackScript.IsDiscovered)
            {
                    continentactif.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
