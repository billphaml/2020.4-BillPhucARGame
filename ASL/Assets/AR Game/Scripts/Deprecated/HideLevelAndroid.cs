/*
******************************************************************************
* HideLevelAndroid.cs
* Authors: Bill Pham & Phuc Tran
* 
* Script to replace the playable map with a transparent outlined version on
* Android clients so users "play in their environment".
* 
* Assumptions:
* - The level will always start with the desired default material that PC
*   players will see
******************************************************************************
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLevelAndroid : MonoBehaviour
{
    // Reference to the map
    public GameObject level;

    public GameObject lineHolder;

    public GameObject[] lines;

    void Start()
    {
        foreach (GameObject line in lines)
        {
            BakeLineDebuger(line);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            level.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            lineHolder.SetActive(false);
        }
    }

    public static void BakeLineDebuger(GameObject lineObj)
    {
        var lineRenderer = lineObj.GetComponent<LineRenderer>();
        var meshFilter = lineObj.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, Camera.main, true);
        meshFilter.sharedMesh = mesh;

        var meshRenderer = lineObj.AddComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_Color", Color.white);

        GameObject.Destroy(lineRenderer);
    }
}
