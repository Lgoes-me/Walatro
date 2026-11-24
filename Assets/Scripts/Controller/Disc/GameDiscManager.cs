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


    private Dictionary<GameDisc, GameDiscController> GameDiscs { get; set; }

    private GameDiscService GameDiscService { get; set; }

    private void Start()
    {
        GameDiscs = new Dictionary<GameDisc, GameDiscController>();
        GameDiscService = Application.Instance.GetService<GameDiscService>();
    }

    public void ShowDisc(GameDisc gameDisc)
    {
        if (GameDiscs.ContainsKey(gameDisc))
            return;

        var gameDiscController = Instantiate(
            GameDiscPrefab, 
            Pivot.position, 
            Quaternion.identity, 
            this.transform).Init(gameDisc, this);
            
        GameDiscs.Add(gameDisc, gameDiscController);

        PlaceAtPile(gameDiscController);
    }

    public async UniTask PlaceAllAtPile()
    {
        foreach(var gameDisc in GameDiscService.DiscardedGames)
        {
            var gameDiscController = GameDiscs[gameDisc];
            gameDiscController.GoToPile();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        }
    }
    
    public void HideAll()
    {
        foreach(var gameDisc in GameDiscs)
        {
            gameDisc.Value.Hide();
        }
    }


    public void PlaceAtPile(GameDiscController gameDisc)
    {
        gameDisc.transform.position += (GameDiscService.AvailableGames?.Count ?? GameDiscService.Games.Count) * (new Vector3(0, 0.15f, 0));
        gameDisc.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(-23f, 40f), 0f);
    }

    public void PlaceAtDiscard(GameDiscController gameDisc)
    {
        gameDisc.transform.position += GameDiscService.DiscardedGames.Count * (new Vector3(0, 0.15f, 0));
        gameDisc.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(-23f, 40f), 0f);
    }
}
