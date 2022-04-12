using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace quiz_3
{
    public partial class Form1 : Form
    {
        public List<Grocery> groceries { get; set; }
        public List<Summary> filteredList { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initListView();
            initUI();

            listView1.View = View.Details;
            listView1.Columns.Add("Product Name");
            listView1.Columns.Add("Unit");
            listView1.Columns.Add("Price");

            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.Columns[i].Width = -2;
            }
        }

        private void initUI()
        {
            labelReportSuccessful.Visible = false;
        }

        private void initListView()
        {
            listView1.View = View.Details;
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnAllReadData_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\PC\source\repos\quiz-3\quiz-3\bin\Debug\grocery.txt";

            if (!File.Exists(path))
            {
                string createText = "Hello and Welcome" + Environment.NewLine;
                File.WriteAllText(path, createText, Encoding.UTF8);
            }

            try
            {
                using (StreamReader sr = new StreamReader("grocery.txt"))
                {
                    string line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = sr.ReadLine();

                        Grocery grocery = new Grocery(line);
                        groceries.Add(grocery);
                    }
                }

                fillListView();
            }
            catch (IOException ex)
            {
                MessageBox.Show("File read error\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message);
            }
        }

        private void fillListView()
        {
            listView1.Items.Clear();

            var node = listView1.Items.Add("Product Name");
            string[] items = groceries.Select(x => x.productName).Distinct().ToArray();
            Array.Sort(items);
            foreach (var item in items)
            {
                node.SubItems.Add(item).Tag = "productName";
            }

            node = listView1.Items.Add("Unit");

            items = groceries.Select(x => x.unit).Distinct().ToArray();
            Array.Sort(items);
            foreach (var item in items)
            {
                node.SubItems.Add(item).Tag = "unit";
            }

            node = listView1.Items.Add("Price");

            items = groceries.Select(x => x.price.ToString()).Distinct().ToArray();
            Array.Sort(items);
            foreach (var item in items)
            {
                node.SubItems.Add(item).Tag = "price";
            }

            filteredList = new List<Summary>();
            
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string[] productNames = filteredList.Select(x => x.ProductName).Distinct().ToArray();
                Array.Sort(productNames);

                using (StreamWriter sw = new StreamWriter("report.txt"))
                {
                    foreach (var productName in productNames)
                    {
                        var min = filteredList.FindAll(x => x.ProductName == productName).Min(y => y.minPrice);
                        var max = filteredList.FindAll(x => x.ProductName == productName).Max(y => y.maxPrice);

                        sw.WriteLine(productName + "," + min + "," + max );
                    }
                }
                labelReportSuccessful.Visible = true;
            }
            catch (IOException ex)
            {
                labelReportSuccessful.Visible = false;
                MessageBox.Show("File write error\n" + ex.Message);
            }
            catch (Exception ex)
            {
                labelReportSuccessful.Visible = false;
                MessageBox.Show("Error\n" + ex.Message);
            }
        }
    }
}
