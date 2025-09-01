using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour{
    void Update(){
        Destroy(gameObject,1.0f);
    }
}
