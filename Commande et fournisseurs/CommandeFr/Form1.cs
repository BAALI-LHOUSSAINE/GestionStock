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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CommandeFr
{
    public partial class Form1 : Form
    {
        
      
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            textPassword.PasswordChar = '*';
        }
        private void connexion_Click(object sender, EventArgs e)
        {
            string enteredUsername = textUsername.Text;
            string enteredPassword = textPassword.Text;
            using (SqlConnection conn = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True"))
            {
                string query = "SELECT email, mdp FROM Gestion_de_stock.dbo.moderateur WHERE email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", enteredUsername);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader["mdp"].ToString();

                                if (enteredPassword == dbPassword)
                                {
                                    Form2 form2 = new Form2();                                   
                                    form2.Show();                                   
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid password");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("User not found");
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }
  

        private void annuler_Click(object sender, EventArgs e)
        {
            textUsername.Text = "";
            textPassword.Text = "";
        }

        private void textUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
