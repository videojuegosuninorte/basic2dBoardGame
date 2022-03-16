using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int width = 4;
    public int height = 4;
    public Block blockPrefab;
    public Figure figurePrefab;
    public SpriteRenderer boardPrefab;
    private List<Block> blocks;
    private List<Block> orderBlocksList;
    private int currentIndex;
    public int currentX, currentY;
    private Block selectedBlock;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        blocks = new List<Block>();
        GenerateBoard();
        SpawnFigure(3);
        orderBlocksList = blocks.OrderBy(b => b.Pos.x).ThenBy(b => b.Pos.y).ToList();
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
                block.Init(index);
                index++;
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);

        var board = Instantiate(boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -20);
    }

    public void setCurrentBlock(Block block)
    {
        if (block == null)
        {
            Debug.Log("Block is null");
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
            figure.Init(block, (int)block.Pos.x, (int)block.Pos.y);
            setCurrentBlock(block);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {    
            if (currentX < width - 1)
            {
                if (orderBlocksList[currentIndex + width].isOccupied)
                    return;
                currentIndex = currentIndex + width;
                currentX = currentX + 1;
                moveFigure(orderBlocksList[currentIndex]);
                Debug.Log(currentX + "  " + orderBlocksList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentX > 0)
            {
                if (orderBlocksList[currentIndex - width].isOccupied)
                    return;
                currentIndex = currentIndex - width;
                currentX--;
                moveFigure(orderBlocksList[currentIndex]);
                Debug.Log(currentX + "  " + orderBlocksList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentY < height - 1)
            {
                if (orderBlocksList[currentIndex + 1].isOccupied)
                    return;
                currentY++;
                currentIndex++;
                moveFigure(orderBlocksList[currentIndex]);
                Debug.Log(currentY + "  " + orderBlocksList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentY > 0)
            {
                if (orderBlocksList[currentIndex - 1].isOccupied)
                    return;
                currentY--;
                currentIndex--;
                moveFigure(orderBlocksList[currentIndex]);
                Debug.Log(currentIndex + "  " + orderBlocksList[currentIndex].Pos.ToString());
            }
        }
    }

    private void moveFigure(Block newBlock)
    {
        Figure figure = selectedBlock.occupidedFigure;
        selectedBlock = newBlock;
        figure.move(selectedBlock);
    }

}
