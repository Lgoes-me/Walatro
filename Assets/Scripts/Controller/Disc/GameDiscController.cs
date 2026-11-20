using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GameDiscController : MonoBehaviour
{
    [field: SerializeField] private Material Material { get; set; }
    [field: SerializeField] private DecalProjector DecalProjector { get; set; }

    private GameDisc GameDisc { get; set; }

    public GameDiscController Init(GameDisc gameDisc)
    {
        GameDisc = gameDisc;
        
        var instancedMaterial = new Material(Material);
        DecalProjector.material = instancedMaterial;
        DecalProjector.material.SetTexture("Base_Map", GameDisc.Texture); 

        GameDisc.GameDiscTriggered += GiveFeedback;

        return this;
    }

    public void Hide()
    {
        Destroy(DecalProjector.material);

        GameDisc.GameDiscTriggered -= GiveFeedback;
        GameDisc = null;
        
        Destroy(this.gameObject);
    }

    private void GiveFeedback()
    {
        transform.DOShakePosition(0.15f, 0.15f);
    }
}
