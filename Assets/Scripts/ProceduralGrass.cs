using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralGrassGeometry
{
    List<Vector3> verticesList = new List<Vector3>();
    List<Vector2> uvList = new List<Vector2>();
    List<int> trianglesList = new List<int>();

    public ProceduralGrassGeometry(float grassWidth, float grassHeight, int grassSplitCnt, float bending)
    {
        var unitHeight = grassHeight / (float)grassSplitCnt;
        var Unitbending = bending / ((float)grassSplitCnt * (float)grassSplitCnt);

        for (int i = 0; i < grassSplitCnt; i++)
        {
            verticesList.AddRange(new Vector3[]
                {
                        new Vector3(0f          ,unitHeight * i         , i * i * Unitbending) ,
                        new Vector3(grassWidth  ,unitHeight * i         , i * i * Unitbending) ,
                });
            uvList.AddRange(new Vector2[]
                {
                        new Vector2(0f ,(1f / grassSplitCnt) * i),
                        new Vector2(1f ,(1f / grassSplitCnt) * i),
                }
            );
        }

        // 草の頂
        verticesList.Add(new Vector3(grassWidth / 2f, unitHeight * grassSplitCnt, bending));
        uvList.Add(new Vector2(1f, 1f));

        for (int i = 0; i < grassSplitCnt - 1; i++)
        {
            trianglesList.AddRange(new int[] {
                    ( i * 2 ),
                    ( i * 2 + 2),
                    ( i * 2 + 1),
                    ( i * 2 + 1),
                    ( i * 2 + 2),
                    ( i * 2 + 3),
                });
        }

        // 頂点インデックスを設定
        trianglesList.AddRange(new int[] {
                    ( (grassSplitCnt - 1) * 2 ),
                    ( (grassSplitCnt - 1) * 2 + 2),
                    ( (grassSplitCnt - 1) * 2 + 1),
                });
    }

    public (IEnumerable<Vector3> verticesList, IEnumerable<Vector2> uvList, IEnumerable<int> trianglesList) CreateGrass(Vector3 worldPosision, Quaternion rotaion, Vector3 scale, int VerticesOffset)
    {
        return (
            verticesList.Select(xx => rotaion * Vector3.Scale(xx, scale) + worldPosision)
            , uvList.Select(xx => xx)
            , trianglesList.Select(xx => xx + VerticesOffset)
        );
    }


    List<Vector3> verticesChainList = new List<Vector3>();
    List<int> trianglesChainList = new List<int>();
    List<Vector2> uvChainList = new List<Vector2>();

    public void CreateChain(Vector3 worldPosision, Quaternion rotaion, Vector3 scale)
    {
        var geo = CreateGrass(worldPosision, rotaion, scale, verticesChainList.Count());

        verticesChainList.AddRange(geo.verticesList);
        trianglesChainList.AddRange(geo.trianglesList);
        uvChainList.AddRange(geo.uvList);
    }

    public Mesh GetMesh()
    {
        var mesh = new Mesh();
        mesh.vertices = verticesChainList.ToArray();
        mesh.triangles = trianglesChainList.ToArray(); // geo.trianglesList.ToArray();
        mesh.SetUVs(0, uvChainList.ToArray());

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        return mesh;
    }

    public void ResetChain()
    {
        verticesChainList.Clear();
        trianglesChainList.Clear();
        uvChainList.Clear();
    }
}
