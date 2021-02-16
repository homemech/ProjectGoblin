using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInteractiveIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer examineIconSpriteRenderer;
    private bool iconRendererIsEnabled = false;

    private void Start()
    {
        iconRendererIsEnabled = false;
        examineIconSpriteRenderer.enabled = false;
    }

    public void SwitchIconVisibility()
    {
        iconRendererIsEnabled = !iconRendererIsEnabled;
        examineIconSpriteRenderer.enabled = iconRendererIsEnabled;
    }
}