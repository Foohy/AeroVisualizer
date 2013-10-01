using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RuleManager;

namespace AeroVisualizerRedux
{
    public partial class RuleEdit : Form
    {
        public RuleManager.Rule RuleInfo { get; private set; }

        public RuleEdit(RuleCollection collection, RuleManager.Rule loadRule)
        {
            InitializeComponent();

            //Add currently registered outputs
            foreach (var output in collection.Outputs)
            {
                comboOutput.Items.Add(output);
            }
            comboOutput.SelectedIndex = 0;

            //Add some output modes
            var modes = Enum.GetNames(typeof(AppendMethod));
            foreach (var mode in modes)
            {
                comboOutputMode.Items.Add(mode);
            }
            comboOutputMode.SelectedIndex = 0;

            //If we were loaded with a rule in mind, load all of its info
            if (loadRule != null)
            {
                textBoxName.Text = loadRule.Name;
                comboOutput.SelectedItem = loadRule.OutputVal;
                comboOutputMode.SelectedItem = loadRule.AppendMode;
                textBoxModifier.Text = loadRule.ModifierString;
            }
        }

        private void RuleEdit_Load(object sender, EventArgs e)
        {
            //Load output info
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Rule information-ASSEMBLE
            RuleInfo = new RuleManager.Rule();
            RuleInfo.Name = textBoxName.Text;
            RuleInfo.SetOutput((Output)comboOutput.SelectedItem);
           // RuleInfo.SetAppendMode((AppendMethod)comboOutputMode.SelectedItem);
            bool success = RuleInfo.SetModiferString(textBoxModifier.Text);
            if (!success) return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
