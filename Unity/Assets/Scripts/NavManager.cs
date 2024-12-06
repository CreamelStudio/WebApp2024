using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

using static UnityEditor.FilePathAttribute;
public class NavManager : MonoBehaviour
{
    public int bLocation;
    public int nowLoaction;

    public Transform cafeteria;
    public Transform enterance;
    public Transform garbageDump;
    NavMeshAgent nmAgent;

    public int robotNumber;

    [System.Serializable]
    public class LocationData
    {
        public int location;
    }

    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(GetRobotRoutine());
    }

    public void ToCafeteria()
    {
        nmAgent.SetDestination(cafeteria.position);
        StartCoroutine(SendPutRequest(2));
    }
    public void ToEnterance()
    {
        nmAgent.SetDestination(enterance.position);
        StartCoroutine(SendPutRequest(1));
    }
    public void ToGarbageDump()
    {
        nmAgent.SetDestination(garbageDump.position);
        StartCoroutine(SendPutRequest(0));
    }

    IEnumerator GetRobotRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RobotListGet());
        
        StartCoroutine(GetRobotRoutine());
    }

    IEnumerator RobotListGet() {
        string url = "http://127.0.0.1:8000/robot/robot" + robotNumber.ToString();

        // UnityWebRequest에 내장되있는 GET 메소드를 사용한다.
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();  // 응답이 올때까지 대기한다.

        if (www.error == null)  // 에러가 나지 않으면 동작.
        {
            JObject parsedJson = JObject.Parse(www.downloadHandler.text);
            int robotValue = (int)parsedJson["robot" + robotNumber];
            Debug.Log($"Robot Number : {robotNumber} Robot Value : {robotValue}");
            nowLoaction = robotValue;
            if(bLocation != nowLoaction)
            {
                switch (nowLoaction)
                {
                    case 0: nmAgent.SetDestination(garbageDump.position); break;
                    case 1: nmAgent.SetDestination(enterance.position); break;
                    case 2:
                        nmAgent.SetDestination(cafeteria.position); break;
                        bLocation = nowLoaction;
                }
            }
            
        }
        else
        {
            Debug.Log("error");
        }
    }

    private IEnumerator SendPutRequest(int location)
    {
        LocationData data = new LocationData { location = location };
        string jsonData = JsonUtility.ToJson(data);

        // UnityWebRequest 객체 생성
        UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:8000/robot/robot" + robotNumber.ToString(), "PUT");

        // 요청 본문 설정
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // 응답 핸들러 설정
        request.downloadHandler = new DownloadHandlerBuffer();

        // 헤더 설정
        request.SetRequestHeader("Content-Type", "application/json");

        // 요청 보내기
        yield return request.SendWebRequest();

        // 요청 결과 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("요청 성공: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("요청 실패: " + request.error);
        }
    }

}
