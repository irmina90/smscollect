using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Reflection;
using System.Diagnostics;



namespace Usos
{
    class Program
    {
        static private ApiConnector apiConnector = new ApiConnector(new ApiInstallation { base_url = "http://usosapps.amu.edu.pl/" });
        static String url = "http://usosapps.amu.edu.pl/services/users/search_current_teachers?name=jassem";
       // static String url = "http://usosapps.amu.edu.pl/services/groups/lecturer?fields=course_id&oauth_consumer_key=UNkjKMYP9rzcCJQuyKem&oauth_nonce=6434844&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1395762851&oauth_version=1.0&user_id=1872&oauth_signature=AZCfn863GO3H7yNCfRygKDVZVmE%3d";
        static void Main(string[] args)
        {
            //apiConnector.SwitchInstallation(new ApiInstallation { base_url = "http://usosapps.amu.edu.pl/" });
        /*    string signature = oauth.GenerateSignature(new System.Uri(url), consumer_key,
                consumer_secret, token, token_secret, "GET", timestamp, nonce, out normalized_url,
                out normalized_params); */
            apiConnector.GetResponse(url);

            Console.WriteLine(apiConnector.GetResponse(url)); //+ "&oauth_signature=" + HttpUtility.UrlEncode(signature)));
            Console.Read();
        }
    }
}
