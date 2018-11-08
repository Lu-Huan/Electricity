using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    public Transform follw=null;
    public Role role;
    public Image UI_BarrelHP_HP;
    public void Init(Transform parent)
    {
        follw = parent;
        if (parent.tag == "Monster")
        {

            role = follw.GetComponent<Monster>();
             
        }
        else if (parent.tag == "Tower")
        {
            role = follw.GetComponent<Tower>();
        }
        role.HpChanged += Role_HpChanged;
        role.Dead += Role_Dead;
        UI_BarrelHP_HP = transform.Find("hp").GetComponent<Image>();
        UI_BarrelHP_HP.fillAmount = 1f;
    }

    private void Role_Dead(Role obj)
    {
        Game.Instance.ObjectPool.Unspawn(gameObject);
    }

    private void Role_HpChanged(int arg1, int arg2)
    {
        float hp = arg1;
        float maxhp = arg2;
        UI_BarrelHP_HP.fillAmount = hp / maxhp;
    }

    private void Update()
    {
        if (follw==null)
        {
            return;
        }
        Vector2 pos = Camera.main.WorldToScreenPoint(follw.position);
        transform.position = pos + new Vector2(0, 50);
    }

}