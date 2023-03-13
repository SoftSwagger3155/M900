using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionCard
{
    public enum SignalImageList
    {
        Gray,
        Grenn
    }
    public partial class InSignalLed : UserControl
    {
        public InSignalLed()
        {
            InitializeComponent();
        }

        private MotionBase motion;
        public MotionBase Motion
        {
            set { motion = value; }
        }

        private CardType cardSelect;
        public CardType CardSelect
        {
            get { return cardSelect; }
            set
            {
                cardSelect = value;
                switch (CardSelect)
                {
                    case CardType.正运动:
                        //this.mationBase = new Zmotion();
                        break;
                    case CardType.固高:
                        break;
                }
            }
        }

        public override void Refresh()
        {
            bool inSignal = false;
            motion.GetInSignal(ioNum, ref inSignal);
            this.Invoke(new Action(() =>
            {
                base.Refresh();
                {
                    ImageSelect = inSignal ? SignalImageList.Grenn : SignalImageList.Gray;
                }
            }));
        }

        public int CardIndex { get; set; }

        private int ioNum;
        [Category("自定义属性")]
        public int IoNum
        {
            get { return ioNum; }
            set { ioNum = value; }
        }
        private SignalImageList imageSelect = SignalImageList.Gray;
        public SignalImageList ImageSelect
        {
            get { return imageSelect; }
            set
            {
                imageSelect = value;
                switch (imageSelect)
                {
                    case SignalImageList.Gray:
                        label1.Image = MotionCard.Properties.Resources.Circle_Gray;
                        break;
                    case SignalImageList.Grenn:
                        label1.Image = MotionCard.Properties.Resources.Circle_Green;
                        break;
                }
            }
        }
        [Category("自定义属性")]
        public string SignalName
        {
            get { return label2.Text; }
            set
            {
                label2.Text = value;
            }
        }
    }
}
