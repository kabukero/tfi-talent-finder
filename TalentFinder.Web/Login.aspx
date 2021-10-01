<%@ Page Language="C#" Title="Talent Finder - Login" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TalentFinder.Web.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="row vertical-offset-100">
		<div class="col-md-4 col-md-offset-4">
			<div class="panel panel-default">
				<div class="panel-heading">
					<h3 class="panel-title">Login</h3>
				</div>
				<div class="panel-body">
					<fieldset>
						<div class="form-group">
							<label for="TxtEmail">E-mail</label>
							<asp:TextBox ID="TxtEmail" placeholder="E-mail" CssClass="form-control" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ControlToValidate="TxtEmail" Text="Ingrese el e-mail" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
						</div>
						<div class="form-group">
							<label for="TxtPassword">Password</label>
							<asp:TextBox ID="TxtPassword" placeholder="Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
							<asp:RequiredFieldValidator ControlToValidate="TxtPassword" Text="Ingrese el password" CssClass="text-danger" runat="server"></asp:RequiredFieldValidator>
						</div>
						<asp:Button ID="Button1" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="Ingresar_Click" />
						<div class="form-group">
							<asp:Panel ID="PanelErrorLogin" CssClass="alert alert-danger" Font-Bold="false" runat="server"></asp:Panel>
						</div>
					</fieldset>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
