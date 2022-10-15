using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DivinationApp
{
    /// <summary>
    /// 十二親干法 表示画面
    /// </summary>
    public partial class FormJuniSinKanHou : DialogBase
    {
        public delegate void CloseHandler();


        JuniSinKanHou juniSinKanHou = null;

        DrawJuniSinKanhoun drawJuniSinKanhoun = null;


        //文字描画領域サイズ
        public FormJuniSinKanHou()
        {
            InitializeComponent();

            juniSinKanHou = new JuniSinKanHou();
            drawJuniSinKanhoun = new DrawJuniSinKanhoun();

        }
        public void Update(Person person)
        {
            var node = juniSinKanHou.Create(person);

            drawJuniSinKanhoun.Draw(person, pictureBox1, node);

        }

    }
}