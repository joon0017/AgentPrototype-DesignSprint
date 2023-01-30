using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    Dictionary<int,string[]> txtData; 

    void Awake()
    {
        txtData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData(){
        txtData.Add(0, new string[]{""});
        
        txtData.Add(1, new string[]{"이곳에서는 용병 파티를 구성할 수 있어.", "하단에 보이는 용병의 초상화를 드래그하여 + 버튼 위에 올려놓으면 해당 용병을 파티에 추가할 수 있어.\n그 후 용병의 기술에 마우스 커서를 올리면 어떤 기술인지 자세한 설명을 볼 수 있을거야.", "파티의 구성을 완료하면 상단에 보이는 두 개의 버튼 중 하나를 누르면 돼.", "\"전투로!!\"버튼은 훈련되어 있는 용병들과 함께 바로 전장으로 나갈 수 있어.\n이미 훈련된 용병들로 다양한 파티 조합을 구성하여 전장에 참여할 수 있지.", "\"훈련장으로\"버튼은 구성한 용병 파티를 단장의 입맛대로 훈련시킬 수 있어.\n훈련되어 있는 용병들의 부족한 부분을 발견했다면 단장이 직접 훈련시키는 것도 좋은 방법이 될 수 있을거야.", "자, 그럼 이제 파티를 구성해봐!"});
        
        txtData.Add(2, new string[]{});
        
        txtData.Add(3, new string[]{});
        
        txtData.Add(4, new string[]{});
    }


    public string GetTxt(int id, int strid){
        if(strid == txtData[id].Length)
            return null;
        else
            return txtData[id][strid];
    }
}
