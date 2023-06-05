using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public int rows = 14;
    public int columns = 14;
    public float gap = 0.02f;
    public static bool gameOver = false;


    //Block[,] blocks;
    public static GameObject[,] blocks;

    public Transform pivot; //scale works best at 0.5
    public GameObject cube;

    void Start()
    {
        Block.numberOfMines = 0;
        gameOver = false;
        //cube.transform.localScale *= 0.03f;

        blocks = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                blocks[i, j] = Instantiate(cube);
                blocks[i, j].GetComponent<Block>().p = new Block.Position(i, j);
                //place cubes in each position from pivot

                Vector3 v = new Vector3(i + (pivot.position.x), 0, j + (pivot.position.z));

                blocks[i, j].gameObject.transform.parent = pivot;
                blocks[i, j].gameObject.transform.position = v;
                blocks[i, j].gameObject.transform.rotation = pivot.rotation;
                blocks[i, j].gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }




    }

    void Update()
    {
        if (Block.numberOfMines - GameObject.FindGameObjectsWithTag(GLOBAL_VARIABLES.tagBlock).Length == 0)
        {
            GameObject.FindGameObjectWithTag(GLOBAL_VARIABLES.tagGameoverText).GetComponent<Text>().enabled = true;
            GameObject.FindGameObjectWithTag(GLOBAL_VARIABLES.tagGameoverText).GetComponent<Text>().text = "YOU WIN";

        }
    }


}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int rows = 14;
    public int columns = 14;
    public float gap = 0.02f;
    public static bool gameOver = false;

    //Block[,] blocks;
    public static GameObject[,] blocks;

    public Transform pivot; //scale works best at 0.5
    public GameObject cube;

    public static Block.Position[] positions = {
        new Block.Position(1, 1),
        new Block.Position(1, 0),
        new Block.Position(1, -1),
        new Block.Position(0, 1),
        new Block.Position(0, -1),
        new Block.Position(-1, 1),
        new Block.Position(-1, 0),
        new Block.Position(-1, -1)
    };


    void Start()
    {
        gameOver = false;
        //cube.transform.localScale *= 0.03f;

        blocks = new GameObject[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                blocks[i, j] = Instantiate(cube);
                blocks[i, j].GetComponent<Block>().pos = new Block.Position(i, j);
                //place cubes in each position from pivot

                Vector3 v = new Vector3(i + (pivot.position.x), 0, j + (pivot.position.z));

                blocks[i, j].gameObject.transform.parent = pivot;
                blocks[i, j].gameObject.transform.position = v;
                blocks[i, j].gameObject.transform.rotation = pivot.rotation;
                blocks[i, j].gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                List<Block> blocksAround = new List<Block>();
                Block block = blocks[i, j].GetComponent<Block>();
                foreach (Block.Position p in positions)
                {
                    Block blockAround = new Block();

                    if (p.x != -1 || block.pos.x != 0)
                    {
                        blockAround.pos.x = p.x;
                    }
                    else
                    {

                    }


                    if (blockAround != null)
                    {
                        if (blockAround.isMine && blockAround != this.gameObject.GetComponent<Block>())
                            blocksAround.Add(blockAround);

                    }




                }
                block.minesAround = blocksAround.Count;


            }

        }
    }





}

*/

