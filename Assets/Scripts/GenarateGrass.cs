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
        var grasses = new Mesh();

        var proceduralGrassGeometry = new ProceduralGrassGeometry(grassWidth, grassHeight, grassSplitCnt);
        var geo = proceduralGrassGeometry.CreateGrass(Vector3.zero, 0);

        Debug.Log(geo.verticesList.Count());

        grasses.vertices = geo.verticesList.ToArray();
        grasses.triangles = geo.trianglesList.ToArray();
        grasses.SetUVs(0, geo.uvList.ToArray());

        grasses.RecalculateNormals();
        grasses.RecalculateBounds();
        grasses.RecalculateTangents();

        var meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = grasses;

        var renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;
    }

}
