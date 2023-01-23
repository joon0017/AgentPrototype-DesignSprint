using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class Gilbert_Agent_Prototype1 : Agent
{
    //Agent Components
    [SerializeField]GameObject enemySpawner;
    [SerializeField]Transform[] targets;
    Rigidbody rb;
    Transform tr;
    Vector3 startPos;
    int maxCount;
    int killCount;
    int lazyPoint;

    //Control Variables
    public float speed;
    bool canMove = true;

    public BoxCollider attackArea;
    Vector3 moveVec;
    public Animator anim;
    public override void Initialize(){
        tr = GetComponent<Transform>();
        startPos = tr.localPosition;
        maxCount = targets.Length;
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin(){
        //reset position
        tr.localPosition = startPos;

        //reset kill count
        killCount = 0;
        lazyPoint = 0;

        //reset enemy
        foreach (Transform target in targets)
        {
            target.gameObject.SetActive(true);
            Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
            target.transform.localPosition = rndVec3 + enemySpawner.transform.localPosition;
        }
    }

    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor){
        //Agent Position
        sensor.AddObservation(tr.localPosition);

        sensor.AddObservation(killCount);

        //Enemy Position
        foreach (Transform target in targets)
        {
            sensor.AddObservation(target.localPosition);
        }
    }

    public override void OnActionReceived(ActionBuffers actions){
        if(canMove){
            float h = Mathf.Clamp(actions.ContinuousActions[0],-1f,1f);
            float v = Mathf.Clamp(actions.ContinuousActions[1],-1f,1f);
            moveVec = new Vector3(h,0,v);
            tr.localPosition += moveVec * speed * Time.deltaTime;
            tr.LookAt(tr.position + moveVec);
            anim.SetBool("Moving",moveVec != Vector3.zero);
        }
        rb.velocity = Vector3.zero;
        SetReward(-(( lazyPoint * (1 -(killCount /  maxCount ))) / 100000f));
    }

    private void Update() {
        if(Mathf.Floor(Time.time ) % 2 == 1){
            ++lazyPoint;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetReward(-0.05f);
        }
        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("DeadZone"))
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        // Debug.Log($"[0] = {continuousActions[0]}, [1] = {continuousActions[1]}");
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Target"){
            canMove=false;
            transform.LookAt(other.transform.position);
            anim.SetTrigger("Attack");
        }
    }

    //anim methods
    public void AttackStart(){
        canMove=false;
    }
    public void Damage(){
        attackArea.enabled=true;
    }
    public void AttackEnd(){
        canMove = true;
        attackArea.enabled=false;
    }

    public void KillEnemy(GameObject hitEnemy){
        hitEnemy.SetActive(false);
        killCount++;
        lazyPoint = 0;
        if(killCount >= maxCount){
            SetReward(+1.0f);
            EndEpisode();
            return;
        }
        SetReward(+0.5f);
    }
}
