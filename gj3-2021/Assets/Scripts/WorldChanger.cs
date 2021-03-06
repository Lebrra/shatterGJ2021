﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChanger : MonoBehaviour
{
    public int worldIndex;

    public List<GameObject> world1Objects;
    public List<GameObject> world2Objects;
    public List<GameObject> world3Objects;

    //this can change to backgrounds sprites later
    public Color[] worldColors;

    private void Start()
    {
        setWorld(0);
    }

    public void setWorld(float value)
    {
        int index = (int) value;

        if (index > 3 || index < 0) return;

        worldIndex = index;
        Camera.main.backgroundColor = worldColors[index];
        for (int i = 0; i < 3; i++)
        {
            if (i == index) setWorldState(i, false);
            else setWorldState(i, true);
        }
    }

    void setWorldState(int index, bool enabled)
    {
        switch (index)
        {
            case 0:
                foreach (GameObject go in world1Objects) go.SetActive(enabled);
                break;

            case 1:
                foreach (GameObject go in world2Objects) go.SetActive(enabled);
                break;

            case 2:
                foreach (GameObject go in world3Objects) go.SetActive(enabled);
                break;

            default: return;
        }
    }
}