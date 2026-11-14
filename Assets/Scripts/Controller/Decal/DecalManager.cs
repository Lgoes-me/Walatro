using System.Collections.Generic;
using UnityEngine;

public class DecalManager : MonoBehaviour
{
    [field: SerializeField] private List<DecalController> Decals { get; set; }

    public bool ShowDecal(Modifier modifier)
    {
        foreach(var decal in Decals)
        {
            if (decal.gameObject.activeInHierarchy)
                continue;

            decal.Show(modifier);
            return true;
        }

        return false;
    }

    public void HideAll()
    {
        foreach(var decal in Decals)
        {
            if (!decal.gameObject.activeInHierarchy)
                continue;

            decal.Hide();
        }
    }
}
