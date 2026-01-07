using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteView : MonoBehaviourÅ@{
    int noteIndex;
    NotesManager manager;

    Vector3 baseLinePos;
    Vector3 offset;
    float speed;



    public void Setup( int index, NotesManager mgr,  Vector3 linePos,  Vector3 localOffset, float noteSpeed,  Quaternion rot) {
        noteIndex = index;
        manager = mgr;
        baseLinePos = linePos;
        offset = localOffset;
        speed = noteSpeed;

        transform.SetParent(null);
        transform.rotation = rot;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    void Update() {
        float remain = manager.NotesTime[noteIndex] - manager.AudioTime;
        float z = remain * speed;

        transform.position = baseLinePos + offset + new Vector3(0f, 0f, z);

        if (remain < -manager.MissTime) manager.DespawnNote(noteIndex);
    }

    public void Release() {
        gameObject.SetActive(false);
        manager.ReleaseNote(this);
    }
}
