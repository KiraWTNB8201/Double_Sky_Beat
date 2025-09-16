using UnityEngine;

public class TapParticleSpawner : MonoBehaviour {
    public GameObject particlePrefab;
    public float defaultDistance = 5f;    // 何も当たらない時の距離
    public float offsetFromHit = 0.1f;    // 当たった面の手前に出す量
    public LayerMask hitMask = ~0;        // 必要なら当たり判定レイヤーを限定

    void Update() {
        if (Input.GetMouseButtonDown(0))
            SpawnEffect(Input.mousePosition);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            SpawnEffect(Input.GetTouch(0).position);
    }

    public Canvas effectCanvas; // Inspectorでエフェクト用Canvasを割り当てる

    void SetLayerRecursively(GameObject obj, int layer) {
        obj.layer = layer;
        foreach (Transform t in obj.transform)
            SetLayerRecursively(t.gameObject, layer);
    }

    void SpawnEffect(Vector2 screenPos) {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f));
        GameObject go = Instantiate(particlePrefab, worldPos, Quaternion.identity);
        SetLayerRecursively(go, LayerMask.NameToLayer("Effect"));
    }


}
