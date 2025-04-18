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

namespace Produits
{
    public partial class EditerProduit : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public EditerProduit()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Nproduit = textBox1.Text;
            int qte = int.Parse(textBox3.Text);
            float prixu = float.Parse(textBox2.Text);

            try
            {
                str = $"UPDATE gestion_de_stock.dbo.produits SET quantite = {qte}, pu = {prixu} where gestion_de_stock.dbo.produits.produit = '{Nproduit}'";
                sqlcmd = sqlcon.CreateCommand();
                sqlcmd.CommandText = str;
                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();


                MessageBox.Show("Les donnees de produits sont editées avec succes");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Produit a modifier...")
            {
                textBox1.Text = ""; // Efface le placeholder
                textBox1.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Produit a modifier..."; // Remet le placeholder
                textBox1.ForeColor = Color.Gray; // Met le texte en gris
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Nouvelle quantité...")
            {
                textBox3.Text = ""; // Efface le placeholder
                textBox3.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Nouvelle quantité..."; // Remet le placeholder
                textBox3.ForeColor = Color.Gray; // Met le texte en gris
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == " Nouveau prix unitaire...")
            {
                textBox2.Text = ""; // Efface le placeholder
                textBox2.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Nouveau prix unitaire..."; // Remet le placeholder
                textBox2.ForeColor = Color.Gray; // Met le texte en gris
            }
        }
    }
}
