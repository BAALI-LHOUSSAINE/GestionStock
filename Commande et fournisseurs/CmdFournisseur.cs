using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandeFr
{
    public partial class CmdFournisseur : Form
    {
        SqlConnection sqlcon = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True");
        SqlCommand sqlcmd;
        SqlDataAdapter sqldta = new SqlDataAdapter();
        DataSet dst = new DataSet();
        String str;
        String str2;
        public CmdFournisseur()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String produit = textBox3.Text;
            String fournisseur  = textBox1.Text;
            int qte = int.Parse(textBox2.Text);
            float pu = float.Parse(textBox4.Text);
            DateTime today = DateTime.Now;

            try
            {

                str = $"IF EXISTS (SELECT 1 FROM produits WHERE produit = {produit}) begin update Gestion_de_stock.dbo.produits.quantite set Gestion_de_stock.dbo.produits.quantite =Gestion_de_stock.dbo.produits.quantite + {qte} end else begin insert into Gestion_de_stock.dbo.produits (produit, quantite, pu ) values ('{produit}', {qte}, {pu}) insert into gestion_de_stock.dbo.fournisseurs (nom_fourn, produit, qte, date ) values ('{fournisseur}', '{produit}', {qte}, '{today}') end ";
                str2 = $@"
        IF EXISTS (SELECT 1 FROM produits WHERE produit = '{produit}')
        BEGIN
            UPDATE Gestion_de_stock.dbo.produits 
            SET quantite = quantite + {qte} 
            WHERE produit = '{produit}'
            INSERT INTO Gestion_de_stock.dbo.fournisseurs (nom_fourn, produit, qte, date) 
            VALUES ('{fournisseur}', '{produit}', {qte}, '{today}')
        END
        ELSE
        BEGIN
            INSERT INTO Gestion_de_stock.dbo.produits (produit, quantite, pu) 
            VALUES ('{produit}', {qte}, {pu});

            INSERT INTO Gestion_de_stock.dbo.fournisseurs (nom_fourn, produit, qte, date) 
            VALUES ('{fournisseur}', '{produit}', {qte}, '{today}')
        END";
                sqlcmd = sqlcon.CreateCommand();
                sqlcmd.CommandText = str2;
                sqlcon.Open();
                sqlcmd.ExecuteNonQuery();
                sqlcon.Close();


                MessageBox.Show("Le commande a été bien ajouté");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
