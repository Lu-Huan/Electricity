using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SelectObjectCommand : Controller
{
    public override void Execute(object data)
    {
        Role role = data as Role;
        if (role.GetComponent<Monster>()!=null)
        {
            Spawner spawner = GetModel<Spawner>();
            Monster monster = data as Monster;
            if (spawner.ListMonsters.Contains(monster))
            {
                foreach (Tower t in spawner.ListTowers)
                {
                    t.HaveTarge(monster);
                }
            }
        }
        else if( role.GetComponent<Box>() != null)
        {

        }
    }
}

