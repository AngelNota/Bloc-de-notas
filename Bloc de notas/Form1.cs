using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloc_de_notas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Archivos de texto|*txt";
            DialogResult res = od.ShowDialog();

            if (res == DialogResult.OK)
                textBox1.Text = File.ReadAllText(od.FileName);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();

            sd.Filter = "Archivos de texto|*.txt";
            DialogResult res = sd.ShowDialog();

            if (res == DialogResult.OK)
                File.WriteAllText(sd.FileName, textBox1.Text);
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();

            sd.Filter = "Archivos de texto|*.txt";
            DialogResult res = sd.ShowDialog();

            if (res == DialogResult.OK)
                File.WriteAllText(sd.FileName, textBox1.Text);
        }
    }
}
