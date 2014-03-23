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

public partial class _Default : System.Web.UI.Page
{
    //czy na pewno factory?
    SMSApi.Api.SMSFactory smsApiClient;
    string connStr = ConfigurationManager.ConnectionStrings["smscollectConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //wczytanie grup zajeciowych z bazy i umieszczenie ich w dropdownlist
        setListGroup("dziekanat");

        smsApiClient = new SMSApi.Api.SMSFactory(client());
        System.Diagnostics.Debug.WriteLine("wysyłanie");
        getNumbersByGroupId(1);
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
    public void sendSMSToGroup()
    {

    }

    public void sendOneSMS(String phoneNumber, String Message)
    {
        try
            {
                //var smsApiClient = new SMSApi.Api.SMSFactory(client());    
                var result =
                        smsApiClient.ActionSend()
                            .SetText(Message)
                            .SetTo(phoneNumber)
                            .SetDateSent(DateTime.Now)
                            .Execute();
                System.Diagnostics.Debug.WriteLine("Send: " + result.Count);

                System.Diagnostics.Debug.WriteLine("Get:");

                waitForResponse(result);
               
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        int groupId = Convert.ToInt32(DropDownList1.SelectedValue);
        ArrayList numbers = getNumbersByGroupId(groupId);
        foreach (var number in numbers)
        {
            String message = TextBox1.Text;
            sendOneSMS(number.ToString(), message);
        }
    }

    public ArrayList getNumbersByDay(int id)
    {
        ArrayList numbers = new ArrayList();
        return numbers;
    }
    
    public ArrayList getNumbersByGroupId(int id)
    {
        ArrayList numbers = new ArrayList();
        
        SqlConnection mySQLConnection = new SqlConnection();
        mySQLConnection.ConnectionString = connStr;
        mySQLConnection.Open();

        String query = "SELECT telefon,aktywny_numer FROM " +
            "Grupa_ INNER JOIN Grupy_Studenci " +
            "ON Grupa_.ID=Grupy_Studenci.grupa INNER JOIN Student ON Student.ID = Grupy_Studenci.student " +
            "WHERE Grupa_.ID = " + id;

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

    public void waitForResponse(SMSApi.Api.Response.Status result)
    {
        //TODO powinno być w osobnym wątku, rozsadniej czekanie na jedna wiadomosc w jednym watku
        //czekaj 30 sekund
        System.Threading.Thread.Sleep(30000);
        foreach (var status in result.List)
        {
            //status.Status przechowuje informacje czy informacja zostala dostarczona 
            System.Diagnostics.Debug.WriteLine("ID: " + status.ID + " NUmber: " + status.Number + " Points:" +
                status.Points + " Status:" + status.Status + " IDx: " + status.IDx);
        }
    }

    public void setListGroup(String login)
    {
        Hashtable weekDays = new Hashtable();
        weekDays[1] = "Poniedziałek";
        weekDays[2] = "Wtorek";
        weekDays[3] = "Środa";
        weekDays[4] = "Czwartek";
        weekDays[5] = "Piątek";
    
        Hashtable listGroupByDay = getListGroup(login);
        foreach (DictionaryEntry entry in listGroupByDay)
        {
            int day = Convert.ToInt32(entry.Key.ToString());
            DropDownList1.Items.Add(new ListItem(weekDays[day].ToString(), "day_" + entry.Key.ToString()));
            ArrayList array = (ArrayList) entry.Value;
            foreach(Hashtable item in array)
            {
                foreach (DictionaryEntry el in item)
                {
                    DropDownList1.Items.Add(new ListItem(el.Value.ToString(), el.Key.ToString()));
                }
            }
        }
    }

    public Hashtable getListGroup(String login)
    {
        Hashtable listGroupByDay = new Hashtable();

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
            Hashtable groupItem = new Hashtable();
            
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
}
    

