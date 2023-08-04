﻿using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Creator.View
{
    /// <summary>
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SettingDialog : UserControl
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            MessageProxy.Container.Close();
        }

        private void Button_Sumit(object sender, RoutedEventArgs e)
        {
            OnSumit();

            Result = true;

            MessageProxy.Container.Close();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SettingDialog));
        //CLR事件包装
        public event RoutedEventHandler Sumit
        {
            add { AddHandler(SumitRoutedEvent, value); }
            remove { RemoveHandler(SumitRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumit()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitRoutedEvent, this);
            RaiseEvent(args);
        }


        public bool Result { get; private set; }

    }


}
