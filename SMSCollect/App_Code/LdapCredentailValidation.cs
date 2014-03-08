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
    }
