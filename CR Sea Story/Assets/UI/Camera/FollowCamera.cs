using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    private Vector3 _playerPos;
    float _deltaTime = 0.0f;

    void LateUpdate()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        _playerPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, _playerPos, 0.03f * _deltaTime);
        _deltaTime += Time.deltaTime;
    }
}
