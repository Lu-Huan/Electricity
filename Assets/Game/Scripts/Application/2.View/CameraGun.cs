using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 脚本挂载新建相机上 —— 新相机Clear Flags清除标记设置为：Solid Color
/// </summary>
public class CameraGun : MonoBehaviour
{
    public event Action Back;
    public void Tri()
    {
        Back();
    }
}