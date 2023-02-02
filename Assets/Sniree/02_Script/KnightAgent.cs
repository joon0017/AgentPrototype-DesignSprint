using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine.UI;

public class KnightAgent : Agent
{
    private Transform tr;
    private Rigidbody rb;
    private Vector3 dir;
    private Vector3 startPos;
    public int AttackNum;
    private int previousChoice = 0;
    private float distance;

    //enemies (target)
    public int enemyNum = 0;
    public Transform[] targetTrs;
    public GameObject killed;
    
    //wall (obstacle)
    public GameObject[] walls;
    private Transform[] wallTrs;

    //trap (obstacle)
    public GameObject[] traps;
    private Transform[] trapTrs;

    public GameObject enemySpawner;
    public GameObject ground;

    public float speed;
    public bool canMove = true;
    public bool canAttack = true;
    public float MissAttack;

    public BoxCollider attackArea;
    public Vector3 moveVec;
    public Animator anim;

    public GameObject cube;



    //외부 객체에서 Reward를 설정할 수 있도록 해 줌
    public void SetRWD(float number){
        AddReward(number);
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
        distance = 0;
    }

    //에피소드 시작 시 한번 불러옴
    public override void OnEpisodeBegin(){
        //공격 회수 초기화
        AttackNum = 0;

        //적 위치 재배치
        for (int i = 0; i<= enemyNum; i++)
        {
            Vector3 rndVec3 = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
            targetTrs[i].transform.localPosition = rndVec3 + enemySpawner.transform.localPosition;
            targetTrs[i].gameObject.SetActive(true);
        }

        //플레이어 위치 초기화
        tr.localPosition = startPos;
        CubeColorChanger(11);

        distance = 0;
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
        //trap 위치
        foreach (Transform t in wallTrs) sensor.AddObservation(t.localPosition);
        //wall 위치
        foreach (Transform t in trapTrs) sensor.AddObservation(t.localPosition);
        //플레이어 위치
        sensor.AddObservation(tr.localPosition);
        //enemy location
        sensor.AddObservation(targetTrs[0].localPosition);
        // sensor.AddObservation(rb.velocity.x);
        // sensor.AddObservation(rb.velocity.z);
        float distanceA = Vector3.Distance(tr.localPosition, targetTrs[0].localPosition);
        sensor.AddObservation(distanceA);
        //target Number
        sensor.AddObservation(enemyNum);
        
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
        float distanceB = Vector3.Distance(tr.localPosition, targetTrs[0].localPosition);

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
            CubeColorChanger(3);
        }

        int currentChoice = actions.DiscreteActions[0];
        if(currentChoice == previousChoice){
            AddReward(0.01f);
            previousChoice = currentChoice;
            CubeColorChanger(4);
        }

        if(distanceB < distance){
            AddReward(0.01f);
            distance = distanceB;
            CubeColorChanger(8);
        }
        else if(distanceB > distance){
            AddReward(-0.01f);
            distance = distanceB;
            CubeColorChanger(6);
        }

        AddReward(-0.0001f);
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
        // Debug.Log($"[0] = {continuousActions[0]} [1] = {continuousActions[1]} : [2] = {discreteActions[0]}");
    }

    //공격 애니메이션 관련 함수들.
    public void AttackStart(){
        canMove=false;
        canAttack=false;
        AddReward(MissAttack);
        AttackNum++;
    }
    public void Damage(){
        attackArea.enabled=true;
    }
    public void AttackEnd(){
        canMove = true;
        attackArea.enabled=false;
        StartCoroutine(CoolTime(2f));
    }


    public void onCollisionExit(Collision c){
        moveVec = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.1f);
            CubeColorChanger(3);
        }
        else if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("DeadZone"))
        {
            AddReward(-3.0f);
            CubeColorChanger(0);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            AddReward(-1.0f);
            CubeColorChanger(0);
            EndEpisode();
        }
    }

    public void killEnemy(){
        AddReward(10.0f);
        enemyNum--;
        if(enemyNum == -1){
            CubeColorChanger(2);
            EndEpisode();
        }
    }

    IEnumerator CoolTime(float time){
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    //이전과 같은 입력을 선택하면 +0.005
    //적 거리 계산 해서 가까워 질수록 +0.005

    public void CubeColorChanger(int num){
        if(num == 0){
            cube.GetComponent<Renderer>().material.color = Color.red;
        }
        else if(num == 1){
            cube.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if(num == 2){
            cube.GetComponent<Renderer>().material.color = Color.green;
        }
        else if(num == 3){
            cube.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if(num == 4){
            cube.GetComponent<Renderer>().material.color = Color.black;
        }
        else if(num == 5){
            cube.GetComponent<Renderer>().material.color = Color.white;
        }
        else if(num == 6){
            cube.GetComponent<Renderer>().material.color = Color.gray;
        }
        else if(num == 7){
            cube.GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if(num == 8){
            cube.GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if(num == 9){
            cube.GetComponent<Renderer>().material.color = Color.clear;
        }
        else{
            cube.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
