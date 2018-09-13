using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;//反射使用的命名空间

namespace DelegateApp
{
    public partial class FrmShowRestult : Form
    {
        //创建集保存嘉宾对象
        private Dictionary<int, Guest> dicCuest = null;
        //创建投票器窗体集合
        private List<Form> frmList = new List<Form>();

        public FrmShowRestult()
        {
            InitializeComponent();

            //初始化嘉宾对象集合
            this.dicCuest = new Dictionary<int, Guest>()
            {
                [1] = new Guest { Number = 1, Name = "李**" },
                [2] = new Guest { Number = 2, Name = "刘**" },
                [3] = new Guest { Number = 3, Name = "王**" }
            };

            this.btnEndVote.Enabled = false;
        }
        //【2】根据委托原型编写对应的方法
        private void Receiver(int num)
        {
            //根据序号找到嘉宾对象，并使投票总数加1
            this.dicCuest[num].VoteCounter++;
            //同步显示投票结果
            this.lblCounter1.Text = this.dicCuest[1].VoteCounter.ToString();
            this.lblCounter2.Text = this.dicCuest[2].VoteCounter.ToString();
            this.lblCounter3.Text = this.dicCuest[3].VoteCounter.ToString();
        }

        //创建一组投票对象
        private void btnStartVote_Click(object sender, EventArgs e)
        {
            int counter = Convert.ToInt32(this.txtCounter.Text.Trim());
            for (int i = 0; i < counter; i++)
            {
                //通用反射创建窗体（根据一个类的完全限定名创建对象的方法）
                FrmVote frmVote = (FrmVote)Assembly.Load("DelegateApp").CreateInstance("DelegateApp.FrmVote");
                frmVote.Text = "投票器：" + (i + 1).ToString();

                //【4】将投票器窗体对象中的委托变量和当前窗体中的委托实现方法关联
                frmVote.voteDelegate = this.Receiver;
                frmVote.Show();

                //添加投票窗体到集合
                this.frmList.Add(frmVote);
                this.btnEndVote.Enabled = true;
            }
        }

        private void btnEndVote_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.frmList)
            {
                item.Close();
            }
            foreach (var item in dicCuest)
            {
                item.Value.VoteCounter = 0;
               
            }
            this.lblCounter1.Text = this.dicCuest[1].VoteCounter.ToString();
            this.lblCounter2.Text = this.dicCuest[2].VoteCounter.ToString();
            this.lblCounter3.Text = this.dicCuest[3].VoteCounter.ToString();

            this.btnEndVote.Enabled = false;


        }
    }
    //【1】声明委托（方法原型）
    public delegate void startVoteDelegate(int num);
}
