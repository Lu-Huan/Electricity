using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerShop : View {

    private Button shop;
    private GameObject cameraShop;
    private GameObject MainCamera;

    public override string Name
    {
        get
        {
            return Consts.V_TriggerShop;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_ExitGunShop);
    }
    private void Start()
    {
       
        MainCamera = GameObject.Find("MainCamera");
        cameraShop = GameObject.Find("Camera");
        shop = GameObject.Find("Canvas").transform.Find("Shop").GetComponent<Button>();
        shop.onClick.AddListener(() =>
        {
            SendEvent(Consts.E_BugGun);
            MainCamera.SetActive(false);
            //cameraShop.SetActive(true);
            cameraShop.GetComponent<Animator>().SetBool("IsShop", true);
        });
        cameraShop.GetComponent<CameraGun>().Back += CameraBack;
        shop.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shop.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shop.gameObject.SetActive(false);
        }
    }
    public void CameraBack()
    {
        MainCamera.SetActive(true);
    }
    public override void HandleEvent(string eventName, object data)
    {
        cameraShop.GetComponent<Animator>().SetBool("IsShop", false);
    }
}
