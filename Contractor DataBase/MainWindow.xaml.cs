using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.IO;
using Contractor_DataBase.DataModels;
using MahApps.Metro.Controls.Dialogs;

namespace Contractor_DataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            InitializeComponent();
            FillContractorList();
            
        }
        //Logging exceptions
        private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            string logPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            Exception e = (Exception) args.ExceptionObject;
            MessageBox.Show("Log will be saved to: " + logPath);
            FileStream fs = new FileStream(logPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Message: " + e.Message);
            sw.WriteLine("InnerException: " + e.InnerException.Message);
            sw.WriteLine(e.ToString());
            sw.Close();
            fs.Close();
        }

        private void FillContractorList()
        {
            using (var db = new ContractorContext())
            {
                ContractorList.Items.Clear();
                if (db.Contractors.ToList().Count <= 0) return;
                foreach (var item in db.Contractors.ToList())
                {
                    ContractorList.Items.Add(item);
                }
            }
        }
        private void FillContractorOrders()
        {
            OrdersListBox.Items.Clear();
            Contractor theOne = (Contractor)ContractorList.SelectedItem;
            if (theOne == null) return;
            using (var db = new ContractorContext())
            {
                foreach (var item in db.Orders.ToList().Where(item => item.ContractorId == theOne.ContractorId))
                {
                        OrdersListBox.Items.Add(item);
                }
            }
        }
        private void FillContractorOrders(object sender, EventArgs e)
        {
            OrdersListBox.Items.Clear();
            Contractor theOne = (Contractor) ContractorList.SelectedItem;
            if (theOne == null) return;
            using (var db = new ContractorContext())
            {
                foreach (var item in db.Orders.ToList())
                {
                    if (item.ContractorId == theOne.ContractorId)
                    {
                        OrdersListBox.Items.Add(item);
                    }
                }
            }
        }
        private void FillContractorDetails()
        {
            if (ContractorList.SelectedItem == null) return;
            Contractor item = (Contractor) ContractorList.SelectedItem;
            ContractorNamelbl.Content = item.ContractorName;
            ManagerNamelbl.Content = item.ManagerName;
            Emaillbl.Content = item.ContactEmail;
            Phonelbl.Content = item.ContactPhone;
        }

        private void ClearContractorDetails()
        {
            ContractorNamelbl.Content = "";
            ManagerNamelbl.Content = "";
            Emaillbl.Content = "";
            Phonelbl.Content = "";
        }

        private void newContractorBtn_Click(object sender, RoutedEventArgs e)
        {
            NewContractor form = new NewContractor();
            form.Show();
            form.Closed += Form_Closed;
        }

        private void Form_Closed(object sender, EventArgs e)
        {
            FillContractorList();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            FillContractorList();
            ClearContractorDetails();
        }

        private void ContractorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillContractorDetails();
            FillContractorOrders();
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ContractorList.SelectedItem != null)
            {
                Contractor item = (Contractor) ContractorList.SelectedItem;
                AddOrder order = new AddOrder(item);
                order.Show();
                order.Closed += FillContractorOrders;
            }
            else
            {
                this.ShowMessageAsync("Ошибка", "Выбирите поставщика");
            }
            
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ContractorList.SelectedItem == null) return;
            Contractor item = (Contractor) ContractorList.SelectedItem;
            using (var db = new ContractorContext())
            {

                if (db.Contractors.Find(item.ContractorId).Orders != null)
                {
                    foreach (var order in db.Contractors.Find(item.ContractorId).Orders)
                    {
                        db.Orders.Remove(order);
                    }
                }
                db.Contractors.Remove(db.Contractors.Find(item.ContractorId));
                db.SaveChanges();
                FillContractorList();
            }
        }

        private void OrdersListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrdersListBox.SelectedItem == null) return;
            AddOrder order = new AddOrder((Contractor)ContractorList.SelectedItem, (Order)OrdersListBox.SelectedItem);
            order.Show();
            order.Closed += FillContractorOrders;
        }

        private void DeleteOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListBox.SelectedItem == null) return;
            using (var db = new ContractorContext())
            {
                Order item = (Order) OrdersListBox.SelectedItem;
                db.Orders.Remove(db.Orders.Find(item.OrderId));
                db.SaveChanges();
                FillContractorOrders();
                ClearContractorDetails();
            }
        }

        private void ContractorList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewContractor form = new NewContractor((Contractor)ContractorList.SelectedItem);
            form.Show();
            form.Closed += Form_Closed;
        }
    }
}
