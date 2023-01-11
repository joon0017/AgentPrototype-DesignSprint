using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
public class AgentScript : Agent
{
    
    private Transform tr;
    private Rigidbody rb;

    public Transform targetTr;
    public Renderer indicatorRd;
    
    private Material originMt;      
    public Material badMt;          
    public Material goodMt;         
    //초기화 작업을 위해 한번 호출되는 메소드
    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        originMt = indicatorRd.material;
    }

    public override void OnEpisodeBegin(){
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        tr.localPosition = new Vector3(Random.Range(-6f,6f),0.1f,Random.Range(-6f,6f));
        targetTr.localPosition = new Vector3(Random.Range(-6f,6f),0.1f,Random.Range(-6f,6f));
    }
}
