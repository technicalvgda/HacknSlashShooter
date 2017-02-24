﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform _player;
    public float CameraHeight = 11, CameraHorizontal = 0, CameraVertical = -5;
    public bool LockedToPlayer = true;
    public float CameraFollowDelay = 0.4f; //How behind the camera is when the player moves
    private float _screenHeight, _screenWidth, _cameraHeight, _cameraTimetoReach;
    private Vector3 _target;
    private Vector3 _camVelocity = Vector3.zero;
    // Use this for initialization
    void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        _cameraTimetoReach = CameraFollowDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //camera following the player
        if (LockedToPlayer)
        {
            _target = _player.position + new Vector3(CameraHorizontal, CameraHeight, CameraVertical);
        }
        this.transform.position = Vector3.SmoothDamp(transform.position, _target, ref _camVelocity, _cameraTimetoReach);
    }

    //Moves the camera to the targetPos in timeToReach seconds
    public void MoveCamera(Transform targetPos, float timeToReach)
    {
        _target = targetPos.position;
        _cameraTimetoReach = timeToReach;
        LockedToPlayer = false;
    }

    //Moves the camera back onto focusing the player
    public void RelockCamera()
    {
        LockedToPlayer = true;
        _cameraTimetoReach = CameraFollowDelay;
        
    }
}
