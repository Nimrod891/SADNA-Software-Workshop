<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="client.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;username<br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;password<br />
            <br />

        </div>
        <asp:Button ID="Login_btn" runat="server" Text="Login" OnClick="Register_btn_Click" />
    &nbsp;
        <asp:Label ID="Response" runat="server" Text="Label" Visible="False"></asp:Label>
    </form>
</body>
</html>
