using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotionCard
{
    public class Zmotion: MotionBase
    {
        private int PciCardsCount;
        private string IPAddress = string.Empty;
        private bool isPci = false;

        private IntPtr Handle;

        public bool InitedOK;

        public Zmotion()
        {
            PciCardsCount= zmcaux.ZAux_GetMaxPciCards();
            isPci = true;
            CardTypeSelect = CardType.正运动;
        }
        public Zmotion(string IPAddress = "192.168.0.11")
        {
            this.IPAddress = IPAddress;
            isPci = false;
            CardTypeSelect = CardType.正运动;
        }
        /// <summary>
        /// 搜索IP地址集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetEhList()
        {
            StringBuilder ipAddressList = new StringBuilder(100);

            uint addbufferlength = 1000;

            int error = zmcaux.ZAux_SearchEthlist(ipAddressList, addbufferlength, 10);

            if (error == 0)
            {
                string result = ipAddressList.ToString().Trim();

                if (result.Contains(' '))
                {

                    return result.Split(' ').ToList();
                }
                else
                {
                    return new List<string>() { result };
                }

            }
            return new List<string>();
        }

        /// <summary>
        /// 初始化板卡
        /// </summary>
        /// <returns></returns>
        public override OperationResult InitCard()
        {
            
            int error = 0;
            if (isPci)
            {
                for (int i = 0; i < PciCardsCount - 1; i++)
                {
                    error = zmcaux.ZAux_OpenPci(Convert.ToUInt32(i), out Handle);
                }
            }
            else
            {
                error = zmcaux.ZAux_OpenEth(this.IPAddress, out Handle);
            }
            //InitedOK = true;
            if (error == 0 && Handle != IntPtr.Zero)
            {
                InitedOK = true;
                return OperationResult.CreateSuccessResult();
            }
            else
            {
                InitedOK = false;
                return OperationResult.CreateFailResult();
            }
        }

        /// <summary>
        /// 关闭板卡
        /// </summary>
        /// <returns></returns>
        public override OperationResult CloseCard()
        {

            if (zmcaux.ZAux_Close(Handle) == 0)
            {
                Handle = IntPtr.Zero;
                InitedOK = false;
                return OperationResult.CreateSuccessResult();
            }
            return OperationResult.CreateFailResult();
        }

        /// <summary>
        /// 设置脉冲当量
        /// </summary>
        public override void SetPulseEquival(Dictionary<float,int> dicPulse)
        {
            int node = 0;
            zmcaux.ZAux_BusCmd_GetNodeNum(Handle, 0, ref node);
            if (node != 0)
            {
                for (int i = 0; i < node; i++)
                {
                    zmcaux.ZAux_Direct_SetUnits(Handle, i, dicPulse[i]);
                }
            }
        }

        /// <summary>
        /// 连续运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">运行速度</param>
        /// <param name="dir">方向</param>
        /// <param name="velMin">最小速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="sramp">S曲线时间</param>
        /// <returns>操作结果</returns>
        public override OperationResult VMove(short axis, float vel, bool dir, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //创建错误码
            int error = 0;

            try
            {
                /*
                 Atype类型 描述
                0 虚拟轴。
                1 脉冲方向方式的步进或伺服 。
                2 模拟信号控制方式的伺服 。
                3 正交编码器 。
                4 步进+编码器 。
                6 脉冲方向方式的编码器，可用于手轮输入。
                7 脉冲方向方式步进或伺服+EZ信号输入。
                8 ZCAN扩展脉冲方向方式步进或伺服 。
                9 ZCAN扩展正交编码器。
                10 ZCAN扩展脉冲方向方式的编码器。
                 */

                //设置轴类型
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_Vmove(Handle, axis, dir ? 1 : -1);
                ErrorHandler("ZAux_Direct_Single_Vmove", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="distance"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public override OperationResult MoveRelative(short axis, float vel, float distance, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //创建错误码
            int error = 0;

            try
            {
                //设置轴类型

                /*
                 Atype类型 描述
                0 虚拟轴。
                1 脉冲方向方式的步进或伺服 。
                2 模拟信号控制方式的伺服 。
                3 正交编码器 。
                4 步进+编码器 。
                6 脉冲方向方式的编码器，可用于手轮输入。
                7 脉冲方向方式步进或伺服+EZ信号输入。
                8 ZCAN扩展脉冲方向方式步进或伺服 。
                9 ZCAN扩展正交编码器。
                10 ZCAN扩展脉冲方向方式的编码器。
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                

                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_Move(Handle, axis, distance);
                ErrorHandler("ZAux_Direct_Single_Move", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }
      
        public override float ReadAbsSupeed(short axis)
        {
            float value = 0;
            zmcaux.ZAux_Direct_GetSpeed(Handle, axis, ref value);
            return value;
        }
        /// <summary>
        /// 绝对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="pos"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public override OperationResult MoveAbs(short axis, float vel, float pos, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);
            if (!result.IsSuccess) return result;

            if (Math.Abs(GetPos(axis) - pos) < 15)
                vel = vel / 10.0f;
            //创建错误码
            int error = 0;

            try
            {
                /*
                 Atype类型 描述
                0 虚拟轴。
                1 脉冲方向方式的步进或伺服 。
                2 模拟信号控制方式的伺服 。
                3 正交编码器 。
                4 步进+编码器 。
                6 脉冲方向方式的编码器，可用于手轮输入。
                7 脉冲方向方式步进或伺服+EZ信号输入。
                8 ZCAN扩展脉冲方向方式步进或伺服 。
                9 ZCAN扩展正交编码器。
                10 ZCAN扩展脉冲方向方式的编码器。
                 */
                //设置轴类型
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_MoveAbs(Handle, axis, pos);
                ErrorHandler("ZAux_Direct_Single_MoveAbs", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        public override OperationResult MoveAbs(short axis, float pos,AxisSpeed axisSpeed,float percentum, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);
            if (!result.IsSuccess) return result;
            float vec = axisSpeed.vel * percentum;
            if (Math.Abs(GetPos(axis) - pos) < 15)
                vec = axisSpeed.vel / 10.0f;
            //创建错误码
            int error = 0;

            try
            {
                //设置轴类型
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, axisSpeed.minVel);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vec);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, axisSpeed.acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, axisSpeed.dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_MoveAbs(Handle, axis, pos);
                ErrorHandler("ZAux_Direct_Single_MoveAbs", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        /// <summary>
        /// 带锁的轴连续运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">运行速度</param>
        /// <param name="dir">方向</param>
        /// <param name="velMin">最小速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="sramp">S曲线时间</param>
        /// <returns>操作结果</returns>
        public override OperationResult LockVMove(short axis,int ioNum, float vel, bool dir, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;
            SetOutSignal(ioNum, 1);
            //创建错误码
            int error = 0;

            try
            {
                //设置轴类型

                /*
                 Atype类型 描述
                0 虚拟轴。
                1 脉冲方向方式的步进或伺服 。
                2 模拟信号控制方式的伺服 。
                3 正交编码器 。
                4 步进+编码器 。
                6 脉冲方向方式的编码器，可用于手轮输入。
                7 脉冲方向方式步进或伺服+EZ信号输入。
                8 ZCAN扩展脉冲方向方式步进或伺服 。
                9 ZCAN扩展正交编码器。
                10 ZCAN扩展脉冲方向方式的编码器。
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_Vmove(Handle, axis, dir ? 1 : -1);
                ErrorHandler("ZAux_Direct_Single_Vmove", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        /// <summary>
        /// 带锁的轴相对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="distance"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public override OperationResult LockMoveRelative(short axis,int ioNum, float vel, float distance, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;
            SetOutSignal(ioNum, 1);
            //创建错误码
            int error = 0;

            try
            {
                //设置轴类型

                /*
                 Atype类型 描述
                0 虚拟轴。
                1 脉冲方向方式的步进或伺服 。
                2 模拟信号控制方式的伺服 。
                3 正交编码器 。
                4 步进+编码器 。
                6 脉冲方向方式的编码器，可用于手轮输入。
                7 脉冲方向方式步进或伺服+EZ信号输入。
                8 ZCAN扩展脉冲方向方式步进或伺服 。
                9 ZCAN扩展正交编码器。
                10 ZCAN扩展脉冲方向方式的编码器。
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

               

                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_Move(Handle, axis, distance);
                ErrorHandler("ZAux_Direct_Single_Move", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// 带锁的轴绝对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="pos"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public override OperationResult LockMoveAbs(short axis,int ioNum, float vel, float pos, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;
            SetOutSignal(ioNum, 1);

            //创建错误码
            int error = 0;

            try
            {
                //设置轴类型

                /*
                 Atype类型 描述
                0 虚拟轴。
                1 脉冲方向方式的步进或伺服 。
                2 模拟信号控制方式的伺服 。
                3 正交编码器 。
                4 步进+编码器 。
                6 脉冲方向方式的编码器，可用于手轮输入。
                7 脉冲方向方式步进或伺服+EZ信号输入。
                8 ZCAN扩展脉冲方向方式步进或伺服 。
                9 ZCAN扩展正交编码器。
                10 ZCAN扩展脉冲方向方式的编码器。
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);


                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置S曲线
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //设置方向并运动
                error = zmcaux.ZAux_Direct_Single_MoveAbs(Handle, axis, pos);
                ErrorHandler("ZAux_Direct_Single_MoveAbs", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }

            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// 2轴相对定位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="distance"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public OperationResult Move2DRelative(short[] axis, float[] vel, float[] distance, float[] velMin, float[] acc, float[] dec, float[] sramp)
        {
            if (axis.Length == 2 && vel.Length == 2 && distance.Length == 2 && velMin.Length == 2 && acc.Length == 2 && dec.Length == 2 && sramp.Length == 2)
            {
                OperationResult result = new OperationResult();

                //相对定位
                for (int i = 0; i < 2; i++)
                {
                    result = MoveRelative(axis[i], vel[i], distance[i], velMin[i], acc[i], dec[i], sramp[i]);

                    if (!result.IsSuccess) return result;
                }

                //等待停止
                for (int i = 0; i < 2; i++)
                {
                    result = WaitStop(axis[i]);

                    if (!result.IsSuccess) return result;
                }

                return OperationResult.CreateSuccessResult();

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数长度不正确"
            };

        }

        /// <summary>
        /// 2轴绝对定位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="distance"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public OperationResult Move2DAbs(short[] axis, float[] vel, float[] pos, float[] velMin, float[] acc, float[] dec, float[] sramp)
        {
            if (axis.Length == 2 && vel.Length == 2 && pos.Length == 2 && velMin.Length == 2 && acc.Length == 2 && dec.Length == 2 && sramp.Length == 2)
            {
                OperationResult result = new OperationResult();

                //相对定位
                for (int i = 0; i < 2; i++)
                {
                    result = MoveAbs(axis[i], vel[i], pos[i], velMin[i], acc[i], dec[i], sramp[i]);

                    if (!result.IsSuccess) return result;
                }

                //等待停止
                for (int i = 0; i < 2; i++)
                {
                    result = WaitStop(axis[i]);

                    if (!result.IsSuccess) return result;
                }

                return OperationResult.CreateSuccessResult();

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数长度不正确"
            };

        }


        /// <summary>
        /// 3轴相对定位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="distance"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public OperationResult Move3DRelative(short[] axis, float[] vel, float[] distance, float[] velMin, float[] acc, float[] dec, float[] sramp)
        {
            if (axis.Length == 3 && vel.Length == 3 && distance.Length == 3 && velMin.Length == 3 && acc.Length == 3 && dec.Length == 3 && sramp.Length == 3)
            {
                OperationResult result = new OperationResult();

                //2轴定位
                result = Move2DRelative(new short[] { axis[0], axis[1] }, new float[] { vel[0], vel[1] }, new float[] { distance[0], distance[1] },

                    new float[] { velMin[0], velMin[1] }, new float[] { acc[0], acc[1] }, new float[] { dec[0], dec[1] }, new float[] { sramp[0], sramp[1] });
                if (!result.IsSuccess) return result;


                result = MoveRelative(axis[2], vel[2], distance[2], velMin[2], acc[2], dec[2], sramp[2]);
                if (!result.IsSuccess) return result;

                //等待停止
                result = WaitStop(axis[2]);
                if (!result.IsSuccess) return result;

                return OperationResult.CreateSuccessResult();
            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数长度不正确"
            };

        }


        /// <summary>
        /// 3轴绝对定位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <param name="distance"></param>
        /// <param name="velMin"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="sramp"></param>
        /// <returns></returns>
        public OperationResult Move3DAbs(short[] axis, float[] vel, float[] distance, float[] velMin, float[] acc, float[] dec, float[] sramp)
        {
            if (axis.Length == 3 && vel.Length == 3 && distance.Length == 3 && velMin.Length == 3 && acc.Length == 3 && dec.Length == 3 && sramp.Length == 3)
            {
                OperationResult result = new OperationResult();

                //先动Z轴
                result = MoveAbs(axis[2], vel[2], 0.0f, velMin[2], acc[2], dec[2], sramp[2]);
                if (!result.IsSuccess) return result;

                //等待停止
                result = WaitStop(axis[2]);
                if (!result.IsSuccess) return result;

                //2轴定位
                result = Move2DAbs(new short[] { axis[0], axis[1] }, new float[] { vel[0], vel[1] }, new float[] { distance[0], distance[1] },

                    new float[] { velMin[0], velMin[1] }, new float[] { acc[0], acc[1] }, new float[] { dec[0], dec[1] }, new float[] { sramp[0], sramp[1] });
                if (!result.IsSuccess) return result;


                result = MoveAbs(axis[2], vel[2], distance[2], velMin[2], acc[2], dec[2], sramp[2]);
                if (!result.IsSuccess) return result;

                //等待停止
                result = WaitStop(axis[2]);
                if (!result.IsSuccess) return result;

                return OperationResult.CreateSuccessResult();
            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数长度不正确"
            };

        }


        /// <summary>
        /// 多轴相对直线插补
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public OperationResult MoveLineRelative(int[] axis, float[] distance, float vel, float acc, float dec)
        {

            if (axis.Length >= 2 && axis.Length == distance.Length)
            {
                OperationResult result = new OperationResult();

                //判断每个轴是否满足要求
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //选择 BASE 轴列表
                    error = zmcaux.ZAux_Direct_Base(Handle, axis.Length, axis);
                    ErrorHandler("ZAux_Direct_Base", error);

                    error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis[0], vel);
                    ErrorHandler("ZAux_Direct_SetSpeed", error);

                    error = zmcaux.ZAux_Direct_SetAccel(Handle, axis[0], acc);
                    ErrorHandler("ZAux_Direct_SetAccel", error);

                    error = zmcaux.ZAux_Direct_SetDecel(Handle, axis[0], dec);
                    ErrorHandler("ZAux_Direct_SetDecel", error);

                    error = zmcaux.ZAux_Direct_Move(Handle, axis.Length, axis, distance);
                    ErrorHandler("ZAux_Direct_Move", error);

                    return OperationResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数不正确"
            };
        }

        /// <summary>
        /// 多轴绝对直线插补
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public OperationResult MoveLineAbs(int[] axis, float[] pos, float vel, float acc, float dec)
        {

            if (axis.Length >= 2 && axis.Length == pos.Length)
            {
                OperationResult result = new OperationResult();

                //判断每个轴是否满足要求
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //选择 BASE 轴列表
                    error = zmcaux.ZAux_Direct_Base(Handle, axis.Length, axis);
                    ErrorHandler("ZAux_Direct_Base", error);

                    error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis[0], vel);
                    ErrorHandler("ZAux_Direct_SetSpeed", error);

                    error = zmcaux.ZAux_Direct_SetAccel(Handle, axis[0], acc);
                    ErrorHandler("ZAux_Direct_SetAccel", error);

                    error = zmcaux.ZAux_Direct_SetDecel(Handle, axis[0], dec);
                    ErrorHandler("ZAux_Direct_SetDecel", error);

                    error = zmcaux.ZAux_Direct_MoveAbs(Handle, axis.Length, axis, pos);
                    ErrorHandler("ZAux_Direct_Move", error);

                    return OperationResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数不正确"
            };
        }

        /// <summary>
        /// XY圆弧相对插补(圆心定位)
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="circlecenter"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public OperationResult MoveCircleRelative(int[] axis, float[] distance, float[] circlecenter,  float vel, float acc, float dec,int dir)
        {

            if (axis.Length == 2 && axis.Length == distance.Length)
            {
                OperationResult result = new OperationResult();

                //判断每个轴是否满足要求
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //选择 BASE 轴列表
                    error = zmcaux.ZAux_Direct_Base(Handle, axis.Length, axis);
                    ErrorHandler("ZAux_Direct_Base", error);

                    error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis[0], vel);
                    ErrorHandler("ZAux_Direct_SetSpeed", error);

                    error = zmcaux.ZAux_Direct_SetAccel(Handle, axis[0], acc);
                    ErrorHandler("ZAux_Direct_SetAccel", error);

                    error = zmcaux.ZAux_Direct_SetDecel(Handle, axis[0], dec);
                    ErrorHandler("ZAux_Direct_SetDecel", error);

                    error = zmcaux.ZAux_Direct_MoveCirc(Handle, axis.Length, axis, distance[0], distance[1],circlecenter[0],circlecenter[1],dir);
                    ErrorHandler("ZAux_Direct_MoveCirc", error);

                    return OperationResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数不正确"
            };
        }


        /// <summary>
        /// XY圆弧绝对插补(圆心定位)
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="circlecenter"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public OperationResult MoveCircleAbs(int[] axis, float[] distance, float[] circlecenter, float vel, float acc, float dec, int dir)
        {

            if (axis.Length == 2 && axis.Length == distance.Length)
            {
                OperationResult result = new OperationResult();

                //判断每个轴是否满足要求
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //选择 BASE 轴列表
                    error = zmcaux.ZAux_Direct_Base(Handle, axis.Length, axis);
                    ErrorHandler("ZAux_Direct_Base", error);

                    error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis[0], vel);
                    ErrorHandler("ZAux_Direct_SetSpeed", error);

                    error = zmcaux.ZAux_Direct_SetAccel(Handle, axis[0], acc);
                    ErrorHandler("ZAux_Direct_SetAccel", error);

                    error = zmcaux.ZAux_Direct_SetDecel(Handle, axis[0], dec);
                    ErrorHandler("ZAux_Direct_SetDecel", error);

                    error = zmcaux.ZAux_Direct_MoveCircAbs(Handle, axis.Length, axis, distance[0], distance[1], circlecenter[0], circlecenter[1], dir);
                    ErrorHandler("ZAux_Direct_MoveCircAbs", error);

                    return OperationResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数不正确"
            };
        }


        /// <summary>
        /// XY圆弧相对插补(中点定位)
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="midpos"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public OperationResult MoveCircle2Relative(int[] axis, float[] distance, float[] midpos, float vel, float acc, float dec)
        {

            if (axis.Length == 2 && axis.Length == distance.Length)
            {
                OperationResult result = new OperationResult();

                //判断每个轴是否满足要求
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //选择 BASE 轴列表
                    error = zmcaux.ZAux_Direct_Base(Handle, axis.Length, axis);
                    ErrorHandler("ZAux_Direct_Base", error);

                    error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis[0], vel);
                    ErrorHandler("ZAux_Direct_SetSpeed", error);

                    error = zmcaux.ZAux_Direct_SetAccel(Handle, axis[0], acc);
                    ErrorHandler("ZAux_Direct_SetAccel", error);

                    error = zmcaux.ZAux_Direct_SetDecel(Handle, axis[0], dec);
                    ErrorHandler("ZAux_Direct_SetDecel", error);

                    error = zmcaux.ZAux_Direct_MoveCirc2(Handle, axis.Length, axis, midpos[0], midpos[1], distance[0], distance[1]);
                    ErrorHandler("ZAux_Direct_MoveCirc", error);

                    return OperationResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数不正确"
            };
        }


        /// <summary>
        /// XY圆弧绝对插补(中点定位)
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="distance"></param>
        /// <param name="midpos"></param>
        /// <param name="vel"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        public OperationResult MoveCircle2Abs(int[] axis, float[] distance, float[] midpos, float vel, float acc, float dec)
        {

            if (axis.Length == 2 && axis.Length == distance.Length)
            {
                OperationResult result = new OperationResult();

                //判断每个轴是否满足要求
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //选择 BASE 轴列表
                    error = zmcaux.ZAux_Direct_Base(Handle, axis.Length, axis);
                    ErrorHandler("ZAux_Direct_Base", error);

                    error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis[0], vel);
                    ErrorHandler("ZAux_Direct_SetSpeed", error);

                    error = zmcaux.ZAux_Direct_SetAccel(Handle, axis[0], acc);
                    ErrorHandler("ZAux_Direct_SetAccel", error);

                    error = zmcaux.ZAux_Direct_SetDecel(Handle, axis[0], dec);
                    ErrorHandler("ZAux_Direct_SetDecel", error);

                    error = zmcaux.ZAux_Direct_MoveCirc2Abs(Handle, axis.Length, axis, midpos[0], midpos[1],distance[0], distance[1] );
                    ErrorHandler("ZAux_Direct_MoveCirc2Abs", error);

                    return OperationResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }

            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数不正确"
            };
        }

        /// <summary>
        /// 驱动器回零
        /// </summary>
        /// <param name="axis"></param>
        public override void ZeroAxisHome(uint axis)
        {
            zmcaux.ZAux_BusCmd_Datum(Handle, axis, 35);
        }

        /// <summary>
        /// 通用回零操作
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">速度</param>
        /// <param name="creep">爬行速度</param>
        /// <param name="homeio">HomeIO</param>
        /// <param name="distance">原限距离</param>
        /// <param name="velMin">最小速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="sramp">s曲线时间</param>
        /// <param name="homemode">回零模式</param>
        /// <returns>操作结果</returns>
        public override bool ZeroAxis(short axis, float vel, float creep, int homeio, int fwd_In, int rev_In, float distance, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);
            uint homeStatus = 1;
            uint revStatus = 1;
            int homemode = 0;
            zmcaux.ZAux_Direct_SetRevIn(Handle, axis, rev_In); //设置负限位
            zmcaux.ZAux_Direct_SetFwdIn(Handle, axis, fwd_In); //设置正限位
            if (!result.IsSuccess) return false;

            zmcaux.ZAux_Direct_GetIn(Handle, homeio, ref homeStatus);
            zmcaux.ZAux_Direct_GetIn(Handle, rev_In, ref revStatus);

            //先往原点方向一直走，直到到达限位
            if (homeStatus != 0 && revStatus != 0)
                result = MoveRelative(axis, vel, -3000, velMin, acc, dec, sramp);
            if (!result.IsSuccess) return false;
            if (resetStop)
                StopAxis(axis);
            //回零方式判断
            while (!resetStop)  //IsMoving(axis)  resetStop
            {
                zmcaux.ZAux_Direct_GetIn(Handle, homeio, ref homeStatus);
                zmcaux.ZAux_Direct_GetIn(Handle, rev_In, ref revStatus);
                if (homeStatus == 0)
                {
                    homemode = 4;
                    StopAxis(axis);
                    break;
                }
                else if (revStatus == 0)
                {
                    homemode = 3;
                    StopAxis(axis);
                    //System.Windows.Forms.MessageBox.Show("负限位信号触发");
                    break;
                }
                if (resetStop)
                {
                    StopAxis(axis);
                    break;
                }
                Thread.Sleep(10);
            }

            //等待停止
            result = WaitStop(axis);
            if (!result.IsSuccess) return false;

            //直接回零
            if (resetStop)
                return false;
            result = DirectZeroAxis(axis, vel, creep, homeio, velMin, acc, dec, homemode);

            result = WaitStop(axis);
            //result = WaitHomeStop(axis);
            if (!result.IsSuccess) return false;
            Thread.Sleep(50);

            if (resetStop)
                return false;
            result = MoveRelative(axis, 20, distance, velMin, acc, dec, sramp);
            if (!result.IsSuccess) return false;
            result = WaitStop(axis);
            if (!result.IsSuccess) return false;
            Thread.Sleep(20);

            //回零
            if (resetStop)
                return false;
            result = DirectZeroAxis(axis, 1.6F, creep, homeio, 0.6F, 200, 200, 4);
            if (!result.IsSuccess) return false;
            //等待回零停止
            //result = WaitHomeStop(axis);
            Thread.Sleep(100);
            result = WaitStop(axis);
            zmcaux.ZAux_Direct_SetMpos(Handle, axis, 0.0f);  //重置编码器位置
            ZeroAxisHome((uint)axis);
            //返回成功
            return result.IsSuccess;
        }
        public override bool ZeroAxis2(AxisPara axisPara, float vel, float creep, float distance, float velMin, float acc, float dec, float sramp)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axisPara.AxisNum);
            uint homeStatus = 1;
            uint revStatus = 1;
            int homemode = 0;
            zmcaux.ZAux_Direct_SetRevIn(Handle, axisPara.AxisNum, axisPara.Rev); //设置负限位
            zmcaux.ZAux_Direct_SetFwdIn(Handle, axisPara.AxisNum, axisPara.Fwd); //设置正限位
            if (!result.IsSuccess) return false;

            zmcaux.ZAux_Direct_GetIn(Handle, axisPara.homeIo, ref homeStatus);
            zmcaux.ZAux_Direct_GetIn(Handle, axisPara.Rev, ref revStatus);

            //先往原点方向一直走，直到到达限位
            if (homeStatus != 0 && revStatus != 0)
                result = MoveRelative(axisPara.AxisNum, vel, -3000, velMin, acc, dec, sramp);
            if (!result.IsSuccess) return false;
            if (resetStop)
                StopAxis(axisPara.AxisNum);
            //回零方式判断
            while (!resetStop)  //IsMoving(axis)  resetStop
            {
                zmcaux.ZAux_Direct_GetIn(Handle, axisPara.homeIo, ref homeStatus);
                zmcaux.ZAux_Direct_GetIn(Handle, axisPara.Rev, ref revStatus);
                if (homeStatus == 0)
                {
                    homemode = 4;
                    StopAxis(axisPara.AxisNum);
                    break;
                }
                else if (revStatus == 0)
                {
                    homemode = 3;
                    StopAxis(axisPara.AxisNum);
                    //System.Windows.Forms.MessageBox.Show("负限位信号触发");
                    break;
                }
                if (resetStop)
                {
                    StopAxis(axisPara.AxisNum);
                    break;
                }
                Thread.Sleep(10);
            }

            //等待停止
            result = WaitStop(axisPara.AxisNum);
            if (!result.IsSuccess) return false;

            //直接回零
            if (resetStop)
                return false;
            result = DirectZeroAxis(axisPara.AxisNum, vel, creep, axisPara.homeIo, velMin, acc, dec, homemode);

            result = WaitStop(axisPara.AxisNum);
            //result = WaitHomeStop(axis);
            if (!result.IsSuccess) return false;
            Thread.Sleep(50);

            if (resetStop)
                return false;
            result = MoveRelative(axisPara.AxisNum, 20, distance, velMin, acc, dec, sramp);
            if (!result.IsSuccess) return false;
            result = WaitStop(axisPara.AxisNum);
            if (!result.IsSuccess) return false;
            Thread.Sleep(20);

            //回零
            if (resetStop)
                return false;
            result = DirectZeroAxis(axisPara.AxisNum, 1.6F, creep, axisPara.homeIo, 0.6F, 200, 200, 4);
            if (!result.IsSuccess) return false;
            //等待回零停止
            //result = WaitHomeStop(axis);
            Thread.Sleep(100);
            result = WaitStop(axisPara.AxisNum);
            zmcaux.ZAux_Direct_SetMpos(Handle, axisPara.AxisNum, 0.0f);  //重置编码器位置
            ZeroAxisHome((uint)axisPara.AxisNum);
            //返回成功
            return result.IsSuccess;
        }

        /// <summary>
        /// 直接回原点
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">速度</param>
        /// <param name="creep">爬行速度</param>
        /// <param name="homeio">原点IO点</param>
        /// <param name="velMin">最小速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="homemode">回零模式</param>
        /// <returns>操作</returns>
        public override OperationResult DirectZeroAxis(short axis, float vel, float creep, int homeio, float velMin, float acc, float dec, int homemode = 3)
        {
            // 判断是否满足运动条件
            var result = CommonMotionValidate(axis);
            if (!result.IsSuccess) return result;
            
            //创建错误码
            int error = 0;

            try
            {
                //设置最小速度
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //设置运行速度
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //设置加速度
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //设置减速度
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //设置爬行速度
                error = zmcaux.ZAux_Direct_SetCreep(Handle, axis, creep);
                ErrorHandler("ZAux_Direct_SetCreep", error);

                //设置HomeIO
                error = zmcaux.ZAux_Direct_SetDatumIn(Handle, axis, homeio);
                ErrorHandler("ZAux_Direct_SetDatumIn", error);

                //执行回原点
                error = zmcaux.ZAux_Direct_Single_Datum(Handle, axis, homemode);
                ErrorHandler("ZAux_Direct_Single_Datum", error);
                WaitStop(axis);
                //ZeroPos(axis);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }
      
        /// <summary>
        /// 根据零位回零
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        public override bool AxisGoHome(short axis, float vel)
        {
            float mPos = GetMpos(axis);
            if (mPos > 0)
            {
                MoveRelative(axis, vel, -3000, 1, 200, 100, 0);
                while (!resetStop)
                {
                    mPos = GetMpos(axis);
                    if (mPos == 0)
                        break;
                }
                StopAxis(axis);
            }
            else if (mPos < 0)
            {
                MoveRelative(axis, vel, 3000, 1, 200, 1000, 0);
                while (!resetStop)
                {
                    mPos = GetMpos(axis);
                    if (mPos == 0)
                        break;
                }
                StopAxis(axis);
            }
           
            MoveRelative(axis, 5, 3, 0.5f, 100, 100, 0);
            WaitStop(axis);
            //MoveRelative(axis, 0.3f, -5, 0.1f, 10, 10, 0);
            //while (!resetStop)
            //{
            //    mPos = GetMpos(axis);
            //    if (mPos == 0)
            //        break;
            //}
            //StopAxis(axis);
            return GetMpos(axis) == 0 ? true : false;
        }
        /// <summary>
        /// 等待停止
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns></returns>
        public override OperationResult WaitStop(short axis)
        {
            // 判断是否满足初始化条件
            var result = CommonInitedValidate();
            if (!result.IsSuccess) return result;
            int error = 0;
            int runstate = 0;
            Thread.Sleep(20);
            do
            {
                error = zmcaux.ZAux_Direct_GetIfIdle(Handle, axis, ref runstate);
                ErrorHandler("ZAux_Direct_GetIfIdle", error);

            } while (runstate == 0);
            Thread.Sleep(50);
            return OperationResult.CreateSuccessResult();
        }
        /// <summary>
        /// 等待轴到达点位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool WaitInPlace(short axis, float point)
        {
            var result = CommonInitedValidate();
            if (!result.IsSuccess) return result.IsSuccess;
            int error = 0;
            int runstate = 0;
            
            //Thread.Sleep(20);
            do
            {
                error = zmcaux.ZAux_Direct_GetIfIdle(Handle, axis, ref runstate);
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
            } while (runstate == 0);
            Thread.Sleep(20);
            if (Math.Abs(GetMpos(axis) - GetPos(axis)) > 0.1) return false;
            
            return Math.Abs(point - GetPos(axis)) < 0.03 ? true : false;
        }

        public  void WaitMitunInPlace(short[] axis, float point, ref float offPos)
        {
            var result = CommonInitedValidate();
            //if (!result.IsSuccess) return result.IsSuccess;
            int error = 0;
            int runstate = 0;
            //Thread.Sleep(20);
            for (int i = 0; i < axis.Length; i++)
            {
                do
                {
                    error = zmcaux.ZAux_Direct_GetIfIdle(Handle, axis[i], ref runstate);
                    ErrorHandler("ZAux_Direct_GetIfIdle", error);
                } while (runstate == 0);
            }
           
            Thread.Sleep(100);
            //offPos = GetMpos(axis) - GetPos(axis);
            //if (Math.Abs(GetMpos(axis) - GetPos(axis)) > 0.1) return false;

            //return Math.Abs(point - GetPos(axis)) < 0.03 ? true : false;
        }

        /// <summary>
        /// 等待回零停止
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns></returns>
        public override OperationResult WaitHomeStop(short axis)
        {
            // 判断是否满足初始化条件
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            int error = 0;
            uint homestate = 0;
            
            do
            {
                if(WaitStop(axis).IsSuccess)  //!IsMoving(axis)
                    error = zmcaux.ZAux_Direct_GetHomeStatus(Handle, axis, ref homestate);

                ErrorHandler("ZAux_Direct_GetHomeStatus", error);

            } while (homestate == 0);
            return OperationResult.CreateSuccessResult();
        }

        /// <summary>
        /// 等待信号
        /// </summary>
        /// <param name="signNum"></param>
        /// <returns></returns>
        public override bool WaitInSignal(int signNum, double waitTime)
        {
            bool signal = false;
            bool result = false;
            DateTime reocrdtime = DateTime.Now;
            while (true)
            {
                GetInSignal(signNum, ref signal);
                if (signal)
                {
                    result = true;
                    break;
                }

                if (WaitTime(reocrdtime, waitTime))
                {
                    result = false;
                    break;
                }
                
            }
            return result;
        }

        public override bool WaitTime(DateTime recordTime,double waitTime)
        {
            DateTime timeB = DateTime.Now;	//获取当前时间
            TimeSpan ts = timeB - recordTime;	//计算时间差
            double time = Convert.ToDouble(ts.TotalSeconds);	//将时间差转换为秒
            if (Convert.ToDouble(ts.TotalSeconds) >= waitTime)
                return true;
            else
                return false;
        }

        public override bool WaitTime(double startMinute, double startSecond, double waitTime)
        {
            double nowMinute = DateTime.Now.Minute;
            double nowSecond = DateTime.Now.Second;
            if (startMinute == nowMinute)
            {
                if ((nowSecond - startSecond) > waitTime)
                    return true;
                else
                    return false;
            }
            else
            {
                if (((nowSecond + 60) - startSecond) > waitTime)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 单轴停止运行
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        public override OperationResult StopAxis(short axis)
        {
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //错误码
            int error = 0;

            try
            {
                /*         
                0 （缺省）取消当前运动
                1 取消缓冲的运动
                2 取消当前运动和缓冲运动
                3 立即中断脉冲发送
                 */
                
                error = zmcaux.ZAux_Direct_Single_Cancel(Handle, axis, 2);
                error = zmcaux.ZAux_Direct_Single_Cancel(Handle, axis, 3);
                ErrorHandler("ZAux_Direct_Single_Cancel", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        /// <summary>
        /// 停止多轴
        /// </summary>
        /// <param name="axisList">轴列表</param>
        /// <returns></returns>
        public override OperationResult StopMoreAxis(int[] axisList)
        {
            var result = CommonInitedValidate();
            if (!result.IsSuccess) return result;

            //错误码
            int error = 0;

            try
            {
                /*         
                    0取消当前运动 。
                    1取消缓冲的运动 。
                    2取消当前运动和缓冲运动 。
                    3 立即停止。
                 */

                error = zmcaux.ZAux_Direct_CancelAxisList(Handle, axisList.Length, axisList, 3) ;
                ErrorHandler("ZAux_Direct_CancelAxisList", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();

        }

        /// <summary>
        /// 停止所有轴
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override OperationResult StopAllAxis(int model)
        {
            OperationResult result = new OperationResult();
            try
            {
                int error = zmcaux.ZAux_Direct_Rapidstop(Handle, model);
                ErrorHandler("ZAux_Direct_Rapidstop", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        /// <summary>
        /// 获取实时速度
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override float GetVel(short axis)
        {
            //判断是否满足初始化条件
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return 0.0f;

            //定义速度
            float vel = 0.0f;

            //定义错误码
            int error = 0;

            try
            {
                error = zmcaux.ZAux_Direct_GetVpSpeed(Handle, axis, ref vel);

                ErrorHandler("ZAux_Direct_GetVpSpeed", error);

                return vel;

            }
            catch (Exception)
            {
                return 0.0f;
            }
        }

        /// <summary>
        /// 获取编码器位置
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override float GetMpos(short axis)
        {
            //判断是否满足初始化条件
            var result = CommonInitedValidate();
            if (!result.IsSuccess) return 0.0f;

            float pos = 0;
            int error = 0;

            try
            {
                error = zmcaux.ZAux_Direct_GetMpos(Handle, axis, ref pos);
                ErrorHandler("ZAux_Direct_GetMpos", error);

                return pos;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }

        /// <summary>
        /// 获取轴命令位置
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override float GetPos(short axis)
        {

            //判断是否满足初始化条件
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return 0.0f;

            //定义位置
            float pos = 0.0f;
            //定义错误码
            int error = 0;

            try
            {
                error = zmcaux.ZAux_Direct_GetDpos(Handle, axis, ref pos);

                ErrorHandler("ZAux_Direct_GetDpos", error);

                return pos;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }


        /// <summary>
        /// 位置清零
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override OperationResult ZeroPos(short axis)
        {
            //判断是否满足初始化条件
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //定义错误码
            int error = 0;

            try
            {
                error = zmcaux.ZAux_Direct_SetMpos(Handle, axis, 0);
                error = zmcaux.ZAux_Direct_SetDpos(Handle, axis, 0);
               
                ErrorHandler("ZAux_Direct_SetMpos", error);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// 通用运动验证
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        private OperationResult CommonMotionValidate(short axis)
        {
            OperationResult result = CommonInitedValidate();
            //判断是否已经初始化
            if (!result.IsSuccess) return result;

            //判断是否正在运行
            if (IsMoving(axis))
            {
                result.IsSuccess = false;
                result.ErrorMsg = "轴正在运行";
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// 通用初始化验证
        /// </summary>
        /// <returns></returns>
        private OperationResult CommonInitedValidate()
        {
            OperationResult result = new OperationResult();
            //判断是否已经初始化
            if (!InitedOK)
            {
                result.IsSuccess = false;
                result.ErrorMsg = "控制器未初始化";
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="command">执行命令</param>
        /// <param name="error">错误码</param>
        private void ErrorHandler(string command, int error)
        {
            string result = string.Empty;
            switch (error)
            {
                case 0: break;
                default:
                    result = string.Format("{0}" + "指令执行错误，错误码为{1}", command, error);
                    break;
            }
            if (result.Length > 0)
            {
                //throw new Exception(result);
                //System.Windows.Forms.MessageBox.Show(result);
            }
        }


        /// <summary>
        /// 判断某个轴是否正在运行
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override bool IsMoving(short axis)
        {
            OperationResult result = CommonInitedValidate();
            //判断是否已经初始化
            if (!result.IsSuccess) return false;
            //定义运行状态
            int runstate = -1;
            //定义错误码
            int error = 0;
            try
            {
                //获取轴状态
                error = zmcaux.ZAux_Direct_GetIfIdle(Handle, axis, ref runstate);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                return runstate == 0;
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 读取输入信号
        /// </summary>
        /// <param name="ioNum">IO口</param>
        /// <param name="inSignal">信号结果</param>
        /// <returns></returns>
        public override void GetInSignal(int ioNum,ref bool inSignal)
        {
            int error = 0;
            UInt32 result=0;
            try
            {
                error = zmcaux.ZAux_Direct_GetIn(Handle, ioNum, ref result);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                inSignal = result == 0 ? false : true;
            }
            catch (Exception)
            {

            }
            
        }
        /// <summary>
        /// 读取输出信号状态
        /// </summary>
        /// <param name="ioNum"></param>
        /// <param name="inSignal"></param>
        public override void GetOutSignal(int ioNum, ref bool inSignal)
        {
            int error = 0;
            UInt32 result = 0;
            try
            {
                error = zmcaux.ZAux_Direct_GetOp(Handle, ioNum, ref result);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                inSignal = result == 0 ? false : true;
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 设置输出信号
        /// </summary>
        /// <param name="ioNum">IO口</param>
        /// <param name="ioValue">状态值</param>
        /// <returns></returns>
        public override void SetOutSignal(int ioNum,UInt32 ioValue)
        {
            int error = 0;
            try
            {
                error = zmcaux.ZAux_Direct_SetOp(Handle,ioNum, ioValue);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
            }
            catch (Exception)
            {
                
            }
        }
        /// <summary>
        /// 获取运动最终值
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="planValue"></param>
        public override void GetPlanValue(short axis,ref float planValue)
        {
            int error = 0;
            try
            {
                error=zmcaux.ZAux_Direct_GetEndMove(Handle, axis, ref planValue);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
            }
            catch (Exception)
            {
                
            }
        }
      
        public override float GetMoveBufferPos(int axis)
        {
            int error = 0;
            float posVal = 0;
            error = zmcaux.ZAux_Direct_GetEndMoveBuffer(Handle, axis, ref posVal);
            //错误码验证
            ErrorHandler("ZAux_Direct_GetIfIdle", error);
            return posVal;
        }
        /// <summary>
        /// 获取轴内部编码器值
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="encoderValue"></param>
        public override void GetEncoderValue(short axis, ref float encoderValue)
        {
            int error = 0;
            try
            {
                error = zmcaux.ZAux_Direct_GetEncoder(Handle, axis, ref encoderValue);

                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        public override bool SetAllAxisEnable(int axisNum, int value)
        {
            try
            {
                for (int i = 0; i < axisNum; i++)
                {
                    SetAxisEnable(i, value);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置轴使能
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="value"></param>
        public override string SetAxisEnable(int axis,int value)
        {
            OperationResult result = new OperationResult();
            int error = 0;
            try
            {
                error = zmcaux.ZAux_Direct_SetAxisEnable(Handle, axis, value);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                return string.Empty;
            }
            catch (Exception)
            {
                return "使能设置失败!，请检查是否通电...";
            }
        }
        /// <summary>
        ///获取轴力矩
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="torqueValue"></param>
        public override string GetDriveTorque(UInt32 axis,ref int torqueValue)
        {
            int error = 0;
            try
            {
                zmcaux.ZAux_BusCmd_GetDriveTorque(Handle, axis, ref torqueValue);
                //错误码验证
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                return string.Empty;
            }
            catch (Exception)
            {
                return "获取轴力矩失败!，请检查轴...";
            }
            
        }
        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="axisStatus"></param>
        public override void GetAxisStatus(int axis,ref AxisStatus axisStatus)
        {
            int status = 0;
            int originValue = 0;
            uint originSignal = 0;
            int enable = 0;
            zmcaux.ZAux_Direct_GetDatumIn(Handle, axis, ref originValue);
            zmcaux.ZAux_Direct_GetIn(Handle, originValue, ref originSignal);  //轴原点
            zmcaux.ZAux_Direct_GetAxisStatus(Handle, axis, ref status);  //轴状态
            zmcaux.ZAux_Direct_GetAxisEnable(Handle, axis, ref enable);  //轴使能
            switch (status)
            {
                case 16:
                    axisStatus.positLimit = 1;
                    break;
                case 32:
                    axisStatus.minusLimit = 1;
                    break;
                case 48:
                    axisStatus.positLimit = 1;
                    break;
                case 4194304:
                    axisStatus.servoError = 1;
                    break;
            }
            axisStatus.origin = originSignal == 1 ? 1 : 0;  //原点信号
            axisStatus.AxisEnadle = enable == 1 ? 1 : 0;    //伺服使能
        }

        /// <summary>
        /// 获取轴状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="axisStatus"></param>
        public override void GetAxisStatus2(int axis,int fwd,int rev, ref AxisStatus axisStatus)
        {
            int status = 0;
            int originValue = 0;
            uint originSignal = 0;
            uint revSignal = 0;
            uint fwdSignal = 0;
            int enable = 0;
            zmcaux.ZAux_Direct_GetDatumIn(Handle, axis, ref originValue);
            zmcaux.ZAux_Direct_GetIn(Handle, originValue, ref originSignal);  //轴原点
            zmcaux.ZAux_Direct_GetIn(Handle, fwd, ref fwdSignal);  //轴正限位
            zmcaux.ZAux_Direct_GetIn(Handle, rev, ref revSignal);  //轴负限位
            zmcaux.ZAux_Direct_GetAxisStatus(Handle, axis, ref status);  //轴状态
            zmcaux.ZAux_Direct_GetAxisEnable(Handle, axis, ref enable);  //轴使能
           
            axisStatus.positLimit = fwdSignal == 0 ? 0 : 1;
            axisStatus.minusLimit = revSignal == 0 ? 0 : 1;
            axisStatus.servoError = status == 4194304 ? 1 : 0;
            axisStatus.servoError = status == 8 ? 1 : 0;
            axisStatus.origin = originSignal == 1 ? 1 : 0;  //原点信号
            axisStatus.AxisEnadle = enable == 1 ? 1 : 0;    //伺服使能
        }
        /// <summary>
        /// 清除报警
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="model"></param>
        public override void ClearAlarm(UInt32 axis,int model)
        {
            zmcaux.ZAux_BusCmd_DriveClear(Handle, axis, 0);
        }
    }

    /// <summary>
    /// 操作结果类
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }


        public static OperationResult CreateSuccessResult()
        {
            return new OperationResult()
            {
                IsSuccess = true,
                ErrorMsg = "Success"
            };
        }

        public static OperationResult CreateFailResult()
        {
            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "Fail"
            };
        }

    }

}
