using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrefabService : MonoBehaviour, IService
{
    [field: SerializeField] private GameDiscController GameDisc { get; set; }
    [field: SerializeField] private GameObject ModifierPrize { get; set; }

    public GameDiscController InstantiateGameDisc(GameDisc gameDisc, Transform pivot, Transform parent)
    {
        return Instantiate(
            GameDisc, 
            pivot.position, 
            Quaternion.identity, 
            parent).Init(gameDisc);
    }
    
    public GameObject InstantiateModifierPrize(Modifier modifier, Transform parent)
    {
        return Instantiate(
            ModifierPrize, 
            parent.position, 
            Quaternion.identity, 
            parent);
    }
}