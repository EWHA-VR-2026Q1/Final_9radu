using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasLayerMove : MonoBehaviour
{

    public Transform _trackedObject;
    public bool _track;
    public float _rotateSpeed;

    [HideInInspector] public float _deltaX;
    [HideInInspector] public float _deltaY;


    // Use this for initialization
    void Start()
    {
        if (_trackedObject == null)
        {
            _trackedObject = Camera.main.transform;
        }
    }

    // Update is called once per frame
    // 수정된 Update 함수
    void Update()
    {
        if (_trackedObject == null) return;

        // 그래프 출렁임을 위한 델타값 계산 (이건 원래 잘 되던 거니 그대로 유지)
        _deltaX = Mathf.Abs(Mathf.DeltaAngle(this.transform.eulerAngles.x, _trackedObject.eulerAngles.x));
        _deltaY = Mathf.Abs(Mathf.DeltaAngle(this.transform.eulerAngles.y, _trackedObject.eulerAngles.y));

        if (_trackedObject == null) return;

        if (_track)
        {
            // 1. 회전은 기존처럼 부드러운 보간 (여기에 쫀득함이 다 들어있어)
            Quaternion targetRotation = _trackedObject.rotation;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Mathf.SmoothStep(0.0f, 1.0f, _rotateSpeed));

            // 2. 위치도 똑같이 부드러운 보간 (여기가 핵심!)
            // '순간이동'이 아니라 '부드러운 추적'을 하니까 뻣뻣함이 사라져.
            Vector3 targetPos = _trackedObject.position + (_trackedObject.forward * 0.6f);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, _rotateSpeed);
        }
    }
}
