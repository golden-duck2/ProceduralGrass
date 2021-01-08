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

        var proceduralGrassGeometry = new ProceduralGrassGeometry(grassWidth, grassHeight, grassSplitCnt, 0.2f);

        proceduralGrassGeometry.CreateChain(Vector3.zero, Quaternion.AngleAxis(90f, Vector3.up));
        proceduralGrassGeometry.CreateChain(Vector3.one, Quaternion.AngleAxis(30f, Vector3.up));
        proceduralGrassGeometry.CreateChain(Vector3.right, Quaternion.AngleAxis(0f, Vector3.up));

        // Debug.Log(geo.verticesList.Count());

        grasses.vertices = proceduralGrassGeometry.verticesChainList.ToArray();
        grasses.triangles = proceduralGrassGeometry.trianglesChainList.ToArray(); // geo.trianglesList.ToArray();
        grasses.SetUVs(0, proceduralGrassGeometry.uvChainList.ToArray());

        grasses.RecalculateNormals();
        grasses.RecalculateBounds();
        grasses.RecalculateTangents();

        var meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = grasses;

        var renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material = material;
    }

}
