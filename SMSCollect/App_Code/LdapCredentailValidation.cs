using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.Protocols;
using System.Net;

/// <summary>
/// Summary description for LdapCredentailValidation
/// </summary>
public class LdapCredentailValidation
    {
        private string[] _ldapServers;
        private int _ldapPort;
        private string _userSuffix;
        private string _ldapRoot;

        public LdapCredentailValidation(String[] Servers, Int32 Port, String UserSuffix, String LdapRoot)
        {
            _ldapServers = Servers;
            _userSuffix = UserSuffix;
            _ldapPort = Port;
            _ldapRoot = LdapRoot;
        }

        public bool CheckUserCredential(String UserName, String Password)
        {
            try
            {
                LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier(_ldapServers, _ldapPort, true, false);
                LdapConnection lc = new LdapConnection(ldi);

                lc.AuthType = AuthType.Kerberos;

                String ldapUser = String.Format("{0}@{1}", UserName, _userSuffix);
                lc.Credential = new NetworkCredential(ldapUser, Password);

                lc.Bind();
                return true;


            }
            catch (Exception e)
            {
                throw;
            }
        }

        public String name, lastname;
        public String[] user = new String[2];

        public String[] GetUserData(String Login)
        {
            try
            {
                LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier(_ldapServers, _ldapPort, true, false);
                LdapConnection lc = new LdapConnection(ldi);
                lc.AuthType = AuthType.Anonymous;
                lc.Bind();

                string filter = String.Format("(&(objectCategory=person)(sAMAccountName={0}))", Login);
                string[] attributesToReturn = { "sAMAccountName", "givenname", "sn" };

                SearchRequest sreq = new SearchRequest(_ldapRoot, filter, SearchScope.Subtree, attributesToReturn);
                SearchResponse sres = lc.SendRequest(sreq) as SearchResponse;

                foreach (SearchResultEntry result in sres.Entries)
                {
                    foreach (string i in result.Attributes["givenname"].GetValues(typeof(String)))
                    {
                        user[0] = i;
                        //name = i;

                    }

                    foreach (string i in result.Attributes["sn"].GetValues(typeof(String))) 
                    {
                        user[1] = i;
                        //lastname = i; 
                    }

                }

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while getting user data:" + e.ToString());
                user[0] = "Exception while getting user data:" + e.ToString();
                return user;
            }
        }

    }
