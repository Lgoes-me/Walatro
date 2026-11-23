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
    private GameDiscManager GameDiscManager { get; set; }
    private bool Selectable { get; set; }
    
    private static readonly int BaseMapID = Shader.PropertyToID("_BaseMap");

    public GameDiscController Init(GameDisc gameDisc, GameDiscManager gameDiscManager)
    {
        GameDisc = gameDisc;
        GameDiscManager = gameDiscManager;
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
        GameDisc.GameDiscChosen += MoveToPosition;
        GameDisc.GameDiscComplete += MoveAway;

        return this;
    }

    public void Hide()
    {
        foreach(var material in MeshRenderer.materials)
        {
            Destroy(material);
        }

        GameDisc.GameDiscTriggered -= GiveFeedback;
        GameDisc.GameDiscChosen -= MoveToPosition;
        GameDisc.GameDiscComplete -= MoveAway;
        GameDisc = null;
        
        Destroy(this.gameObject);
    }

    private void GiveFeedback()
    {
        transform.DOShakePosition(0.15f, 0.15f);
    }

    private void MoveToPosition()
    {
        Selectable = true;
        
        var transformHand = GameDiscManager.Hand;
        var transformInsert = GameDiscManager.Insert;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transformHand.position, 1f));
        seq.Join(transform.DORotate(transformHand.eulerAngles, 1f));
        seq.Join(transform.DOScale(transformHand.localScale, 1f));
        seq.Append(transform.DOMove(transformInsert.position, 1f));
        seq.Join(transform.DORotate(transformInsert.eulerAngles, 1f));
        seq.Join(transform.DOScale(transformInsert.localScale, 1f));

        seq.Play();
    }

    private void MoveAway()
    {
        Selectable = false;
        
        var transformHand = GameDiscManager.Hand;
        var transformDiscard = GameDiscManager.Discard;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transformHand.position, 0.5f));
        seq.Append(transform.DOMove(transformDiscard.position, 0.5f).OnComplete(() => GameDiscManager.PlaceAtDiscard(this)));

        seq.Play();
    }

    public void GoToPile()
    {
        var transformHand = GameDiscManager.Hand;
        var transformPivot = GameDiscManager.Pivot;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transformHand.position, 0.5f));
        seq.Append(transform.DOMove(transformPivot.position, 0.5f).OnComplete(() => GameDiscManager.PlaceAtPile(this)));

        seq.Play();
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Selectable || GameDisc.Modifier is DiscModifier.None)
            return;
        
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Show(this.gameObject, GameDisc, GameDisc.Rarity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }

    private void OnDestroy()
    {
        Selectable = false;
        Application.Instance.GetService<InterfaceService>().InfoDescriptor.Hide();
    }
}
