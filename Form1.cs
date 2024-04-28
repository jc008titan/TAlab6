using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Farmacie
{
    public partial class Form1 : Form
    {


        public Medicament[] medicamente = new Medicament[] { };
        public Medicament CitireMedicament()
        {
            string nume = textBox1.Text.Trim();
            string data_expirare = monthCalendar1.SelectionStart.ToString();
            double pret = 0;
            double.TryParse(textBox2.Text, out pret);
            int cantitate = 0;
            int.TryParse(textBox3.Text, out cantitate);
            Medicament.Reteta reteta = 0;
            int selectat=1;
            if (radioButton1.Checked)
                selectat = 0;
            else if(radioButton2.Checked)
                selectat = 1;
            reteta = (Farmacie.Medicament.Reteta)selectat;

            string varste= "";
            if (checkBox1.Checked)
                varste += " Copii0_3";
            if (checkBox2.Checked)
                varste += " Copii4_9";
            if (checkBox3.Checked)
                varste += " Copii10_17";
            if (checkBox4.Checked)
                varste += " Adulti";
            if (checkBox5.Checked)
                varste += " Batrani";
            string[] grupevarsta = varste.Split();
            Medicament.Varsta varsta = 0;
            Medicament.Varsta varstacitita;
            for (int i = 0; i < grupevarsta.Length; i++)
            {
                Enum.TryParse(grupevarsta[i], out varstacitita);
                varsta |= varstacitita;
            }
            comboBox2.Items.Add(nume);

            Medicament medicament = new Medicament(nume, data_expirare, pret, cantitate, reteta, varsta);
            Array.Resize(ref medicamente, medicamente.Length + 1);
            medicamente[medicamente.Length - 1] = medicament;
            return medicament;

        }
        public void AfisareMedicament()
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedMedicamentName = comboBox2.SelectedItem.ToString();

                foreach (var medicament in medicamente)
                {
                    if (medicament.nume == selectedMedicamentName)
                    {
                        label16.Text = medicament.nume;
                        label17.Text = medicament.data_expirare;
                        label18.Text = medicament.pret.ToString();
                        label19.Text = medicament.cantitate.ToString();
                        label14.Text = medicament.reteta.ToString();
                        label15.Text = medicament.varsta.ToString();

                        // Assuming there's only one medication with the selected name, so we break out of the loop
                        break;
                    }
                }
            }
        }
        public void CautareMedicamentNume()
        {
            Console.WriteLine("Introduceti numele medicamentului cautat:");
            string s = Console.ReadLine().Trim();
            bool gasit = false;
            foreach (var m in medicamente)
            {
                if (m.nume.ToUpper().Contains(s.ToUpper()))
                {
                    Console.WriteLine($"Medicamentul gasit:\n\tnume: {m.nume}\n\tdata expirare: {m.data_expirare}" +
                        $"\n\tpret: {m.pret}\n\tcantitate: {m.cantitate}\n");
                    gasit = true;
                }
            }
            if (!gasit) Console.WriteLine($"Nu a fost gasit niciun medicament cu {s}\n");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CitireMedicament();
            this.Width=1300;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AfisareMedicament();
        }
    }
}
