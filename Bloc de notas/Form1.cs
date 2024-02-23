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
        //Objeto del formulario 2 (buscador)
        Form buscador = new Buscador();
        //Variable bandera que detecta si el archivo ha sido guardado o no
        private bool archivoGuardado = false;
        //Variable que guarda el nombre del archivo
        private string nombreDeArchivo = "";
        //Variable para saber si se han guardado los cambios o no
        private bool cambiosNoGuardados = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cambiosNoGuardados)
            {
                DialogResult resultado = MessageBox.Show("¿Deseas guardar los cambios antes de abrir otro archivo?", "Guardar cambios", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    // Guarda los cambios antes de abrir otro archivo
                    if (!GuardarArchivo())
                    {
                        // Si el usuario cancela el diálogo de guardar, no continúes abriendo otro archivo
                        return;
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    // Si el usuario cancela, no continúes abriendo otro archivo
                    return;
                }
                // Si el resultado es 'No', simplemente continúa abriendo otro archivo
            }

            // Restablece la bandera y abre el nuevo archivo
            cambiosNoGuardados = false;

            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Archivos de texto|*.txt";
            DialogResult res = od.ShowDialog();

            if (res == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(od.FileName);
                nombreDeArchivo = od.FileName;
                archivoGuardado = true;
            }
        }


        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (archivoGuardado)
                // Guarda el archivo directamente sin abrir una nueva ventana de guardar
                GuardarArchivo();
            else
                // Abre la ventana de guardar
                if (GuardarComoArchivo())
                {
                    // Actualiza las variables indicando que el archivo ha sido guardado
                    archivoGuardado = true;
                    cambiosNoGuardados = false;
                }
        }

        private bool GuardarArchivo()
        {
            if (!string.IsNullOrEmpty(nombreDeArchivo))
                try
                {
                    File.WriteAllText(nombreDeArchivo, textBox1.Text);
                    cambiosNoGuardados = false;
                    toolStripStatusLabel2.Text = "Archivo guardado exitosamente";
                    return true;
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel2.Text = "Error al guardar el archivo intenta nuevamente.";
                    return false;
                }
            else
            {
                // Actualiza la variable indicando que el archivo ha sido guardado
                archivoGuardado = true;
                return GuardarComoArchivo();
            }
        }

        private bool GuardarComoArchivo()
        {
            SaveFileDialog sd = new SaveFileDialog();
            // Configura el SaveFileDialog según tus necesidades
            sd.Filter = "Archivos de texto|*.txt";

            DialogResult res = sd.ShowDialog(); // Almacena el resultado en una variable

            if (res == DialogResult.OK) // Usa la variable res en lugar de llamar ShowDialog nuevamente
            {
                try
                {
                    File.WriteAllText(sd.FileName, textBox1.Text);
                    cambiosNoGuardados = false;
                    toolStripStatusLabel2.Text = "Archivo guardado exitosamente.";
                    // Devuelve true si el archivo se guarda correctamente
                    return true;
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel2.Text = "Error al guardar el archivo.";
                    // Devuelve false si se produce un error
                    return false;
                }
            }
            else
            {
                // Devuelve false si se cancela la operación
                toolStripStatusLabel2.Text = "Operación cancelada.";
                return false;
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(GuardarComoArchivo())
                archivoGuardado = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cambiosNoGuardados = true;
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buscador.Show();
            toolStripStatusLabel2.Text = "Buscador abierto";
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cambiosNoGuardados)
            {
                DialogResult resultado = MessageBox.Show("¿Deseas guardar los cambios antes de crear un nuevo archivo?", "Guardar cambios", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                    // Guarda los cambios antes de abrir otro archivo
                    if (!GuardarArchivo())
                        // Si el usuario cancela el diálogo de guardar, no continúes abriendo otro archivo
                        return;
                else if (resultado == DialogResult.Cancel)
                    // Si el usuario cancela, no continuar abriendo otro archivo
                    return;
                // Si el resultado es 'No', continuar a crear un nuevo archivo
            }

            // Restablece la bandera y crea el nuevo archivo
            cambiosNoGuardados = false;
            textBox1.Clear();
            nombreDeArchivo = "";
            toolStripStatusLabel2.Text = "Nuevo archivo creado.";
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cambiosNoGuardados)
            {
                DialogResult result = MessageBox.Show("¿Seguro que quieres salir sin guardar los cambios?", "Salir sin guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancela el cierre del formulario
                }
            }
        }
    }
}
