using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldView))]
public class FieldViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldView fow = (FieldView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.forward, 360, fow.viewRadius);
    }
}
