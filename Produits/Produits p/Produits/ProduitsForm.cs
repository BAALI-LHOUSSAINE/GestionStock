using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Produits
{
    public partial class Form1 : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=LEGACY\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string procedureName = "table_produits";
            sqlcmd = new SqlCommand(procedureName, sqlcon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcon.Open();
            sqldta.SelectCommand = sqlcmd;
            dst.Clear();
            sqldta.Fill(dst, "produits");
            dataGridView1.DataSource = dst.Tables["produits"];
            sqlcon.Close();
            if (dataGridView1.Columns["edit"] != null)
            {
                dataGridView1.Columns["edit"].DisplayIndex = dataGridView1.Columns.Count - 1;
            }
            if (dataGridView1.Columns["supprimer"] != null)
            {
                dataGridView1.Columns["supprimer"].DisplayIndex = dataGridView1.Columns.Count - 1;
            }
            if (dataGridView1.Columns["commander"] != null)
            {
                dataGridView1.Columns["commander"].DisplayIndex = dataGridView1.Columns.Count - 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            String Nproduit = textBox1.Text;
            

            try
            {
                
                
               
                str = $" SELECT p.produit, p.quantite, p.pu, ISNULL(SUM(c.qte), 0) as 'NU_vendues' FROM Gestion_de_stock.dbo.produits p LEFT JOIN Gestion_de_stock.dbo.commandes c ON p.produit = c.produit where p.produit like '{Nproduit}'   GROUP BY p.produit, p.quantite, p.pu ";
                sqlcmd = sqlcon.CreateCommand();
                sqlcmd.CommandText = str;
                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                
                dataGridView1.DataSource = dt;
                sqlcon.Close();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string procedureName = "table_produits";
            sqlcmd = new SqlCommand(procedureName, sqlcon)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcon.Open();
            sqldta.SelectCommand = sqlcmd;
            dst.Clear();
            sqldta.Fill(dst, "produits");
            dataGridView1.DataSource = dst.Tables["produits"];
            sqlcon.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Search...")
            {
                textBox1.Text = ""; // Efface le placeholder
                textBox1.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Search..."; // Remet le placeholder
                textBox1.ForeColor = Color.Gray; // Met le texte en gris
            }
        }
    }
}
