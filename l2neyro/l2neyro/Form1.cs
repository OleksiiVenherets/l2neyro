using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace l2neyro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] inputIris = new double[4];

            inputIris[0] = Convert.ToDouble(textBox1.Text);
            inputIris[1] = Convert.ToDouble(textBox2.Text);
            inputIris[2] = Convert.ToDouble(textBox3.Text);
            inputIris[3] = Convert.ToDouble(textBox4.Text);
            double sigma = Convert.ToDouble(textBox5.Text);

            var calculation = new Calculation();
            var irisList = ReadFromFile();
            
            var probabilities = calculation.GetProbability(irisList, inputIris, sigma);
            int m = Maxindex(probabilities);

            textBox6.Text = Convert.ToString(probabilities[0]);
            textBox7.Text = Convert.ToString(probabilities[1]);
            textBox8.Text = Convert.ToString(probabilities[2]);
            switch (m)
            {
                case 0:
                    textBox9.Text = "Setosa";
                    break;
                case 1:
                    textBox9.Text = "Versicolor";
                    break;
                case 2:
                    textBox9.Text = "Virginica";
                    break;
            }
        }
        
        private List<Iris> ReadFromFile()
        {
            var irisList = new List<Iris>();

            var fs = new StreamReader(@"IrisTraining.txt");
            string s = "";
            while (true)
            {
                s = fs.ReadLine();
                if (s == null)
                    break;
                char[] d = new char[2];
                d[0] = ' ';
                d[1] = '\t';
                string[] substrings = s.Split(d);
                var iris = new Iris {
                    SepalLength = Convert.ToDouble(substrings[0]),
                    SepalWidth = Convert.ToDouble(substrings[1]),
                    PetalLength = Convert.ToDouble(substrings[2]),
                    PetalWidth = Convert.ToDouble(substrings[3]),
                    Name = substrings[4]
                };
                irisList.Add(iris);
            }

            foreach (var iris in irisList)
                dataGridView1.Rows.Add(iris.SepalLength, iris.SepalWidth, iris.PetalLength, iris.PetalWidth, iris.Name);


            return irisList;
        }


        private int Maxindex (double [] arr)
        {
            int index = 0;
            double max = arr[0];
            for (var i = 1; i < 3; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
