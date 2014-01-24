using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TableRow row1 = new TableRow();
        TableCell imie = new TableCell();
        imie.Text = "Marta";
        TableCell nazwisko = new TableCell();
        nazwisko.Text = "Klobukowska";
        TableCell indeks = new TableCell();
        indeks.Text = "362636";
        TableCell nrtel = new TableCell();
        nrtel.Text = "123456789";
        row1.Cells.Add(imie);
        row1.Cells.Add(nazwisko);
        row1.Cells.Add(indeks);
        row1.Cells.Add(nrtel);
        myTable.Rows.Add(row1);

        TableRow row2 = new TableRow();
        TableCell imie2 = new TableCell();
        imie2.Text = "Weronika";
        TableCell nazwisko2 = new TableCell();
        nazwisko2.Text = "Zietek";
        TableCell indeks2 = new TableCell();
        indeks2.Text = "362704";
        TableCell nrtel2 = new TableCell();
        nrtel2.Text = "456789123";
        row2.Cells.Add(imie2);
        row2.Cells.Add(nazwisko2);
        row2.Cells.Add(indeks2);
        row2.Cells.Add(nrtel2);
        myTable.Rows.Add(row2);

        TableRow row3 = new TableRow();
        TableCell imie3 = new TableCell();
        imie3.Text = "Anita";
        TableCell nazwisko3 = new TableCell();
        nazwisko3.Text = "Lipinska";
        TableCell indeks3 = new TableCell();
        indeks3.Text = "347812";
        TableCell nrtel3 = new TableCell();
        nrtel3.Text = "147852369";
        row3.Cells.Add(imie3);
        row3.Cells.Add(nazwisko3);
        row3.Cells.Add(indeks3);
        row3.Cells.Add(nrtel3);
        myTable.Rows.Add(row3);

        TableRow row4 = new TableRow();
        TableCell imie4 = new TableCell();
        imie4.Text = "Robert";
        TableCell nazwisko4 = new TableCell();
        nazwisko4.Text = "Napruszewski";
        TableCell indeks4 = new TableCell();
        indeks4.Text = "331471";
        TableCell nrtel4 = new TableCell();
        nrtel4.Text = "888111888";
        row4.Cells.Add(imie4);
        row4.Cells.Add(nazwisko4);
        row4.Cells.Add(indeks4);
        row4.Cells.Add(nrtel4);
        myTable.Rows.Add(row4);

       
    }
}
