using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int width = 4;
    public int height = 4;
    public Block blockPrefab;
    public Figure figurePrefab;
    public SpriteRenderer boardPrefab;
    private List<Block> blocks;
    private int currentX, currentY, currentIndex;
    private Block selectedBlock;
    void Start()
    {
        blocks = new List<Block>();
        GenerateBoard();
        SpawnBlock();
        
    }

    void GenerateBoard()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var block = Instantiate(blockPrefab, new Vector2(i, j), Quaternion.identity);
                blocks.Add(block);
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);

        var board = Instantiate(boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -20);
    }

    void SpawnBlock()
    {
        Block block = blocks[0];
        Figure figure = Instantiate(figurePrefab, block.Pos, Quaternion.identity);
        figure.Init(2);
        block.occupidedFigure = figure;
        currentX = 0;
        currentY = 0;
        currentIndex = 0;
        selectedBlock = block;
    }



    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("main mouse click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
           

            if (Physics.Raycast(ray, 100f))
            {
                Debug.Log("............");
            //    //Select stage    
            //    //if (hit.transform.name == "Cube")
            //    //{
            //    //    SceneManager.LoadScene("SceneTwo");
            //    //}
            }
        }

        //Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 click2D = new Vector2(mousePos.x, mousePos.y);

        //    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        //    if (hit.collider != null)
        //    {
        //        Debug.Log(hit.collider.gameObject.name);
        //    }
        

        var orderList = blocks.OrderBy(b => b.Pos.x).ThenBy(b => b.Pos.y).ToList();

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {    
            if (currentX < width)
            {
                currentX = currentX + 1;
                currentIndex = currentIndex + width;
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentIndex + "  " + orderList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentX > 0)
            {
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                currentIndex = currentIndex - width;
                currentX--;
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentIndex + "  " + orderList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentY < height)
            {
                currentY++;
                currentIndex++;
                Figure f = selectedBlock.occupidedFigure;
                selectedBlock.removeFigure();
                selectedBlock = orderList[currentIndex];
                selectedBlock.setFigure(f);
                Debug.Log(currentIndex + "  " + orderList[currentIndex].Pos.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentY > 0)
            {
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
