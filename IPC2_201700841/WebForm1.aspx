<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="IPC2_201700841.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body ms_poaitioning="GridLayout">
    <form id="form2" runat="server">
        Subir archivo<br />
        <input type="file" runat="server" name="oFile" id="oFile" accept=".xml" />
        <br />
        <p></p>
        <p></p>
<asp:Button ID="CargarArchivo" runat="server" OnClick="CargarArchivo_Click" Text="Cargar al Juego" />
        <asp:Label id="lblUploadResult" Runat="server"></asp:Label>
        <br />
        <br />
        <asp:TextBox id="TextBox1" runat="server" Height="192px" Width="523px"></asp:TextBox>
        <br />
        <br />
        <br />
    </form>
</body>
</html>


