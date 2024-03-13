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
    public partial class Flugzeuge_Verwalten : Form
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommandBuilder odcb = null;
        int Account = Anmelden.AccountID;

        private void spaltenformatierung()
        {
            dataGridView1.Columns.Remove("FZ_ID");
            DataGridViewTextBoxColumn tb0 = new DataGridViewTextBoxColumn();
            tb0.DataPropertyName = "FZ_ID";
            tb0.HeaderText = "ID";
            tb0.DisplayIndex = 0;
            dataGridView1.Columns.Add(tb0);

            dataGridView1.Columns.Remove("FZ_Art");
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();
            tb1.DataPropertyName = "FZ_Art";
            tb1.HeaderText = "Flugzeug";
            tb1.DisplayIndex = 1;
            dataGridView1.Columns.Add(tb1);

            dataGridView1.Columns.Remove("FZ_Maxgewicht");
            DataGridViewTextBoxColumn tb2 = new DataGridViewTextBoxColumn();
            tb2.DataPropertyName = "FZ_Maxgewicht";
            tb2.HeaderText = "Gewicht Kapazität (KG)";
            tb2.DisplayIndex = 2;
            dataGridView1.Columns.Add(tb2);

            dataGridView1.Columns.Remove("FZ_Maxpassagier");
            DataGridViewTextBoxColumn tb3 = new DataGridViewTextBoxColumn();
            tb3.DataPropertyName = "FZ_Maxpassagier";
            tb3.HeaderText = "Passagier Kapazität";
            tb3.DisplayIndex = 3;
            dataGridView1.Columns.Add(tb3);
        }

        void datagridwiew()
        {
            cmd.CommandText = "select * from Flugzeuge";
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
                MessageBox.Show("Datenbankfehler:\n" + a);
                this.Close();
            }
        }

        public Flugzeuge_Verwalten()
        {
            InitializeComponent();
        }

        private void Flugzeuge_Verwalten_Load(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0 ; Data Source = FlughafenDB.accdb;";
                con.Open();               
            }
            catch (Exception a)
            {
                MessageBox.Show("Updatefehler:\n" + a);
                this.Close();
            }
            AdminHomepage.LoadAccount(cmd, dr, con, Account, label2, label3);            
            datagridwiew();
            dataGridView1.Columns[0].ReadOnly = true;
            odcb = new OleDbCommandBuilder(da);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.Update(ds, "Accounts");
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var AdminHomepage = new AdminHomepage();
            AdminHomepage.Location = this.Location;
            AdminHomepage.StartPosition = FormStartPosition.Manual;
            AdminHomepage.FormClosing += delegate { this.Show(); };
            AdminHomepage.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Flugzeuge where FZ_ID like '%{this.textBox3.Text}%' AND FZ_Art like '%{this.textBox2.Text}%' AND FZ_Maxgewicht like '%{this.textBox4.Text}%' AND FZ_Maxpassagier like '%{this.textBox1.Text}%'", con);
            try
            {
                ds.Clear();
                anzeige.Fill(ds, "Accounts");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Accounts";
            }
            catch
            {
                MessageBox.Show("Filternfehler");
            }

        }
    }
}
