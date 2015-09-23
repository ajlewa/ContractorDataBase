using System.Data.Entity.Migrations;
using System.Windows;
using MahApps.Metro.Controls;
using Contractor_DataBase.DataModels;

namespace Contractor_DataBase
{
    /// <summary>
    /// Interaction logic for NewContractor.xaml
    /// </summary>
    public partial class NewContractor : MetroWindow
    {
        private Contractor _contractor;
        public NewContractor()
        {
            InitializeComponent();
        }

        public NewContractor(Contractor _contractor)
        {
            InitializeComponent();
            this._contractor = _contractor;
            ContractorNameBox.Text = _contractor.ContractorName;
            ManagerNameBox.Text = _contractor.ManagerName;
            Email.Text = _contractor.ContactEmail;
            PhoneNumber.Text = _contractor.ContactPhone;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_contractor != null)
            {
                using (var db = new ContractorContext())
                {
                    _contractor.ContractorName = ContractorNameBox.Text;
                    _contractor.ManagerName = ManagerNameBox.Text;
                    _contractor.ContactEmail = Email.Text;
                    _contractor.ContactPhone = PhoneNumber.Text;

                    db.Contractors.AddOrUpdate(_contractor);
                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new ContractorContext())
                {
                    Contractor nc = new Contractor()
                    {
                        ContractorName = ContractorNameBox.Text,
                        ManagerName = ManagerNameBox.Text,
                        ContactEmail = Email.Text,
                        ContactPhone = PhoneNumber.Text
                    };

                    db.Contractors.AddOrUpdate(nc);
                    db.SaveChanges();
                }
            }
            this.Close();
        }
    }
}
