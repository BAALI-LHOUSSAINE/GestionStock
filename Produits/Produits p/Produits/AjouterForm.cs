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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Produits
{
    public partial class Form2 : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=LEGACY\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String produit = textBox1.Text;
            int qte = int.Parse(textBox3.Text);
            float pu = float.Parse(textBox2.Text);

            try
            {

                str = $"insert into gestion_de_stock.dbo.produits (produit, quantite, pu ) values ('{produit}', '{qte}', '{pu}') ";
                sqlcmd = sqlcon.CreateCommand();
                sqlcmd.CommandText = str;
                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();


                MessageBox.Show("Le produit a été bien ajouté");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Quantité...")
            {
                textBox3.Text = ""; // Efface le placeholder
                textBox3.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Quantité..."; // Remet le placeholder
                textBox3.ForeColor = Color.Gray; // Met le texte en gris
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Produit...")
            {
                textBox1.Text = ""; // Efface le placeholder
                textBox1.ForeColor = Color.Black; // Remet la couleur du texte normal
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Produit..."; // Remet le placeholder
                textBox1.ForeColor = Color.Gray; // Met le texte en gris
            }

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Prix Unitaire...")
            {
                textBox2.Text = ""; // Efface le placeholder
                textBox2.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Prix Unitaire..."; // Remet le placeholder
                textBox2.ForeColor = Color.Gray; // Met le texte en gris
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
