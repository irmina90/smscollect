using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Account_Login : System.Web.UI.Page
{

    public static string[] servers = { @"dc1.labs.wmi.amu.edu.pl", @"dc2.labs.wmi.amu.edu.pl" };
    public static string suffix = @"labs.wmi.amu.edu.pl";
    //int port = 636;
    public static int port = 389;
    public static string root = @"DC=labs,DC=wmi,DC=amu,DC=edu,DC=pl";

    public String[] user = new String[2];

    LdapCredentailValidation lucv = new LdapCredentailValidation(servers, port, suffix, root);
  
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool login(string user, string pass)
    {



        
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
           user = lucv.GetUserData(LoginUser.UserName);
           Session["NAME"] = user[0];
           Session["LASTNAME"] = user[1];
        }
         else
        {
            // cos tam
        }

    }

}
