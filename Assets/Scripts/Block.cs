using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Figure occupidedFigure;
    public int index;
    public bool isOccupied => occupidedFigure != null;


    public void Init(int index)
    {
        this.index = index;
    }

    public Vector2 Pos => transform.position;

    public void setFigure(Figure figure)
    {
        occupidedFigure = figure;

    }

    public void removeFigure()
    {
        if (occupidedFigure != null) {
            occupidedFigure.block = null;
            occupidedFigure = null;
        }
        
    }

}
