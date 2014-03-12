using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Account_Login : System.Web.UI.Page
{


  
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool login(string user, string pass)
    {

        string[] servers = { @"dc1.labs.wmi.amu.edu.pl", @"dc2.labs.wmi.amu.edu.pl" };
        string suffix = @"labs.wmi.amu.edu.pl";
        //int port = 636;
        int port = 389;
        string root = @"DC=labs,DC=wmi,DC=amu,DC=edu,DC=pl";


        LdapCredentailValidation lucv = new LdapCredentailValidation(servers, port, suffix, root);
        try
        {
            lucv.CheckUserCredential(LoginUser.UserName, LoginUser.Password);
            // Console.WriteLine(lucv.CheckUserCredential(login, haslo));
            return true; ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception while checking credentials:" + e.ToString());
            return false;

        }

    }



    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool ok = false;
        ok = login(LoginUser.UserName, LoginUser.Password);
        e.Authenticated = ok;
        if (ok)
        {

            // cos tam


        }
        else
        {
            // cos tam
        }

    }

}
