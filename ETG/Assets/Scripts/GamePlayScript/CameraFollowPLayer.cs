using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPLayer : MonoBehaviour
{
    public static CameraFollowPLayer CInstance { get; private set; }
    public GameController controller;
    Vector3 target, mousePos, refVel, shakeOffset;

    float cameraDist = 3.5f;

    float smoothTime = 0.2f, zStart = -50; //zStart tu gan

    float shakeMag, shakeTimeEnd;

    Vector3 shakeVector;

    bool shaking;
    private void Awake()
    {
        if (CInstance != null && CInstance != this) Destroy(GameObject.FindGameObjectsWithTag("MainCamera")[1]);
        else CInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(controller.playerObj != null) FollowMouse();
        else transform.position = new Vector3(5f, -3f, -10f);
    }
    void FollowMouse()
    {
        mousePos = CaptureMousePos();
        shakeOffset = UpdateShake();
        target = UpdateTargetPos();
        UpdateCameraPostion();
    }

    Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max) ret = ret.normalized;
        return ret;
    }

    Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = controller.playerObj.transform.position + mouseOffset;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }

    void UpdateCameraPostion()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;

    }

    public void Shake(Vector3 direction, float magnitude, float length)
    {
        shaking = true;
        shakeVector = direction;
        shakeMag = magnitude;
        shakeTimeEnd = Time.time + length;
    }

    Vector3 UpdateShake()
    {
        if (!shaking || Time.time > shakeTimeEnd)
        {
            shaking = false;
            return Vector3.zero;
        }
        Vector3 tempOffset = shakeVector;
        tempOffset *= shakeMag;
        return tempOffset;
    }
}
