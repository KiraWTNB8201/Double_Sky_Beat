using UnityEngine;
using UnityEngine.UI;

public class ParallelogramPanel : Graphic {
    [SerializeField] private float skew = 30f; // 傾き量（ピクセル単位）

    protected override void OnPopulateMesh(VertexHelper vh) {
        vh.Clear();

        Rect rect = rectTransform.rect;
        float left = rect.xMin;
        float right = rect.xMax;
        float top = rect.yMax;
        float bottom = rect.yMin;

        // 左右にskew分ずらす
        Vector3 v0 = new Vector3(left + skew, bottom);
        Vector3 v1 = new Vector3(right + skew, bottom);
        Vector3 v2 = new Vector3(right - skew, top);
        Vector3 v3 = new Vector3(left - skew, top);

        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;

        vert.position = v0;
        vh.AddVert(vert);
        vert.position = v1;
        vh.AddVert(vert);
        vert.position = v2;
        vh.AddVert(vert);
        vert.position = v3;
        vh.AddVert(vert);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
}