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
    Transform tr;
    Vector3 startPos;
    int maxCount;
    int killCount;

    //Control Variables
    public float speed;
    public bool canMove = true;
    public bool canAttack = true;

    public BoxCollider attackArea;
    public Vector3 moveVec;
    public Animator anim;
    public override void Initialize(){
        tr = GetComponent<Transform>();
        startPos = tr.localPosition;
        maxCount = targets.Length;
    }

    public override void OnEpisodeBegin(){
        //reset position
        tr.localPosition = startPos;

        //reset kill count
        killCount = 0;

        //reset enemy
        foreach (Transform target in targets)
        {
            Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
            target.transform.localPosition = rndVec3 + enemySpawner.transform.localPosition;
            target.gameObject.SetActive(true);
        }
    }

    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor){
        //Agent Position
        sensor.AddObservation(tr.localPosition);

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
        SetReward(-0.0005f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetReward(-0.01f);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("DeadZone"))
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        Debug.Log($"[0] = {continuousActions[0]}, [1] = {continuousActions[1]}");
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Target"){
            canMove=false;
            transform.LookAt(other.transform.position);
            anim.SetTrigger("Attack");
        }
    }

    //reset velocity
    public void onCollisionExit(Collision c){
        moveVec = Vector3.zero;
    }

    //anim methods
    public virtual void AttackStart(){
        canMove=false;
        canAttack=false;
    }
    public virtual void Damage(){
        attackArea.enabled=true;
    }
    public virtual void AttackEnd(){
        canMove = true;
        canAttack = true;
        attackArea.enabled=false;
    }

    public void KillEnemy(){
        killCount++;
        if(killCount >= maxCount){
            SetReward(0.3f);
            EndEpisode();
        }
    }
}
