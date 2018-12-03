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
        /*foreach (Arsenal item in arsenal)
        {
            if (item.rightGun != null)
            {
                item.rightGun = Instantiate(item.rightGun);

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
        }*/
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = Game.Instance.StaticData.Controllers[0];
    }
    public void EquipGun(int Index)
    {
        int Role = GetComponent<MainCharacter>().ID;
        int gunindex = Game.Instance.StaticData.People[Role].EquipGunId[Index];


        GunInfo gun = Game.Instance.StaticData.GetGunInfo(gunindex);
        string path = "Prefabs/Guns/" + gun.PrefabName;
        CreatGun(gun.GunType, path);
    }

    public void EquipGun(Gun gun)
    {
        if (gun.GunType==GunType.FreeHand)
        {
            CreatGun(gun.GunType, null);
            return;
        }
        string path = "Prefabs/Guns/" + gun.gameObject.name;
        CreatGun(gun.GunType, path);
    }

    private void CreatGun(GunType gunType, string path)
    {
        animator.runtimeAnimatorController = Game.Instance.StaticData.Controllers[(int)gunType];
        if (leftGunBone.childCount > 0)
        {
            Destroy(leftGunBone.GetChild(0).gameObject);
        }
        if (rightGunBone.childCount > 0)
        {
            Destroy(rightGunBone.GetChild(0).gameObject);
        }
        switch (gunType)
        {
            case GunType.FreeHand:

                break;
            case GunType.OnePistol:
                GameObject rightOne = Resources.Load<GameObject>(path);

                SetGun(rightOne, true);

                break;
            case GunType.TwoPistol:
                GameObject rightTwo = Resources.Load<GameObject>(path);
                SetGun(rightTwo, true);
                GameObject leftTwo = Resources.Load<GameObject>(path);
                SetGun(leftTwo, false);
                break;
            case GunType.Rifle:
                GameObject rifle = Resources.Load<GameObject>(path);
                SetGun(rifle, true);
                break;
            default:
                break;
        }
    }

    private void SetGun(GameObject gunPrafab, bool IsRight)
    {
        GameObject gun = Instantiate(gunPrafab);
        if (IsRight)
        {
            gun.transform.parent = rightGunBone;
        }
        else
        {
            gun.transform.parent = leftGunBone;
        }
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    /*public void SetArsenal(int index)
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
    }*/
}


