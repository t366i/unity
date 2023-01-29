using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour 
{
    PlayerData MyPlayerData;
    MyJson MyJsonChanger;

    string path;

    private void Awake()
    {
        MyPlayerData = new PlayerData();
        MyJsonChanger = new MyJson();

        path = Application.dataPath + "/Resources/";
    }

    private void Start()
    {

        // Test
        MyPlayerData.Name = "TestArcher";
        MyPlayerData.Type = PlayerType.Archer;
        MyPlayerData.playerDamage = 50.0f ;
        MyPlayerData.PlayerAlive = true;
        MyPlayerData.Hp = 100;

        // 파일 불러오기
        // saveData 가 null 인 경우, 새롭게 생성.

        // Json -> string 전환
        // string JsonDatas = JsonUtility.ToJson(MyPlayerData);

        // string(Json) -> Object(MyPlayerData) 로 전환


        // 수정 파일 저장

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log($"Player Name is : {MyPlayerData.Name}");
            Debug.Log($"Player Type is : {MyPlayerData.Type}");
            Debug.Log($"Player playerDamage is : {MyPlayerData.playerDamage}");
            Debug.Log($"Player Alive is : {MyPlayerData.PlayerAlive}");
            Debug.Log($"Player Hp is : {MyPlayerData.Hp}");


            string tempJson = JsonUtility.ToJson(MyPlayerData);

            File.WriteAllText(path + "saveData5", tempJson);

        }
    }


}
