using UnityEngine;

public class Levelckpt : MonoBehaviour
{

    [Header("level01")]
    public bool L1S1Tabel_book;
    public bool L1S1Adam_knife;
    public bool L1S1Pic_null;

    [Header("level02")]
    public bool ckpt1;//虎脚印
    public bool ckpt2;//鞋子脚印
    public bool ckpt3;//门空
    public bool ckpt4;//门虎
    public bool ckpt5;//虎取下
    public bool ckpt6;//画取下

    public bool Level1CKPTResultHE ()
    {
        if (L1S1Tabel_book && L1S1Adam_knife && L1S1Pic_null)
        {
            return true;
        }
        return false;//关卡1默认BE-迷糊
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

        return false;//关卡2默认BE-朋友
    }

    public bool Level2CKPTResultBE1()
    {
        if (ckpt4&&(!Leve2CKPTResultHE()))
        {

            return true;
        }


        return false;//关卡2默认BE-朋友
    }
    public bool Level2CKPTResultBE2()
    {

        if (ckpt3 && (!Level2CKPTResultBE1()))
        {

            return true;
        }

        return false;//关卡2默认BE-朋友
    }
    public bool Level2CKPTResultBE3()
    {

        if (ckpt5 && (!Level2CKPTResultBE2()))
        {

            return true;
        }

        return false;//关卡2默认BE-朋友
    }
}
