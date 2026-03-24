using UnityEngine;

public class SpeciesStackHolderScript : MonoBehaviour
{
    public GameObject SpeciesStackHolder;
    public GameObject SpeciesStack; //On dis quel genre d'entité notre "holder" va faire apparaitre
    public int SpeciesStackAmount;
    public UnityEngine.Vector3 SpeciesStackOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); //On change l'échelle du holder pour que les SpeciesStack générés soit plus petits.
        SpeciesStackOffset = transform.position;    
        for (int i = 0; i < SpeciesStackAmount; i++)
        {
            Instantiate(SpeciesStack, SpeciesStackOffset, transform.rotation, this.transform);
            SpeciesStack.layer = 4;
            SpeciesStackOffset += Vector3.right * 2 * transform.localScale.x; // On décale vers la droite de 2 multiplié par la scale du holder pour conserver l'écart
        }   
    }
}
