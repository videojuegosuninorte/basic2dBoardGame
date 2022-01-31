using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Figure occupidedFigure;
    public int index;
    private GameManager gameManager;

    public bool isOccupied => occupidedFigure != null;



    public void Init(GameManager gm, int index)
    {
        gameManager = gm;
        this.index = index;
    }

    public Vector2 Pos => transform.position;

    public void setFigure(Figure figure)
    {
        occupidedFigure = figure;
        figure.block = this;
        figure.transform.position = transform.position;
        figure.Init(gameManager,this, (int)Pos.x, (int)Pos.y);
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
