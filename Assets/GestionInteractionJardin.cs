using UnityEngine;
using UnityEngine.UI;

public class GestionInteractionJardin : MonoBehaviour
{
    public Carte carte_contenue;
    int indexCarte;
    bool contientCarte = false;
    Player aLaCarte;

    public void JardinCliqued()
    {
        if (TurnHandler.instance.recolte_pioche_en_cours && carte_contenue == null)
        {
            GestionPostRecolte.instance.PiocheToJardinOuPiocheToBanque = 'J';
            GestionPostRecolte.instance.jardin_cible = gameObject;
            GestionPostRecolte.instance.AjoutConstantesEctAuTurnHandlerEtCloture();
        }
        else if (TurnHandler.instance.recolte_en_cours && GestionPostRecolte.instance.PiocheOuBanque == 'B' && carte_contenue == null)
        {
            GestionPostRecolte.instance.BanqueReloadOuBanqueToJardin = 'J';
            GestionPostRecolte.instance.jardin_cible = gameObject;
            GestionPostRecolte.instance.AjoutConstantesEctAuTurnHandlerEtCloture();
        }
        else if (TurnHandler.instance.PlayerActuel.restaurationEnCours)
        {
            int prixAction = 3;
            if (carte_contenue.continent_name == TurnHandler.instance.PlayerActuel.continent.name)
            {
                prixAction = 2;
            }
            if (TurnHandler.instance.PlayerActuel.VerifMontant(prixAction))
            {
                foreach (Player p in GameObject.Find("GameLogic").GetComponent<GameLogic>().Liste_Joueurs)
                {
                    for (int i = 0; i < p.continent.defausse.Count; i++)
                    {
                        if (p.continent.defausse[i].nom == carte_contenue.nom)
                        {
                            contientCarte = true;
                            aLaCarte = p;
                            indexCarte = i;
                        }
                    }
                }
                if (contientCarte)
                {
                    GameObject.Find(GameObject.Find("GameLogic").GetComponent<GameLogic>().Dico_NomCarte_Attache[aLaCarte.continent.defausse[indexCarte].nom]).GetComponent<SpeciesStackScript>().Ajouter(aLaCarte.continent.defausse[indexCarte]);
                    GameObject.Find(GameObject.Find("GameLogic").GetComponent<GameLogic>().Dico_NomCarte_Attache[aLaCarte.continent.defausse[indexCarte].nom]).GetComponent<SpeciesStackScript>().IncreaseCardAmount();
                    aLaCarte.continent.defausse.RemoveAt(indexCarte);
                    Debug.Log("Carte restauree !");
                }
                else
                {
                    Debug.Log("Y a pas ce qui faut dans la defausse");
                }
            }
            else
            {
                Debug.Log("Montant non acquis, action annulée, cheh");
            }
            TurnHandler.instance.PlayerActuel.FinRestauration();
        }
    }

    public void MiseAJourUI()
    {
        if (carte_contenue != null)
        {
            var go = gameObject;
            Sprite new_sprite = Resources.Load<Sprite>(carte_contenue.PetitPathImage);
            Image image = go.GetComponent<Image>();
            image.sprite = new_sprite;
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Visible;
        } else if (carte_contenue == null)
        {
            var go = gameObject;
            Image image = go.GetComponent<Image>();
            image.color = TurnHandler.instance.PlayerActuel.continent.banque.Invisible;
        }
    }
}
