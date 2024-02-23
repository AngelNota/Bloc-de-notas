using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloc_de_notas
{
    public partial class Buscador : Form
    {
        //variable para almacenar la posicion de la busqueda
        private int posicionDeBusqueda = 0;
        //variable para almacenar la palabra buscada
        private string palabraBuscada = "";
        //Variable para hacer referencia al textbox del formulario 1
        private RichTextBox textBoxDelBlocDeNotas;
        public event EventHandler BuscadorCerrado;

        public Buscador(RichTextBox textBoxDelBlocDeNotas)
        {
            InitializeComponent();
            this.textBoxDelBlocDeNotas = textBoxDelBlocDeNotas;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = textBox1.Text;

            // Obtén el texto del bloc de notas
            string textoBlocDeNotas = textBoxDelBlocDeNotas.Text;

            // Reinicia el formato del texto antes de la nueva búsqueda
            ResetearFormatoTexto();

            // Busca la próxima ocurrencia del término
            int indice = textoBlocDeNotas.IndexOf(terminoBusqueda, posicionDeBusqueda);

            if (indice != -1)
            {
                // Resalta la próxima palabra o letra cambiando el color de fondo
                textBoxDelBlocDeNotas.Select(indice, terminoBusqueda.Length);
                textBoxDelBlocDeNotas.SelectionBackColor = Color.Yellow;

                // Actualiza la posición de búsqueda para la próxima búsqueda
                posicionDeBusqueda = indice + terminoBusqueda.Length;
            }
            else
            {
                // Si no se encuentra, reinicia la búsqueda desde el principio
                MessageBox.Show("No se encontraron más palabras o letras relacionadas.", "Búsqueda finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                posicionDeBusqueda = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Llama al mismo código que el evento del botón principal
            button3_Click(sender, e);
        }

        private void ResetearFormatoTexto()
        {
            // Resetea el formato del texto cambiando el color de fondo al color original
            textBoxDelBlocDeNotas.Select(0, textBoxDelBlocDeNotas.Text.Length);
            textBoxDelBlocDeNotas.SelectionBackColor = textBoxDelBlocDeNotas.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string terminoBusqueda = textBox1.Text;

            // Obtén el texto del bloc de notas
            string textoBlocDeNotas = textBoxDelBlocDeNotas.Text;

            // Reinicia el formato del texto antes de la nueva búsqueda
            ResetearFormatoTexto();

            // Si la posición de búsqueda es mayor que 0, busca la ocurrencia anterior del término
            if (posicionDeBusqueda > 0 && !string.IsNullOrEmpty(terminoBusqueda))
            {
                // Asegúrate de que la posición de búsqueda no sea mayor que la longitud del texto
                posicionDeBusqueda = Math.Min(posicionDeBusqueda, textoBlocDeNotas.Length);

                // Busca la ocurrencia anterior del término
                int indice = textoBlocDeNotas.LastIndexOf(terminoBusqueda, posicionDeBusqueda - 1);

                if (indice >= 0 && indice + terminoBusqueda.Length <= textoBlocDeNotas.Length)
                {
                    // Resalta la palabra o letra encontrada cambiando el color de fondo
                    textBoxDelBlocDeNotas.Select(indice, terminoBusqueda.Length);
                    textBoxDelBlocDeNotas.SelectionBackColor = Color.Yellow;

                    // Actualiza la posición de búsqueda para la próxima búsqueda
                    posicionDeBusqueda = indice;
                }
                else
                {
                    // Si no se encuentra, muestra un mensaje y reinicia la posición de búsqueda
                    MessageBox.Show("No se encontraron más palabras o letras relacionadas.", "Búsqueda finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    posicionDeBusqueda = 0;
                }
            }
        }

        private void Buscador_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Notificar al formulario principal que el buscador ha sido cerrado
            BuscadorCerrado?.Invoke(this, EventArgs.Empty);
        }
    }
}
