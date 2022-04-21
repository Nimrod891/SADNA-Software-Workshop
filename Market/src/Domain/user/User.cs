namespace Domain;
using System;
using datalayer;
using Cart;
using Basket; 


public class User : UserIF  {

private  bool isLogged;
private string userName;
private bool isAdmin; 
private Cart cart;
private PurchaseHistory ph;
private AppointedToStore appointed_to_store;


public User(string userName, Cart cart){
    this.isLogged = false;
    this.userName = userName;
    this.cart = cart;
    this.ph = new List();
    this.appointed_to_store = None;
    this.isAdmin = false;
}

public void logIn(){
    if (isLogged)
    throw new LoginException("Already log In");
    isLogged = true;
         
}


public void logOut(){
    if (!isLogged)
    throw new LoginException("Already log Out");
    isLogged = false;
         
}

public bool is_Admin(){
    return isAdmin;
}

public void addProductToUser( string store, int itemId , int qun){

    cart.addProductToCart(store, itemId, qun);
}

public void removeProductToCart( string store, int itemId , int qun){

    cart.removeProductToCart(store, itemId, qun);
}

public void cleanCart()
{
    cart = Cart();
}


}