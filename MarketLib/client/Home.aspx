<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="client.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="CartButton" runat="server" OnClick="GetCart" Text="Cart" />
            <asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click" />
            <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />
        </div>
    </form>
</body>
</html>
