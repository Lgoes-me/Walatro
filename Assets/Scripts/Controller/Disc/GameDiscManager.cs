using System.Collections.Generic;
using UnityEngine;

public class GameDiscManager : MonoBehaviour
{
    [field: SerializeField] private Transform Pivot { get; set; }
    [field: SerializeField] private GameDiscController GameDiscPrefab { get; set; }

    private List<GameDiscController> GameDiscs { get; set; }

    public void ShowDisc(GameDisc gameDisc)
    {
        GameDiscs ??= new List<GameDiscController>();

        var gameDiscController = Instantiate(
            GameDiscPrefab, 
            Pivot.position + GameDiscs.Count * (new Vector3(0, 0.15f, 0)), 
            Quaternion.Euler(0f, Random.Range(-23f, 40f), 0f), this.transform)
                .Init(gameDisc);

        GameDiscs.Add(gameDiscController);
    }

    public void HideAll()
    {
        foreach(var gameDisc in GameDiscs)
        {
            gameDisc.Hide();
        }
    }
}
