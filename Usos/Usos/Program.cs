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
            //updater.update_course("1955");
            updater.update_all_courses();
            

        }
    }
}
