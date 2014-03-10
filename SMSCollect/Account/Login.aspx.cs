using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Account_Login : System.Web.UI.Page
{

    Program p = new Program();
  
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {

       // string login = LoginUser.UserName;
        //string haslo = LoginUser.Password; 
        //string odpowiedz;

        //p.loguj(login, haslo);
        //odpowiedzText.Text = odpowiedz;
    }

    protected void zaloguj_Click(object sender, EventArgs e)
    {
        string login = loginText.Text;
        string haslo = hasloText.Text;
        string odpowiedz;

        odpowiedz = p.loguj(login, haslo);
        odpowiedzText.Text = odpowiedz;
    }
}
