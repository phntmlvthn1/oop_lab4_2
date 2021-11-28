using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace oop_lab4_2
{
    public partial class Form1 : Form
    {
        Model model; // создаю объект класса Model для изменения/получения значений a,b,c

        public Form1()
        {
            InitializeComponent();
            model = new Model();
            model.observers += new EventHandler(this.UpdateFromModel); // для обновления формы
            Closing += OnClosing; // для сохранения значений переменных при закрытии программы
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs) // в Properties.Settings записываются значения переменных
        {
            Properties.Settings.Default.a = model.GetValueA();
            Properties.Settings.Default.b = model.GetValueB();
            Properties.Settings.Default.c = model.GetValueC();
            Properties.Settings.Default.Save();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                model.SetValueA(int.Parse(textBox1.Text)); // вызов метода у model для записи значения переменной А при изменения текста в текстбоксе, ниже - аналогично
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                model.SetValueB(int.Parse(textBox2.Text));
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                model.SetValueC(int.Parse(textBox3.Text));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            model.SetValueA(trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            model.SetValueB(trackBar2.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            model.SetValueC(trackBar3.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.SetValueA(decimal.ToInt32(numericUpDown1.Value));
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.SetValueB(decimal.ToInt32(numericUpDown2.Value));
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            model.SetValueC(decimal.ToInt32(numericUpDown3.Value));
        }

        private void UpdateFromModel(object sender, EventArgs e)
        {
            textBox1.Text = model.GetValueA().ToString();
            textBox2.Text = model.GetValueB().ToString();
            textBox3.Text = model.GetValueC().ToString();
            numericUpDown1.Value = model.GetValueA();
            numericUpDown2.Value = model.GetValueB();
            numericUpDown3.Value = model.GetValueC();
            trackBar1.Value = model.GetValueA();
            trackBar2.Value = model.GetValueB();
            trackBar3.Value = model.GetValueC();
        } // Обновление формы

        private void Form1_Load(object sender, EventArgs e) // при загрузке формы записываются значения переменных из прошлой сессии программы
        {
            model.valueA = Properties.Settings.Default.a;
            model.valueB = Properties.Settings.Default.b;
            model.valueC = Properties.Settings.Default.c;
            textBox1.Text = Properties.Settings.Default.a.ToString();
            textBox2.Text = Properties.Settings.Default.b.ToString();
            textBox3.Text = Properties.Settings.Default.c.ToString();
            numericUpDown1.Value = Properties.Settings.Default.a;
            numericUpDown2.Value = Properties.Settings.Default.b;
            numericUpDown3.Value = Properties.Settings.Default.c;
            trackBar1.Value = Properties.Settings.Default.a;
            trackBar2.Value = Properties.Settings.Default.b;
            trackBar3.Value = Properties.Settings.Default.c;

        }

    }

    public class Model
    {
        public int valueA, valueB, valueC;
        public EventHandler observers;

        public void SetValueA(int value)
        {
            if (value > 0 && value < 100) // проверка на одз
            {
                if (value <= valueC)
                {
                    valueA = value;
                    if (valueB < value)
                    {
                        valueB = value;
                    }
                }
                else
                {
                    valueA = value;
                    valueB = value;
                    valueC = value;
                }
            }
            else
            if (value >= 100)
            {
                valueA = 100;
                valueB = 100;
                valueC = 100;
            }
            else if (value <= 0)
            {
                valueA = 0;
            }
            observers.Invoke(this, null);

        } // установка значения для А, ниже - аналогично

        public void SetValueB(int value)
        {
            if (value >= 0 && value <= 100)
            {
                if (value >= valueA && value <= valueC)
                    valueB = value;
            }
            observers.Invoke(this, null);


        }

        public void SetValueC(int value)
        {
            if (value > 0 && value < 100)
            {
                if (value >= valueA)
                {
                    valueC = value;
                    if (valueB > value)
                    {
                        valueB = value;
                    }
                }
                else
                {
                    valueC = value;
                    valueA = value;
                    valueB = value;
                }

            }
            else if (value >= 100)
            {
                valueC = 100;
            }
            else if (value <= 0)
            {
                valueC = 0;
                valueB = 0;
                valueA = 0;
            }
            observers.Invoke(this, null);

        }

        public int GetValueA() // возвращает значение А
        {
            return valueA;
        }
        public int GetValueB()
        {
            return valueB;
        }
        public int GetValueC()
        {
            return valueC;
        }

    }





}
