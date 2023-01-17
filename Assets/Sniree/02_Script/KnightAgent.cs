using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class KnightAgent : Agent
{
    private Transform tr;
    private Rigidbody rb;
    private Vector3 dir;

    //enemies (target)
    public GameObject[] targets;
    private Transform[] targetTrs;
    
    //wall (obstacle)
    public GameObject[] walls;
    private Transform[] wallTrs;

    //trap (obstacle)
    public GameObject[] traps;
    private Transform[] trapTrs;

    public GameObject enemySpawner;

    public void SetRWD(float number){
        SetReward(number);
    }
    
    public override void Initialize()
    {
        targetTrs = new Transform[targets.Length];
        wallTrs = new Transform[walls.Length];
        trapTrs = new Transform[traps.Length];
        for (int i = 0; i < walls.Length; i++) wallTrs[i] = walls[i].GetComponent<Transform>();
        for (int i = 0; i < traps.Length; i++) trapTrs[i] = traps[i].GetComponent<Transform>();
    }

    public override void OnEpisodeBegin(){
        enemySpawner.GetComponent<GenerateMap>().Spawn();
        StartCoroutine(setTargets());
    }

    IEnumerator setTargets(){
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < targets.Length; i++) targetTrs[i] = targets[i].GetComponent<Transform>();
    }

    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        foreach (Transform t in targetTrs) sensor.AddObservation(t.localPosition);
        foreach (Transform t in wallTrs) sensor.AddObservation(t.localPosition);
        foreach (Transform t in trapTrs) sensor.AddObservation(t.localPosition);
        sensor.AddObservation(tr.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        SetReward(-0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        continuousActions[2] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
        Debug.Log($"[0] = {continuousActions[0]} [1] = {continuousActions[1]} : [2] = {continuousActions[2]}");
    }
}
