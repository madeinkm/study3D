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

    [SerializeField] List<GameObject> listCam; // 0���� ���� ���� �����
    
    void Start()
    {
        /*Camera[] cams = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        int camCount = cams.Length;
        for (int iNum = 0; iNum < camCount; iNum++)
        {
            listCam.AddRange(cams)
        }// �ø���������ʵ� ���� �� ����ó�� �Ҵ��������*/
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
            btnArr[iNum].onClick.AddListener(() => SwitchCamera(value));//���ٽ��� iNum�� ���� �����ϱ⿡ value�� ���� ���� �̾Ƽ� �����
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
            //listCam[iNum].SetActive(isOn); // �� if���� �ڵ�� ����

            listCam[iNum].SetActive(iNum == _camNum); // �� ����¥�� �ڵ带 ���ٷ� ����
        }
    }
}
