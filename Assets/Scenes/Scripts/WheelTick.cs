using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WheelTick : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_wheelText;
    [SerializeField]
    private Image m_wheelImage;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private RectTransform m_rectTransform;
    
    public void SetRectTransform(float rowHeight)
    {
        m_rectTransform.anchorMin = new Vector2(0f, 0.5f);
        m_rectTransform.anchorMax = new Vector2(1f, 0.5f);
        m_rectTransform.pivot     = new Vector2(0.5f, 0.5f);
        m_rectTransform.sizeDelta = new Vector2(0f, rowHeight);
    }

    public void SetAnchorPosX(float x, float y)
    {
        m_rectTransform.anchoredPosition = new Vector2(x, y);
    }

    public void SetLocalScale(Vector3 start, Vector3 end, float interpolate)
    {
        m_rectTransform.localScale = Vector3.Lerp(start,end, interpolate);
    }

    public void SetTick(string value, float alpha)
    {
        SetText(value);
        SetAlpha(alpha);
        SetImage(value != string.Empty);
    }
    
    private void SetAlpha(float alpha)
    {
        m_canvasGroup.alpha = alpha;
    }
    
    private void SetText(string text)
    {
        m_wheelText.text = text;
    }
    
    public void SetImage(bool isActive)
    {
        m_wheelImage.gameObject.SetActive(isActive);
    }
}