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

        // UnityWebRequest�� ������ִ� GET �޼ҵ带 ����Ѵ�.
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();  // ������ �ö����� ����Ѵ�.

        if (www.error == null)  // ������ ���� ������ ����.
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

        // UnityWebRequest ��ü ����
        UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:8000/robot/robot" + robotNumber.ToString(), "PUT");

        // ��û ���� ����
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        // ���� �ڵ鷯 ����
        request.downloadHandler = new DownloadHandlerBuffer();

        // ��� ����
        request.SetRequestHeader("Content-Type", "application/json");

        // ��û ������
        yield return request.SendWebRequest();

        // ��û ��� Ȯ��
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("��û ����: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("��û ����: " + request.error);
        }
    }

}
