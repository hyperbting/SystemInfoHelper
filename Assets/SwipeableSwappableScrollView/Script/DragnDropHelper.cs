﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DragnDropHelper : MonoBehaviour
{
    [Header("DragnDrop Setting")]
    public ScrollRect scrollRect;

    [Range(0.5f, 2f)]
    public float secondToDnDMode = 1f;

    private UnityAction onLongPressed;

    public void Reset()
    {
        scrollRect.enabled = true;
        onLongPressed = null;
    }

    public void OnBtnPointerDown(UnityAction _onLongPressed)
    {
        Reset();

        onLongPressed = _onLongPressed;

        //CountDown to Set
        Invoke("SetToDnDMode", secondToDnDMode);
    }

    public void OnBtnPointerUp()
    {
        //TODO: swap here?
        Reset();
    }

    public void OnBtnDrag(MultiTouchDetector _go, UnityEngine.EventSystems.PointerEventData data)
    {
        if (data.pointerEnter.transform == null || _go == data.pointerEnter.transform)
            return;

        //Debug.Log(data.pointerEnter.name + ":" + data.pointerEnter.transform.GetSiblingIndex().ToString());
        _go.transform.SetSiblingIndex(data.pointerEnter.transform.GetSiblingIndex());
    }

    public void CancelCountDownInvoke()
    {
        CancelInvoke("SetToDnDMode");
    }

    void SetToDnDMode()
    {
        Debug.Log("isLongPressed");

        if (onLongPressed != null)
            onLongPressed();

        //Lock scroll
        scrollRect.enabled = false;

        //var holding = EventSystem.current.currentSelectedGameObject.GetComponent<MultiTouchDetector>();
        //if (holding != null)
        //    holding.interactable = false;
    }
}
