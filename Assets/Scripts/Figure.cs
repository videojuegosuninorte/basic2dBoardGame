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
    public float travaleTime = 0.2f;


    public Vector2 Pos => transform.position;

    public void Init(Block block,int x, int y)
    {
        Debug.Log("Figure "+ " " + x + " " + y);
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

        GameManager.Instance.setCurrentBlock(block);
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
