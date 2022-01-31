using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width = 4;
    public int height = 4;
    public Block blockPrefab;
    public Figure figurePrefab;
    public SpriteRenderer boardPrefab;
    private List<Block> blocks;
    private int currentIndex;
    public int currentX, currentY;
    private Block selectedBlock;
    void Start()
    {
        blocks = new List<Block>();
        GenerateBoard();
        SpawnFigure(3);

    }

    void GenerateBoard()
    {
        int index = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var block = Instantiate(blockPrefab, new Vector2(i, j), Quaternion.identity);
                blocks.Add(block);
                block.Init(this, index);
                index++;
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);

        var board = Instantiate(boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -20);
    }

    void setCurrentBlock(Block block)
    {
        if (block == null)
        {
            Debug.Log("Blick is nukk");
            return;
        }
        currentX = (int)block.Pos.x;
        currentY = (int)block.Pos.y;
        currentIndex = block.index;
        selectedBlock = block;
    }

    void SpawnFigure(int amount)
    {
        var orderList = blocks.OrderBy(b => Random.Range(0, blocks.Count)).ToList();
        for (int i = 0; i < amount; i++)
        {
            Block block = orderList[i];
            Figure figure = Instantiate(figurePrefab, block.Pos, Quaternion.identity);
            figure.setValue(i);
            block.setFigure(figure);
           
            setCurrentBlock(block);
        }
        

    }

    public void setCurrentPos(Block block)
    {
       setCurrentBlock(block);
    }


    void Update()
    {

        var orderList = blocks.OrderBy(b => b.Pos.x).ThenBy(b => b.Pos.y).ToList();

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {    
            if (currentX < width - 1)
            {
                if (orderList[currentIndex + width].isOccupied)
                    return;
                currentIndex = currentIndex + width;
                currentX = currentX + 1;
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentX + "  " + orderList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentX > 0)
            {
                if (orderList[currentIndex - width].isOccupied)
                    return;
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                currentIndex = currentIndex - width;
                currentX--;
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentX + "  " + orderList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentY < height - 1)
            {
                if (orderList[currentIndex + 1].isOccupied)
                    return;
                currentY++;
                currentIndex++;
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentY + "  " + orderList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentY > 0)
            {
                if (orderList[currentIndex - 1].isOccupied)
                    return;
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                currentY--;
                currentIndex--;
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentIndex + "  " + orderList[currentIndex].Pos.ToString());
            }
        }
    }

    private void moveFigure(Figure figure, float h)
    {

    }

}
