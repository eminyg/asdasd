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
    public partial class Accounts_Verwalten : Form
    {
        public static int PassagierVerwaltungID
        {
            get; set;
        }
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
        OleDbCommandBuilder odcb = null;
        int Account = Anmelden.AccountID;

        private void spaltenformatierung()
        {
            dataGridView1.Columns.Remove("A_ID");
            DataGridViewTextBoxColumn tb0 = new DataGridViewTextBoxColumn();
            tb0.DataPropertyName = "A_ID";
            tb0.HeaderText = "ID";
            tb0.DisplayIndex = 0;
            dataGridView1.Columns.Add(tb0);

            dataGridView1.Columns.Remove("A_Nachname");
            DataGridViewTextBoxColumn tb1 = new DataGridViewTextBoxColumn();
            tb1.DataPropertyName = "A_Nachname";
            tb1.HeaderText = "Nachname";
            tb1.DisplayIndex = 1;
            dataGridView1.Columns.Add(tb1);

            dataGridView1.Columns.Remove("A_Vorname");
            DataGridViewTextBoxColumn tb2 = new DataGridViewTextBoxColumn();
            tb2.DataPropertyName = "A_Vorname";
            tb2.HeaderText = "Vorname";
            tb2.DisplayIndex = 2;
            dataGridView1.Columns.Add(tb2);

            dataGridView1.Columns.Remove("A_Strasse");
            DataGridViewTextBoxColumn tb3 = new DataGridViewTextBoxColumn();
            tb3.DataPropertyName = "A_Strasse";
            tb3.HeaderText = "Straße";
            tb3.DisplayIndex = 3;
            dataGridView1.Columns.Add(tb3);

            dataGridView1.Columns.Remove("A_PLZ");
            DataGridViewTextBoxColumn tb4 = new DataGridViewTextBoxColumn();
            tb4.DataPropertyName = "A_PLZ";
            tb4.HeaderText = "PLZ";
            tb4.DisplayIndex = 4;
            dataGridView1.Columns.Add(tb4);

            dataGridView1.Columns.Remove("A_Ort");
            DataGridViewTextBoxColumn tb5 = new DataGridViewTextBoxColumn();
            tb5.DataPropertyName = "A_Ort";
            tb5.HeaderText = "Ort";
            tb5.DisplayIndex = 5;
            dataGridView1.Columns.Add(tb5);

            dataGridView1.Columns.Remove("A_Email");
            DataGridViewTextBoxColumn tb6 = new DataGridViewTextBoxColumn();
            tb6.DataPropertyName = "A_Email";
            tb6.HeaderText = "E-Mail";
            tb6.DisplayIndex = 6;
            dataGridView1.Columns.Add(tb6);

            dataGridView1.Columns.Remove("A_Passwort");
            DataGridViewTextBoxColumn tb7 = new DataGridViewTextBoxColumn();
            tb7.DataPropertyName = "A_Passwort";
            tb7.HeaderText = "Passwort";
            tb7.DisplayIndex = 7;
            dataGridView1.Columns.Add(tb7);

            dataGridView1.Columns.Remove("A_Admin");
            DataGridViewTextBoxColumn tb8 = new DataGridViewTextBoxColumn();
            tb8.DataPropertyName = "A_Admin";
            tb8.HeaderText = "Admin";
            tb8.DisplayIndex = 8;
            dataGridView1.Columns.Add(tb8);
        }

        void datagridwiew()
        {
            cmd.CommandText = "select * from Accounts";
            cmd.Connection = con;
            da.SelectCommand = cmd;
            ds.Clear();
            try
            {
                da.Fill(ds, "Accounts");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Accounts";
                spaltenformatierung();
                this.dataGridView1.Columns[7].Visible = false;
            }
            catch (Exception a)
            {
                MessageBox.Show("Datenbankfehler:\n" + a);
                this.Close();
            }
        }


        public Accounts_Verwalten()
        {
            InitializeComponent();
        }

        private void Accounts_Verwalten_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Passagiere_Verwaltung = new Passagiere_Verwaltung();
            Passagiere_Verwaltung.Location = this.Location;
            Passagiere_Verwaltung.StartPosition = FormStartPosition.Manual;
            Passagiere_Verwaltung.FormClosing += delegate { this.Show(); };
            Passagiere_Verwaltung.ShowDialog();
            this.Close();
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string Zugriffsindex;
                DataView dv = new DataView(ds.Tables["Accounts"]);
                DataRowView drv = dv[dataGridView1.CurrentRow.Index];
                Zugriffsindex = drv["A_ID"].ToString();
                PassagierVerwaltungID = Convert.ToInt32(Zugriffsindex);                

                button2.Enabled = true;
            }
            catch (Exception a)
            {
                MessageBox.Show("Tabellen-Zugriffsfehler(Direktes Suchen)" + a);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && checkBox1.Checked == true)
            {
                OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Accounts where A_ID like '%{this.textBox3.Text}%' AND A_Nachname like '%{this.textBox1.Text}%' AND A_Vorname like '%{this.textBox2.Text}%' AND A_Strasse like '%{this.textBox5.Text}%' AND A_PLZ like '%{this.textBox6.Text}%' AND A_Ort like '%{this.textBox7.Text}%' AND A_Email like '%{this.textBox4.Text}%' AND A_Admin = true", con);
                try
                {
                    ds.Clear();
                    anzeige.Fill(ds, "Accounts");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Accounts";
                    this.dataGridView1.Columns[7].Visible = true;
                }
                catch
                {
                    MessageBox.Show("Filternfehler");
                }
            }
            else if (checkBox2.Checked == true && checkBox1.Checked == false)
            {
                OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Accounts where A_ID like '%{this.textBox3.Text}%' AND A_Nachname like '%{this.textBox1.Text}%' AND A_Vorname like '%{this.textBox2.Text}%' AND A_Strasse like '%{this.textBox5.Text}%' AND A_PLZ like '%{this.textBox6.Text}%' AND A_Ort like '%{this.textBox7.Text}%' AND A_Email like '%{this.textBox4.Text}%'", con);
                try
                {
                    ds.Clear();
                    anzeige.Fill(ds, "Accounts");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Accounts";
                    this.dataGridView1.Columns[7].Visible = true;
                }
                catch
                {
                    MessageBox.Show("Filternfehler");
                }
            }
            else if (checkBox2.Checked == false && checkBox1.Checked == true)
            {
                OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Accounts where A_ID like '%{this.textBox3.Text}%' AND A_Nachname like '%{this.textBox1.Text}%' AND A_Vorname like '%{this.textBox2.Text}%' AND A_Strasse like '%{this.textBox5.Text}%' AND A_PLZ like '%{this.textBox6.Text}%' AND A_Ort like '%{this.textBox7.Text}%' AND A_Email like '%{this.textBox4.Text}%' AND A_Admin = true", con);
                try
                {
                    ds.Clear();
                    anzeige.Fill(ds, "Accounts");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Accounts";
                    this.dataGridView1.Columns[7].Visible = false;
                }
                catch
                {
                    MessageBox.Show("Filternfehler");
                }
            }
            else if (checkBox2.Checked == false && checkBox1.Checked == false)
            {
                OleDbDataAdapter anzeige = new OleDbDataAdapter($"select * from Accounts where A_ID like '%{this.textBox3.Text}%' AND A_Nachname like '%{this.textBox1.Text}%' AND A_Vorname like '%{this.textBox2.Text}%' AND A_Strasse like '%{this.textBox5.Text}%' AND A_PLZ like '%{this.textBox6.Text}%' AND A_Ort like '%{this.textBox7.Text}%' AND A_Email like '%{this.textBox4.Text}%'", con);
                try
                {
                    ds.Clear();
                    anzeige.Fill(ds, "Accounts");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Accounts";
                    this.dataGridView1.Columns[7].Visible = false;
                }
                catch
                {
                    MessageBox.Show("Filternfehler");
                }
            }
        }
    }
}
