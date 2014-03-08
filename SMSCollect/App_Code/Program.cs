using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for Program
/// </summary>
public class Program
{
	public Program()
	{
	}


    public String loguj()
    {
        string[] servers = { @"dc1.labs.wmi.amu.edu.pl", @"dc2.labs.wmi.amu.edu.pl" };
            string suffix =  @"labs.wmi.amu.edu.pl";
            int port = 636;
            string root = @"DC=labs,DC=wmi,DC=amu,DC=edu,DC=pl";

           /* LdapAnonymousQuery laq = new LdapAnonymousQuery(servers, port, suffix, root);
            laq.GetUserData("guest001");

            Console.Write("Validating credentials, result: "); */

            FileStream fsr = new FileStream(HttpContext.Current.Server.MapPath("plik.txt"), FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fsr);
            string login = sr.ReadLine();
            string haslo = sr.ReadLine();
            sr.Close();
            fsr.Close();
 

            LdapCredentailValidation lucv = new LdapCredentailValidation(servers, port, suffix, root);
            try 
            { 
                    Console.WriteLine(lucv.CheckUserCredential(login, haslo));
                    return "Zalogowano poprawnie";
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while checking credentials:" + e.ToString());
                return "Niezalogowano";

            }
            Console.Read();
        }


    }
    