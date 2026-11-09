using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PrizeScene : BaseScene<PrizeSceneData>
{
    [field: SerializeField] private Button NextButton { get; set; }
    [field: SerializeField] private Button Item1Button { get; set; }
    [field: SerializeField] private Button Item2Button { get; set; }
    [field: SerializeField] private Button Item3Button { get; set; }
    [field: SerializeField] private Button Item4Button { get; set; }
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
        
        Item1Button.onClick.RemoveAllListeners();
        Item1Button.onClick.AddListener(() => SelectItem(0).Forget());
        
        Item2Button.onClick.RemoveAllListeners();
        Item2Button.onClick.AddListener(() => SelectItem(1).Forget());
        
        Item3Button.onClick.RemoveAllListeners();
        Item3Button.onClick.AddListener(() => SelectItem(2).Forget());

        Item4Button.onClick.RemoveAllListeners();
        Item4Button.onClick.AddListener(() => SelectItem(3).Forget());

        RerollButton.onClick.RemoveAllListeners();
        RerollButton.onClick.AddListener(() => RerollItems().Forget());

        Item4Button.gameObject.SetActive(false);
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