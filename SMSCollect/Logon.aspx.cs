using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Logon : System.Web.UI.Page
{

    Program p = new Program();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void zaloguj_Click(object sender, EventArgs e)
    {
        string login = loginText.Text;
        string haslo = hasloText.Text;
        string odpowiedz;


        /*    FileStream fsr = new FileStream(HttpContext.Current.Server.MapPath("plik.txt"), FileMode.Open, FileAccess.ReadWrite);
            StreamWriter sr = new StreamWriter(fsr);
            sr.WriteLine(login);
            sr.WriteLine(haslo);
            sr.Close();
            fsr.Close(); */

           odpowiedz = p.loguj(login, haslo);
           odpowiedzText.Text = odpowiedz;

        
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
 
        Server.Transfer("Default.aspx"); 
    }
   
}