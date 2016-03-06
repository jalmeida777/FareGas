<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FAREGAS - Login</title>
    <script src="js/jquery.growl.js" type="text/javascript"></script>
<link href="css/jquery.growl.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; width: 300px; height: 200px; margin: auto; top: 50px; right: 0px; bottom: 0px; left: 0px;">
    
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
            CssFilePath="~/App_Themes/PlasticBlue/{0}/styles.css" CssPostfix="PlasticBlue" 
            GroupBoxCaptionOffsetY="-18px" 
            SpriteCssFilePath="~/App_Themes/PlasticBlue/{0}/sprite.css" Width="300px" 
            HeaderText="Login">
            <ContentPaddings PaddingBottom="8px" />
            <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
<table cellpadding="4" cellspacing="4" width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="inputNormal" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label2" runat="server" Text="Contraseña:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" 
                        CssClass="inputNormal" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:ImageButton ID="btnEntrar" runat="server" ImageUrl="~/images/Entrar.gif" 
                        onclick="btnEntrar_Click" />
                </td>
            </tr>
        </table>

</dx:PanelContent>
</PanelCollection>
        </dx:ASPxRoundPanel>
    
        
    
    </div>

    <div style="margin: auto; position: absolute; top: 80px; right: 0px; width: 520px; height: 100px; left: 50px;">
        <asp:Image ID="Image1" runat="server" Height="100px" 
            ImageUrl="~/images/logo_faregas1.png" />
    </div>
    </form>
</body>
</html>
