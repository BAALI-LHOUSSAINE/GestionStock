using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Produits.Properties;

namespace Produits
{
    public partial class CommandePage : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public CommandePage()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string procedureName = "table_commandes";
            sqlcmd = new SqlCommand(procedureName, sqlcon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcon.Open();
            sqldta.SelectCommand = sqlcmd;
            dst.Clear();
            sqldta.Fill(dst, "commandes");
            dataGridView1.DataSource = dst.Tables["commandes"];
            sqlcon.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProduitPage produitPage = new ProduitPage();
            produitPage.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FournisseurPage fournisseurPage = new FournisseurPage();
            fournisseurPage.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RapportPage rapportPage = new RapportPage();
            rapportPage.Show();
            this.Close();
        }
    }
}
