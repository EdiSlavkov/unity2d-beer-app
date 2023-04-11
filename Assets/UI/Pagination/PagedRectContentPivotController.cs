using UnityEngine;

public class PagedRectContentPivotController : MonoBehaviour
{
    [SerializeField] private RectTransform parentRectTransform;
    private RectTransform rectTransform;
    private Vector2 initialPivotValue;
    private bool shouldResetPivot;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPivotValue = rectTransform.pivot;
    }

    public void ChangePivotToParentPivot()
    {
        rectTransform.pivot = parentRectTransform.pivot;
        shouldResetPivot = true;
    }

    public void ResetPivot()
    {
        if (shouldResetPivot)
        {
            rectTransform.pivot = initialPivotValue;
            shouldResetPivot = false;
        }
    }
}
