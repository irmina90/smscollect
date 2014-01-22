using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            client.SetPasswordRAW("szpilka3");

            return client;
        }

    protected void Button2_Click(object sender, EventArgs e)
    {
        
         try
            {
                var smsApi = new SMSApi.Api.SMSFactory(client());

                var result =
                    smsApi.ActionSend()
                        .SetText(TextBox1.Text)
                        .SetTo("694370755")
                        .SetDateSent(DateTime.Now)
                        .Execute();

                System.Console.WriteLine("Send: " + result.Count);

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

                System.Console.WriteLine("Get:");
                result =
                    smsApi.ActionGet()
                        .Ids(ids)
                        .Execute();

                foreach (var status in result.List)
                {
                    System.Console.WriteLine("ID: " + status.ID + " NUmber: " + status.Number + " Points:" + status.Points + " Status:" + status.Status + " IDx: " + status.IDx);
                }

                var deleted =
                    smsApi
                        .ActionDelete()
                            .Id(ids)
                            .Execute();

                System.Console.WriteLine("Deleted: " + deleted.Count);
            }
            catch (SMSApi.Api.ActionException eg)
            {
                /**
                 * Błędy związane z akcją (z wyłączeniem błędów 101,102,103,105,110,1000,1001 i 8,666,999,201)
                 * http://www.smsapi.pl/sms-api/kody-bledow
                 */
                System.Console.WriteLine(eg.Message);
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
                System.Console.WriteLine(eg.Message);
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
                System.Console.WriteLine(eg.Message);
            }
            catch (SMSApi.Api.ProxyException eg)
            {
                // błąd w komunikacji pomiedzy klientem a serverem
                System.Console.WriteLine(eg.Message);
            }
        }

}
    
    

