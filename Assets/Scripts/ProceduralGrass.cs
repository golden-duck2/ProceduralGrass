using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralGrassGeometry
{
    List<Vector3> verticesList = new List<Vector3>();
    List<Vector2> uvList = new List<Vector2>();
    List<int> trianglesList = new List<int>();

    public ProceduralGrassGeometry(float grassWidth, float grassHeight, int grassSplitCnt)
    {
        var unitHeight = grassHeight / (float)grassSplitCnt;

        for (int i = 0; i < grassSplitCnt; i++)
        {
            verticesList.AddRange(new Vector3[]
                {
                        new Vector3(0f          ,unitHeight * i         ,0f) ,
                        new Vector3(grassWidth  ,unitHeight * i         ,0f) ,
                });
            uvList.AddRange(new Vector2[]
                {
                        new Vector2(0f ,(1f / grassSplitCnt) * i),
                        new Vector2(1f ,(1f / grassSplitCnt) * i),
                }
            );
        }

        // 草の頂
        verticesList.Add(new Vector3(grassWidth / 2f, unitHeight * grassSplitCnt, 0f));
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


    public (IEnumerable<Vector3> verticesList, IEnumerable<Vector2> uvList, IEnumerable<int> trianglesList) CreateGrass(Vector3 worldPosision, int VerticesOffset)
    {
        return (
            verticesList.Select(xx => xx + worldPosision)
            , uvList.Select(xx => xx)
            , trianglesList.Select(xx => xx + VerticesOffset)
        );
    }

}
