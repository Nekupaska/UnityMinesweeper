using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pointer : MonoBehaviour
{
    public float height = 1f;
    public bool flagMode = false;

    public Material flagMat;
    public Material blockMat;

    public Material menuFlagMat;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Ended)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(t.position);
                //Vector3 touchPosition = Camera.main.ScreenToViewportPoint(t.position);
                touchPosition.y = height;
                transform.position = touchPosition;

                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction));
                //bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction));
                if (isHit)
                {
                    GameObject obj = hit.collider.gameObject;

                    switch (obj.tag)
                    {
                        case GLOBAL_VARIABLES.tagBlock:
                            if (!Generator.gameOver)
                            {
                                Block b = hit.collider.gameObject.GetComponent<Block>();
                                if (flagMode)
                                {
                                    if (b.isMarked)
                                    {
                                        b.GetComponent<MeshRenderer>().material = blockMat;
                                    }
                                    else
                                    {
                                        b.GetComponent<MeshRenderer>().material = flagMat;
                                    }
                                    b.isMarked = !b.isMarked;
                                }
                                else
                                {
                                    if (!b.isMarked)
                                        b.doAction();

                                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
                                }
                            }
                            break;

                        case GLOBAL_VARIABLES.tagFlag:
                            if (flagMode)
                            {
                                obj.GetComponent<MeshRenderer>().material = menuFlagMat;
                            }
                            else
                            {
                                obj.GetComponent<MeshRenderer>().material = flagMat;
                            }
                            flagMode = !flagMode;
                            break;

                        case GLOBAL_VARIABLES.tagReset:
                            Scene scene = SceneManager.GetActiveScene();
                            SceneManager.LoadScene(scene.name);
                            break;

                        case GLOBAL_VARIABLES.tagExit:
                            Application.Quit();
                            break;

                    }


                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                }

            }
        }
    }
}


/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pointer : MonoBehaviour
{
    public float height = 1f;
    public bool flagMode = false;

    public Material flagMat;
    public Material blockMat;

    public Material menuFlagMat;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Ended)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(t.position);
                //Vector3 touchPosition = Camera.main.ScreenToViewportPoint(t.position);
                touchPosition.y = height;
                transform.position = touchPosition;

                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction));
                //bool isHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, LayerMask.GetMask(GLOBAL_VARIABLES.layerInteraction));
                if (isHit)
                {
                    GameObject obj = hit.collider.gameObject;

                    switch (obj.tag)
                    {
                        case GLOBAL_VARIABLES.tagBlock:
                            if (!Generator.gameOver)
                            {
                                Block b = hit.collider.gameObject.GetComponent<Block>();
                                if (flagMode)
                                {
                                    if (b.isMarked)
                                    {
                                        b.GetComponent<MeshRenderer>().material = blockMat;
                                    }
                                    else
                                    {
                                        b.GetComponent<MeshRenderer>().material = flagMat;
                                    }
                                    b.isMarked = !b.isMarked;
                                }
                                else
                                {
                                    if (!b.isMarked)
                                        b.doAction();

                                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
                                }
                            }
                            break;

                        case GLOBAL_VARIABLES.tagFlag:
                            if (flagMode)
                            {
                                obj.GetComponent<MeshRenderer>().material = menuFlagMat;
                            }
                            else
                            {
                                obj.GetComponent<MeshRenderer>().material = flagMat;
                            }
                            flagMode = !flagMode;
                            break;

                        case GLOBAL_VARIABLES.tagReset:
                            Scene scene = SceneManager.GetActiveScene();
                            SceneManager.LoadScene(scene.name);
                            break;

                        case GLOBAL_VARIABLES.tagExit:
                            Application.Quit();
                            break;

                    }


                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
                }

            }
        }
    }
}

 */