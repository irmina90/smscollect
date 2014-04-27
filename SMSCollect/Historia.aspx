<%@ Page Title="Historia" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Historia.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <p>
   </p>
       <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <p>
                Zawartoœæ przeznaczona jedynie dla zalogowanych u¿ytkowników. 
            </p>
        </AnonymousTemplate>
        <LoggedInTemplate>  
    <p>Twoja historia wys³¹nych wiadomoœci SMS<asp:GridView ID="GridView1" CssClass="table" runat="server" 
           AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
           AllowPaging="True" AllowSorting="True">
       <Columns>
           <asp:BoundField DataField="Odbiorca" HeaderText="Odbiorca" SortExpression="Odbiorca" />
           <asp:BoundField DataField="Tresc" HeaderText="Tresc" SortExpression="Tresc" />
           <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
           <asp:BoundField DataField="Godzina" HeaderText="Godzina" SortExpression="Godzina"><ItemStyle CssClass="extension-col"></ItemStyle></asp:BoundField>
           <asp:BoundField DataField="Ilosc_dostarczonych" HeaderText="Ilosc_dostarczonych" SortExpression="Ilosc_dostarczonych"><ItemStyle CssClass="extension-col"></ItemStyle></asp:BoundField>
           <asp:BoundField DataField="Ilosc_wyslanych" HeaderText="Ilosc_wyslanych" SortExpression="Ilosc_wyslanych"><ItemStyle CssClass="extension-col"></ItemStyle></asp:BoundField>
       </Columns>
       </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:smscollectConnectionString %>" 
           SelectCommand="SELECT [Odbiorca], [Tresc], [Data], [Godzina], [Ilosc_dostarczonych], [Ilosc_wyslanych] FROM [tresc_sms]  WHERE (([imie] = @imie) AND ([nazwisko] = @nazwisko))">
       
       <SelectParameters>
        <asp:SessionParameter Name="imie" SessionField="name" Type="String" />
        <asp:SessionParameter Name="nazwisko" SessionField="lastname" Type="String" />
       </SelectParameters>
       </asp:SqlDataSource>
    </p>   
    </LoggedInTemplate> 
    </asp:LoginView>
</asp:Content>

