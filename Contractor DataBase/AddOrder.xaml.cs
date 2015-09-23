using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using Contractor_DataBase.DataModels;

namespace Contractor_DataBase
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : MetroWindow
    {
        private readonly Contractor _contractor;
        private readonly Order _order;
        private ICollection<PositionsQty> GridItems;

        public AddOrder(Contractor item)
        {
            InitializeComponent();
            _contractor = item;
            OrderPositions.Columns[0].Width = OrderPositions.Width*70/100;
            OrderPositions.Columns[1].Width = OrderPositions.Width * 30 / 100;
            HandleStuff();
            GridItems = new List<PositionsQty>();
            OrderPositions.ItemsSource = GridItems;
            this.DataContext = this;
        }

        public AddOrder(Contractor contractor, Order item)
        {
            InitializeComponent();
            _contractor = contractor;
            _order = item;
            OrderPositions.Columns[0].Width = OrderPositions.Width * 70 / 100;
            OrderPositions.Columns[1].Width = OrderPositions.Width * 30 / 100;
            HandleStuff();
            GridItems = new List<PositionsQty>();
            FillOrderInfo();
            OrderPositions.ItemsSource = GridItems;
            this.DataContext = this;
        }

        private void HandleStuff()
        {
            PositionBox.GotFocus += RemoveTextPos;
            PositionBox.LostFocus += AddTextPos;
            QtyBox.GotFocus += RemoveText;
            QtyBox.LostFocus += AddText;
        }
        private void FillOrderInfo()
        {
            InvoiceNumberBox.Text = _order.InvoiceNumber;
            OrderDatePick.SelectedDate = _order.OrderDate;
            DeliveryDatePick.SelectedDate = _order.DeliveryDate;
            InvoiceSumBox.Text = _order.Sum.ToString(CultureInfo.CurrentCulture);
            using (var db = new ContractorContext())
            {
                foreach (var item in db.PositionsQties.ToList().Where(item => item.OrderId == _order.OrderId))
                {
                    GridItems.Add(item);
                }

            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ContractorContext())
            {
                if (_order == null)
                {
                    Order order = new Order();
                    if (InvoiceNumberBox.Text != null)
                        order.InvoiceNumber = InvoiceNumberBox.Text;
                    order.DeliveryDate = DeliveryDatePick.SelectedDate ?? DateTime.Now;
                    order.OrderDate = OrderDatePick.SelectedDate ?? DateTime.Now;
                    if (InvoiceSumBox.Text != null)
                        order.Sum = Decimal.Parse(InvoiceSumBox.Text.Replace('.', ','));
                    order.ContractorId = _contractor.ContractorId;
                    order.PositionsQty = new List<PositionsQty>();
                    foreach (PositionsQty item in GridItems)
                    {
                        db.PositionsQties.AddOrUpdate(item);
                    }
                    db.Orders.AddOrUpdate(order);
                    db.SaveChanges();
                    this.Close();
                }
                else
                {
                    Order order = _order;
                    if (InvoiceNumberBox.Text != null)
                        order.InvoiceNumber = InvoiceNumberBox.Text;
                    order.DeliveryDate = DeliveryDatePick.SelectedDate ?? DateTime.Now;
                    order.OrderDate = OrderDatePick.SelectedDate ?? DateTime.Now;
                    if (InvoiceSumBox.Text != null)
                        order.Sum = decimal.Parse(InvoiceSumBox.Text.Replace('.', ','));
                    order.ContractorId = _contractor.ContractorId;
                    order.PositionsQty = new List<PositionsQty>();
                    foreach (PositionsQty item in GridItems)
                    {
                        db.PositionsQties.AddOrUpdate(item);
                    }
                    db.Orders.AddOrUpdate(order);
                    db.SaveChanges();
                    this.Close();
                }
                
            }
        }

        private void AddItem()
        {
            var item = new PositionsQty()
            {
                Position = PositionBox.Text,
                Qty = int.Parse(QtyBox.Text)
            };
            if (_order != null) item.OrderId = _order.OrderId;
            GridItems.Add(item);
            QtyBox.Text = "Количество";
            PositionBox.Text = "Новая Позиция";
            OrderPositions.Items.Refresh();
        }

        private void AddPositionItemBtn_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }


        private void RemoveText(object sender, EventArgs e)
        {
            QtyBox.Text = "";
        }

        private void AddText(object sender, EventArgs e)
        {
            if (QtyBox.Text == "")
                QtyBox.Text = "Количество";
        }

        private void RemoveTextPos(object sender, EventArgs e)
        {
            PositionBox.Text = "";
        }

        private void AddTextPos(object sender, EventArgs e)
        {
            if (PositionBox.Text == "")
                PositionBox.Text = "Новая Позиция";
        }

        private void delPositionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_order != null)
            {
                using (var db = new ContractorContext())
                {
                    if (((PositionsQty)OrderPositions.SelectedItem).OrderId == _order.OrderId)
                    {
                        db.PositionsQties.Remove(db.PositionsQties.Find(((PositionsQty)OrderPositions.SelectedItem).Id));
                        db.SaveChanges();
                    }
                }
            }
            GridItems.Remove((PositionsQty)OrderPositions.SelectedItem);
            OrderPositions.Items.Refresh();
        }
    }
}
