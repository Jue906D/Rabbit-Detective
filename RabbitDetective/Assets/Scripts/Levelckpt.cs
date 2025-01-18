using System;
using System.Collections.Generic;
using UnityEngine;

public class Levelckpt : MonoBehaviour
{

    [Header("level01")]
    public bool L1S1Tabel_book;
    public bool L1S1Adam_knife;
    public bool L1S1Pic_null;
    public List<CheckPoint> CheckPointsListLv1;

    [Header("level02")]
    public bool ckpt1;//����ӡ
    public bool ckpt2;//Ь�ӽ�ӡ
    public bool ckpt3;//�ſ�
    public bool ckpt4;//�Ż�
    public bool ckpt5;//��ȡ��
    public bool ckpt6;//��ȡ��
    public List<CheckPoint> CheckPointsListLv2;

    public static Levelckpt instance;
    
     void Awake()
    {
        instance = this;
    }


    public void GetLv1CKPT(List<CheckPoint> list)
    {
        CheckPointsListLv1 = list;
        L1S1Tabel_book = CheckPointsListLv1[0];
        L1S1Adam_knife = CheckPointsListLv1[1];
        L1S1Pic_null = CheckPointsListLv1[2];
    }

    public bool Level1CKPTResultHE ()
    {
        if (L1S1Tabel_book && L1S1Adam_knife && L1S1Pic_null)
        {
            return true;
        }
        return false;//�ؿ�1Ĭ��BE-�Ժ�
    }



    public void GetLv2CKPT(List<CheckPoint> list)
    {
        CheckPointsListLv1 = list;
        ckpt1 = CheckPointsListLv1[0];
        ckpt2 = CheckPointsListLv1[1];
        ckpt3 = CheckPointsListLv1[2];
        ckpt4 = CheckPointsListLv1[3];
        ckpt5 = CheckPointsListLv1[4];
        ckpt6 = CheckPointsListLv1[5];
    }

    public bool Leve2CKPTResultHE()
    {
        if (ckpt1)
        {
            return true;
        }
        if (ckpt2)
        {
            return true;
        }
        if (ckpt5 && ckpt6)
        {
            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }
        
    }

    public bool Level2CKPTResultBE1()
    {
        if (ckpt4)
        {

            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }

        
    }
    public bool Level2CKPTResultBE2()
    {

        if (ckpt3 )
        {

            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }
        
    }
    public bool Level2CKPTResultBE3()
    {

        if (ckpt5)
        {

            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }
        
    }
}
