using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ChangGunBackArgs
{   
    public int GunID;
    public string GunName;    
}
public class ChangGunRequset
{
    public bool IsRight;
    public int GunID;
}

public class EquipGunRequset
{
    public int GunID;
}

public class UninstallGun
{
   public  int GunID;
}
public class TrialGun
{
    public int GunID;
}
