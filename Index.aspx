<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ERPproject.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login!</title>
    <link href="Login.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
</head>
<body id ="body" runat="server" background="MyImages/Background.jpg">
    <form id="form1" runat="server">     
        <div class="header">
            <h1 class="Head-top">The ERP Software.</h1>
        </div>
        <hr />
        <div class="err-con">
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </div>
        <div class="Primary-con">
        <div class="Main-con">
            <div class="heading"><h2>Login</h2></div>
            <br />
            <br />
            <div class="sub-con">
                <table>
                    <tr height="50px">
                        <th><p>Username:</p></th>
                        <td><asp:Textbox runat="server" ID="ID" class="text" autocomplete="off" type="Email"></asp:Textbox> </td>
                    </tr>
                    <tr height="10px"></tr>
                    <tr height="50px">
                        <th><p>Password:</p></th>
                        <td><asp:Textbox runat="server" ID="Password" class="text" autocomplete="off" type="password"></asp:Textbox> </td>
                    </tr>
                </table>
            </div>
            <div class="main-btn-con">
            <div class="btn-conn">
                <br /><br />
                <asp:Button ID="Login" class="sub-btn" runat="server" Text="Login" OnClick="Login_Click" />
                <br />
               <a class="abbs" href="#">Create New Login!</a><br />
               <a class="abbs" href="#">&nbsp;Forgot Password!</a>
               <br />
            </div>
            </div>
        </div>
        </div>    
    </form>
</body>
</html>
