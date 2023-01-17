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
    private Vector3 startPos;
    public int AttackNum;

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


    public float speed;
    public bool canMove = true;
    public bool canAttack = true;
    public float MissAttack;

    public BoxCollider attackArea;
    public Vector3 moveVec;
    public Animator anim;




    public void SetRWD(float number){
        SetReward(number);
    }
    
    public override void Initialize()
    {
        AttackNum = 0;
        targets = new GameObject[enemySpawner.GetComponent<GenerateMap>().EnemyNum];
        targetTrs = new Transform[targets.Length];
        wallTrs = new Transform[walls.Length];
        trapTrs = new Transform[traps.Length];
        tr = GetComponent<Transform>();
        startPos = tr.localPosition;
        for (int i = 0; i < walls.Length; i++) wallTrs[i] = walls[i].GetComponent<Transform>();
        for (int i = 0; i < traps.Length; i++) trapTrs[i] = traps[i].GetComponent<Transform>();

        enemySpawner.GetComponent<GenerateMap>().Agent = this.gameObject;
    }

    public override void OnEpisodeBegin(){
        enemySpawner.GetComponent<GenerateMap>().Spawn();
        tr.localPosition = startPos;

        StartCoroutine(setTargets());
    }

    IEnumerator setTargets(){
        //wait for all targets to find its position
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < targets.Length; i++) targetTrs[i] = targets[i].GetComponent<Transform>();
    }

    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        foreach (Transform t in targetTrs) sensor.AddObservation(t.localPosition);
        foreach (Transform t in wallTrs) sensor.AddObservation(t.localPosition);
        foreach (Transform t in trapTrs) sensor.AddObservation(t.localPosition);
        sensor.AddObservation(tr.localPosition);
        sensor.AddObservation(AttackNum);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if(canMove){
            float h = Mathf.Clamp(actions.ContinuousActions[0], -1.0f, 1.0f);
            float v = Mathf.Clamp(actions.ContinuousActions[1], -1.0f, 1.0f);
            moveVec = new Vector3(h,0,v);
            tr.position += moveVec * speed * Time.deltaTime;
            tr.LookAt(tr.position + moveVec);
            anim.SetBool("Moving",moveVec != Vector3.zero);
        }
        if(actions.ContinuousActions[2] == 1 && canAttack)
        {
            anim.SetTrigger("Attack");
        }
        SetReward(-0.0001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        continuousActions[2] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
        // Debug.Log($"[0] = {continuousActions[0]} [1] = {continuousActions[1]} : [2] = {continuousActions[2]}");
    }

    public void AttackStart(){
        canMove=false;
        canAttack=false;
        SetReward(MissAttack);
        AttackNum++;
    }
    public void Damage(){
        attackArea.enabled=true;
    }
    public void AttackEnd(){
        canMove = true;
        canAttack = true;
        attackArea.enabled=false;
    }


    public void onCollisionExit(Collision c){
        moveVec = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetReward(-0.01f);
        }
        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("DeadZone"))
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }
}
