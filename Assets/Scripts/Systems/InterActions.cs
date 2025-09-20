using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来判定玩家是否可以进行交互
public class InterActions : Singleton<InterActions>
{
    public bool PlayerIsDragging{ get; set;} = false;

    public bool PlayerCanInteract()
    {
        //如果没有事件正在进行中
        if(!ActionSystem.Instance.IsPerforming) return true;
        else return false;
    }

    public bool PlayerCanHover()
    {
        if(PlayerIsDragging) return false;
        return true;
    }
}
