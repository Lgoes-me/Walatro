using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class GameDiscManager : MonoBehaviour
{
    [field: SerializeField] public Transform Pivot { get; set; }

    [field: SerializeField] public Transform Hand { get; set; }

    [field: SerializeField] public Transform Insert { get; set; }

    [field: SerializeField] public Transform Discard { get; set; }

    [field: SerializeField] private GameDiscController GameDiscPrefab { get; set; }

    private List<GameDiscController> GameDiscs { get; set; }
    private List<GameDiscController> DiscardedGameDiscs { get; set; }

    private void Start()
    {
        GameDiscs = new List<GameDiscController>();
        DiscardedGameDiscs = new List<GameDiscController>();
    }

    public void ShowDisc(GameDisc gameDisc)
    {
        var gameDiscController = Instantiate(
            GameDiscPrefab, 
            Pivot.position, 
            Quaternion.identity, 
            this.transform)
                .Init(gameDisc, this);
            
        PlaceAtPile(gameDiscController);

        GameDiscs.Add(gameDiscController);
    }

    public async UniTask PlaceAllAtPile()
    {
        for (var i = DiscardedGameDiscs.Count - 1; i >= 0; i--)
        {
            var gameDisc = DiscardedGameDiscs[i];
            gameDisc.GoToPile();

            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
            
            DiscardedGameDiscs.Remove(gameDisc);
            GameDiscs.Add(gameDisc);
        }

        DiscardedGameDiscs.Clear();
    }
    
    public void HideAll()
    {
        foreach(var gameDisc in GameDiscs)
        {
            if (gameDisc == null)
                continue;
            
            gameDisc.Hide();
        }

        foreach(var gameDisc in DiscardedGameDiscs)
        {
            if (gameDisc == null)
                continue;
            
            gameDisc.Hide();
        }

        GameDiscs.Clear();
        DiscardedGameDiscs.Clear();
    }


    public void PlaceAtPile(GameDiscController gameDisc)
    {
        gameDisc.transform.position += GameDiscs.Count * (new Vector3(0, 0.15f, 0));
        gameDisc.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(-23f, 40f), 0f);
    }

    public void PlaceAtDiscard(GameDiscController gameDisc)
    {
        GameDiscs.Remove(gameDisc);
        DiscardedGameDiscs.Add(gameDisc);

        gameDisc.transform.position += DiscardedGameDiscs.Count * (new Vector3(0, 0.15f, 0));
        gameDisc.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(-23f, 40f), 0f);
    }
}
