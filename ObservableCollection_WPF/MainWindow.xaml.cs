using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ObservableCollection_WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, INotifyPropertyChanging
    {
        
        public class Person
        {
            public string Name { get; set; }
            public string Address { get; set; }
        }
        #region MainWindow
        private ObservableCollection<Person> person;
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        bool isLoading;
        public MainWindow()
        {
            InitializeComponent();
            isLoading = true;
            person = new ObservableCollection<Person>()
            {
                 new Person(){Name="Prabhat",Address="India"},
                  new Person(){Name="Smith",Address="US"},
                   new Person(){Name="Alan",Address="Canada"},
                    new Person(){Name="Müller",Address="Germany"},
                     new Person(){Name="Fortunato",Address="Italy"}

            };

            TestBinding = person;
            DataContext = this;
        }
        #region Methods
        private void OrderByName(MenuItem menuitem)
        {
            lstNames.SelectionChanged -= lstNames_SelectionChanged_1;
            if (menuitem.IsChecked == true)
            {
                person = new ObservableCollection<Person>(person.OrderBy(x => x.Name));

            }
            else
            {
                person = new ObservableCollection<Person>(person.OrderByDescending(x => x.Name.Substring(0, 1)));
            }
            lstNames.ItemsSource = null;
            lstNames.ItemsSource = person;
            lstNames.SelectionChanged += lstNames_SelectionChanged_1;
        }
        #endregion Methods
        #region Events
        private void btnNames_Click(object sender, RoutedEventArgs e)
        {
            person.Add(new Person() { Name = txtName.Text, Address = txtAddress.Text });
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void NameCM_Click(object sender, RoutedEventArgs e)
        {
            OrderByName(ascendMenuItem);
        }




        private void ascendMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ascendMenuItem.IsChecked = true;
            decendMenuItem.IsChecked = false;
        }

        private void decendMenuItem_Click(object sender, RoutedEventArgs e)
        {
            decendMenuItem.IsChecked = true;
            ascendMenuItem.IsChecked = false;

        }
        private void lstNames_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (isLoading == false)
            {
                var test = sender as ListView;
                Person name = (Person)test.SelectedItem;

                MessageBoxResult result = MessageBox.Show("Möchten Sie diese Person entfernen ?", "MyAPP", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    lstNames.SelectionChanged -= lstNames_SelectionChanged_1;
                    TestBinding.Remove(name);
                    test.SelectedItem = null;
                    lstNames.SelectionChanged += lstNames_SelectionChanged_1;

                }
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            isLoading = false;

        }

        private void AddressCM_Click(object sender, RoutedEventArgs e)
        {
            OrderByName(ascendMenuItemAddress);
        }

        private void ascendMenuItemAddress_Click(object sender, RoutedEventArgs e)
        {
            ascendMenuItemAddress.IsChecked = true;
            decendMenuItemAddress.IsChecked = false;
        }

        private void decendMenuItemAddress_Click(object sender, RoutedEventArgs e)
        {
            ascendMenuItemAddress.IsChecked = false;
            decendMenuItemAddress.IsChecked = true;
        }
        #endregion Events



        #region TestBinding

        private ObservableCollection<Person> _testBinding;

        public ObservableCollection<Person> TestBinding
        {
            get
            {
                return _testBinding;
            }
            set
            {
                if (_testBinding != value)
                {
                    NotifyPropertyChanging("TestBinding");
                    _testBinding = value;
                    NotifyPropertyChanged("TestBinding");
                }
            }
        }
        #endregion
        #region selectedItem

        private Person _selectedItem;

        public Person selectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    NotifyPropertyChanging("selectedItem");
                    _selectedItem = value;
                    NotifyPropertyChanged("selectedItem");
                }
            }
        }
        #endregion
        #region INotifyPropertyChanged Members
        // Used to notify the page that a data context property changed
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

       

        // Used to notify the data context that a data context property is about to change
        protected void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

       
        #endregion MainWindow
    }



}
