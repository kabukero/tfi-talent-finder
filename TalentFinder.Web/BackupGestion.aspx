<%@ Page Language="C#" Title="Listado Backups" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackupGestion.aspx.cs" Inherits="TalentFinder.Web.BackupGestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<h2>Listado de backups</h2>

		<asp:GridView ID="gvListado" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-striped table-hover" OnRowCommand="gvListado_RowCommand">
			<Columns>
				<asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
				<asp:BoundField DataField="Usuario.NombreCompleto" HeaderText="Realizado Por" />
				<asp:BoundField DataField="FileName" HeaderText="FileName" />
				<asp:BoundField DataField="FechaHora" HeaderText="Fecha" />
				<asp:TemplateField HeaderText="Acción">
					<ItemTemplate>
						<asp:Button ID="BtnRestore" runat="server" CausesValidation="false" CommandName="Restore" Text="Restore" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			<HeaderStyle CssClass="thead-dark" />
			<EmptyDataTemplate>
				<h5>No hay datos cargados</h5>
			</EmptyDataTemplate>
		</asp:GridView>

		<div class="row col-md-12">
			<div class="form-group col-md-4">
				<asp:Button ID="BtnCrearBackup" runat="server" Text="Crear Backup" CssClass="btn btn-primary" OnClick="CrearBackup_Click" />
			</div>
		</div>

		<div class="row col-md-12">
			<div class="form-group">
				<asp:Panel ID="PanelError" CssClass="alert alert-danger" Font-Bold="false" runat="server"></asp:Panel>
			</div>
		</div>
	</div>
</asp:Content>
