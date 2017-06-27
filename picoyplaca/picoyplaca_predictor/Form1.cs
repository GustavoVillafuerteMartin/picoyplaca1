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

namespace picoyplaca_predictor
{
    public partial class Form1 : Form
    {
        private int[] array_predictor = { 5, 1, 1, 2, 2, 3, 3, 4, 4, 5 };
        private string[] weekDays2 = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        private int dayofweek = 0;
        private int ndigit_placa = 0;
        private int leng_placa = 0;
        private string cdigit_placa = string.Empty;
        private string string_date = DateTime.Now.ToShortDateString().Trim();
        private DateTime date = DateTime.Now;
        private bool autoriza_circulacion = false;
        private int modo = 0;
        private bool valid_placa = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = string_date;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (modo == 0)
            {
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    MessageBox.Show("Fecha NO puede estar en blanco...", "Fecha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();
                }
                else
                {
                    if (string.IsNullOrEmpty(textBox2.Text.Trim()))
                    {
                        MessageBox.Show("Placa NO puede estar en blanco...", "Fecha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox2.Focus();
                    }
                    else
                    {
                        try
                        {
                            date = DateTime.Parse(textBox1.Text.Trim());
                            dayofweek = (int)date.DayOfWeek;
                            autoriza_circulacion = predictor(dayofweek, textBox2.Text.Trim());
                            if (valid_placa)
                            {
                                modo = 1;
                                label6.Text = weekDays2[dayofweek];
                                label6.Visible = true;
                                textBox1.Enabled = false;
                                textBox2.Enabled = false;
                                label7.Text = string.Format("Dia de parada : {0}", weekDays2[array_predictor[ndigit_placa]]);
                                button1.Text = "Limpiar";
                                if (autoriza_circulacion)
                                {
                                    label4.Text = "!! SI !! PUEDE CIRCULAR EN LA FECHA SELECCIONADA...";
                                }
                                else
                                {
                                    label4.Text = "!! NO !! PUEDE CIRCULAR EN LA FECHA SELECCIONADA...";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ultimo digito de la placa debe ser un valor numerico...", "Placa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox2.Focus();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Fecha Invalida...", "Fecha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Focus();
                        }
                    }
                }
            }
            else
            {
                modo = 0;
                button1.Text = "Consultar";
                valid_placa = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                label7.Text = string.Empty;
                label4.Text = "INTRODUSCA FECHA Y PLACA";
                label6.Text = string.Empty;
                label6.Visible = false;
                textBox1.Text = string_date;
                textBox2.Text = string.Empty;
                textBox1.Focus();
            }
        }

        public bool predictor(int ndayofweek,string splaca)
        {
            bool breturn = true;
            leng_placa = splaca.Trim().Length;
            cdigit_placa = splaca.Trim().Substring(leng_placa - 1, 1);
            if (cdigit_placa == "0" | cdigit_placa == "1" | cdigit_placa == "2" | cdigit_placa == "3" | cdigit_placa == "4" | cdigit_placa == "5" | cdigit_placa == "6" | cdigit_placa == "7" | cdigit_placa == "8" | cdigit_placa == "9")
            {
                valid_placa = true;
                ndigit_placa = int.Parse(cdigit_placa);
                if (array_predictor[ndigit_placa].ToString() == ndayofweek.ToString())
                {
                    breturn = false;
                }
            }
            else
            {
                valid_placa = false;
            }
            return breturn;
        }
    }
}
