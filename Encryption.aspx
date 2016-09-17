<%@ Page Title="Encryption" Language="C#" MasterPageFile="~/Site.Master" EnableViewState="true" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Encryption.aspx.cs" Inherits="Encryption._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="css/Boots.css" rel="stylesheet">

    <form class="form-horizontal">
        <fieldset>
            <legend>Encryption or Hashing Methods</legend>
            <div class="form-group">
                <label for="inputEncrypt" class="col-lg-2 control-label">String to Encrypt</label>
                <div class="col-lg-10">
                    <input type="text" class="form-control" id="inputEncrypt" placeholder="abc123" runat="server">
                </div>
            </div>
           <!-- <div class="form-group">
                <label class="col-lg-2 control-label">Radios</label>
                <div class="col-lg-10">
                    <div class="radio">
                        <label>
                            <input type="radio" name="optionsRadios" id="optionsRadios1" value="option1" checked="true" runat="server" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckedChanged" Enabled ="true" >
                            Symmetrical Encryption
                        </label>
                    </div>
                    <div class="radio">
                        <label>
                            <input type="radio" name="optionsRadios" id="optionsRadios2" value="option2" checked="true" runat="server" AutoPostBack="True" OnCheckedChanged="RadioButton_CheckedChanged" Enabled="true">
                            Asymmetrical Encryption
                        </label>
                    </div>
                </div>
            </div>--->
            <div class="form-group">
                <label for="select" class="col-lg-2 control-label">Encryption or Hashing Type</label>
                <div class="col-lg-10">
                    <select class="form-control" id="select" runat="server">
                        <option value="SEnAES">AES</option>
                        <option value="SEnDES">DES</option>
                        <option value="SEnRC2">RC2</option>
                        <option value="SEnRijndael">Rijndael</option>
                        <option value="SEnTripleDES">TripleDES</option>
                        <option value="HMD5">MD5</option>
                        <option value="HSHA1">SHA1</option>
                        <option value="HRIPEMD160">RIPEMD160</option>
                    </select>
                </div>
            </div>
            <div class="form-group">
                <div class="col-lg-10 col-lg-offset-2">
                    <asp:Button ID="CancelBtn" runat="server" OnClick="Cancel_Click" Text="Cancel" type="reset" class="btn btn-default" />
                    <asp:Button ID="SubmitBtn" runat="server" OnClick="Submit_Click" Text="Submit" type="submit" class="btn btn-primary" />
                </div>
            </div>
        </fieldset>
    </form>
    <br />
    <div class="panel panel-info">
        <div class="panel-heading">
            <h3 class="panel-title">Results</h3>
        </div>
        <div class="panel-body" id="enResults" runat="server">
            
        </div>
    </div>
    
</asp:Content>
