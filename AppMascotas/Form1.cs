using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AppMascotas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            string connstr = "server=localhost; port=3306; user id=root; pwd='Ing.Gomez19#'; database=RESQL; SSL Mode=None;";
            MySqlConnection conn = new MySqlConnection(connstr);
            try
            {
                conn.Open();
                string sql = "SELECT id, nombre, raza, edad FROM mascotas";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<ReportSQL> lrp = new List<ReportSQL>();
                while (rdr.Read())
                {
                    ReportSQL rp = new ReportSQL();
                    rp.id = (int)rdr[0];
                    rp.nombre = rdr[1].ToString();
                    rp.raza = rdr[2].ToString();
                    rp.edad = (int)rdr[3];
                    lrp.Add(rp);
                    rp = null;
                }
                rdr.Close();
                ReportDataSource rds = new ReportDataSource("ReportSQL", lrp);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "AppMascotas.Report1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
                this.reportViewer1.Width = 750;
                this.reportViewer1.Height = 500;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            this.reportViewer1.RefreshReport();
        }
        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
