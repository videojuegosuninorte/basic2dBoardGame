using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Figure : MonoBehaviour
{
    private int value, x, y;
    public TextMeshPro textMeshPro;
    public Block block;
    private GameManager gameManager;
    public float travaleTime = 0.2f;


    public Vector2 Pos => transform.position;

    public void Init(GameManager gm, Block block,int x, int y)
    {
        Debug.Log("Figure "+ " " + x + " " + y);
        gameManager = gm;
        this.x = x;
        this.y = y;
        this.block = block;
    }

    public void setValue(int value)
    {
        this.value = value;
        textMeshPro.text = value.ToString();
    }

    void OnMouseDown()
    {
        Debug.Log("Figure "+value+" mouse click");
        if (gameManager == null)
        {
            Debug.Log("gameManager is null");
            return;
        }
        gameManager.setCurrentBlock(block);
    }

    public void move(Block newBlock)
    {
        block.removeFigure();
        newBlock.setFigure(this);
        block = newBlock;
        //transform.position = newBlock.Pos;   // use this line to see movement without animation
        transform.DOMove(newBlock.Pos, travaleTime);  // travelTime = 0.2f
    }
}
