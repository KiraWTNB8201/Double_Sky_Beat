using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour {
    public TMP_Text textMesh;
    public float amplitude = 5f;    // óhÇÍÇÃïù
    public float frequency = 1f;    // óhÇÍÇÃë¨Ç≥

    private TMP_TextInfo textInfo;
    private Vector3[][] originalVertices;

    void Start() {
        textMesh.ForceMeshUpdate();
        textInfo = textMesh.textInfo;

        originalVertices = new Vector3[textInfo.meshInfo.Length][];
        for (int i = 0; i < originalVertices.Length; i++) {
            originalVertices[i] = new Vector3[textInfo.meshInfo[i].vertices.Length];
            System.Array.Copy(textInfo.meshInfo[i].vertices, originalVertices[i], textInfo.meshInfo[i].vertices.Length);
        }
    }

    void Update() {
        textMesh.ForceMeshUpdate();
        for (int i = 0; i < textInfo.characterCount; i++) {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            float offsetX = Mathf.PerlinNoise(Time.time * frequency + i, 0) * 2 - 1;
            float offsetY = Mathf.PerlinNoise(Time.time * frequency + i, 1) * 2 - 1;

            for (int j = 0; j < 4; j++) {
                vertices[vertexIndex + j] = originalVertices[materialIndex][vertexIndex + j] + new Vector3(offsetX, offsetY, 0) * amplitude;
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++) {
            textMesh.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
