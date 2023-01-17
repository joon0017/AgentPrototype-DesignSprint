using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    Dictionary<int,string[]> txtData; 
    // Start is called before the first frame update
    void Awake()
    {
        txtData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData(){
        txtData.Add(1, new string[]{ "이곳은 훈련시킬 용병을 선택하는 곳이야.", "하단에 보이는 용병의 초상화를 드래그하여 + 버튼 위에 올려놓으면 해당 용병을 파티에 추가할 수 있어.",
          "한 파티로 구성된 용병들은 다함께 훈련하며 협동능력을 향상시킬 수 있어.", "파티의 구성을 완료했다면 상단의 다음단계 버튼을 눌러서 훈련을 시작할 수 있어.\n단장이 생각하는 가장 이상적인 파티를 구성하여 함께 훈련시켜 봐."});
    }

    // Update is called once per frame
    public string GetTxt(int id, int strid){
        if(strid == txtData[id].Length)
            return null;
        else
            return txtData[id][strid];
    }
}
