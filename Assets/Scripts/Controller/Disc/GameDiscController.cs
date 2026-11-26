using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameDiscController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private Material DiscMaterial { get; set; }
    [field: SerializeField] private Material DecalMaterial { get; set; }
    [field: SerializeField] private MeshRenderer MeshRenderer { get; set; }

    private GameDisc GameDisc { get; set; }
    private bool Selectable { get; set; }
    
    private static readonly int BaseMapID = Shader.PropertyToID("_BaseMap");

    public GameDiscController Init(GameDisc gameDisc)
    {
        GameDisc = gameDisc;
        Selectable = false;
        
        var instancedDiscMaterial = new Material(DiscMaterial);

        var instancedDecalMaterial = new Material(DecalMaterial);
        instancedDecalMaterial.SetTexture(BaseMapID, GameDisc.Texture); 

        var materials = new Material[2]
         {
            instancedDiscMaterial,  
            instancedDecalMaterial
        };

        MeshRenderer.materials = materials;
        GameDisc.GameDiscTriggered += GiveFeedback;

        return this;
    }

    public void Hide()
    {
        foreach(var material in MeshRenderer.materials)
        {
            Destroy(material);
        }

        GameDisc.GameDiscTriggered -= GiveFeedback;
        GameDisc = null;
        
        Destroy(this.gameObject);
    }

    private void GiveFeedback()
    {
        transform.DOShakePosition(0.15f, 0.15f);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Selectable || GameDisc.Modifier is DiscModifier.None)
            return;
        
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(this.gameObject, GameDisc, GameDisc.Rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        if (!Selectable || GameDisc.Modifier is DiscModifier.None)
            return;
            
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }

    private void OnDestroy()
    {
        Selectable = false;
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }
}
