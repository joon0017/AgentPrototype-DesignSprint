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
    public int enemyNum;
    public Transform[] targetTrs;
    public GameObject killed;
    
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
        wallTrs = new Transform[walls.Length];
        trapTrs = new Transform[traps.Length];
        tr = GetComponent<Transform>();
        startPos = tr.localPosition;
        for (int i = 0; i < walls.Length; i++) wallTrs[i] = walls[i].GetComponent<Transform>();
        for (int i = 0; i < traps.Length; i++) trapTrs[i] = traps[i].GetComponent<Transform>();
    }

    public override void OnEpisodeBegin(){
        AttackNum = 0;
        foreach (Transform target in targetTrs)
        {
            Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
            target.transform.localPosition = rndVec3 + enemySpawner.transform.localPosition;
        }
        tr.localPosition = startPos;


    }


    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor)
    {

        try
        {
            foreach (Transform t in targetTrs) {
                if (killed == t.gameObject) continue;
                sensor.AddObservation(t.localPosition);
            }
        foreach (Transform t in wallTrs) sensor.AddObservation(t.localPosition);
        foreach (Transform t in trapTrs) sensor.AddObservation(t.localPosition);
        sensor.AddObservation(tr.localPosition);
        sensor.AddObservation(AttackNum);
        }
        catch (System.Exception)
        {
            throw;
        }
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
        if(actions.DiscreteActions[0] == 1 && canAttack)
        {
            anim.SetTrigger("Attack");
        }

        if(AttackNum > 10)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
        SetReward(-0.0001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        // Debug.Log($"[0] = {continuousActions[0]} [1] = {continuousActions[1]} : [2] = {discreteActions[0]}");
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

    public void killEnemy(){
        SetReward(1.0f);
        EndEpisode();
    }
}
