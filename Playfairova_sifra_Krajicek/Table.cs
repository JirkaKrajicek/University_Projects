using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Playfairova_sifra_Krajicek
{
    public partial class Table : Form
    {
        private char[,] Matice { get; set; }
        private string Klic { get; set; }
        

        private static Table table = null;

        public static Table VratInstanci(char[,] matrix, string klic) //singleton
        {
            if (table == null)
            {
                table = new Table(matrix, klic);
            }
            return table;
        }
        private Table(char[,] matrix, string klic)
        {
            InitializeComponent();

            this.ControlBox = false;
            

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = matrix.GetLength(1);

            Matice = matrix;
            Klic = klic;
                        
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string[] napln = new string[matrix.GetLength(1)];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    napln[j] = Convert.ToString(matrix[i, j]);
                }
                dataGridView1.Rows.Add(napln);
            }

            char[] znakyKlice = klic.ToCharArray();
            
            for (int i = 0; i < znakyKlice.Length; i++)
            {
                int indexRadek = Matrix.IndexZnakuRadek(znakyKlice[i], matrix);
                int indexSloupce = Matrix.IndexZnakuSloupec(znakyKlice[i], matrix);

                dataGridView1.Rows[indexRadek].Cells[indexSloupce].Style.BackColor = Color.Yellow;            
            }           

        }



        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.LightGreen)||(dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.LightPink))
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }                
            }

            string klic = Klic;
            char[,] matrix = Matice;

            char[] znakyKlice = klic.ToCharArray();

            for (int i = 0; i < znakyKlice.Length; i++)
            {
                int indexRadek = Matrix.IndexZnakuRadek(znakyKlice[i], matrix);
                int indexSloupce = Matrix.IndexZnakuSloupec(znakyKlice[i], matrix);

                dataGridView1.Rows[indexRadek].Cells[indexSloupce].Style.BackColor = Color.Yellow;
            }


            try
            {
                DataGridViewSelectedCellCollection cell = dataGridView1.SelectedCells;
                string znak1 = (string)cell[0].Value;
                string znak2 = (string)cell[1].Value;

                char[] poleZnaku = { Convert.ToChar(znak1), Convert.ToChar(znak2) };

                if (checkBox1.Checked)
                {
                    char[] hledaneZnaky = Matrix.ZasifrujRetezec(Matice, poleZnaku);

                    for (int i = 0; i < hledaneZnaky.Length; i++)
                    {
                        int indexRadek = Matrix.IndexZnakuRadek(hledaneZnaky[i], Matice);
                        int indexSloupce = Matrix.IndexZnakuSloupec(hledaneZnaky[i], Matice);

                        dataGridView1.Rows[indexRadek].Cells[indexSloupce].Style.BackColor = Color.LightGreen;
                    }
                }
                else if (checkBox2.Checked)
                {
                    char[] hledaneZnaky = Matrix.RozsifrujRetezec(Matice, poleZnaku);

                    for (int i = 0; i < hledaneZnaky.Length; i++)
                    {
                        int indexRadek = Matrix.IndexZnakuRadek(hledaneZnaky[i], Matice);
                        int indexSloupce = Matrix.IndexZnakuSloupec(hledaneZnaky[i], Matice);

                        dataGridView1.Rows[indexRadek].Cells[indexSloupce].Style.BackColor = Color.LightPink;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Označte právě 2 pole");                
            }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
