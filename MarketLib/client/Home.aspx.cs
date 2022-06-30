using client.CostumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace client
{
    public partial class Home : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Thread.Sleep(6000);
                Session["connection"] = ServerFunctions.connect();
            }
  
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void GetCart(object sender, EventArgs e)
        {
            Response.Redirect("~/Cart.aspx");
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}