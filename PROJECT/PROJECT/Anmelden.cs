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
    public partial class Anmelden : Form 
    {
        public static int AccountID
        {
            get; set;
        }
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr = null;
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter();
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
        }

        public Anmelden()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox1.Text = "";
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


        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dr != null) 
                { 
                    dr.Close(); 
                }
                string email = textBox3.Text;
                string password = textBox1.Text;
                cmd.CommandText = $"select A_ID from Accounts where A_Email = '{email}' AND A_Passwort = '{password}'";
                cmd.Connection = con;
                dr = cmd.ExecuteReader();
                dr.Read();
                int Account = dr.GetInt32(0);
                AccountID = Account;

                try
                {
                    cmd.CommandText = "select * from Accounts where A_ID =" + Account;
                    cmd.Connection = con;
                    if (dr != null)
                    {
                        dr.Close();
                    }
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.GetBoolean(8) == true)
                    {
                        this.Hide();
                        var AdminHomepage = new AdminHomepage();
                        AdminHomepage.Location = this.Location;
                        AdminHomepage.StartPosition = FormStartPosition.Manual;
                        AdminHomepage.FormClosing += delegate { this.Show(); };
                        AdminHomepage.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        var Homepage = new Homepage();
                        Homepage.Location = this.Location;
                        Homepage.StartPosition = FormStartPosition.Manual;
                        Homepage.FormClosing += delegate { this.Show(); };
                        Homepage.ShowDialog();
                        this.Close();
                    }
                    dr.Close();
                }
                catch (Exception asd)
                {
                    MessageBox.Show("Anmeldefehler" + asd);
                }
            }
            catch
            {
                MessageBox.Show("Angegebene Daten sind falsch");
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var kontoerstellen = new kontoerstellen();
            kontoerstellen.Location = this.Location;
            kontoerstellen.StartPosition = FormStartPosition.Manual;
            kontoerstellen.FormClosing += delegate { this.Show(); };
            kontoerstellen.ShowDialog();
            this.Close();          
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DarkSlateBlue;
        }
    }
}
