using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegateApp
{
    public partial class FrmVote : Form
    {
        //【3】创建委托变量(将来通过外部赋值)
        public startVoteDelegate voteDelegate = null;
        public FrmVote()
        {
            InitializeComponent();
            this.btnVote1.Click += new System.EventHandler(this.btnVote_Click);
            this.btnVote2.Click += new System.EventHandler(this.btnVote_Click);
            this.btnVote3.Click += new System.EventHandler(this.btnVote_Click);
        }

        //开始投票
        private void btnVote_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //【5】通过委托变量将数据传递过去
            voteDelegate(Convert.ToInt32(btn.Tag));
            //投票后按钮禁用
            foreach (Control item in this.Controls)
            {
                if (item is Button)
                {
                    item.Enabled = false;
                    this.Text = "投票完成！";
                }
            }
        }
    }
}
