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
    public partial class Gepäcke_Verwalten : Form
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
            dataGridView1.Columns.Remove("G_ID");
            DataGridViewTextBoxColumn tb0 = new DataGridViewTextBoxColumn();
            tb0.DataPropertyName = "G_ID";
            tb0.HeaderText = "ID";
            tb0.DisplayIndex = 0;
            dataGridView1.Columns.Add(tb0);

            dataGridView1.Columns.Remove("G_Art");
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();
            tb1.DataPropertyName = "G_Art";
            tb1.HeaderText = "Gepäck";
            tb1.DisplayIndex = 1;
            dataGridView1.Columns.Add(tb1);
        }

        void datagridwiew()
        {
            cmd.CommandText = "select * from Gepackarten";
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

        public Gepäcke_Verwalten()
        {
            InitializeComponent();
        }

        private void Gepäcke_Verwalten_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Anmelden = new Anmelden();
            Anmelden.Location = this.Location;
            Anmelden.StartPosition = FormStartPosition.Manual;
            Anmelden.FormClosing += delegate { this.Show(); };
            Anmelden.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Gepackarten where G_ID like '%{this.textBox3.Text}%' AND G_Art like '%{this.textBox2.Text}%'", con);
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
