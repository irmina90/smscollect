using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class _Default : System.Web.UI.Page
{
    SMSApi.Api.SMSFactory smsApiClient;
    string connStr = ConfigurationManager.ConnectionStrings["smscollectConnectionString"].ConnectionString;
    String name, lastname;

    protected void Page_Load(object sender, EventArgs e)
    {

        //wczytanie grup zajeciowych z bazy i umieszczenie ich w dropdownlist
        setListGroup("dziekanat");

        smsApiClient = new SMSApi.Api.SMSFactory(client());
        System.Diagnostics.Debug.WriteLine("wysyłanie");
        //getNumbersByGroupId(1);

        parseJson();

        name = (string)Session["NAME"];
        lastname = (string)Session["LASTNAME"];

       // ((Label)LoginView1.FindControl("lUser")).Text = name + " " + lastname;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        int groupId = Convert.ToInt32(((DropDownList)LoginView1.FindControl("DropDownList1")).SelectedValue);
        
        string[] numbers = (string[])getNumbersByGroupId(groupId,"dziekanat").ToArray(typeof(string));

        String message = ((TextBox)LoginView1.FindControl("TextBox1")).Text;
        sendSMS(numbers, message);

        //dane z wysyłanej wiadomości
      
       // String login = LoginName1.FormatString;
        String login = name + "" + lastname;
        String odbiorca = ((DropDownList)LoginView1.FindControl("DropDownList1")).SelectedItem.Text;
        String date = DateTime.Today.ToString("yyyy-MM-dd");
        String time = DateTime.Now.ToString("HH:mm:ss");

        SqlConnection mySQLConnection = new SqlConnection();
        mySQLConnection.ConnectionString = connStr;
        mySQLConnection.Open();

        //dodawanie do bazy wiadomości
        SqlCommand c = new SqlCommand("INSERT INTO tresc_sms (imie, nazwisko, odbiorca, tresc, data, godzina, ilosc_dostarczonych, ilosc_wyslanych) VALUES('" + name + "','" + lastname + "','" + odbiorca + "','" + message + "','" + date + "','" + time + "','" + 0 + "','" + 0 + "')", mySQLConnection);
        c.ExecuteNonQuery();

        //zapisywanie wiadomości jako szablon
        if (((CheckBox)LoginView1.FindControl("CheckBox2")).Checked)
        {
            SqlCommand d = new SqlCommand("INSERT INTO szablony (tresc, imie, nazwisko) VALUES('" + message + "','" + name + "','" + lastname + "')", mySQLConnection);
            d.ExecuteNonQuery();
        }
        mySQLConnection.Close();
    }


    [WebMethod]
    public static void Usun_szablon()
    {
        System.Diagnostics.Debug.WriteLine("szablon usuniety");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void wybierz_szablon(object sender, EventArgs e)
    {
   
    }
    public SMSApi.Api.Client client()
    {
            SMSApi.Api.Client client = new SMSApi.Api.Client("mklobukowska@gmail.com");
            client.SetPasswordRAW("szpileczka3");

            return client;
     }


    public void sendSMS(string[] phoneNumbers, String Message)
    {
        try
            {
                //var smsApiClient = new SMSApi.Api.SMSFactory(client());    
                var result =
                        smsApiClient.ActionSend()
                            .SetText(Message)
                            .SetTo(phoneNumbers)
                            .SetDateSent(DateTime.Now)
                            .Execute();
                System.Diagnostics.Debug.WriteLine("Send: " + result.Count);

                System.Diagnostics.Debug.WriteLine("Get:");

                Thread thread = new Thread(new ParameterizedThreadStart(waitForResponse));
                thread.Start(result);

               
            }
            catch (SMSApi.Api.ActionException eg)
            {
                /**
                 * Błędy związane z akcją (z wyłączeniem błędów 101,102,103,105,110,1000,1001 i 8,666,999,201)
                 * http://www.smsapi.pl/sms-api/kody-bledow
                 */
                System.Diagnostics.Debug.WriteLine(eg.Message);
            }
            catch (SMSApi.Api.ClientException eg)
            {
                /**
                 * 101 Niepoprawne lub brak danych autoryzacji.
                 * 102 Nieprawidłowy login lub hasło
                 * 103 Brak punków dla tego użytkownika
                 * 105 Błędny adres IP
                 * 110 Usługa nie jest dostępna na danym koncie
                 * 1000 Akcja dostępna tylko dla użytkownika głównego
                 * 1001 Nieprawidłowa akcja
                 */
                System.Diagnostics.Debug.WriteLine(eg.Message);
            }
            catch (SMSApi.Api.HostException eg)
            {
                /* błąd po stronie servera lub problem z parsowaniem danych
                 * 
                 * 8 - Błąd w odwołaniu
                 * 666 - Wewnętrzny błąd systemu
                 * 999 - Wewnętrzny błąd systemu
                 * 201 - Wewnętrzny błąd systemu
                 * SMSApi.Api.HostException.E_JSON_DECODE - problem z parsowaniem danych
                 */
                System.Diagnostics.Debug.WriteLine(eg.Message);
            }
            catch (SMSApi.Api.ProxyException eg)
            {
                // błąd w komunikacji pomiedzy klientem a serverem
                System.Diagnostics.Debug.WriteLine(eg.Message);
            }       
    }

    public ArrayList getNumbersByGroupId(int id, String login)
    {
        ArrayList numbers = new ArrayList();
        
        SqlConnection mySQLConnection = new SqlConnection();
        mySQLConnection.ConnectionString = connStr;
        mySQLConnection.Open();

        String query;
        query = "SELECT telefon,aktywny_numer FROM " +
                "Grupa_ INNER JOIN Grupy_Studenci " +
                "ON Grupa_.ID=Grupy_Studenci.grupa INNER JOIN Student ON Student.ID = Grupy_Studenci.student " +
                "INNER JOIN Pracownik ON Grupa_.prowadzacy = Pracownik.ID ";
        if (id > 0)
        {
            query = query + "WHERE Grupa_.ID = " + id;
        }
        else
        {
            if (login != "dziekanat")
            {
                query = query + "WHERE dzien = " + -id + " AND login = \'" + login + "\'";
            }
            else
            {
                query = query + "WHERE dzien = " + -id;
            }
        }

        SqlCommand cmd = new SqlCommand(query, mySQLConnection);
        cmd.ExecuteNonQuery();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            String number = dt.Rows[i]["telefon"].ToString();
            String active = dt.Rows[i]["aktywny_numer"].ToString();
            if (number != "" && active == "True")
            {
                System.Diagnostics.Debug.WriteLine(number);
                numbers.Add(number);
            }
        }
        mySQLConnection.Close();
        return numbers;
    }

    public static void waitForResponse(object obj)
    {
        //czekaj 30 sekund
        System.Threading.Thread.Sleep(30000);
        SMSApi.Api.Response.Status result = (SMSApi.Api.Response.Status)obj;
        
        string[] ids = new string[result.Count];
        for (int i = 0, l = 0; i < result.List.Count; i++)
        {
            if (!result.List[i].isError())
            {
                //Nie wystąpił błąd podczas wysyłki (numer|treść|parametry... prawidłowe)
                if (!result.List[i].isFinal())
                {
                    //Status nie jest koncowy, może ulec zmianie
                    ids[l] = result.List[i].ID;
                    l++;
                }
            }
        }

        foreach (var status in result.List)
        {
            System.Diagnostics.Debug.WriteLine("ID: " + status.ID + " NUmber: " + status.Number + " Points:" +
                status.Points + " Status:" + status.Status + " IDx: " + status.IDx);
        }
       
    }

    public void setListGroup(String login)
    {
        DropDownList lista = ((DropDownList)LoginView1.FindControl("DropDownList1"));

        if (lista != null && lista.Items.Count <= 0)
        {
            Hashtable weekDays = new Hashtable();
            weekDays[1] = "Poniedziałek";
            weekDays[2] = "Wtorek";
            weekDays[3] = "Środa";
            weekDays[4] = "Czwartek";
            weekDays[5] = "Piątek";

            SortedDictionary<int, ArrayList> listGroupByDay = getListGroup(login);

            foreach (KeyValuePair<int, ArrayList> entry in listGroupByDay)
            {
                int day = Convert.ToInt32(entry.Key.ToString());
                ((DropDownList)LoginView1.FindControl("DropDownList1")).Items.Add(new ListItem(weekDays[day].ToString(), "-" + entry.Key.ToString()));
                ArrayList array = (ArrayList)entry.Value;
                foreach (SortedDictionary<int, string> item in array)
                {
                    foreach (KeyValuePair<int, string> el in item)
                    {
                        ((DropDownList)LoginView1.FindControl("DropDownList1")).Items.Add(new ListItem(el.Value.ToString(), el.Key.ToString()));
                    }
                }
            }
        }
    }

    public SortedDictionary<int, ArrayList> getListGroup(String login)
    {
        SortedDictionary<int, ArrayList> listGroupByDay = new SortedDictionary<int, ArrayList>();

        SqlConnection mySQLConnection = new SqlConnection();
        mySQLConnection.ConnectionString = connStr;
        mySQLConnection.Open();
        String query = "SELECT Grupa_.ID, kod, pelna_nazwa, grupa, godzina, dzien from " +
        "Pracownik INNER JOIN Grupa_ ON Pracownik.ID = Grupa_.prowadzacy";
        if (login != "dziekanat")
        {
            query = query + " WHERE Pracownik.login = \'" + login + "\'";
        }
        query = query + " ORDER BY dzien";

        SqlCommand cmd = new SqlCommand(query, mySQLConnection);
        cmd.ExecuteNonQuery();

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            SortedDictionary<int, String> groupItem = new SortedDictionary<int, String>();
            
            int id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
            String code = dt.Rows[i]["kod"].ToString();
            String name = dt.Rows[i]["pelna_nazwa"].ToString();
            String group = dt.Rows[i]["grupa"].ToString();
            String time = dt.Rows[i]["godzina"].ToString();
            int day = Convert.ToInt32(dt.Rows[i]["dzien"].ToString());
            String listString = code + " - " + name + " - " + group + " - " + time;
            groupItem.Add(id, listString);
            if(!listGroupByDay.ContainsKey(day))
            {
                ArrayList array = new ArrayList();
                array.Add(groupItem);
                listGroupByDay.Add(day,array);
            }
            else
            {
                ArrayList array = (ArrayList) listGroupByDay[day];
                array.Add(groupItem);
            }
        }
        mySQLConnection.Close();
        return listGroupByDay;
    }



    protected void user_Click(object sender, EventArgs e)
    {
  
    }

    public void parseJson()
    {
        String data = "{\"course_id\": \"06-DZJNUI0\", \"group_number\": 1,\"course_name\": { \"en\": \"Application of Information Technology in Natural Language Processing\", \"pl\": \"Zastosowania informatyki w przetwarzaniu języka naturalnego\" }, \"class_type\": { \"en\": \"lecture\", \"pl\": \"Wykład\"}}";
        JObject o = JObject.Parse(data);
        System.Diagnostics.Debug.WriteLine("id: "+o["course_id"]);
        System.Diagnostics.Debug.WriteLine("nazwa: " + o["course_name"]["pl"]);
        System.Diagnostics.Debug.WriteLine("typ zajęć: " + o["class_type"]["pl"]);
    }
}
    

