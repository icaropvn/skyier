
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ToggleSelectionVisual : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler
{
    [SerializeField] private GameObject selectionOutline;

    private void OnEnable()
    {
        bool isSelected = EventSystem.current != null &&
            EventSystem.current.currentSelectedGameObject == gameObject;

        selectionOutline.SetActive(isSelected);
    }

    public void OnSelect(BaseEventData eventData)
    {
        selectionOutline.SetActive(true);
        
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
        {
            SFXManager.Instance.Play(SFXManager.Instance.menuNavigationSound);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selectionOutline.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    private void OnDisable()
    {
        selectionOutline.SetActive(false);
    }

    public void PlayClickSound()
    {
        SFXManager.Instance.Play(SFXManager.Instance.menuConfirmSound);
    }
}
