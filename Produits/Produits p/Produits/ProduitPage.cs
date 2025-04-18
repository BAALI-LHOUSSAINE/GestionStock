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
    public partial class ProduitPage : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public ProduitPage()
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
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True"))
            {
                string query = @"SELECT produit, quantite 
                        FROM Gestion_de_stock.dbo.produits 
                        WHERE quantite <= @minQuantity";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@minQuantity", 5);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            string lowStockProducts = "";
                            while (reader.Read())
                            {
                                string produit = reader["produit"].ToString();
                                int quantite = Convert.ToInt32(reader["quantite"]);
                                lowStockProducts += $"{produit} (Quantité: {quantite})\n";
                            }

                            if (!string.IsNullOrEmpty(lowStockProducts))
                            {
                                MessageBox.Show($"Attention! Les produits suivants sont en rupture de stock:\n\n{lowStockProducts}",
                                              "Alerte de Stock",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RapportPage rapportPage = new RapportPage();
            rapportPage.Show();
            this.Close();
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
            AjouterProduit form2 = new AjouterProduit();
            form2.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
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
            EditerProduit form3 = new EditerProduit();
            form3.ShowDialog();
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
            FournisseurPage fournisseurPage = new FournisseurPage();
            fournisseurPage.Show();
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CommandePage commandePage = new CommandePage();
            commandePage.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FournisseurPage fournisseurPage = new FournisseurPage();
            fournisseurPage.Show();
            this.Close();
        }
    }
}
