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
    public Transform wallTr1;
    public Transform wallTr2;
    public Transform wallTr3;
    
    private Material originMt;      
    public Material badMt;          
    public Material goodMt;    

    private Vector3 dir;
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

        tr.localPosition = new Vector3(Random.Range(-6f,6f),0.05f,Random.Range(-6f,6f));
        targetTr.localPosition = new Vector3(Random.Range(-6f,6f),0f,Random.Range(-6f,6f));
        wallTr1.localPosition = new Vector3(Random.Range(-6f,6f),0.55f,Random.Range(-6f,6f));
        wallTr2.localPosition = new Vector3(Random.Range(-6f,6f),0.55f,Random.Range(-6f,6f));
        wallTr3.localPosition = new Vector3(Random.Range(-6f,6f),0.55f,Random.Range(-6f,6f));

        StartCoroutine(RevertMaterial());
    }
    
    IEnumerator RevertMaterial()
    {
        yield return new WaitForSeconds(0.4f);
        indicatorRd.material = originMt;
    }
    
    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        sensor.AddObservation(targetTr.localPosition);
        sensor.AddObservation(tr.localPosition);
        sensor.AddObservation(wallTr1.localPosition);
        sensor.AddObservation(wallTr2.localPosition);
        sensor.AddObservation(wallTr3.localPosition);
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float h = Mathf.Clamp(actions.ContinuousActions[0], -1.0f, 1.0f);
        float v = Mathf.Clamp(actions.ContinuousActions[1], -1.0f, 1.0f);
        dir = (Vector3.forward * v) + (Vector3.right * h);
        rb.AddForce(dir.normalized * 150.0f);
        
        SetReward(-0.001f);

    }
    private void LateUpdate() {
        tr.rotation = Quaternion.LookRotation(dir);
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        Debug.Log($"[0] = {continuousActions[0]} [1] = {continuousActions[1]}");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("DEAD_ZONE"))
        {
            indicatorRd.material = badMt;
            SetReward(-1.0f);
            EndEpisode();
        }
        else if(collision.gameObject.CompareTag("TARGET"))
        {
            indicatorRd.material = goodMt;
            SetReward(1.0f);
            EndEpisode();
        }
    }
}
