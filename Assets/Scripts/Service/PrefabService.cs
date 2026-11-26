using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrefabService : MonoBehaviour, IService
{
    [field: SerializeField] public GameDiscController GameDisc { get; set; }

    public GameDiscController InstantiateGameDisc(GameDisc gameDisc, GameDiscManager gameDiscManager)
    {
        return Instantiate(
            GameDisc, 
            gameDiscManager.Pivot.position, 
            Quaternion.identity, 
            gameDiscManager.transform).Init(gameDisc, gameDiscManager);
    }
}