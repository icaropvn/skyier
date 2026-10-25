using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageModalController : MonoBehaviour
{
    [Header("Modal")]
    [SerializeField] private GameObject modalRoot;
    [SerializeField] private Selectable defaultSelection;

    [Header("Background")]
    [SerializeField] private CanvasGroup backgroundCanvasGroup;

    private GameObject previousSelection;

    public void Open()
    {
        if (modalRoot.activeSelf)
            return;

        previousSelection = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);

        backgroundCanvasGroup.interactable = false;
        backgroundCanvasGroup.blocksRaycasts = false;

        modalRoot.SetActive(true);

        EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
    }

    public void Close()
    {
        if (!modalRoot.activeSelf)
            return;

        EventSystem.current.SetSelectedGameObject(null);

        modalRoot.SetActive(false);

        backgroundCanvasGroup.interactable = true;
        backgroundCanvasGroup.blocksRaycasts = true;

        if (previousSelection != null && previousSelection.activeInHierarchy)
        {
            Selectable selectable = previousSelection.GetComponent<Selectable>();

            if (selectable != null && selectable.IsInteractable())
            {
                EventSystem.current.SetSelectedGameObject(previousSelection);
            }
        }

        previousSelection = null;
    }
}
