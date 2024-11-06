<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeLogin.aspx.cs" Inherits="ERPproject.HomeLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="styleindex.css" rel="stylesheet" type="text/css" />
</head>
<body id ="body" runat="server" background="MyImages/Background.jpg">
    <form id="form1" runat="server">     
        <div class="header">
            <h1 class="Head-top">The ERP Software.</h1>
        </div>
        <div class="logout-con">
            <a class="signupBtn" href="Index.aspx">LOG OUT
                <span class="arrow" style="color:purple;">
                   →
                </span>
            </a>
        </div>
        
        <table class="tab-1">
            <tr><th>Employees</th></tr>
            <tr>
                <td><a class="btn" href="Employee.aspx">Employee Form &raquo;</a></td>
                <td><a class="btn"  href="EmployeeView.aspx">Employee View &raquo;</a></td>
            </tr>
            <tr><th>Stocks</th></tr>
            <tr>
                <td><a class="btn" href="Stocks.aspx">Stock Form &raquo;</a></td>
                <td><a class="btn" href="StocksView.aspx" >Stock View &raquo;</a></td>
            </tr>
            <tr><th>Supplier</th></tr>
            <tr>
                <td><a class="btn" href="Suppliers.aspx">Supplier Form &raquo;</a></td>
                <td><a class="btn" href="SuppliersView.aspx">Supplier View &raquo;</a></td>
                
            </tr>
            <tr><th>Purchase</th></tr>
            <tr>
                <td><a class="btn" href="PurchaseForm.aspx">Purchase Form &raquo;</a></td>
                <td><a class="btn" href="PurchaseView.aspx">Purchase View &raquo;</a></td>
                <td><a class="btn" href="PurchaseReport.aspx">Purchase Report &raquo;</a></td>
            </tr>
            <tr><th>Receive</th></tr>
            <tr>
                <td><a class="btn" href="ReceiveForm.aspx">Receive Form &raquo;</a></td>
                <td><a class="btn" href="ReceiveView.aspx">Receive View &raquo;</a></td>
                <td><a class="btn">Receive Report &raquo;</a></td>
            </tr>
            <tr><th>Sales</th></tr>
            <tr>
                <td><a class="btn" href="Sales.aspx">Sales Form &raquo;</a></td>
                <td><a class="btn" href="SalesView.aspx">Sales View &raquo;</a></td>
                <td><a class="btn">Sales Report &raquo;</a></td>
            </tr>
        </table>

    
    </form>
</body>
</html>
