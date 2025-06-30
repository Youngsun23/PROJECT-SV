using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolTipPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemInfo;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Camera uiCamera;

    private float fixedWidth = 600f;
    private float textPaddingSize = 4f;
    private float heightPaddingSize = 200f;
    private Vector2 tooltipOffset = new Vector2(80f, -30f);

    //protected override void Awake()
    //{
    //    base.Awake();
    //    Hide();
    //}

    //private void Update()
    //{
    //    Vector2 localPoint;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), uiCamera, out localPoint);
    //    transform.localPosition = localPoint;
    //}

    public void UpdateToolTip(string nameString, string infoString)
    {
        panel.gameObject.SetActive(true);

        itemName.text = nameString;
        itemInfo.text = infoString;

        Vector2 panelSize = new Vector2(fixedWidth, itemInfo.preferredHeight + textPaddingSize + heightPaddingSize);
        panel.sizeDelta = panelSize;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        panel.transform.position = mousePos + tooltipOffset;
    }

    //public void Hide()
    //{
    //    gameObject.SetActive(false);
    //}

}
