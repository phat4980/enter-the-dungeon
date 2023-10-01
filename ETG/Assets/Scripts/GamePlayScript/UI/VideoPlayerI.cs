using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerI : MonoBehaviour
{
    public VideoPlayer v;

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null) v.targetCamera = Camera.main;
    }
}
