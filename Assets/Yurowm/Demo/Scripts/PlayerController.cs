using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Transform rightGunBone;
    public Transform leftGunBone;

    public List<Arsenal> arsenal;
    private Animator animator;
    private int Current = 0;

    void Awake()
    {
        foreach (Arsenal item in arsenal)
        {
            if (item.rightGun != null)
            {
                item.rightGun = Instantiate(item.rightGun);
                item.rightGun.transform.parent = rightGunBone;
                item.rightGun.transform.localPosition = Vector3.zero;
                item.rightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                item.rightGun.SetActive(false);
            }
            if (item.leftGun != null)
            {
                item.leftGun = Instantiate(item.leftGun);
                item.leftGun.transform.parent = leftGunBone;
                item.leftGun.transform.localPosition = Vector3.zero;
                item.leftGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                item.leftGun.SetActive(false);
            }
        }
        animator = GetComponent<Animator>();
        if (arsenal.Count > 0)
            SetArsenal(0);
    }


    public void SetArsenal(int index)
    {
        if (arsenal[Current].rightGun != null)
        {
            arsenal[Current].rightGun.SetActive(false);
        }
        if (arsenal[Current].leftGun != null)
        {
            arsenal[Current].leftGun.SetActive(false);
        }


        if (arsenal[index].rightGun != null)
        {
            arsenal[index].rightGun.SetActive(true);
        }
        if (arsenal[index].leftGun != null)
        {
            arsenal[index].leftGun.SetActive(true);
        }
        animator.runtimeAnimatorController = Game.Instance.StaticData.Controllers[index];
        Current = index;
    }
}


