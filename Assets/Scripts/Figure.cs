using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public int value;
    public TextMeshPro textMeshPro;
    public Block block;

    public Vector2 Pos => transform.position;

    public void Init(int v)
    {
        value = v;
        textMeshPro.text = v.ToString();

    }
    void OnMouseDown()
    {
        Debug.Log("Figure "+value+" mouse click");
    }
}
