﻿using EzioDll;
using LabelPrinter.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace LabelPrinter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GodexPrinter Printer = new GodexPrinter();
        MainViewModel main;

        public MainWindow()
        {
            main = new MainViewModel();
            DataContext = main;
        }

        void UpdateLabelEvent(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;

            if (vm != null)
            {
                vm.PreviewLabel();
            }
        }
        //------------------------------------------------------------------------
        // Label Setup
        //------------------------------------------------------------------------
        private void LabelSetup()
        {
            PaperMode value = PaperMode.PlainPaperLabel;
            Printer.Config.LabelMode(value, 40, 3);
            Printer.Config.LabelWidth(54);
            Printer.Config.Dark(10);
            Printer.Config.Speed(3);
            Printer.Config.PageNo(1);
            Printer.Config.CopyNo(1);
        }
        private void DisconnectPrinter()
        {
            Printer.Close();
        }
        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            int PosX = 10;
            int PosY = 10;
            int FontHeight = 34;
            
            Printer.Open(PortType.USB);
            LabelSetup();
            
            // Print Text
            Printer.Command.Start();
            Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "First Printing");
            Printer.Command.PrintBarCode(BarCodeType.Code39, PosX, PosY += 40, "123123");
            //Printer.Command.PrintImage(PosX, PosY += 40, "", 0);
            Printer.Command.End();
           
            DisconnectPrinter();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter objWriter = new StreamWriter("NORSEL-IMPORT.txt"))
            {
                objWriter.Write(Row1Text.Text+"\n");
                objWriter.Write(Row2Text.Text+"\n");
                objWriter.Write(Row3Text.Text);
                MessageBox.Show("Details have been saved");
            }

        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            var desiredText = File.ReadAllLines("NORSEL-IMPORT.txt");
            if (desiredText.Length > 0)
                Row1Text.Text = desiredText[0];
            if (desiredText.Length > 1)
                Row2Text.Text = desiredText[1];
            if(desiredText.Length>2)
                Row3Text.Text = desiredText[2];
        }
    }
}
