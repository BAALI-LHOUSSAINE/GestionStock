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
        SqlConnection sqlcon = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
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
            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce produit?",
                                 "Confirmation",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string Nproduit = textBox1.Text;
                try
                {
                    str = $@"IF EXISTS (SELECT c.produit FROM commandes c WHERE c.produit = '{Nproduit}') BEGIN Delete from commandes WHERE produit = '{Nproduit}' Delete from produits WHERE produit = '{Nproduit}' if EXISTS (SELECT fr.produit FROM fournisseurs fr WHERE fr.produit = '{Nproduit}') BEGIN Delete from fournisseurs WHERE produit = '{Nproduit}' END END Else begin Delete from produits WHERE produit = '{Nproduit}' End ";
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
            else
            {
                MessageBox.Show("Opération annulée.");
            }
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Produit a supprimer...")
            {
                textBox1.Text = ""; 
                textBox1.ForeColor = Color.Black; 
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Produit a supprimer..."; 
                textBox1.ForeColor = Color.Gray; 
            }
        }
    }
}
