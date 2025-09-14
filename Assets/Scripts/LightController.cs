using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour{
    [SerializeField] float Speed = 3;
    [SerializeField] float num = 0;
    Renderer rend;
    float alfa = 0.0f;

    void Start() {
        rend = GetComponent<Renderer>();
    }

    void Update() {
        if (!(rend.material.color.a <= 0)) {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
        }

        if (num == 1) if (Input.GetKeyDown(KeyCode.F)) ColorChange();
        if (num == 2) if (Input.GetKeyDown(KeyCode.V)) ColorChange();
        if (num == 3) if (Input.GetKeyDown(KeyCode.N)) ColorChange();
        if (num == 4) if (Input.GetKeyDown(KeyCode.J)) ColorChange();
        alfa -= Time.deltaTime * Speed;
    }

    public void ColorChange() {
        alfa = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
    }
}
