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
    public partial class Form4 : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=LEGACY\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Nproduit = textBox1.Text;

            try
            {
                str = $"IF EXISTS (SELECT c.produit FROM commandes c WHERE c.produit = '{Nproduit}') BEGIN Delete from commandes WHERE produit = '{Nproduit}' Delete from produits WHERE produit = '{Nproduit}' END";
                sqlcmd = sqlcon.CreateCommand();
                sqlcmd.CommandText = str;
                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();


                MessageBox.Show("Le produit a été supprimé avec succes");
                textBox1.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Produit a supprimer...")
            {
                textBox1.Text = ""; // Efface le placeholder
                textBox1.ForeColor = Color.Black; // Remet la couleur du texte normal
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Produit a supprimer..."; // Remet le placeholder
                textBox1.ForeColor = Color.Gray; // Met le texte en gris
            }
        }
    }
}
