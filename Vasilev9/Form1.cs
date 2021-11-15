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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BankAccount account1 = new BankAccount();
            BankAccount account2 = new BankAccount();
            BankAccount account3 = new BankAccount();

            Build build = new Build();
            Build build2 = new Build();
            Build build3 = new Build();

            richTextBox1.Text += $"{build.number}\n";
            richTextBox1.Text += $"{build2.number}\n";
            richTextBox1.Text += $"{build3.number}\n";

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
            int staticNumber = rnd.Next(0, 19999);
            if (staticNumbers.Contains(staticNumber))
                NumberGenerate();
            else
                staticNumbers.Add(staticNumber);
        }
        public void setHeight(double height)
        {
            if (height > 0)
                this.height = height;
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
