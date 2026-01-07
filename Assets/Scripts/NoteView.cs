using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteView : MonoBehaviour
{
    private int noteIndex;
    private NotesManager manager;

    public void Setup(int index, NotesManager mgr, Vector3 spawnPos, Quaternion rot, Transform parent)
    {
        noteIndex = index;
        manager = mgr;
        transform.SetParent(parent);
        transform.position = spawnPos;
        transform.rotation = rot;
        gameObject.SetActive(true);
    }

    public void Release()
    {
        gameObject.SetActive(false);
        manager.ReleaseNote(this);
    }
}
