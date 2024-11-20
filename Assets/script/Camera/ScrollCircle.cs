using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollCircle : ScrollRect
{
    float radius = 0;
    public Vector2 output=new();

    // Start is called before the first frame update
    private void Start()
    {
        radius = (transform as RectTransform).rect.size.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        output = content.localPosition / radius;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        base.OnDrag(eventData);
        Vector2 pos = content.anchoredPosition;
        if (pos.magnitude > radius)
        {
            pos = pos.normalized * radius;
            SetContentAnchoredPosition(pos);
        }
    }
}

