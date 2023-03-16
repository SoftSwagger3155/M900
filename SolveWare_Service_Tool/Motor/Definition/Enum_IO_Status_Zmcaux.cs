using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveWare_Service_Tool.Motor.Definition
{
    public enum IO_Status_Zmcaux
    {
        随动误差超限告警 = 2,
        与远程轴通讯出错 = 4,
        远程驱动器报错 =8,
        正向硬限位 = 16,
        反向硬限位 = 32,
        找原点中 = 64,
        HOLD速度保持信号输入 = 128,
        随动误差超限出错 = 256,
        超过正向软限位 = 512,
        超过负向软限位 = 1024,
        CANCEL执行中 = 2048,
        脉冲频率超过MAX_SPEED限制需要修改降速或修改MAX_SPEED = 4096,
        机械手指令坐标错误 = 16384,
        电源异常 = 262144,
        精准输出缓冲溢出 = 524288,
        运动中触发特殊运动指令失败 = 2097152,
        告警信号输入 = 4194304,
        轴进入了暂停状态 = 8388608
    }
}
