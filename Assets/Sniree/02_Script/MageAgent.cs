using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class MageAgent : Agent
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

    public Vector3 moveVec;
    public Animator anim;


    public GameObject spell;
    public Vector3 spellSpawn;
    public float rayDistance = 5f;
    public float raySpread = 10f;
    Ray ray1;
    Ray ray2;
    Ray ray3;
    Vector3 rayDirection;
    Quaternion spreadRotation;
    Vector3 spreadDirection;
    Vector3 origin;
    float temp = 0;

    //외부 객체에서 Reward를 설정할 수 있도록 해 줌
    public void SetRWD(float number){
        SetReward(number);
    }
    
    //새로 생성 되었을 때 기존 값을 입력해줌
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

    //에피소드 시작 시 한번 불러옴
    public override void OnEpisodeBegin(){
        //공격 회수 초기화
        AttackNum = 0;

        //적 위치 재배치
        foreach (Transform target in targetTrs)
        {
            Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
            target.transform.localPosition = rndVec3 + enemySpawner.transform.localPosition;
        }

        //플레이어 위치 초기화
        tr.localPosition = startPos;


    }


    //관측값 입력받음.
    public override void CollectObservations(Unity.MLAgents.Sensors.VectorSensor sensor)
    {
        try
        {
            foreach (Transform t in targetTrs) {
                if (killed == t.gameObject) continue;
                sensor.AddObservation(t.localPosition);
            }
        //taget location
        foreach (Transform t in targetTrs) {
            if (killed == t.gameObject) continue;
            sensor.AddObservation(t.localPosition);
        }
        //target Number
        sensor.AddObservation(enemyNum);
        //trap 위치
        foreach (Transform t in wallTrs) sensor.AddObservation(t.localPosition);
        //wall 위치
        foreach (Transform t in trapTrs) sensor.AddObservation(t.localPosition);
        //플레이어 위치
        sensor.AddObservation(tr.localPosition);
        //플레이어 방향
        sensor.AddObservation(tr.forward);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    //입력값을 받을 때 마다 실행됨
    /*
    대체적으로 움직임 관련 함수를 여기에 넣으면 됨.
    Update()와 비슷한 기능을 함. (매 프레임마다 새로운 Action을 받기 때문)
    Editor 상에 Max Step을 설정해주면, Max Step이 되면 자동으로 Episode가 끝나게 됨.
    */
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
       
        SetReward(-0.0001f);
    }

    //유저가 테스트를 하기 위해 사용됨. 입력 받는 값이 ActionBuffers에 저장됨으로, 외부 Script에서 움직임 관련 함수가 있으면 
    //입력값이 넘어가지 않게 되어서 실행이 안됨.
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    //공격 애니메이션 관련 함수들.
    public void AttackStart(){
        canMove=false;
        canAttack=false;
        SetReward(MissAttack);
        AttackNum++;

        RaycastHit hit;

        //ray는 항상 캐릭터의 앞을 바라본다
        rayDirection = transform.forward;
        //바닥에서 1 떨어져야 적을 확인 가능
        origin = transform.position + new Vector3(0, 1, 0);
        //직선 앞으로 쏘아지는 ray
        ray1 = new Ray(origin, rayDirection);
        Debug.DrawRay(ray1.origin, ray1.direction * rayDistance, Color.red);

        //ray의 각도를 계산하는 식
        spreadRotation = Quaternion.AngleAxis(-raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        //2번째 ray (왼쪽 10도로 쏘아지는 ray)
        ray2 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray2.origin, ray2.direction * rayDistance, Color.green);

        //ray의 각도를 계산하는 식
        spreadRotation = Quaternion.AngleAxis(raySpread, transform.up);
        spreadDirection = spreadRotation * rayDirection;
        //3번째 ray (오른쪽 10도로 쏘아지는 ray)
        ray3 = new Ray(origin, spreadDirection);
        Debug.DrawRay(ray3.origin, ray3.direction * rayDistance, Color.blue);
        
        //ray1||2||3에 적이 있을 경우, 공격 가능하고, 마법 소환 위치를 적이 있는 위치로 변경
        if(Physics.Raycast(ray1, out hit, rayDistance)||Physics.Raycast(ray2, out hit, rayDistance)||Physics.Raycast(ray3, out hit, rayDistance)){
            if(hit.collider.tag == "Target"){
                canAttack=true;
                spellSpawn = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
                canMove=false;
                anim.SetTrigger("Attack");
            }   
        }
        //적을 못찾으면 공격 불가능
        //swap rayDistance and temp ->  공격을 하는 중에는 다시 공격을 가능하게 하면 안되기 때문에 ray의 길이를 0으로 함으로써 적을 찾지 못하게 함
        (rayDistance, temp) = (temp, rayDistance);
    }
    public void Damage(){
        Instantiate(spell, spellSpawn, transform.rotation);
    }
    public void AttackEnd(){
        canMove = true;
        canAttack = true;
        (rayDistance, temp) = (temp, rayDistance);
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
        else if (collision.gameObject.CompareTag("Target"))
        {
            SetReward(-0.5f);
            EndEpisode();
        }
    }

    public void killEnemy(){
        SetReward(1.0f);
        enemyNum--;
        if(enemyNum == 0){
            EndEpisode();
        }
    }
}
