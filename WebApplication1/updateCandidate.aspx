<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateCandidate.aspx.cs" Inherits="WebApplication1.updateCandidate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <center>
               <h1>Candidate Details</h1>
               <asp:Table ID="Table1" runat="server" CssClass="align-content-center" Width="50%" BorderStyle ="Dotted">
                  <asp:TableRow>
                     <asp:TableCell>
                        <asp:Label ID="c_name" runat="server">Candidate Name</asp:Label>
                     </asp:TableCell>
                     <asp:TableCell>
                        <asp:TextBox ID="tb_cname" runat="server"></asp:TextBox>
                     </asp:TableCell>
                  </asp:TableRow>
                  <asp:TableRow>
                     <asp:TableCell>
                        <asp:Label ID="lb_dob" runat="server" Text="Date of Birth">Date of Birth</asp:Label>
                     </asp:TableCell>
                     <asp:TableCell>
                        <asp:TextBox ID="tb_dob" runat="server"  type="Date"></asp:TextBox>
                     </asp:TableCell>
                  </asp:TableRow>
                  <asp:TableRow>
                     <asp:TableCell>
                        <asp:Label ID="lb_age" runat="server" Text="Label">Age</asp:Label>
                     </asp:TableCell>
                     <asp:TableCell>
                        <asp:TextBox ID="tb_age" runat="server" Enabled="false" ></asp:TextBox>
                     </asp:TableCell>
                  </asp:TableRow>
                  <asp:TableRow>
                     <asp:TableCell>
                        <asp:Label ID="Label3" runat="server" Text="Label">City</asp:Label>
                     </asp:TableCell>
                     <asp:TableCell>
                        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                     </asp:TableCell>
                  </asp:TableRow>
                  <asp:TableRow>
                     <asp:TableCell>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update" CssClass="btn btn-danger" Font-Bold="true"/>
                     </asp:TableCell>
                   </asp:TableRow>
                  </asp:Table>
            </center>
        </div>
    </form>
</body>
</html>
