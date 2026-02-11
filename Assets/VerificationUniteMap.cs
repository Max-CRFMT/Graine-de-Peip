using UnityEngine;

public class VerificationUniteMap : MonoBehaviour
{
    public TMPro.TMP_Dropdown DropdownSource;
    public void VerifierUniciteMap()
    {
        GameLogic.instance.VerifUniteMap(DropdownSource);
    }
}
