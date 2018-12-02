using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Transform rightGunBone;
    public Transform leftGunBone;
    public Arsenal[] arsenal;

    public event Action<Transform> GunYM3Right;
    public event Action<Transform> GunYM3Left;
    public event Action<Transform> GunYM27;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (arsenal.Length > 0)
            SetArsenal(arsenal[0].name);
    }

    public void SetArsenal(string name)
    {
        foreach (Arsenal hand in arsenal)
        {
            if (hand.name == name)
            {
                if (rightGunBone.childCount > 0)
                    Destroy(rightGunBone.GetChild(0).gameObject);
                if (leftGunBone.childCount > 0)
                    Destroy(leftGunBone.GetChild(0).gameObject);
                if (hand.rightGun != null)
                {
                    GameObject newRightGun = Instantiate(hand.rightGun);
                    newRightGun.transform.parent = rightGunBone;
                    newRightGun.transform.localPosition = Vector3.zero;
                    newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    if (name== "YM-3 Pistol"||name== "Two Pistols")
                    {
                        GunYM3Right(newRightGun.transform.Find("Start"));
                    }
                    if (name == "YM-27 Rifle")
                    {
                        GunYM27(newRightGun.transform);
                    }
                }
                if (hand.leftGun != null)
                {
                    GameObject newLeftGun = Instantiate(hand.leftGun);
                    newLeftGun.transform.parent = leftGunBone;
                    newLeftGun.transform.localPosition = Vector3.zero;
                    newLeftGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    GunYM3Left(newLeftGun.transform.Find("Start"));
                }
                animator.runtimeAnimatorController = hand.controller;
                return;
            }
        }
    }

    [System.Serializable]
    public struct Arsenal
    {
        public string name;
        public GameObject rightGun;
        public GameObject leftGun;
        public RuntimeAnimatorController controller;
    }
}
