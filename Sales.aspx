<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="ERPproject.Sales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    
    <style type="text/css">
        .auto-style1 {
            border-radius: 10px;
            padding: 5px 15px 10px 15px;
            border: solid black;
            color: white;
            background-color: rgba(76, 110, 115, 1);
            margin: 20px 10px 10px 10px;
        }
    </style>
    
</head>
<body background="MyImages/Background.jpg">
    <form id="form1" runat="server">
        <div>
            <section>          
            <div class="Head-con">
                <h1>THE ERP SOFTWARE</h1>
            </div>
        </section>
        <div class="nav-con">
             <div class="nav-div1">
                <h3><asp:Label ID="UserLogin" runat="server" Text="ERP Things"></asp:Label></h3>
            </div>
            <nav class="navbar navbar-expand-sm sticky-top">          
                <div class="container-fluid">
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#collapsibleNavbar">  
                    </button>
                    <div class="collapse navbar-collapse" id="collapsibleNavbar">
                        <ul class="navbar-nav">
                                <li class="nav-item">
                                  <a class="nav-link" href="HomeLogin.aspx">Home</a>
                               </li>
                               <li class="nav-item dropdown"> 
                                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                      <span class="caret"></span>
                                       Tools
                                    </a>
                                    <ul class="dropdown-menu" style="background-color:  rgba(76, 110, 115);" aria-labelledby="navbarDropdown">         
                                        <li class="dropdown-item">
                                            <a href="Employee.aspx">Employee Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="EmployeeView.aspx">Employee View</a>
                                                </li>
                                            </ul>
                                        </li> 
                                        <li class="dropdown-item">
                                            <a href="Stocks.aspx">Stocks Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="StokcsView.aspx">Stocks View</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="dropdown-item">
                                            <a href="Suppliers.aspx">Supplier Form &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="SuppliersView.aspx">Supplier View</a>
                                                </li>
                                            </ul>
                                        </li>                                       
                                        <li class="dropdown-item">
                                            <a href="PurchaseForm.aspx">Purchase Goods &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="PurchaseView.aspx">Purchase View</a>
                                                </li>
                                                 <li>
                                                   <a class="dropdown-item" href="PurchaseReport.aspx">Purchase Report</a>
                                                 </li>
                                            </ul>
                                        </li>           
                                        <li class="dropdown-item">
                                            <a href="ReceiveForm.aspx">Receive Goods &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="ReceiveView.aspx">Receive View</a>
                                                </li>
                                                 <li>
                                                   <a class="dropdown-item" href="#">Receive Report</a>
                                                 </li>
                                            </ul>
                                        </li>
                                        <li class="dropdown-item">
                                            <a href="#">Sales &raquo;</a>
                                            <ul class="dropdown-menu dropdown-submenu" style="background-color:  rgba(76, 110, 115);">
                                                <li>
                                                   <a class="dropdown-item" href="SalesView.aspx">Sales View</a>
                                                </li>
                                            </ul>
                                        </li>
                                   </ul>
                              </li>
                              <li class="nav-item">
                                   <a class="nav-link" href="#fotter">Contact</a>
                              </li>
                               <li class="nav-item">
                                    <a class="nav-link" href="#thrdhead">Performance</a>
                              </li>     
                       </ul>
                          <a  class="btn btn-danger" href="Index.aspx">Logout</a>
                   </div>
              </div>
         </nav>
      </div>
      <div class="head1"><h1>Sales</h1></div>
        <div class="err-lab">
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </div>
        <div class="table-con">
            <div class="tab">        
                <div style="display:flex;">
                    <table>
                        <tr>
                            <th>Invoice No.</th>
                            <td>&nbsp; <asp:TextBox runat="server" class="text" ID ="InvoiceNo" Width="208px"></asp:TextBox></td>
                            <td><asp:Button runat="server" ID="New" height="30px" BackColor="#66ccff" Text="New" OnClick="New_Click" /></td>
                            <th>&nbsp;&nbsp;&nbsp;&nbsp; Employee Id.</th>
                            <td>&nbsp; <asp:TextBox runat="server" class="text" ID ="EmployeeId" Height="30px" Width="208px"></asp:TextBox> </td>
                            <td><asp:TextBox runat="server" class="text" Width="100px" Enabled="false" ID="StockQty"></asp:TextBox></td>
                            <td><asp:TextBox runat="server" class="text" Width="100px" Enabled="false" ID="Currstkqty" ></asp:TextBox></td>
                        </tr>
                     </table>                    
                </div>
                <br />
                <div>
                    <table>
                        <tr height="50px">
                            <th>Mobile No.&nbsp;&nbsp;&nbsp; </th>
                            <td><asp:TextBox runat="server" class="text" ID ="MobileNo" Height="30px" Width="208px"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
                <div class="btn-con" style="width: 1200px;">                          
                     <asp:Button ID="Save" runat="server" CssClass="mybttn" Width="140px" Text="Complete" OnClick="Save_Click"/>
                      <asp:Button ID="Delete_entry" runat="server" BorderStyle="Solid" Text="Void Entry" Width="140px" CssClass="mybttn" OnClick="Delete_entry_Click" />                   
                </div>
                <br />
                <div style="display:flex; justify-content:space-between;">                          
                <div>           
                <table>
                   <thead>
                        <tr>
                          <th>Barcode*</th>
                          <th>Description</th>
                          <th>Retail Price</th>
                          <th>&nbsp;&nbsp;&nbsp;Qty*</th>
                          <th>&nbsp;&nbsp;&nbsp;Disc</th>
                          <th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Price</th>
                       </tr>
                  </thead>
                  <tbody runat="server" id="invoiceItems">
                      <tr>                          
                          <td>
                              <asp:TextBox runat="server" class="text" Height ="35px" Width="208px" ID ="BarCode" OnTextChanged="BarCode_TextChanged"></asp:TextBox>
                          </td>
                          <td>
                              <asp:TextBox runat="server" class="text" Height ="35px" Width="208px" ID ="Description"></asp:TextBox>
                          </td>
                          <td>
                              <asp:TextBox runat="server" class="text" Height ="35px" Width="100px" ID ="Retail"></asp:TextBox>
                          </td> 
                          <td>
                              <asp:TextBox runat="server" class="text" Height ="35px" Width="60px" ID ="Qty" OnTextChanged="Qty_TextChanged"></asp:TextBox>
                          </td>
                          <td>
                              <asp:TextBox runat="server" class="text" Height ="35px" Text="0" Width="60px" ID ="Disc"></asp:TextBox>
                          </td>
                          <td>
                              <asp:TextBox runat="server" class="text" Height ="35px" Width="100px" ID ="Price"></asp:TextBox>
                          </td>                               
                      </tr>                      
                  </tbody>
                </table>
                <table>
                    <tr height ="30px">
                          <th>Total Price: </th>                           
                     </tr>
                    <tr height ="30px">
                           <td><asp:TextBox runat="server" class="text" Height ="35px" Width="100px" Text="0" ID ="TotalPrice"></asp:TextBox></td>
                     </tr>
                </table>
                <asp:Button runat="server" Text="Add Item" ID="Add" CssClass="mybttn" type="button" OnClick="Add_Click" />
                <br />
                </div>
                    <div class="btn-con">
                        <asp:Button ID="Void_Earlier_Entry" runat="server" BorderStyle="Solid" Text="Void Earlier Entry" Height="48px" Width="188px" CssClass="auto-style1" OnClick="Void_Earlier_Entry_Click" />
                        <asp:Button ID="Void_transaction" runat="server" BorderStyle="Solid" Text="Void transaction" Height="48px" Width="180px" CssClass="mybttn" OnClick="Void_transaction_Click"/>
                   </div>   
                </div>
                <asp:GridView ID="Salesgrid" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" Width="690px">
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>

            </div>
        </div>       
        </div>
    </form>
</body>
</html>
