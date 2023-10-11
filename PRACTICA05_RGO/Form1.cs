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
using System.Text.RegularExpressions;

namespace Practica_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tb_Edad.TextChanged += ValidarEdad;
            tb_Estatura.TextChanged += ValidarEstatura;
            tb_Telefono.TextChanged += ValidarTelefono;
            tb_Nombre.TextChanged += ValidarNombre;
            tb_Apellidos.TextChanged += ValidarApellidos;
        }

        private void bt_Guardar_Click(object sender, EventArgs e)
        {

            string Nombres = tb_Nombre.Text;
            string Apellidos = tb_Apellidos.Text;
            string Telefono = tb_Telefono.Text;
            string Estatura = tb_Estatura.Text;
            string Edad = tb_Edad.Text;

            string Genero = "";
            if (rb_Hombre.Checked)
            {
                Genero = "Hombre";
            }
            else if (rb_Mujer.Checked)
            {
                Genero = "Mujer";
            }
            if (EsEnterovalido(Edad) && EsDecimalvalido(Estatura) && EsEnterovalidoDe10Digitos(Telefono) && EsTextovalido(Nombres) && EsTextovalido(Apellidos))
            {
                string datos = $"Nombres: {Nombres}\r\nApellios: {Apellidos}\r\nTelefono: {Telefono} kg\r\nEstatura: {Estatura} cm\r\nEdad: {Edad} años\r\nGenero: {Genero}";

                string rutaarchivo = "C:/Users/moram/Documents/3Mdatos.txt";

                bool archivoExiste = File.Exists(rutaarchivo);

                Console.WriteLine(archivoExiste);
                if (archivoExiste == false)
                {
                    File.WriteAllText(rutaarchivo, datos);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(rutaarchivo, true))
                    {
                        if (archivoExiste)
                        {
                            writer.WriteLine();
                        }
                        writer.WriteLine(datos);
                    }
                }
                MessageBox.Show("Datos guardados con exito:\n\n" + datos, "informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Por favor, ingrese datos validos en los campos. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool EsEnterovalido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }
        private bool EsDecimalvalido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);

        }
        private bool EsEnterovalidoDe10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length == 10;
        }
        private bool EsTextovalido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");
        }

        private void ValidarEdad(object senter, EventArgs e)
        {
            TextBox textBox = (TextBox)senter;
            if (!EsEnterovalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una edad valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarEstatura(object senter, EventArgs e)
        {
            TextBox textBox = (TextBox)senter;
            if (!EsDecimalvalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una estatura valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarTelefono(object senter, EventArgs e)
        {
            TextBox textBox = (TextBox)senter;
            string input = textBox.Text;
            if (input.Length > 10)
            {
                if (!EsEnterovalidoDe10Digitos(input))
                {
                    MessageBox.Show("Por favor, ingrese un numero de telefono valido de 10 digitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }

            }
            else if (!EsEnterovalidoDe10Digitos(input))
            {
                MessageBox.Show("Por favor, ingrese un numero de telefono valido de 10 digitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidarNombre(object senter, EventArgs e)
        {
            TextBox textBox = (TextBox)senter;
            if (!EsTextovalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre valido (solo letras y espacio).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarApellidos(object senter, EventArgs e)
        {
            TextBox textBox = (TextBox)senter;
            if (!EsTextovalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese apellidos validos (solo letras y espacio).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void bt_Cancelar_Click(object sender, EventArgs e)
        {
            tb_Nombre.Clear();
            tb_Apellidos.Clear();
            tb_Estatura.Clear();
            tb_Telefono.Clear();
            tb_Edad.Clear();
            rb_Hombre.Checked = false;
            rb_Mujer.Checked = false;
        }
    }
}