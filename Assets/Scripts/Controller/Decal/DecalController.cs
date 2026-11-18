using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DecalController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] private Material Material { get; set; }
    [field: SerializeField] private DecalProjector DecalProjector { get; set; }

    private Modifier Modifier { get; set; }
    
    public void Show(Modifier modifier)
    {
        Modifier = modifier;
        
        var instancedMaterial = new Material(Material);
        DecalProjector.material = instancedMaterial;
        DecalProjector.material.SetTexture("Base_Map", Modifier.Texture); 

        Modifier.ModifierTriggered += GiveFeedback;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        Destroy(DecalProjector.material);

        Modifier.ModifierTriggered -= GiveFeedback;
        Modifier = null;
    }

    private void GiveFeedback()
    {
        transform.DOShakePosition(0.15f, 0.15f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(this.gameObject, Modifier, Modifier.Rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }

    private void OnDisable()
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }
}
