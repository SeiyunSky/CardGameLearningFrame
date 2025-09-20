using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseUtil
{
    private static Camera camera = Camera.main;
    public static Vector3 GetMouseWorldPosition(float zValue = 0f)
    {
        //Plane的意思是构造一个平面
        //传入参数第一个是法线，第二个是平面上的点，然后就能找到面了
        Plane dragPlane = new (camera.transform.forward,new Vector3(0,0,zValue));
        //Ray是射线，这里从相机发射一条到鼠标位置的射线
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(dragPlane.Raycast(ray,out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
