using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Figure occupidedFigure;

    public Vector2 Pos => transform.position;

    public void setFigure(Figure figure)
    {
        occupidedFigure = figure;
        figure.block = this;
        figure.transform.position = transform.position;
    }

    void OnMouseDown()
    {
      //  Debug.Log("mouse click");
    }

    public void removeFigure()
    {
        if (occupidedFigure != null) {
            occupidedFigure.block = null;
            occupidedFigure = null;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
