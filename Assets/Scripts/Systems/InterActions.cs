using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ж�����Ƿ���Խ��н���
public class InterActions : Singleton<InterActions>
{
    public bool PlayerIsDragging{ get; set;} = false;

    public bool PlayerCanInteract()
    {
        //���û���¼����ڽ�����
        if(!ActionSystem.Instance.IsPerforming) return true;
        else return false;
    }

    public bool PlayerCanHover()
    {
        if(PlayerIsDragging) return false;
        return true;
    }
}
