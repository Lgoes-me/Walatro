using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PrizeScene : BaseScene<PrizeSceneData>
{
    [field: SerializeField] private Button NextButton { get; set; }
    [field: SerializeField] private List<PrizeItemController> Items { get; set; }
    [field: SerializeField] private Button RerollButton { get; set; }

    private SceneService SceneService { get; set; }
    private GameSaveService GameSaveService { get; set; }
    private ModifierService ModifierService { get; set; }
    private PrizeService PrizeService { get; set; }

    private List<Modifier> Modifiers { get; set; }

    protected override async void Init()
    {
        SceneService = Application.GetService<SceneService>();
        GameSaveService = Application.GetService<GameSaveService>();
        ModifierService = Application.GetService<ModifierService>();
        PrizeService = Application.GetService<PrizeService>();
        
        Modifiers = await PrizeService.GetCurrentPrizeItems(); 

        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => Next().Forget());
        
        for (int i = 0; i < 4; i ++)
        {
            Items[i].Init(Modifiers[i], () => SelectItem(i).Forget());
        }

        RerollButton.onClick.RemoveAllListeners();
        RerollButton.onClick.AddListener(() => RerollItems().Forget());

        Items[^1].gameObject.SetActive(false);
        RerollButton.gameObject.SetActive(false);
    }

    private async UniTask SelectItem(int i)
    {
        //feedback visual
        ModifierService.AddModifier(Modifiers[i]);
        //desativa outros botoes
    }

    private async UniTask RerollItems()
    {
        //load desativando botões
        Modifiers = await PrizeService.GetCurrentPrizeItems(); 
        //rerenderiza botoes
    }

    private async UniTask Next()
    {
        await SceneService.OpenScene(new GameplaySceneData());
    }
}

public class PrizeSceneData : BaseSceneData
{
    public override string SceneName => "Prize";
}