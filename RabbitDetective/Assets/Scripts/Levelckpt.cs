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

    [Header("level03")]
    public bool ckpt_box;
    public bool ckpt_scream;
    public bool ckpt_bus;
    public List<CheckPoint> CheckPointsListLv3;


    public static Levelckpt instance;
    
     void Awake()
    {
        instance = this;
    }


    public void GetLv1CKPT(List<CheckPoint> list)
    {
        CheckPointsListLv1 = list;
        L1S1Tabel_book = CheckPointsListLv1[1].isChecked;
        L1S1Adam_knife = CheckPointsListLv1[2].isChecked;
        L1S1Pic_null = CheckPointsListLv1[3].isChecked;
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
        CheckPointsListLv2 = list;
        ckpt1 = CheckPointsListLv2[0].isChecked;
        ckpt2 = CheckPointsListLv2[1].isChecked;
        ckpt3 = CheckPointsListLv2[2].isChecked;
        ckpt4 = CheckPointsListLv2[3].isChecked;
        ckpt5 = CheckPointsListLv2[4].isChecked;
        ckpt6 = CheckPointsListLv2[5].isChecked;
    }

    public void GetLv3CKPT(List<CheckPoint> list)
    {
        CheckPointsListLv3 = list;

        ckpt_box = CheckPointsListLv3[0].isChecked;
        ckpt_scream = CheckPointsListLv3[1].isChecked;
        ckpt_bus = CheckPointsListLv3[2].isChecked;
        
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

    public bool Leve3CKPTResultHE()
    {
        if (ckpt_box)
        {
            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }

    }
    public bool Level3CKPTResultBE1()
    {
        if (ckpt_scream)
        {

            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }
    }
    public bool Level3CKPTResultBE2()
    {
        if (ckpt_bus)
        {

            return true;
        }
        else
        {
            return false;//�ؿ�2Ĭ��BE-����
        }
    }
}
