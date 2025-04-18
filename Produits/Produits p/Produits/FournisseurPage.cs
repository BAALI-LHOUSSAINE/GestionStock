using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Produits.Properties;

namespace Produits
{
    public partial class FournisseurPage : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public FournisseurPage()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string procedureName = "table_fournisseurs";
            sqlcmd = new SqlCommand(procedureName, sqlcon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcon.Open();
            sqldta.SelectCommand = sqlcmd;
            dst.Clear();
            sqldta.Fill(dst, "fournisseurs");
            dataGridView1.DataSource = dst.Tables["fournisseurs"];
            dataGridView1.ClearSelection();
            sqlcon.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            CmdFournisseur form4 = new CmdFournisseur();
            form4.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string procedureName = "table_fournisseurs";
            sqlcmd = new SqlCommand(procedureName, sqlcon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcon.Open();
            sqldta.SelectCommand = sqlcmd;
            dst.Clear();
            sqldta.Fill(dst, "fournisseurs");
            dataGridView1.DataSource = dst.Tables["fournisseurs"];
            dataGridView1.ClearSelection();
            sqlcon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProduitPage produitPage = new ProduitPage();
            produitPage.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CommandePage commandePage = new CommandePage();
            commandePage.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RapportPage rapportPage = new RapportPage();
            rapportPage.Show();
            this.Close();
        }
    }
}
