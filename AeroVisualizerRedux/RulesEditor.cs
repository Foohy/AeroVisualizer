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
    public partial class RulesEditor : Form
    {
        Main MainForm;

        public RulesEditor( Main parentForm )
        {
            InitializeComponent();

            this.MainForm = parentForm;

            //Load in the current rules
            LoadRules(this.MainForm.AudioRules);
        }

        public ListViewItem AddRule( string name, string modifier, Output output, AppendMethod appendmode )
        {
            var item = listRules.Items.Add( name );
            item.SubItems.Add(modifier).Tag = modifier;
            item.SubItems.Add(output.Name).Tag = output;
            item.SubItems.Add( appendmode.ToString() ).Tag = appendmode;

            return item;
        }

        public void ApplyRules()
        {
            MainForm.AudioRules.Clear();
            foreach (ListViewItem item in listRules.Items)
            {
                RuleManager.Rule rule = new RuleManager.Rule();
                rule.SetModiferString((string)item.SubItems[1].Tag);
                rule.SetOutput((Output)item.SubItems[2].Tag);
                MainForm.AudioRules.Add(rule);
            }
        }

        private void LoadRules(RuleCollection rules)
        {
            listRules.Items.Clear();
            foreach (var rule in rules)
            {
                AddRule(rule.Name, rule.ModifierString, rule.OutputVal, rule.AppendMode).Tag = rule;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RuleEdit editor = new RuleEdit(this.MainForm.AudioRules, null);
            if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var rule = editor.RuleInfo;
                AddRule(rule.Name, rule.ModifierString, rule.OutputVal, rule.AppendMode).Tag = rule;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listRules.SelectedItems.Count < 1) return;

            var selected = listRules.SelectedItems[0];
            if (selected == null || selected.Tag == null || !(selected.Tag is RuleManager.Rule)) return;

            RuleEdit editor = new RuleEdit(this.MainForm.AudioRules, (RuleManager.Rule)selected.Tag);
            if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Remove the rule we're replacing
                listRules.Items.Remove(selected);

                var rule = editor.RuleInfo;
                AddRule(rule.Name, rule.ModifierString, rule.OutputVal, rule.AppendMode).Tag = rule;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyRules();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ApplyRules();
            this.Close();
        }
    }
}
