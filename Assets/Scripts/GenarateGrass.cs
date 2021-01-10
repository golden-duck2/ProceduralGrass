using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class GenarateGrass : MonoBehaviour
{
    public Material material;

    public int grassSplitCnt = 3;
    public float grassHeight = 0.3f;
    public float grassWidth = 0.01f;


    void Start()
    {
        var proceduralGrassGeometry = new ProceduralGrassGeometry(grassWidth, grassHeight, grassSplitCnt, 0.1f);

        foreach (var x in Enumerable.Range(0, 50))
        {
            foreach (var z in Enumerable.Range(0, 50))
            {
                proceduralGrassGeometry.CreateChain(new Vector3(x * 0.03f, 0f, z * 0.03f), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), Vector3.one * Mathf.Clamp01(Mathf.PerlinNoise(x, z) + 0.1f)); //* 0.5f + 0.5f));
            }
        }

        var grasses = proceduralGrassGeometry.GetMesh();

        // Debug.Log(geo.verticesList.Count());

        var meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = grasses;

        var renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;
    }

}
