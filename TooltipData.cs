using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipData : MonoBehaviour
{
    public TMP_Text tooltipText;

    private RectTransform rect;
    
    private void Awake()
    {
        tooltipText = GetComponentInChildren<TMP_Text>();
        gameObject.SetActive(false);

        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.anchoredPosition = Input.mousePosition;
    }
}