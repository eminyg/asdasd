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

    public partial class Passagiere_Verwaltung : Form
    {        
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommandBuilder odcb = null;
        int Account = Anmelden.AccountID;
        int Zugriffsindex = Accounts_Verwalten.PassagierVerwaltungID;

        void accountdaten()
        {
            try
            {
                cmd.CommandText = "select * from Accounts where A_ID =" + Zugriffsindex;
                cmd.Connection = con;
                if (dr != null)
                {
                    dr.Close();
                }
                dr = cmd.ExecuteReader();
                dr.Read();
                label18.Text = dr.GetInt32(0).ToString();
                label19.Text = dr.GetString(2);
                label20.Text = dr.GetString(1);
                label21.Text = dr.GetString(6);
                label22.Text = dr.GetString(3);
                label23.Text = dr.GetString(4);
                label24.Text = dr.GetString(5);
                if (dr.GetBoolean(8) == true)
                {
                    checkBox1.Checked = true;   
                }
                checkBox1.AutoCheck = false;
                dr.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Accountdaten konnte nicht gerufen werden. " + a);
            }

        }
        private void spaltenformatierung()
        {
            dataGridView1.Columns.Remove("P_ID");
            DataGridViewTextBoxColumn tb0 = new DataGridViewTextBoxColumn();
            tb0.DataPropertyName = "P_ID";
            tb0.HeaderText = "ID";
            tb0.DisplayIndex = 0;
            dataGridView1.Columns.Add(tb0);

            dataGridView1.Columns.Remove("P_Vorname");
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();
            tb1.DataPropertyName = "P_Vorname";
            tb1.HeaderText = "Vorname";
            tb1.DisplayIndex = 1;
            dataGridView1.Columns.Add(tb1);

            dataGridView1.Columns.Remove("P_Nachname");
            DataGridViewTextBoxColumn tb2 = new DataGridViewTextBoxColumn();
            tb2.DataPropertyName = "P_Nachname";
            tb2.HeaderText = "Nachname";
            tb2.DisplayIndex = 2;
            dataGridView1.Columns.Add(tb2);

            dataGridView1.Columns.Remove("A_ID");
            DataGridViewTextBoxColumn tb3 = new DataGridViewTextBoxColumn();
            tb3.DataPropertyName = "A_ID";
            tb3.HeaderText = "Hier '"+ Zugriffsindex+"' geben";
            tb3.DisplayIndex = 3;
            dataGridView1.Columns.Add(tb3);
        }

        void datagridwiew()
        {
            cmd.CommandText = $"select * from Passagiere where A_ID = {Zugriffsindex};";
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

        public Passagiere_Verwaltung()
        {
            InitializeComponent();
        }

        private void Passagiere_Verwaltung_Load(object sender, EventArgs e)
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
            accountdaten();
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
            var Accounts_Verwalten = new Accounts_Verwalten();
            Accounts_Verwalten.Location = this.Location;
            Accounts_Verwalten.StartPosition = FormStartPosition.Manual;
            Accounts_Verwalten.FormClosing += delegate { this.Show(); };
            Accounts_Verwalten.ShowDialog();
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
            OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Passagiere where P_ID like '%{this.textBox2.Text}%' AND P_Vorname like '%{this.textBox3.Text}%' AND P_Nachname like '%{this.textBox1.Text}%' AND A_ID = {Zugriffsindex}", con);
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
