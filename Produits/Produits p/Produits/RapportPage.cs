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
using Produits.Properties;
using System.Windows.Forms.DataVisualization.Charting;

namespace Produits
{
    public partial class RapportPage : Form
    {
        private Chart chart1;
        private Chart chart2;
        private double recettess;
        private double depenses;
        public RapportPage()
        {
            InitializeComponent();
            InitializeChart();
            this.Load += Form1_Load;
            CreateCharts();

        }

        private void CreateCharts()
        {
            
            chart1 = CreateDonutChart("En chiffres (DH)", System.Drawing.Color.SkyBlue, System.Drawing.Color.Orange);
            chart1.Location = new System.Drawing.Point(150, 100);

            // Create second chart
            chart2 = CreateDonutChart("En pourcentage", System.Drawing.Color.LightGreen, System.Drawing.Color.Pink);
            // Set position for second chart (x=500, y=50)
            chart2.Location = new System.Drawing.Point(480, 100);

            // Add charts directly to form
            this.Controls.Add(chart1);
            this.Controls.Add(chart2);

            txtMontantNet = new TextBox();
            txtMontantNet.Location = new Point(520, 340);
            txtMontantNet.Width = 100;
            txtMontantNet.ReadOnly = true;
            this.Controls.Add(txtMontantNet);

            // Calculer et afficher montant net
            double montantNet = recettess - depenses;
            txtMontantNet.Text = montantNet.ToString("F2") + "  DH";

        }


        private Chart CreateDonutChart(string titleText, System.Drawing.Color color1, System.Drawing.Color color2)
        {
            double recettess = 0;
            double depenses = 0;
            Chart chart = new Chart();
            chart.Width = 300;
            chart.Height = 200;

            // Create chart area
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Create series
            Series series = new Series();
            series.ChartType = SeriesChartType.Doughnut;

            // Add data points
            DataPoint dp1 = new DataPoint();
            dp1.YValues = new double[] { depenses };
            dp1.Label = depenses.ToString();
            dp1.Color = color1;

            DataPoint dp2 = new DataPoint();
            dp2.YValues = new double[] { recettess };
            dp2.Label = recettess.ToString();
            dp2.Color = color2;

            series.Points.Add(dp1);
            series.Points.Add(dp2);

            // Add series to chart
            chart.Series.Add(series);
            if (titleText == "En pourcentage")
            {
                series.Label = "#PERCENT{P0}";
                series.LabelFormat = "P0";
            }
            else
            {
                series.Label = "#VAL{F2}";
                series.LabelFormat = "F2";
            }

            // Customize appearance
            series["DoughnutRadius"] = "60";
            //series.Label = "#PERCENT{P0}";
            //series.LabelFormat = "P0";
            series.Font = new System.Drawing.Font("Arial", 10f);

            // Add title
            
            Title title = new Title();
            title.Text = titleText;
            title.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            chart.Titles.Add(title);
            using (SqlConnection conn = new SqlConnection(" Data Source=DESKTOP-VI59G3H\\SQLEXPRESS; Initial Catalog=Gestion_de_stock ; Integrated Security = True"))
            {
                string query = "SELECT SUM(ISNULL(c.qte, 0) * ISNULL(p.pu, 0)) AS Recettes FROM Gestion_de_stock.dbo.produits p LEFT JOIN Gestion_de_stock.dbo.commandes c ON p.produit = c.produit";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //string recettes = reader["Recettes"].ToString();
                                recettess = reader["Recettes"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Recettes"]);
                                this.recettess = reader["Recettes"] == DBNull.Value ? 0 : Convert.ToDouble(reader["Recettes"]);
                                //recettess = double.Parse(recettes);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    conn.Close();
                }
                dp2.YValues = new double[] { recettess };
                dp2.Label = recettess.ToString("F2");
                //DEPENSES
                String query2 = " SELECT SUM(ISNULL(f.qte, 0) * p.pu) AS Depenses FROM Gestion_de_stock.dbo.fournisseurs f JOIN Gestion_de_stock.dbo.produits p ON f.produit = p.produit ";
                using (SqlCommand cmd = new SqlCommand(query2, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader2 = cmd.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                depenses = reader2["Depenses"] == DBNull.Value ? 0 : Convert.ToDouble(reader2["Depenses"]);
                                this.depenses = reader2["Depenses"] == DBNull.Value ? 0 : Convert.ToDouble(reader2["Depenses"]);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    
                }
                dp1.YValues = new double[] { depenses };
                dp1.Label = depenses.ToString("F2");
                conn.Close();

            }

            // Ajouter la légende
            Legend legend = new Legend();
            legend.Alignment = StringAlignment.Center;
            legend.Docking = Docking.Bottom;
            chart.Legends.Add(legend);

            // Associer les items de légende aux points de données
            dp1.LegendText = "Dépenses";
            dp2.LegendText = "Recettes";
            if (titleText == "En pourcentage")
            {
                dp1.Label = "#PERCENT";
                dp2.Label = "#PERCENT";
            }
            else
            {
                dp1.Label = dp1.YValues[0].ToString("F2");
                dp2.Label = dp2.YValues[0].ToString("F2");
            }
            return chart;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }


        private void InitializeChart()
        {
            



        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FournisseurPage fournisseurPage = new FournisseurPage();
            fournisseurPage.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CommandePage commandePage = new CommandePage();
            commandePage.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProduitPage produitPage = new ProduitPage();
            produitPage.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }
    }
}
