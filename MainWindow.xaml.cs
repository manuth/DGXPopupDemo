﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using MahApps.Metro.Controls;

namespace DGXPopupDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            dataGrid.DataContext = new List<Customer>()
            {
                new Customer()
                {
                    ID = 0,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = new MailAddress("john.doe@contoso.com"),
                    IsMember = false,
                    Status = OrderStatus.Pending
                },
                new Customer()
                {
                    ID = 12,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = new MailAddress("jane.doe@contoso.com"),
                    IsMember = true,
                    Status = OrderStatus.Paid
                },
                new Customer()
                {
                    ID = 23,
                    FirstName = "Max",
                    LastName = "Mustermann",
                    Email = new MailAddress("max.mustermann@contoso.com"),
                    IsMember = true,
                    Status = OrderStatus.Shipped
                },
                new Customer()
                {
                    ID = 34,
                    FirstName = "Jack",
                    LastName = "Black",
                    Email = new MailAddress("jack.black@contoso.com"),
                    IsMember = true,
                    Status = OrderStatus.Finished
                }
            };
        }
    }
}
