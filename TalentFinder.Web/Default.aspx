<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TalentFinder.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row text-center">
        <h2>Bienvenido a Talent Finder</h2>
    </div>
    <div class="row">Gestión Sistema</div>
    <div class="row">
        <div class="col-md-3 col-12 mb-3">
            <asp:Button ID="BtnUsuarioGestion" runat="server" Text="Gestión Usuarios" CssClass="btn btn-primary" OnClick="GoToPage_Click" />
        </div>
        <div class="col-md-3 col-12 mb-3">
            <asp:Button ID="BtnBitacoraGestion" runat="server" Text="Gestión Bitácora" CssClass="btn btn-md btn-primary" OnClick="GoToPage_Click" />
        </div>
        <div class="col-md-3 col-12 mb-3">
            <asp:Button ID="BtnBackupGestion" runat="server" Text="Gestión Backups" CssClass="btn btn-md btn-primary" OnClick="GoToPage_Click" />
        </div>
        <div class="col-md-3 col-12 mb-3">
            <asp:Button ID="BtnIdiomaGestion" runat="server" Text="Gestión Idiomas" CssClass="btn btn-md btn-primary" OnClick="GoToPage_Click" />
        </div>
    </div>
<%--    <div class="row">Gestión Negocio</div>
    <div class="row">
        <div class="col-md-3 col-12 mb-3">
            <a href="UsuarioListado" class="btn btn-primary btn-md">Listado Usuarios</a>
        </div>
        <div class="col-md-3 col-12 mb-3">
            <a href="EmpresaListado" class="btn btn-primary btn-md">Listado Empresas</a>
        </div>
        <div class="col-md-3 col-12 mb-3">
            <a href="DestinoListado" class="btn btn-primary btn-md">Listado Destinos</a>
        </div>
        <div class="col-md-3 col-12 mb-3">
            <a href="DestinoListado" class="btn btn-primary btn-md">Listado Destinos</a>
        </div>
    </div>--%>
</asp:Content>
