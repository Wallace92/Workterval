using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupWheelPicker : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI Parts")]
    public RectTransform wheelContainer;        // where rows are placed (a small column)
    public RectTransform selectionBar;          // thin image line behind the center row
    public Button confirmButton;                // optional
    public Button cancelButton;                 // optional
    public RectTransform rowPrefab;             // prefab containing a centered TextMeshProUGUI

    [Header("Behavior")]
    public int visibleRows = 7;                 // must be odd: 5, 7, 9...
    public float rowHeight = 60f;               // px
    public float dragSensitivity = 1.0f;        // 1.0 = 1px moves 1px
    public float snapSpeed = 12f;               // higher = snappier// null = unbounded upward

    [Header("Style")]
    public float centerScale = 1.15f;
    public float sideAlpha = 0.35f;

    // callbacks
    public event Action<int> OnValueChanged;    // while moving/snapping
    public event Action<int> OnConfirm;         // when confirm pressed
    public event Action OnCancel;

    // internals
    readonly List<RectTransform> _rows = new();
    int _current;                   // selected integer (>=0)
    float _offset;                  // fractional offset in rows (+down/-up visually)
    bool _dragging;
    Vector2 _lastPointer;

    void Awake()
    {
        visibleRows = Mathf.Max(3, visibleRows | 1); // ensure odd
        BuildRows();
        
        if (confirmButton) confirmButton.onClick.AddListener(() =>
        {
            OnConfirm?.Invoke(_current); 
            Hide();
        });
        
        if (cancelButton) cancelButton.onClick.AddListener(() =>
        {
            OnCancel?.Invoke(); 
            Hide();
        });

        // start hidden
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Ease toward snapping to nearest integer when not dragging
        if (!_dragging)
        {
            float target = Mathf.Round(_offset);
            float t = Mathf.Exp(-snapSpeed * Time.unscaledDeltaTime);
            _offset = Mathf.Lerp(target, _offset, t);

            if (Mathf.Abs(_offset - target) < 0.001f)
                _offset = target;

            // If we reached a whole step, apply it to value
            if (Mathf.Abs(_offset) >= 1f - 1e-4f)
            {
                int steps = Mathf.RoundToInt(_offset);
                ApplySteps(steps);
                _offset = 0f;
            }
            UpdateRows();
        }
    }

    void BuildRows()
    {
        if (!wheelContainer || !rowPrefab) { Debug.LogError("Assign wheelContainer & rowPrefab"); return; }

        // size the container to visible rows
        var s = wheelContainer.sizeDelta; s.y = visibleRows * rowHeight; wheelContainer.sizeDelta = s;

        // instantiate rows
        for (int i = 0; i < visibleRows; i++)
        {
            var r = Instantiate(rowPrefab, wheelContainer);
            r.anchorMin = new Vector2(0, 1);
            r.anchorMax = new Vector2(1, 1);
            r.pivot = new Vector2(0.5f, 1);
            var d = r.sizeDelta; d.y = rowHeight; d.x = 0; r.sizeDelta = d;

            var cg = r.GetComponent<CanvasGroup>(); if (!cg) cg = r.gameObject.AddComponent<CanvasGroup>();
            _rows.Add(r);
        }

        // selection bar centered
        if (selectionBar)
        {
            selectionBar.anchorMin = new Vector2(0, 0.5f);
            selectionBar.anchorMax = new Vector2(1, 0.5f);
            selectionBar.pivot = new Vector2(0.5f, 0.5f);
            var sd = selectionBar.sizeDelta; sd.y = 2f; selectionBar.sizeDelta = sd;
        }
    }

    void ApplySteps(int steps)
    {
        if (steps == 0) return;
        long next = (long)_current + steps;
        if (next < 0) next = 0;

        int prev = _current;
        _current = (int)next;
        if (_current != prev) OnValueChanged?.Invoke(_current);
    }

    public void Show(int start, int? max = null)
    {
        _current = Mathf.Max(0, start);
        _offset = 0f;
        _dragging = false;
        gameObject.SetActive(true);
        UpdateRows();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // --- Interaction ---

    public void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
        _lastPointer = eventData.position;

        // Tap top/bottom halves to nudge
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            wheelContainer, eventData.position, eventData.pressEventCamera, out var local);

        float half = wheelContainer.rect.height * 0.5f;
        float centerBand = rowHeight * 0.6f; // safe zone
        if (Mathf.Abs(local.y) > centerBand && !_dragMoved)
        {
            if (local.y > 0) Nudge(-1); else Nudge(+1);  // local.y > 0 is above center (increase value visually up? we invert below)
        }
        _dragMoved = false;
    }

    bool _dragMoved = false;

    public void OnDrag(PointerEventData eventData)
    {
        _dragMoved = true;
        var cur = eventData.position;
        float dy = (cur.y - _lastPointer.y) * dragSensitivity;

        // convert pixel delta to row delta: dragging up (positive dy) should move the content upward → value decreases
        _offset -= dy / rowHeight;

        // If offset crosses whole steps, consume them into value (with clamping)
        while (Mathf.Abs(_offset) >= 1f)
        {
            int step = _offset > 0 ? +1 : -1;
            // step>0 means user dragged down → visually content moves down → value increases
            ApplySteps(step);
            _offset -= step;
        }

        _lastPointer = cur;
        UpdateRows();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
        SnapNow();                // ← snap to the visually centered value
    }
    
    void Nudge(int steps)
    {
        // steps: +1 = move down one row (value+1), -1 = up one row (value-1 but clamped at 0)
        ApplySteps(steps);
        _offset = 0f;
        UpdateRows();
    }

    int ClampValue(int v)
    {
        if (v < 0)
        {
            v = 0;
        }
        
        return v;
    }
    
    void SnapNow()
    {
        var target = ClampValue(Mathf.RoundToInt(_current));

        if (target != _current)
        {
            _current = target;
            OnValueChanged?.Invoke(_current);
        }
        _offset = 0f;
        
        UpdateRows();
    }
    
    void UpdateRows()
    {
        int centerIndex = visibleRows / 2;

        for (int i = 0; i < visibleRows; i++)
        {
            int delta = i - centerIndex;

            // value for this visual row (fractional offset supported)
            float vf = _current - _offset + delta;
            int valueForRow = Mathf.RoundToInt(vf);

            // position this row (top-anchored/pivot Y=1)
            float topFromCenter = (delta - _offset) * rowHeight;
            float yTop = (wheelContainer.rect.height * 0.5f) - rowHeight * 0.5f - topFromCenter;
            var rt = _rows[i];
            rt.anchoredPosition = new Vector2(0f, yTop);

            var text = rt.GetComponentInChildren<TextMeshProUGUI>(true);
            var cg   = rt.GetComponent<CanvasGroup>() ?? rt.gameObject.AddComponent<CanvasGroup>();

            // hide negatives instead of clamping them to 0 (prevents “too many zeros”)
            if (valueForRow < 0)
            {
                if (text) text.text = "";
                cg.alpha = 0f;
            }
            else
            {
                if (text) text.text = valueForRow.ToString();   // <-- use the computed value
                // scale/fade toward center
                float dist = Mathf.Abs(delta - _offset);
                float k = Mathf.Clamp01(1f - Mathf.Min(dist, 1f));
                rt.localScale = Vector3.Lerp(Vector3.one, new Vector3(centerScale, centerScale, 1f), k);
                cg.alpha = Mathf.Lerp(sideAlpha, 1f, k);
            }
        }
    }
}