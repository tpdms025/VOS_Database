using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ReuseGridMenu
{
    [MenuItem("NGUI/Create/Reuse Scroll View With Grid", false, 50)]
    static void AddReuseScrollViewAndGrid()
    {
        AddReuseScrollView();
        AddReuseGrid();
    }

    [MenuItem("NGUI/Create/Reuse Scroll View", false, 50)]
    static void AddReuseScrollView()
    {
        UIPanel panel = NGUISettings.AddPanel(NGUIEditorTools.SelectedRoot());
        if (panel == null) panel = NGUIEditorTools.SelectedRoot(true).GetComponent<UIPanel>();
        panel.clipping = UIDrawCall.Clipping.SoftClip;
        panel.name = "Reuse Scroll View";
        panel.gameObject.AddComponent<UIReuseScrollView>();
        Selection.activeGameObject = panel.gameObject;
    }

    [MenuItem("NGUI/Create/Reuse Grid", false, 50)]
    static void AddReuseGrid()
    {
        UIReuseGrid grid = NGUITools.AddChild<UIReuseGrid>(NGUIEditorTools.SelectedRoot());

        UIReuseScrollView scrollView = grid.transform.parent.GetComponent<UIReuseScrollView>();
        if (scrollView != null)
        {
            grid.m_ReuseScrollView = scrollView;
        }

        UIPanel panel = grid.transform.parent.GetComponent<UIPanel>();
        if (panel != null)
        {
            grid.transform.localPosition = new Vector3(panel.finalClipRegion.z * -0.5f + panel.finalClipRegion.x, panel.finalClipRegion.w * 0.5f + panel.finalClipRegion.y, 0.0f);
        }

        Selection.activeGameObject = grid.gameObject;
    }
}