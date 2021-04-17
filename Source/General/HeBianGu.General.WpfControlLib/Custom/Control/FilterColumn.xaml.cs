﻿using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public partial class FilterColumn : ContentControl
    {
        public FilterColumn()
        {
            CommandBinding binding = new CommandBinding(CommandService.Sure, (l, k) =>
               {
                   if (this.DataSource == null) return;

                   //  Do ：刷新输出数据源 
                   var cache = Activator.CreateInstance(this.DataSource.GetType()) as IList;

                   foreach (var item in this.DataSource)
                   {
                       if (this.Filter == null )
                       {
                           cache.Add(item);
                       }
                       else
                       {
                           bool m = this.Filter.IsMatch(item);

                           if (!m) continue;

                           cache.Add(item);
                       }
                   }

                   this.OutSource = cache;
               });

            this.CommandBindings.Add(binding);

            this.Loaded +=(l, k) =>
             {
                 this.RefreshSetting();
             };

        }


        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(FilterColumn), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 FilterColumn control = d as FilterColumn;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

                 control.RefreshSetting();

             }));


        public IEnumerable OutSource
        {
            get { return (IEnumerable)GetValue(OutSourceProperty); }
            set { SetValue(OutSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutSourceProperty =
            DependencyProperty.Register("OutSource", typeof(IEnumerable), typeof(FilterColumn), new PropertyMetadata(default(IEnumerable), (d, e) =>
            {
                FilterBox control = d as FilterBox;

                if (control == null) return;

                IEnumerable config = e.NewValue as IEnumerable;

            }));

        void RefreshSetting()
        {
            if (this.DataSource == null) return;

            var ctype = this.DataSource.GetType();

            if (!ctype.IsGenericType) return;

            var type = ctype.GetGenericArguments()?.FirstOrDefault();

            if (type == null) return;

            var p = type.GetProperty(this.PropertyName);

            this.Filter = FilterFactory.Create(p, this.DataSource);

            this.OutSource = this.DataSource;
        }


        public IFilter Filter
        {
            get { return (IFilter)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilter), typeof(FilterColumn), new PropertyMetadata(default(IFilter), (d, e) =>
             {
                 FilterColumn control = d as FilterColumn;

                 if (control == null) return;

                 IFilter config = e.NewValue as IFilter;

             }));


        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(FilterColumn), new PropertyMetadata(default(string), (d, e) =>
             {
                 FilterColumn control = d as FilterColumn;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));
    }
}
