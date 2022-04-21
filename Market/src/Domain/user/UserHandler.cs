namespace Domain;
using System;
using datalayer;using System.Collections.Generic;
using System.Threading;
using System.Random;
using System.Security.Cryptography;
using System.Linq;

private static Random random = new Random();

static string RandomString(int length)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
}
const int GUEST_NAME_LENGTH = 20;
public class UserHandler{
private Dictionary<string, UserIF> usersMap; //# key-userName, value - User

public UserHandler(){
 this.usersMap = new Dictionary<string, User>();
}

public void printUsers(){
     foreach (string user in usersMap.Keys){
        Console.WriteLine(user);
    }
}


  public void register(string username){
        //TODO acquire this.thread before 
        UserIF user = new User(username, Cart());
        users.add_user(user);
        permissions.add_permission(user_name, REGISTERED_PERMMISIONS);
        //TODO release this.thread before 
   
  }

}