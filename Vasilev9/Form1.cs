using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vasilev9
{
    public enum AccountType { текущий, сберегательный }
    public partial class Form1 : Form
    {
        BankAccount[] accounts;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();   
            int count = (int)numericUpDown1.Value;
            accounts = new BankAccount[count];
            for (int acc = 0; acc<count; acc++)
            {
                accounts[acc] = new BankAccount();
                accounts[acc].ChangeBalance(random.Next(100, 1000000));
                accounts[acc].ChangeType((AccountType)random.Next(0, 2));
            }
            DrawBankAccount();
        }

        private void DrawBankAccount()
        {
            richTextBox1.Clear();
            foreach (BankAccount account in accounts)
            {
                richTextBox1.Text += $"Аккаунт: {account.GetNumber()} - <<{account.GetType()}>>. Баланс: {account.GetBalance()} (руб.)\n\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach( BankAccount account in accounts)
            {
                account.PutMoney((int)PutNum.Value);
                account.TryTakeMoney((int)TakeNum.Value);
                if (TypeCheckBox.Checked)
                {
                    if (account.GetType() == AccountType.текущий)
                        account.ChangeType(AccountType.сберегательный);
                    else
                        account.ChangeType(AccountType.текущий);
                }
            }
            DrawBankAccount();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            Random random = new Random();
            Build[] builds = new Build[(int)numericUpDown2.Value];
            string per = new string(Convert.ToChar("-"), 30);
            for (int build = 0;  build < builds.Length; build++)
            {
                builds[build] = new Build();
                builds[build].setHeight(random.NextDouble()*100+2);
                builds[build].setEntryCount(random.Next(1, 6));
                builds[build].setLevelsCount(random.Next(Convert.ToInt32(builds[build].GetHeight() / 3.5), Convert.ToInt32(builds[build].GetHeight()/2)));
                builds[build].setApartmentsCount(random.Next(builds[build].GetLevelsCount()*builds[build].GetEntryCount(), builds[build].GetLevelsCount() * builds[build].GetEntryCount()*5));

                richTextBox2.Text += $"Здание <<{builds[build].GetNumber()}>>:\n\t Высота: {builds[build].GetHeight()} м., {builds[build].GetLevelsCount()} этажей, " +
                    $"{builds[build].GetApartmentsCount()} квартир, {builds[build].GetEntryCount()} подьездов.\nВычисления: \n\tВысота этажа ({builds[build].CalcLevelsHeight()}), " +
                    $"Квартир на этаж ({builds[build].CalcApartmentsInLevel()}), Квартир на подъезд ({builds[build].CalcApartmensInEntry()})\n{per}\n";
            }

        }
    }

    public class BankAccount
    {
        public static int id = 1;

        private int accountNumber;
        private int balance;
        private AccountType type;

        public BankAccount()
        {
            accountNumber = id;
            idPlus();
        }

        public void PutMoney(int count)
        {
            balance += count;
        }

        public bool TryTakeMoney(int count)
        {
            if (count > balance | count < 0)
                return false;
            else
            {
                balance -= count;
                return true;
            }
        }

        private static void idPlus()
        {
            id++;
        }

        public int GetBalance()
        {
            return balance;
        }

        public AccountType GetType()
        {
            return type;
        }

        public int GetNumber()
        {
            return accountNumber;
        }

        public void ChangeBalance(int money)
        {
            this.balance = money;
        }

        public void ChangeType(AccountType type)
        {
            this.type = type;
        }
    }

    public class Build
    {
        private static List<int> staticNumbers = new List<int>();
        private int number;
        private double height;
        private int levelsCount;
        private int apartmentsCount;
        private int entryCount;
        
        public Build()
        {
            NumberGenerate();
            number = staticNumbers[staticNumbers.Count-1];

        }

        private static void NumberGenerate() //Генерируем программно номер здания
        {
            Random rnd = new Random();
            int staticNumber = rnd.Next(0, 999);
            if (staticNumbers.Contains(staticNumber))
                NumberGenerate();
            else
                staticNumbers.Add(staticNumber);
        }
        public void setHeight(double height)
        {
            if (height > 0)
                this.height = Math.Round(height, 3);
        }

        public void setLevelsCount(int Count)
        {
            if (Count > 0)
                levelsCount = Count;
        }

        public void setApartmentsCount(int Count)
        {
            if (Count > 0)
                apartmentsCount = Count;    
        }

        public void setEntryCount(int Count)
        {
            if (Count > 0)
                entryCount = Count;
        }

        public int GetNumber() {return number;}
        public double GetHeight() {return height;}
        public int GetLevelsCount() { return levelsCount;}
        public int GetApartmentsCount() { return apartmentsCount;}
        public int GetEntryCount() { return entryCount;}

        public double CalcLevelsHeight()
        {
            if (levelsCount > 0 && height > 0)
                return Math.Round(height / levelsCount, 4);
            else
                return -1;
        }

        public double CalcApartmentsInLevel()
        {
            if (levelsCount > 0 && apartmentsCount > 0)
                return Math.Round((double)apartmentsCount / levelsCount, 2);
            else
                return -1;
        }

        public double CalcApartmensInEntry()
        {
            if (apartmentsCount > 0 && entryCount > 0) return Math.Round((double)apartmentsCount/entryCount, 2);
            else return -1;
        }
    }
}
