using System.Collections.Generic;
using UnityEngine;

public class GameDiscManager : MonoBehaviour
{
    [field: SerializeField] public Transform Pivot { get; set; }

    [field: SerializeField] public Transform Hand { get; set; }

    [field: SerializeField] public Transform Insert { get; set; }

    [field: SerializeField] public Transform Discard { get; set; }

    [field: SerializeField] private GameDiscController GameDiscPrefab { get; set; }

    private List<GameDiscController> GameDiscs { get; set; }
    private List<GameDiscController> DiscardedGameDiscs { get; set; }

    public void ShowAll()
    {
        //todo melhorar para nao ficar criando e destruindo varias vezes
        HideAll();

        var box = Application.Instance.GetService<GameBoxService>().GameBox;

        foreach(var game in box.Games)
        {
            ShowDisc(game);
        }
    }

    public void ShowDisc(GameDisc gameDisc)
    {
        GameDiscs ??= new List<GameDiscController>();

        var gameDiscController = Instantiate(
            GameDiscPrefab, 
            Pivot.position, 
            Quaternion.identity, 
            this.transform)
                .Init(gameDisc, this);

        PlaceAtPile(gameDiscController);

        GameDiscs.Add(gameDiscController);
    }

    public void HideAll()
    {
        foreach(var gameDisc in GameDiscs)
        {
            if (gameDisc == null)
                continue;
            
            gameDisc.Hide();
        }

        GameDiscs.Clear();
    }

    public void PlaceAtPile(GameDiscController gameDisc)
    {
        gameDisc.transform.position += GameDiscs.Count * (new Vector3(0, 0.15f, 0));
        gameDisc.transform.rotation = Quaternion.Euler(0f, Random.Range(-23f, 40f), 0f);
    }

    public void PlaceAtDiscard(GameDiscController gameDisc)
    {
        DiscardedGameDiscs ??= new List<GameDiscController>();

        GameDiscs.Remove(gameDisc);
        DiscardedGameDiscs.Add(gameDisc);

        gameDisc.transform.position += DiscardedGameDiscs.Count * (new Vector3(0, 0.15f, 0));
        gameDisc.transform.rotation = Quaternion.Euler(0f, Random.Range(-23f, 40f), 0f);
    }
}
