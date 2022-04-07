using System;
using System.Collections.Generic;
using System.Threading;
using System.Random;
using System.Security.Cryptography;
using Domain;
namespace datalayer;
public class PremissionsData{
 private Dictionary<string,UserPermissions > permissions = new Dictionary<string, UserPermissions>();    //{user_name: UserPermissions}

    public UserPermissions getPermissionsByUserName(string userName){


     UserPermissions user_permissions  = permissions[userName];
     if (user_permissions == null)
         throw new  getPremissionsByUserNameException("The user does not exist or something went wrong!");
       return user_permissions ;
    }

    public void addPremission(string user, int prem){
        permissions[user] = new UserPermissions(user,prem);

    }
}
