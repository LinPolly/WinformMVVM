﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformMVVM
{
    public partial class Form1 : Form
    {
        private List<IDisposable> Disposables { get; } = new List<IDisposable>();
        private SampleVModel SampleVModel { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SampleVModel = new SampleVModel();

            label1.DataBindings.Add(nameof(label1.Text), SampleVModel, nameof(SampleVModel.Counter));

            Disposables.Add(btnIncrement.BindCommand(SampleVModel.IncrementCommand));
            Disposables.Add(btnReset.BindCommand(SampleVModel.ResetCommand));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in Disposables)
            {
                item?.Dispose();
            }
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            SampleVModel.IncrementCommand.ExecuteAsync();
            //Thread.Sleep(3000);
        }
    }
}
