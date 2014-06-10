using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;


namespace Usos
{
    class Program
    {
       
        static void Main(string[] args)
        {


            Updater updater = new Updater();
            //updater.update_course("1872");
            
            //updater.update_all_courses();
            //updater.update_participants();
            updater.aktualizuj_nasze_nr_tel();
            Console.WriteLine("Koniec");
            Console.Read();
            

        }
    }
}
