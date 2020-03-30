using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Footballers
{
    public partial class MainWindow : Window
    {
        List<Player> PlayerList = new List<Player>();
        public class Player
        {
            private string name;
            private string surname;
            private double weight;
            private int yearOfBirth;

            public Player(string name, string surname, double weight, int age)
            {
                this.name = name;
                this.surname = surname;
                this.weight = weight;
                this.yearOfBirth = CurrentYear() - age;
            }

            public Player(Player p)
            {
                this.name = p.name;
                this.surname = p.surname;
                this.weight = p.weight;
                this.yearOfBirth = p.yearOfBirth;
            }
            public override string ToString()
            {
                string sWeight = weight.ToString();
                string sAge = (CurrentYear() - this.yearOfBirth).ToString();
                return $"{name} {surname}, masa: {sWeight}, wiek: {sAge}";
            }

            public int CurrentYear()
            {
                int.TryParse(DateTime.Now.Year.ToString(), out int year); //get the current year as int
                return year;
            }

            public string GetName()
            {
                return this.name;
            }

            public string GetSurname()
            {
                return this.surname;
            }

            public int GetAge()
            {
                return CurrentYear() - this.yearOfBirth;
            }

            public double GetWeight()
            {
                return this.weight;
            }

        }
        private void CreateCombo()
        {
            for(int i=15; i<=60; i++)
            {
                Age_cb.Items.Add(i);
            }
            
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            string name = Name_tb.Text;
            string surname = Surname_tb.Text;
            double weight = Weight_sl.Value;
            Int32.TryParse(Age_cb.SelectedValue.ToString(), out int age);
            Player newPlayer = new Player(name, surname, weight, age);

            string information = "";
            if(name=="" || name=="Podaj imie")
            {
                information = "Prosze wpisac imie zawodnika";
            }
            if(surname=="" || surname=="Podaj nazwisko")
            {
                information = "Prosze wpisac nazwisko zawodnika";
            }
            if(age==0)
            {
                information = "Prosze wybrac wiek";
            }
            for (int i = 0; i < PlayerList.Count(); i++)
            {
                if (PlayerList[i].ToString() == newPlayer.ToString())
                {
                    information = "Ten zawodnik jest juz dodany";
                    break;
                }
            }
            if (information == "")
            {
                PlayerList.Add(newPlayer);
                Players_lb.Items.Add(newPlayer.ToString());
            }
            else
            {
                MessageBox.Show(information);
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (Players_lb.SelectedItem != null)
            {
                string selectedItem = Players_lb.SelectedItem.ToString();
                int id = GetPlayerIdFromString(selectedItem);
                if (id != -1)
                {
                    PlayerList.RemoveAt(id);
                    Players_lb.Items.RemoveAt(id);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano zawodnika do usuniecia");
            }
        }

        private void OnItemInListBoxClick(object sender, RoutedEventArgs e)
        {
            if (Players_lb.SelectedItem != null)
            {
                string selectedItem = Players_lb.SelectedItem.ToString();
                int id = GetPlayerIdFromString(selectedItem);
                if (id != -1)
                {
                    Name_tb.Text = PlayerList[id].GetName();
                    Surname_tb.Text = PlayerList[id].GetSurname();
                    Weight_sl.Value = PlayerList[id].GetWeight();
                    Age_cb.SelectedItem = PlayerList[id].GetAge();
                }
            }
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            if (Players_lb.SelectedItem != null)
            {
                string name = Name_tb.Text;
                string surname = Surname_tb.Text;
                double weight = Weight_sl.Value;
                Int32.TryParse(Age_cb.SelectedValue.ToString(), out int age);
                Player newPlayer = new Player(name, surname, weight, age);

                string selectedItem = Players_lb.SelectedItem.ToString();
                int id = GetPlayerIdFromString(selectedItem);

                string information = "";
                if (name == "" || name == "Podaj imie")
                {
                    information = "Prosze wpisac imie zawodnika";
                }
                if (surname == "" || surname == "Podaj nazwisko")
                {
                    information = "Prosze wpisac nazwisko zawodnika";
                }
                if (age == 0)
                {
                    information = "Prosze wybrac wiek";
                }
                for (int i = 0; i < PlayerList.Count(); i++)
                {
                    if (PlayerList[i].ToString() == newPlayer.ToString())
                    {
                        information = "Ten zawodnik jest juz dodany";
                        break;
                    }
                }
                if (information == "")
                {
                    PlayerList[id] = (newPlayer);
                    Players_lb.Items.RemoveAt(id);
                    Players_lb.Items.Insert(id, newPlayer.ToString());
                }
                else
                {
                    MessageBox.Show(information);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano zawodnika!");
            }
        }
        public void FocusEventOnName(object sender, RoutedEventArgs e)
        {
            Name_tb.Text = string.Empty;
            Name_tb.GotFocus -= FocusEventOnName;
        }

        public void FocusEventOnSurname(object sender, RoutedEventArgs e)
        {
            Surname_tb.Text = string.Empty;
            Surname_tb.GotFocus -= FocusEventOnSurname;
        }

        private int GetPlayerIdFromString(string searchFor)
        {
            int id = -1;
            for (int i = 0; i < PlayerList.Count(); i++)
            {
                if (PlayerList[i].ToString() == searchFor)
                {
                    id = i;
                    break;
                }
            }
            return id;
        }

        private void AtStart()
        {
            CreateCombo();
        }

        public MainWindow()
        {
            InitializeComponent();
            AtStart();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
