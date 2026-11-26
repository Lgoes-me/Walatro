using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class GameDiscManager : MonoBehaviour
{
    [field: SerializeField] public Transform Pivot { get; set; }
    [field: SerializeField] public Transform Hand { get; set; }
    [field: SerializeField] public Transform Insert { get; set; }
    [field: SerializeField] public Transform Discard { get; set; }

    private Dictionary<GameDisc, GameDiscController> GameDiscs { get; set; }
    
    private GameDiscService GameDiscService { get; set; }
    private PrefabService PrefabService { get; set; }

    private void Start()
    {
        GameDiscs = new Dictionary<GameDisc, GameDiscController>();
        GameDiscService = Application.Instance.GetService<GameDiscService>();
        PrefabService = Application.Instance.GetService<PrefabService>();
    }

    public void ShowDisc(GameDisc gameDisc)
    {
        if (GameDiscs.ContainsKey(gameDisc))
            return;

        var gameDiscController = PrefabService.InstantiateGameDisc(gameDisc, Pivot, this.transform);
            
        GameDiscs.Add(gameDisc, gameDiscController);

        PlaceAtPile(gameDiscController);
    }

    public async UniTask PlaceAllAtPile()
    {
        foreach(var gameDisc in GameDiscService.DiscardedGames)
        {
            SendToPile(gameDisc);
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

    public void MoveToPosition(GameDisc gameDisc)
    {        
        Sequence seq = DOTween.Sequence();

        var gameDiscController = GameDiscs[gameDisc];

        seq.Append(gameDiscController.transform.DOMove(Hand.position, 1f));
        seq.Join(gameDiscController.transform.DORotate(Hand.eulerAngles, 1f));
        seq.Join(gameDiscController.transform.DOScale(Hand.localScale, 1f));
        seq.Append(gameDiscController.transform.DOMove(Insert.position, 1f));
        seq.Join(gameDiscController.transform.DORotate(Insert.eulerAngles, 1f));
        seq.Join(gameDiscController.transform.DOScale(Insert.localScale, 1f));

        seq.Play();
    }

    public void MoveAway(GameDisc gameDisc)
    {
        Sequence seq = DOTween.Sequence();
        
        var gameDiscController = GameDiscs[gameDisc];

        seq.Append(gameDiscController.transform.DOMove(Hand.position, 0.5f));
        seq.Append(gameDiscController.transform.DOMove(Discard.position, 0.5f).OnComplete(() => PlaceAtDiscard(gameDiscController)));

        seq.Play();
    }

    private void SendToPile(GameDisc gameDisc)
    {
        Sequence seq = DOTween.Sequence();
        
        var gameDiscController = GameDiscs[gameDisc];

        seq.Append(gameDiscController.transform.DOMove(Hand.position, 0.5f));
        seq.Append(gameDiscController.transform.DOMove(Pivot.position, 0.5f).OnComplete(() => PlaceAtPile(gameDiscController)));

        seq.Play();
    }
}
