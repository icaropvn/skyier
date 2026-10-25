using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonVisuals : MonoBehaviour,
    ISelectHandler, IDeselectHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color selectedColor = Color.black;

    [Header("Scale")]
    public float selectedScale = 1.05f;

    private Vector3 originalScale;
    private bool mouseOver;
    private bool isSelected;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void UpdateVisuals()
    {
        bool highlighted = isSelected || (mouseOver && Cursor.visible);

        buttonText.color = highlighted ? selectedColor : normalColor;

        transform.localScale = highlighted
            ? originalScale * selectedScale
            : originalScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        SFXManager.Instance.Play(SFXManager.Instance.menuNavigationSound);
        UpdateVisuals();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        UpdateVisuals();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Cursor.visible)
            return;

        mouseOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
        UpdateVisuals();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        UpdateVisuals();
    }

    private void OnDisable()
    {
        mouseOver = false;
        isSelected = false;
        transform.localScale = originalScale;
        buttonText.color = normalColor;
    }

    public void PlayClickSound()
    {
        SFXManager.Instance.Play(SFXManager.Instance.menuConfirmSound);
    }
}
