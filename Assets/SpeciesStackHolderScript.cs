using UnityEngine;

public class SpeciesStackHolderScript : MonoBehaviour
{
    public GameObject SpeciesStack; //On dis quel genre d'entité notre "holder" va faire apparaitre
    public int SpeciesStackAmount;
    public UnityEngine.Vector3 SpeciesStackOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpeciesStackOffset = transform.position;
        for (int i = 0; i < SpeciesStackAmount; i++)
        {
            Instantiate(SpeciesStack, SpeciesStackOffset, transform.rotation);
            SpeciesStackOffset += Vector3.right * 3;
        }   
    }

    // Update is called once per frame
    void Update()
    {

    }
}
