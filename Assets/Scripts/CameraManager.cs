using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] List<GameObject> listCam; // 0번이 제일 먼저 실행됨
    
    void Start()
    {
        /*Camera[] cams = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        int camCount = cams.Length;
        for (int iNum = 0; iNum < camCount; iNum++)
        {
            listCam.AddRange(cams)
        }// 시리얼라이즈필드 안할 시 위에처럼 할당해줘야함*/
        init();
        SwitchCamera(0);
    }
    private void init()
    {
        Transform trsCams = GameObject.Find("Btns").transform;
        Button[] btnArr = trsCams.GetComponentsInChildren<Button>();
        int count = btnArr.Length;
        for (int iNum = 0; iNum < count; iNum++) 
        {
            int value = iNum;
            btnArr[iNum].onClick.AddListener(() => SwitchCamera(value));//람다식은 iNum이 같이 증가하기에 value로 따로 변수 뽑아서 사용함
        }
    }

    public void SwitchCamera(int _camNum)
    {
        int camCount = listCam.Count;
        for (int iNum = 0; iNum < camCount; iNum++)
        {            
            //if (iNum == _camNum)
            //{
            //    listCam[iNum].SetActive(true);
            //}
            //else 
            //{
            //    listCam[iNum].SetActive(false);
            //}

            //bool isOn = iNum == _camNum;
            //listCam[iNum].SetActive(isOn); // 위 if문의 코드와 동일

            listCam[iNum].SetActive(iNum == _camNum); // 위 두줄짜리 코드를 한줄로 줄임
        }
    }
}
