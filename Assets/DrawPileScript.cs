using UnityEngine;

public class DrawPileScript : MonoBehaviour
{
    public GameObject DrawPile;
    public SpriteRenderer DrawPileSprite;

    void Awake()
    {
        DrawPileSprite = DrawPile.GetComponent<SpriteRenderer>();
        Sprite loadedSprite = Resources.Load("Cards/Back" + gameObject.name, typeof(Sprite)) as Sprite; // Utiliser le nom "Back[Nom du continent]" pour nom du GameObject.
        DrawPileSprite.sprite = loadedSprite;
    }
}
