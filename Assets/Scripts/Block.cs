using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{

    public static float chanceOfBeingMine = 1.5f;
    public static int numberOfMines;
    public int minesAround = 0;

    public Position p;
    public GameObject mine, one, two, three, four, five, six, seven, eight;
    //public Material mat;

    private static Vector3[] rays = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right,
        Vector3.forward+Vector3.right,Vector3.forward+Vector3.left,Vector3.back+Vector3.right,Vector3.back+Vector3.left};

    public bool isMine = false;
    public bool isMarked = false;
    public Block(int x, int y)
    {
        this.p = new Position(x, y);
        if (Random.Range(0, 10) < chanceOfBeingMine)
        {
            this.isMine = true;
            //Block.numberOfMines++;
        }
    }

    public class Position
    {
        public int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }

    void Start()
    {
        if (Random.Range(0, 10) < chanceOfBeingMine)
        {
            //this.GetComponent<MeshRenderer>().material = mat;
            this.isMine = true;
            Block.numberOfMines++;
            GameObject.FindGameObjectWithTag(GLOBAL_VARIABLES.tagCounterText).GetComponent<Text>().text = "MINES: " + Block.numberOfMines;
        }
    }

    public void doAction()
    {
        if (this.isMine)
        {
            foreach (GameObject mine in GameObject.FindGameObjectsWithTag(GLOBAL_VARIABLES.tagBlock))
            {
                if (mine.GetComponent<Block>().isMine)
                {
                    mine.GetComponent<Block>().explode();
                }
                //this.explode();
            }
            Generator.gameOver = true;
            GameObject.FindGameObjectWithTag(GLOBAL_VARIABLES.tagGameoverText).GetComponent<Text>().enabled = true;
        }
        else
        {
            this.checkSurroundingBoxes();
        }
    }

    public void checkSurroundingBoxes()
    {
        //print("checking surrondings: " + this.p.x + " - " + this.p.y);
        disableColliders();
        List<Block> blocks = new List<Block>();
        RaycastHit hit;

        foreach (Vector3 v in rays)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(v), Color.white);
        }

        foreach (Vector3 v in rays)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(v), Color.white);
            if (Physics.Raycast(transform.position, transform.TransformDirection(v), out hit, 1, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction)))
            {
                Block b = hit.transform.GetComponent<Block>();
                if (b != null)
                {
                    if (b.isMine && b != this.gameObject.GetComponent<Block>())
                        blocks.Add(b);

                }
                else if (hit.transform.tag == GLOBAL_VARIABLES.tagMine)
                {
                    Block dummy = new Block(-1, -1);
                    dummy.isMine = true;
                    blocks.Add(dummy);
                }
            }
        }

        switch (blocks.Count)
        {
            case 0:

                foreach (Vector3 v in rays)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(v), Color.white);
                    if (Physics.Raycast(transform.position, transform.TransformDirection(v), out hit, 1, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction)))
                    {
                        Block b = hit.transform.GetComponent<Block>();
                        b.checkSurroundingBoxes();
                    }
                }
                changeModel(null, false);
                break;
            case 1: changeModel(one, true); break;
            case 2: changeModel(two, true); break;
            case 3: changeModel(three, true); break;
            case 4: changeModel(four, true); break;
            case 5: changeModel(five, true); break;
            case 6: changeModel(six, true); break;
            case 7: changeModel(seven, true); break;
            case 8: changeModel(eight, true); break;
        }


    }

    private void changeModel(GameObject model, bool lower)
    {
        if (model != null)
        {
            var instance = Instantiate(model);
            instance.transform.position = this.transform.position;
            if (lower)
            {
                instance.transform.position += new Vector3(0, -0.4f, 0);
            }
            instance.transform.localScale = this.transform.localScale * 0.5f;
        }
        Destroy(this.gameObject);
    }

    public void disableColliders()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    public void explode()
    {
        changeModel(mine, false);

    }

}

/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{

    public static float chanceOfBeingMine = 2;
    public static int numberOfMines = 0;
    public int minesAround = 0;

    public Position pos;
    public GameObject mine, one, two, three, four, five, six, seven, eight;
    //public Material mat;

    public static Vector3[] rays = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right,
        Vector3.forward+Vector3.right,Vector3.forward+Vector3.left,Vector3.back+Vector3.right,Vector3.back+Vector3.left};

    public bool isMine = false;
    public bool isMarked = false;
    
    public Block()
    {

    }
    public Block(int x, int y)
    {
        this.pos = new Position(x, y);
        if (Random.Range(0, 10) < chanceOfBeingMine)
        {
            this.isMine = true;
            numberOfMines++;
        }
    }

    public class Position
    {
        public int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }

    void Start()
    {
        if (Random.Range(0, 10) < chanceOfBeingMine)
        {
            //this.GetComponent<MeshRenderer>().material = mat;
            this.isMine = true;
            numberOfMines++;
        }
    }

    public void doAction()
    {
        if (this.isMine)
        {
            this.explode();
            Generator.gameOver = true;
            GameObject.FindGameObjectWithTag("gameover").GetComponent<Text>().enabled = true;
        }
        else
        {
            this.checkSurroundingBoxes();
        }
    }

    public void checkSurroundingBoxes()
    {
        //print("checking surrondings: " + this.p.x + " - " + this.p.y);
        disableColliders();
        List<Block> blocks = new List<Block>();
        RaycastHit hit;

        foreach (Vector3 v in rays)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(v), Color.white);
        }

        foreach (Vector3 v in rays)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(v), Color.white);
            if (Physics.Raycast(transform.position, transform.TransformDirection(v), out hit, 1, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction)))
            {
                Block b = hit.transform.GetComponent<Block>();
                if (b != null)
                {
                    if (b.isMine)
                        explode();
                    else
                    {
                        switch (b.minesAround)
                        {
                            case 0:

                                foreach (Vector3 vv in rays)
                                {
                                    Debug.DrawRay(transform.position, transform.TransformDirection(vv), Color.white);
                                    if (Physics.Raycast(transform.position, transform.TransformDirection(vv), out hit, 1, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction)))
                                    {
                                        Block bAround = hit.transform.GetComponent<Block>();
                                        if (bAround.minesAround == 0)
                                            bAround.checkSurroundingBoxes();
                                    }
                                }
                                changeModel(null, false);
                                break;
                            case 1: changeModel(one, true); break;
                            case 2: changeModel(two, true); break;
                            case 3: changeModel(three, true); break;
                            case 4: changeModel(four, true); break;
                            case 5: changeModel(five, true); break;
                            case 6: changeModel(six, true); break;
                            case 7: changeModel(seven, true); break;
                            case 8: changeModel(eight, true); break;
                        }
                    }

                }
            }
        }




    }

    private void changeModel(GameObject model, bool lower)
    {
        if (model != null)
        {
            var instance = Instantiate(model);
            instance.transform.position = this.transform.position;
            if (lower)
            {
                instance.transform.position += new Vector3(0, -0.4f, 0);
            }
            instance.transform.localScale = this.transform.localScale * 0.5f;
        }
        Destroy(this.gameObject);
    }

    public void disableColliders()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    public void explode()
    {
        changeModel(mine, false);

    }

}
*/