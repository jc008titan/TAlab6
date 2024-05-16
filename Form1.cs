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

        double profit;
        public void CitireMedicamentFisier()
        {
            
            string fisierprofit = "D:\\facultatea\\PIU\\TAlab6\\Profit.txt";
            if (!File.Exists(fisierprofit))
                using (StreamWriter textfisier = new StreamWriter(fisierprofit))
                {
                    textfisier.WriteLine(0);
                }
            using (StreamReader textfisier = new StreamReader(fisierprofit))
            {
                if (double.TryParse(textfisier.ReadToEnd(), out profit))
                    label29.Text = profit.ToString();
            }
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
                    string data_expirare = "";
                    if (parts[0].Trim() != "")
                        nume = parts[0].Trim();
                    else validare = false;
                    DateTime a;
                    if (DateTime.TryParseExact(parts[1], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out a))
                    {
                        data_expirare = parts[1];
                    }
                    else validare = false;
                    double pret = 0;
                    if (!(double.TryParse(parts[2], out pret)))
                        validare = false;
                    else if (Math.Round(pret, 2) != pret)
                        validare = false;
                    else if (pret<0)
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
            bool[] validat = { true, true, true, true, true, true };
            string nume = "";
            if (textBox1.Text.Trim() != "")
                nume = textBox1.Text.Trim();
            else
            {
                validare = false;
                validat[0] = false;
                label24.Text = "Nume invalid!";
            }
            if (validat[0] == true)
                label24.Text = "";
            string data_expirare = "";
            if (dateTimePicker1.Value.Date >= DateTime.Now.Date)
            {
                data_expirare = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                validare = false;
                validat[1] = false;
                label25.Text = "Data expirata!";
            }
            if (validat[1] == true)
                label25.Text = "";
            double pret = 0;
            if (!double.TryParse(textBox2.Text, out pret))
            {
                validare = false;
                validat[2] = false;
                label26.Text = "Pret invalid!";
            }
            else if (Math.Round(pret,2)!=pret)
            {
                validare = false;
                validat[2] = false;
                label26.Text = "Maxim 2 zecimale!";
            }
            else if (pret<0)
            {
                validare = false;
                validat[2] = false;
                label26.Text = "Introdu pozitiv!";
            }
            if (validat[2] == true)
                label26.Text = "";
            int cantitate = 0;
            if (!int.TryParse(textBox3.Text, out cantitate))
            {
                validare = false;
                validat[3] = false;
                label27.Text = "Cantitate invalida!";
            }
            if (validat[3] == true)
                label27.Text = "";
            Medicament.Reteta reteta = 0;
            int selectat = 1;
            if (radioButton1.Checked)
                selectat = 1;
            else if (radioButton2.Checked)
                selectat = 0;
            else
            {
                validare = false;
                validat[4] = false;
                label22.Text = "Selecteaza!";
            }
            if (validat[4] == true)
                label22.Text = "";
            reteta = (Farmacie.Medicament.Reteta)selectat;
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false)
            {
                validare = false;
                validat[5] = false;
                label23.Text = "Selecteaza cel putin una!";
            }
            if (validat[5] == true)
                label23.Text = "";
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
                reset();
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
            changelistBox1();
            //this.Width=1300;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string fisier = "D:\\facultatea\\PIU\\TAlab6\\Profit.txt";
                string selectedMedicamentName = comboBox2.SelectedItem.ToString();
                foreach (var medicament in medicamente)
                {
                    if (medicament.nume == selectedMedicamentName)
                    {
                        if (medicament.cantitate > 0)
                        {
                            medicament.cantitate -= 1;
                            profit = profit + medicament.pret;
                            label19.Text = medicament.cantitate.ToString();
                            AfisareMedicament();
                            RewriteMedicamenteFile();
                            label29.Text = profit.ToString();
                        }

                        
                        break;
                    }
                    using (StreamWriter textfisier = new StreamWriter(fisier))
                    {
                        textfisier.WriteLine(profit);
                    }
                }
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
                changelistBox1();
            }

        }





        private void RewriteMedicamenteFile()
        {
            string fisier = "D:\\facultatea\\PIU\\TAlab6\\Medicamente.txt";
            
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
                    changelistBox1();
                }
            }
        }

        private void changelistBox1()
        {
            string subsir = textBox4.Text;
            while (listBox1.Items.Count > 0)
            {
                listBox1.Items.RemoveAt(0);
            }


            for (int i = 0; i < medicamente.Length; i++)
            {
                if (medicamente[i].nume.ToUpper().StartsWith(subsir.ToUpper()))
                {
                    listBox1.Items.Add(medicamente[i].nume);
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            changelistBox1();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                comboBox2.Text = listBox1.SelectedItem.ToString();
                AfisareMedicament();
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && listBox1.Items.Count != 0)
                listBox1.SelectedIndex = 0;
        }
    }
}
