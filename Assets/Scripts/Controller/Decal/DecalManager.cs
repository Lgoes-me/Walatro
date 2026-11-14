using System.Collections.Generic;
using UnityEngine;

public class DecalManager : MonoBehaviour
{
    [field: SerializeField] private List<DecalController> Decals { get; set; }

    public bool ShowDecal(Modifier modifier)
    {
        var decals = new List<DecalController>();

        foreach(var decal in Decals)
        {
            if (decal.gameObject.activeInHierarchy)
                continue;

            decals.Add(decal);
        }

        if (decals.Count == 0)
        {
            return false;
        }

        decals[Random.Range(0, decals.Count)].Show(modifier);
        return true;
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
