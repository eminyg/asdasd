using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PROJECT
{
    public partial class AdminHomepage : Form
    {

        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        int Account = Anmelden.AccountID;

        public static void LoadAccount(OleDbCommand cmd, OleDbDataReader dr, OleDbConnection con, int Account, Label label2, Label label3)      
        {
            try
            {
                cmd.CommandText = "select * from Accounts where A_ID =" + Account;

                if (dr != null)
                {
                    dr.Close();
                }
                cmd.Connection = con;
                dr = cmd.ExecuteReader();

                dr.Read();

                label2.Text = dr.GetString(2);
                label3.Text = dr.GetString(1);

                dr.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Anmeldefehler" + a);
            }
        }


        public AdminHomepage()
        {
            InitializeComponent();
        }

        private void spaltenformatierung()
        {
            dataGridView1.Columns.Remove("F_Nr");
            DataGridViewTextBoxColumn tb0 = new DataGridViewTextBoxColumn();
            tb0.DataPropertyName = "F_Nr";
            tb0.HeaderText = "Flugnummer";
            tb0.DisplayIndex = 0;
            dataGridView1.Columns.Add(tb0);

            dataGridView1.Columns.Remove("F_Abflug");
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();
            tb1.DataPropertyName = "F_Abflug";
            tb1.HeaderText = "Abflug";
            tb1.DisplayIndex = 1;
            dataGridView1.Columns.Add(tb1);

            dataGridView1.Columns.Remove("F_Ankunft");
            DataGridViewTextBoxColumn tb2 = new DataGridViewTextBoxColumn();
            tb2.DataPropertyName = "F_Ankunft";
            tb2.HeaderText = "Ankunft";
            tb2.DisplayIndex = 2;
            dataGridView1.Columns.Add(tb2);

            dataGridView1.Columns.Remove("F_Datum");
            DataGridViewTextBoxColumn tb3 = new DataGridViewTextBoxColumn();
            tb3.DataPropertyName = "F_Datum";
            tb3.HeaderText = "Datum";
            tb3.DisplayIndex = 3;
            dataGridView1.Columns.Add(tb3);
        }

        void nachsteflugedatagridwiew()
        {
            cmd.CommandText = "select F_Nr, F_Abflug, F_Ankunft, F_Datum from Flüge where F_Datum >= DATE()";
            cmd.Connection = con;
            da.SelectCommand = cmd;
            ds.Clear();
            try
            {
                da.Fill(ds, "Accounts");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Accounts";
                spaltenformatierung();
            }
            catch (Exception a)
            {
                MessageBox.Show("Datenbanköffnungsfehler\n" + a);
                this.Close();
            }
            AdminHomepage.LoadAccount(cmd, dr, con, Account, label2, label3);          
        }


        private void AdminHomepage_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = FlughafenDB.accdb";
                con.Open();
            }
            catch (Exception a)
            {
                MessageBox.Show("Datenbanköffnungsfehler\n" + a);
                this.Close();
            }
            nachsteflugedatagridwiew();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Flugzeuge_Verwalten = new Flugzeuge_Verwalten();
            Flugzeuge_Verwalten.Location = this.Location;
            Flugzeuge_Verwalten.StartPosition = FormStartPosition.Manual;
            Flugzeuge_Verwalten.FormClosing += delegate { this.Show(); };
            Flugzeuge_Verwalten.ShowDialog();
            this.Close();
        }

        private void Abmelden_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Anmelden = new Anmelden();
            Anmelden.Location = this.Location;
            Anmelden.StartPosition = FormStartPosition.Manual;
            Anmelden.FormClosing += delegate { this.Show(); };
            Anmelden.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Pilot_Verwaltung = new Pilot_Verwaltung();
            Pilot_Verwaltung.Location = this.Location;
            Pilot_Verwaltung.StartPosition = FormStartPosition.Manual;
            Pilot_Verwaltung.FormClosing += delegate { this.Show(); };
            Pilot_Verwaltung.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Gepäcke_Verwalten = new Gepäcke_Verwalten();
            Gepäcke_Verwalten.Location = this.Location;
            Gepäcke_Verwalten.StartPosition = FormStartPosition.Manual;
            Gepäcke_Verwalten.FormClosing += delegate { this.Show(); };
            Gepäcke_Verwalten.ShowDialog();
            this.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Accounts_Verwalten = new Accounts_Verwalten();
            Accounts_Verwalten.Location = this.Location;
            Accounts_Verwalten.StartPosition = FormStartPosition.Manual;
            Accounts_Verwalten.FormClosing += delegate { this.Show(); };
            Accounts_Verwalten.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Flüge_Verwalten = new Flüge_Verwalten();
            Flüge_Verwalten.Location = this.Location;
            Flüge_Verwalten.StartPosition = FormStartPosition.Manual;
            Flüge_Verwalten.FormClosing += delegate { this.Show(); };
            Flüge_Verwalten.ShowDialog();
            this.Close();            
        }
    }
}
