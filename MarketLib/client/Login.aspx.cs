﻿using client.CostumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace client
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Visible = false;
        }

        protected void Register_btn_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;
            string response = ServerFunctions.login(Session["connection"].ToString(),username, password);
            Response.Text = response;
            Response.Visible = true;
        }
    }
}