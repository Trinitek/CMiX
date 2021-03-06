﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CMiX
{
    public partial class ChannelLayer : UserControl
    {
        Messenger message = new Messenger();
        ChannelData cd = new ChannelData();

        public ChannelLayer()
        {
            InitializeComponent();

        }

        private List<string> _ChannelsBlendMode = new List<string>(new[] { "Normal", "Add", "Substract", "Lighten", "Darken", "Multiply" });
        public List<string> ChannelsBlendMode
        {
            get { return _ChannelsBlendMode; }
            set { _ChannelsBlendMode = value; }
        }

        public static readonly DependencyProperty EnabledOSCProperty =
        DependencyProperty.Register("EnabledOSC", typeof(bool), typeof(ChannelLayer), new PropertyMetadata(false));
        [Bindable(true)]
        public bool EnabledOSC
        {
            get { return (bool)GetValue(EnabledOSCProperty); }
            set { SetValue(EnabledOSCProperty, value); }
        }

        public static readonly DependencyProperty MasterPeriodProperty =
        DependencyProperty.Register("MasterPeriod", typeof(double), typeof(ChannelLayer), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double MasterPeriod
        {
            get { return (double)GetValue(MasterPeriodProperty); }
            set { SetValue(MasterPeriodProperty, value); }
        }

        public static readonly DependencyProperty LayerNameProperty =
        DependencyProperty.Register("LayerName", typeof(string), typeof(ChannelLayer), new PropertyMetadata(""));
        [Bindable(true)]
        public string LayerName
        {
            get { return (string)GetValue(LayerNameProperty); }
            set { SetValue(LayerNameProperty, value); }

        }
        private void SliderValueChanged(object sender, EventArgs e)
        {
            if (EnabledOSC == true)
            {
                CustomSlider slider = sender as CustomSlider;
                cd = DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + slider.Name, slider.Value.ToString());
            }
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnabledOSC == true)
            {
                ComboBox combobox = sender as ComboBox;
                if(combobox.SelectedItem != null)
                {
                    cd = DataContext as ChannelData;
                    message.SendOSC(cd.Name + "/" + combobox.Name, combobox.SelectedItem.ToString());
                }
            }
        }

        private void BeatControlsChanged(object sender, EventArgs e)
        {
            if (EnabledOSC == true)
            {
                BeatControls beatcontrols = sender as BeatControls;
                cd = DataContext as ChannelData;
                message.SendOSC(cd.Name + "/" + beatcontrols.Name, beatcontrols.Multiplier.ToString());
            }
        }


        private void ChannelLayer_Loaded(object sender, RoutedEventArgs e)
        {
            //Ch1.IsChecked = true;
        }

        private void Ch_Control_Loaded(object sender, RoutedEventArgs e)
        {
            EnabledOSC = true;
        }
    }
}
