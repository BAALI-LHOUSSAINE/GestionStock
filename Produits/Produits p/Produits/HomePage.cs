using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Produits.Properties;
namespace Produits
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BringToFront();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FournisseurPage fournisseurPage = new FournisseurPage();
            fournisseurPage.Show();
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
