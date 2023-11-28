﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P520233_EmmanuelPerez.Formularios
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnVerContrasennia_MouseDown(object sender, MouseEventArgs e)
        {
            TxtContrasennia.UseSystemPasswordChar = false;
        }

        private void BtnVerContrasennia_MouseEnter(object sender, EventArgs e)
        {
            TxtContrasennia.UseSystemPasswordChar = true; 
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();     
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {

            Globales.ObjetosGlobales.MiFormularioPrincipal.Show();
            this.Hide();    



        }
    }
}
