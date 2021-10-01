<%@ Page Language="C#" Title="Listado Usuarios" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioGestion.aspx.cs" Inherits="TalentFinder.Web.UsuarioGestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row">
		<h2>Listado de Usuarios</h2>

		<asp:GridView ID="gvListado" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-striped table-hover" OnRowCommand="gvListado_RowCommand">
			<Columns>
				<asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
				<asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
				<asp:BoundField DataField="Email" HeaderText="Empresa" />
				<asp:BoundField DataField="Enable" HeaderText="Habilitado" />
				<asp:BoundField DataField="UsuarioEditor.NombreCompleto" HeaderText="Editado Por" />
				<asp:BoundField DataField="FechaEdicion" HeaderText="Fecha Edición" />
				<asp:TemplateField HeaderText="Acción">
					<ItemTemplate>
						<asp:Button ID="BtnSeleccionar" runat="server" CausesValidation="false" CommandName="Seleccionar" Text="Seleccionar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary" />
						<asp:Button ID="BtnEliminar" runat="server" CausesValidation="false" CommandName="Eliminar" Text="Eliminar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-secondary" />
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
				<asp:HiddenField ID="HidId" runat="server"></asp:HiddenField>
				<label for="TxtNombre">Nombre</label>
				<asp:TextBox ID="TxtNombre" placeholder="Nombre" CssClass="form-control" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ControlToValidate="TxtNombre" Text="Ingrese el nombre" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
			</div>
			<div class="form-group col-md-4">
				<label for="TxtApellido">Apellido</label>
				<asp:TextBox ID="TxtApellido" placeholder="Apellido" CssClass="form-control" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ControlToValidate="TxtApellido" Text="Ingrese el apellido" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
			</div>
		</div>

		<div class="row col-md-12">
			<div class="form-group col-md-4">
				<label for="TxtEmail">E-mail</label>
				<asp:TextBox ID="TxtEmail" placeholder="E-mail" CssClass="form-control" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ControlToValidate="TxtEmail" Text="Ingrese el e-mail" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ControlToValidate="TxtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="E-mail incorrecto" runat="server"></asp:RegularExpressionValidator>
			</div>
			<div class="form-group col-md-4">
				<label for="TxtPassword">Password</label>
				<asp:TextBox ID="TxtPassword" placeholder="Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ControlToValidate="TxtPassword" Text="Ingrese el password" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
			</div>
		</div>

		<div class="row col-md-12">
			<div class="row col-md-4">
				<label for="ChkHabilitado">Habilitado</label>
				<asp:CheckBox ID="ChkHabilitado" runat="server"></asp:CheckBox>
			</div>
		</div>

		<div class="row col-md-12">
			<div class="form-group col-md-4">
				<asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="Guardar_Click" />
				<asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="Limpiar_Click" />
			</div>
		</div>
	</div>
</asp:Content>
