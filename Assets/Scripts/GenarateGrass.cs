using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class GenarateGrass : MonoBehaviour
{
    public Material material;

    public int grassSplitCnt = 3;
    public float grassHeight = 0.3f;


    void Start()
    {
        var grasses = new Mesh();

        var unitHeight = grassHeight / (float)grassSplitCnt;


        grasses.vertices = new Vector3[]
        {
            new Vector3(0f,0f,0f),
            new Vector3(1f,0f,0f),
            new Vector3(0.5f,1f,0f)
        };

        // 頂点インデックスを設定
        grasses.triangles = new[] { 0, 1, 2 };
        grasses.SetUVs(0, new Vector2[]{
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(0,1)
        });

        grasses.RecalculateNormals();
        grasses.RecalculateBounds();
        grasses.RecalculateTangents();

        var meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = grasses;

        var renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;
    }

}
