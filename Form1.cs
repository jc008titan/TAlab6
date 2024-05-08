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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Farmacie
{
    public partial class Form1 : Form
    {


        public Medicament[] medicamente = new Medicament[] { };

        public void CitireMedicamentFisier()
        {
            string fisier = "D:\\facultatea\\PIU\\TAlab6\\Medicamente.txt";
            if (!File.Exists(fisier))
                return;
            using (StreamReader textfisier = new StreamReader(fisier))
            {
                string line;
                while ((line = textfisier.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');

                    bool validare = true;
                    string nume = "";
                    if (parts[0].Trim() != "")
                        nume = parts[0].Trim();
                    else validare = false;
                    string data_expirare = parts[1];
                    double pret = 0;
                    if (!double.TryParse(parts[2], out pret))
                        validare = false;
                    int cantitate = 0;
                    if (!int.TryParse(parts[3], out cantitate))
                        validare = false;
                    Medicament.Reteta reteta = 0;
                    int selectat = 1;
                    if (parts[4] == "Cu")
                        selectat = 1;
                    else if (parts[4] == "Fara")
                        selectat = 0;
                    else validare = false;
                    reteta = (Farmacie.Medicament.Reteta)selectat;
                    string varste = parts[5];
                    string[] grupevarsta = varste.Split();
                    Medicament.Varsta varsta = 0;
                    Medicament.Varsta varstacitita;
                    for (int i = 0; i < grupevarsta.Length; i++)
                    {
                        Enum.TryParse(grupevarsta[i], out varstacitita);
                        varsta |= varstacitita;
                    }
                    if (validare == true)
                    {
                        comboBox2.Items.Add(nume);
                        Medicament medicament = new Medicament(nume, data_expirare, pret, cantitate, reteta, varsta);
                        Array.Resize(ref medicamente, medicamente.Length + 1);
                        medicamente[medicamente.Length - 1] = medicament;
                    }
                    else
                    {
                        label20.Text = "Date fisier invalide!";
                        label20.ForeColor = Color.Red;
                    }
                }
            }
        }
        public Medicament CitireMedicament()
        {
            bool validare = true;
            string nume = "";
            if (textBox1.Text.Trim() != "")
                nume = textBox1.Text.Trim();
            else validare = false;
            string data_expirare = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
            double pret = 0;
            if (!double.TryParse(textBox2.Text, out pret))
                validare = false;
            int cantitate = 0;
            if (!int.TryParse(textBox3.Text, out cantitate))
                validare = false;
            Medicament.Reteta reteta = 0;
            int selectat = 1;
            if (radioButton1.Checked)
                selectat = 0;
            else if (radioButton2.Checked)
                selectat = 1;
            else validare = false;
            reteta = (Farmacie.Medicament.Reteta)selectat;
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false)
                validare = false;
            string varste = "";
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
            if (validare == true)
            {
                comboBox2.Items.Add(nume);
                Medicament medicament = new Medicament(nume, data_expirare, pret, cantitate, reteta, varsta);
                Array.Resize(ref medicamente, medicamente.Length + 1);
                medicamente[medicamente.Length - 1] = medicament;
                label20.Text = "Medicament adaugat";
                label20.ForeColor = Color.Green;

                string fisier = "D:\\facultatea\\PIU\\TAlab6\\Medicamente.txt";
                using (StreamWriter textfisier = new StreamWriter(fisier, true))
                {
                    textfisier.WriteLine(medicamente[medicamente.Length - 1].nume + ';' + medicamente[medicamente.Length - 1].data_expirare + ';' + medicamente[medicamente.Length - 1].pret.ToString() + ';' + medicamente[medicamente.Length - 1].cantitate.ToString() + ';' + medicamente[medicamente.Length - 1].reteta.ToString() + ';' + medicamente[medicamente.Length - 1].varsta.ToString());
                }

                return medicament;
            }
            else
            {
                label20.Text = "Date invalide!";
                label20.ForeColor = Color.Red;
                return null;
            }

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
            else
            {
                label16.Text = "";
                label17.Text = "";
                label18.Text = "";
                label19.Text = "";
                label14.Text = "";
                label15.Text = "";
            }
        }
        /*public void CautareMedicamentNume()
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
        }*/

        public Form1()
        {
            InitializeComponent();
        }

        public void reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CitireMedicament() != null)
                comboBox2.Text = medicamente[medicamente.Length - 1].nume;
            AfisareMedicament();
            //this.Width=1300;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.Width == 1300)
            {
                this.Width = 830;
            }
            else
            {
                this.Width = 1300;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            AfisareMedicament();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CitireMedicamentFisier();
            if (medicamente.Length != 0)
            {
                comboBox2.Text = medicamente[medicamente.Length - 1].nume;
                AfisareMedicament();
            }
        }





        private void RewriteMedicamenteFile()
        {
            string fisier = "D:\\facultatea\\PIU\\TAlab6\\Medicamente.txt";
            // Overwrite the existing file with the updated medication list
            using (StreamWriter textfisier = new StreamWriter(fisier))
            {
                foreach (var medicament in medicamente)
                {
                    textfisier.WriteLine(medicament.nume + ';' + medicament.data_expirare + ';' + medicament.pret.ToString() + ';' + medicament.cantitate.ToString() + ';' + medicament.reteta.ToString() + ';' + medicament.varsta.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string nume = comboBox2.SelectedItem.ToString();

                int indexToDelete = -1;
                for (int i = 0; i < medicamente.Length; i++)
                {
                    if (medicamente[i].nume == nume)
                    {
                        indexToDelete = i;
                        break;
                    }
                }

                if (indexToDelete != -1)
                {
                    Medicament[] updatedMedicamente = new Medicament[medicamente.Length - 1];
                    int currentIndex = 0;
                    for (int i = 0; i < medicamente.Length; i++)
                    {
                        if (i != indexToDelete)
                        {
                            updatedMedicamente[currentIndex] = medicamente[i];
                            currentIndex++;
                        }
                    }
                    medicamente = updatedMedicamente;
                    comboBox2.Items.Remove(nume);
                    AfisareMedicament();
                    RewriteMedicamenteFile();
                }
            }
        }
    }
}
