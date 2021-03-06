﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public enum Sensitivity {HIGH,LOW};
    public Sensitivity sensitivity = Sensitivity.HIGH;
    public bool targetInSight = false;
    public float fieldOfVision= 45f;
    public Transform myEyes = null;
    public Transform npcTransform = null;
    private SphereCollider sphereCollider = null;
    public  Vector3 angle2;
    public double distance;
    public double check1Dist;
    public GameObject target;
    public Vector3 lastknownSight = Vector3.zero;

    private void Awake()
    {
        npcTransform= GetComponent<Transform>();
        sphereCollider= GetComponent<SphereCollider>();
        lastknownSight=npcTransform.position;

    }

    bool  InMyFieldofVision(){
        Vector3 dirToTarget = target.transform.position - myEyes.position;
        float angle=Vector3.Angle(myEyes.forward, dirToTarget);
        angle2= dirToTarget;
        distance=Mathf.Sqrt(Mathf.Pow(angle2.x,2) +Mathf.Pow(angle2.y,2)+Mathf.Pow(angle2.z,2));
        
        if(angle<= fieldOfVision){
            return true;
        }
        else{
            return false;

        }

    }


    void  UpdateSight(){
        switch(sensitivity){
            case Sensitivity.HIGH:
            targetInSight=InMyFieldofVision() && ClearLineOfSight();
            break;
            case Sensitivity.LOW:
            targetInSight=InMyFieldofVision() || ClearLineOfSight();
            break;
        }
    }
        bool ClearLineOfSight(){
        if(distance <= 80){
            return true;
        }
        return false;
    }

    private void OnTriggerStay(Collider other){
        UpdateSight();
        if(targetInSight){
            lastknownSight=target.transform.position;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            return;
        }
        targetInSight=false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
