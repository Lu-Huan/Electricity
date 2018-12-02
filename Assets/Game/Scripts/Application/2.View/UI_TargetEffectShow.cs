using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class UI_TargetEffectShow : View
{
    private Role Target;
    public override string Name
    {
        get
        {
            return Consts.V_TargetEffectShow;
        }
    }
    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_SelectTarget);
    }
    public override void HandleEvent(string eventName, object data)
    {
        if (eventName== Consts.E_SelectTarget)
        {
            Target = data as Role;
        }
    }
    private void Update()
    {
        if (Target != null)
        {
            transform.position = Target.Position + new Vector3(0, 0.1f, 0);
        }
        else
        {
            transform.position = new Vector3(0, -5, 0);
        }
    }
}

