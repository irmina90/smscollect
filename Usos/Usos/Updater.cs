using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Usos
{
    class Updater
    {
        static private ApiConnector apiConnector = new ApiConnector(new ApiInstallation { base_url = "http://usosapps.amu.edu.pl/" });
        string connString = "Data Source=mssql.wmi.amu.edu.pl;Initial Catalog=smscollect;User ID=smscollect;Password=P6JMrSRu";
        public String[] getAllIdLecturer()
        {
            
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            String query;

            query = "SELECT * from sms_pracownik;";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();


            String id;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            String[] id_pracownikow = new String[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id = dt.Rows[i]["id"].ToString();
                id_pracownikow[i] = id;
                //Console.WriteLine(id);
            }
            conn.Close();
            return id_pracownikow;
        }

        public String getDescription(int zaj_cyk_id, String tzaj_kod)
        {
            String opis = "";
            int dziesiatki = (zaj_cyk_id / 10);
            if (dziesiatki == 0) opis +="1";
            else opis += dziesiatki;
            if (tzaj_kod == "WYK") opis += "W";
            else opis += "C";
            int jednosci = zaj_cyk_id % 10;

            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            opis += letters[jednosci % letters.Length - 1];

            return opis;
        }

        public void insert_update_course(JToken przedmioty,String id_pracownika)
        {
            if (przedmioty != null)
            {
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();

                String query;

                query = "SELECT imie, nazwisko from sms_pracownik WHERE id=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id_pracownika);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                String imie = dt.Rows[0]["imie"].ToString();
                String nazwisko = dt.Rows[0]["nazwisko"].ToString();

                conn.Close();
                


                //dodajemy pokolei każdy przedmiot

                foreach (var item in przedmioty)
                {
                    //kod zajec na wmi zaczyna sie od 06
                    if (item["course_id"].ToString().Substring(0, 2) == "06")
                    {
                        String opis = getDescription(Convert.ToInt32(item["group_number"]), item["class_type_id"].ToString());
                        /*
                        Console.WriteLine(item["course_id"]);
                        Console.WriteLine(item["course_unit_id"]);
                        Console.WriteLine(item["group_number"]);                     
                        Console.WriteLine(opis);
                        Console.WriteLine(item["term_id"]);
                        Console.WriteLine(item["course_name"]["pl"]);
                        Console.WriteLine(item["class_type_id"]);
                        Console.WriteLine("");
                         * */

                        SqlConnection connection = new SqlConnection(connString);
                        connection.Open();
                        String insert = "";
                        insert = "INSERT INTO sms_przedmioty_pracownika(prac_id,imie,nazwisko,zaj_cyk,nr,tzaj_kod,kod,nazwa,opis,semestr) "+
                            "VALUES(@prac_id,@imie,@nazwisko,@zaj_cyk,@nr,@tzaj_kod,@kod,@nazwa,@opis,@semestr)";
                        SqlCommand d = new SqlCommand(insert, connection);

                        d.Parameters.AddWithValue("@prac_id", id_pracownika);
                        d.Parameters.AddWithValue("@imie", imie);
                        d.Parameters.AddWithValue("@nazwisko", nazwisko);
                        d.Parameters.AddWithValue("@zaj_cyk", item["course_unit_id"].ToString());
                        d.Parameters.AddWithValue("@nr", item["group_number"].ToString());
                        d.Parameters.AddWithValue("@tzaj_kod", item["class_type_id"].ToString());
                        d.Parameters.AddWithValue("@kod", item["course_id"].ToString());
                        d.Parameters.AddWithValue("@nazwa", item["course_name"]["pl"].ToString());
                        d.Parameters.AddWithValue("@opis", opis);
                        d.Parameters.AddWithValue("@semestr", item["term_id"].ToString());
                        d.ExecuteNonQuery();
                        //Console.WriteLine("Dodano");
                        connection.Close();

                        
                    }

                    

                }
            }
        }


        public void update_course(String id_pracownika)
        {
            String url = "http://usosapps.amu.edu.pl/services/groups/lecturer?oauth_consumer_key=UNkjKMYP9rzcCJQuyKem&oauth_nonce=6434844&oauth_signature_method=HMAC-SHA1&oauth_version=1.0&oauth_signature=AZCfn863GO3H7yNCfRygKDVZVmE%3d&fields=course_id|course_unit_id|group_number|class_type_id&user_id=" + id_pracownika;
            String response = apiConnector.GetResponse(url);
            JObject o = JObject.Parse(response);


            if (o["groups"]["2014/SL"] != null) 
            {
                insert_update_course(o["groups"]["2014/SL"], id_pracownika);

            }
            if(o["groups"]["2013/SZ"] != null)
            {
                insert_update_course(o["groups"]["2013/SZ"], id_pracownika);
            }
            if(o["groups"]["2013/2014"] != null)
            {
                insert_update_course(o["groups"]["2013/2014"], id_pracownika);
            }
        

            
            
        }

        public void update_all_courses()
        {

            delete_courses();
            String[] id_pracownikow = getAllIdLecturer();
            foreach (String id in id_pracownikow)
            {
                update_course(id);
            }
        }

        public void delete_courses()
        {
            //usuniecie wszystkiego z bazy aby przy kojelnej synfronizacji z usos nie zdublowac przedmiotow
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            String query;

            query = "delete from sms_przedmioty_pracownika";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            //Console.WriteLine("usunieto");

        }

    }
}
