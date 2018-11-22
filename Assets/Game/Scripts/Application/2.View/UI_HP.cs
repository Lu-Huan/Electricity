using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    private Transform follw = null;
    private Image UI_BarrelHP_HP;
    private void Awake()
    {
        UI_BarrelHP_HP = transform.Find("hp").GetComponent<Image>();
    }
    public void Init(Transform parent)
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        follw = parent;

        Role role = follw.GetComponent<Role>();
        role.HpChanged += Role_HpChanged;
        role.Dead += Role_Dead;

        UI_BarrelHP_HP.fillAmount = 1f;
    }

    private void Role_Dead(Role obj)
    {
        follw = null;
        gameObject.SetActive(false);
    }

    private void Role_HpChanged(int arg1, int arg2)
    {
        float hp = arg1;
        float maxhp = arg2;
        UI_BarrelHP_HP.fillAmount = hp / maxhp;
    }

    private void Update()
    {
        if (follw == null)
        {
            return;
        }
        Vector2 pos = Camera.main.WorldToScreenPoint(follw.position);
        transform.position = pos + new Vector2(0, 50);
    }

}