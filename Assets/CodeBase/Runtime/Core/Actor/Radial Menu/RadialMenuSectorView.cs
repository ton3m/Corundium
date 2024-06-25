using UnityEngine;
using UnityEngine.UI;

public class RadialMenuSectorView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private GameObject _selection;
    
    public void Set(Sprite itemSprite)
    {
        _itemImage.sprite = itemSprite;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        _itemImage.sprite = null;
        gameObject.SetActive(false);
    }

    public void Select() => _selection.SetActive(true);
    
    public void Deselect() => _selection.SetActive(false);
}