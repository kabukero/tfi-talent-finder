<%@ Page Language="C#" Title="Listado Bitacora" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BitacoraGestion.aspx.cs" Inherits="TalentFinder.Web.BitacoraGestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<h2>Listado de la bitácora</h2>

		<asp:GridView ID="gvListado" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-striped table-hover">
			<Columns>
				<asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
				<asp:BoundField DataField="Usuario.NombreCompleto" HeaderText="Realizado Por" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
				<asp:BoundField DataField="FechaHora" HeaderText="Fecha" />
			</Columns>
			<HeaderStyle CssClass="thead-dark" />
			<EmptyDataTemplate>
				<h5>No hay datos cargados</h5>
			</EmptyDataTemplate>
		</asp:GridView>

		<div class="row col-md-12">
			<div class="form-group col-md-4">
				<label for="DDLUsuario">Usuario</label>
				<asp:DropDownList ID="DDLUsuario" placeholder="Usuario" CssClass="form-control" runat="server"></asp:DropDownList>
			</div>
			<div class="form-group col-md-4">
				<label for="CalFechaDesde">Fecha desde</label>
				<asp:Calendar ID="CalFechaDesde" runat="server"></asp:Calendar>
			</div>
			<div class="form-group col-md-4">
				<label for="CalFechaHasta">Fecha hasta</label>
				<asp:Calendar ID="CalFechaHasta" runat="server"></asp:Calendar>
			</div>
		</div>

		<div class="row col-md-12">
			<div class="form-group col-md-4">
				<asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="Buscar_Click" />
			</div>
		</div>
	</div>
</asp:Content>
