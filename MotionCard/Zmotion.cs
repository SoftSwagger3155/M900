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
            CardTypeSelect = CardType.���˶�;
        }
        public Zmotion(string IPAddress = "192.168.0.11")
        {
            this.IPAddress = IPAddress;
            isPci = false;
            CardTypeSelect = CardType.���˶�;
        }
        /// <summary>
        /// ����IP��ַ����
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
        /// ��ʼ���忨
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
        /// �رհ忨
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
        /// �������嵱��
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
        /// �����˶�
        /// </summary>
        /// <param name="axis">���</param>
        /// <param name="vel">�����ٶ�</param>
        /// <param name="dir">����</param>
        /// <param name="velMin">��С�ٶ�</param>
        /// <param name="acc">���ٶ�</param>
        /// <param name="dec">���ٶ�</param>
        /// <param name="sramp">S����ʱ��</param>
        /// <returns>�������</returns>
        public override OperationResult VMove(short axis, float vel, bool dir, float velMin, float acc, float dec, float sramp)
        {
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //����������
            int error = 0;

            try
            {
                /*
                 Atype���� ����
                0 �����ᡣ
                1 ���巽��ʽ�Ĳ������ŷ� ��
                2 ģ���źſ��Ʒ�ʽ���ŷ� ��
                3 ���������� ��
                4 ����+������ ��
                6 ���巽��ʽ�ı��������������������롣
                7 ���巽��ʽ�������ŷ�+EZ�ź����롣
                8 ZCAN��չ���巽��ʽ�������ŷ� ��
                9 ZCAN��չ������������
                10 ZCAN��չ���巽��ʽ�ı�������
                 */

                //����������
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
        /// ����˶�
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
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //����������
            int error = 0;

            try
            {
                //����������

                /*
                 Atype���� ����
                0 �����ᡣ
                1 ���巽��ʽ�Ĳ������ŷ� ��
                2 ģ���źſ��Ʒ�ʽ���ŷ� ��
                3 ���������� ��
                4 ����+������ ��
                6 ���巽��ʽ�ı��������������������롣
                7 ���巽��ʽ�������ŷ�+EZ�ź����롣
                8 ZCAN��չ���巽��ʽ�������ŷ� ��
                9 ZCAN��չ������������
                10 ZCAN��չ���巽��ʽ�ı�������
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                

                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
        /// �����˶�
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
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);
            if (!result.IsSuccess) return result;

            if (Math.Abs(GetPos(axis) - pos) < 15)
                vel = vel / 10.0f;
            //����������
            int error = 0;

            try
            {
                /*
                 Atype���� ����
                0 �����ᡣ
                1 ���巽��ʽ�Ĳ������ŷ� ��
                2 ģ���źſ��Ʒ�ʽ���ŷ� ��
                3 ���������� ��
                4 ����+������ ��
                6 ���巽��ʽ�ı��������������������롣
                7 ���巽��ʽ�������ŷ�+EZ�ź����롣
                8 ZCAN��չ���巽��ʽ�������ŷ� ��
                9 ZCAN��չ������������
                10 ZCAN��չ���巽��ʽ�ı�������
                 */
                //����������
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);
            if (!result.IsSuccess) return result;
            float vec = axisSpeed.vel * percentum;
            if (Math.Abs(GetPos(axis) - pos) < 15)
                vec = axisSpeed.vel / 10.0f;
            //����������
            int error = 0;

            try
            {
                //����������
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, axisSpeed.minVel);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vec);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, axisSpeed.acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, axisSpeed.dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
        /// �������������˶�
        /// </summary>
        /// <param name="axis">���</param>
        /// <param name="vel">�����ٶ�</param>
        /// <param name="dir">����</param>
        /// <param name="velMin">��С�ٶ�</param>
        /// <param name="acc">���ٶ�</param>
        /// <param name="dec">���ٶ�</param>
        /// <param name="sramp">S����ʱ��</param>
        /// <returns>�������</returns>
        public override OperationResult LockVMove(short axis,int ioNum, float vel, bool dir, float velMin, float acc, float dec, float sramp)
        {
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;
            SetOutSignal(ioNum, 1);
            //����������
            int error = 0;

            try
            {
                //����������

                /*
                 Atype���� ����
                0 �����ᡣ
                1 ���巽��ʽ�Ĳ������ŷ� ��
                2 ģ���źſ��Ʒ�ʽ���ŷ� ��
                3 ���������� ��
                4 ����+������ ��
                6 ���巽��ʽ�ı��������������������롣
                7 ���巽��ʽ�������ŷ�+EZ�ź����롣
                8 ZCAN��չ���巽��ʽ�������ŷ� ��
                9 ZCAN��չ������������
                10 ZCAN��չ���巽��ʽ�ı�������
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
        /// ������������˶�
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
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;
            SetOutSignal(ioNum, 1);
            //����������
            int error = 0;

            try
            {
                //����������

                /*
                 Atype���� ����
                0 �����ᡣ
                1 ���巽��ʽ�Ĳ������ŷ� ��
                2 ģ���źſ��Ʒ�ʽ���ŷ� ��
                3 ���������� ��
                4 ����+������ ��
                6 ���巽��ʽ�ı��������������������롣
                7 ���巽��ʽ�������ŷ�+EZ�ź����롣
                8 ZCAN��չ���巽��ʽ�������ŷ� ��
                9 ZCAN��չ������������
                10 ZCAN��չ���巽��ʽ�ı�������
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);

               

                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
        /// ������������˶�
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
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;
            SetOutSignal(ioNum, 1);

            //����������
            int error = 0;

            try
            {
                //����������

                /*
                 Atype���� ����
                0 �����ᡣ
                1 ���巽��ʽ�Ĳ������ŷ� ��
                2 ģ���źſ��Ʒ�ʽ���ŷ� ��
                3 ���������� ��
                4 ����+������ ��
                6 ���巽��ʽ�ı��������������������롣
                7 ���巽��ʽ�������ŷ�+EZ�ź����롣
                8 ZCAN��չ���巽��ʽ�������ŷ� ��
                9 ZCAN��չ������������
                10 ZCAN��չ���巽��ʽ�ı�������
                 */
                //error = zmcaux.ZAux_Direct_SetAtype(Handle, axis, 65);
                //ErrorHandler("ZAux_Direct_SetAtype", error);


                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //����S����
                error = zmcaux.ZAux_Direct_SetSramp(Handle, axis, sramp);
                ErrorHandler("ZAux_Direct_SetSramp", error);

                //���÷����˶�
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
        /// 2����Զ�λ
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

                //��Զ�λ
                for (int i = 0; i < 2; i++)
                {
                    result = MoveRelative(axis[i], vel[i], distance[i], velMin[i], acc[i], dec[i], sramp[i]);

                    if (!result.IsSuccess) return result;
                }

                //�ȴ�ֹͣ
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
                ErrorMsg = "���ݲ������Ȳ���ȷ"
            };

        }

        /// <summary>
        /// 2����Զ�λ
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

                //��Զ�λ
                for (int i = 0; i < 2; i++)
                {
                    result = MoveAbs(axis[i], vel[i], pos[i], velMin[i], acc[i], dec[i], sramp[i]);

                    if (!result.IsSuccess) return result;
                }

                //�ȴ�ֹͣ
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
                ErrorMsg = "���ݲ������Ȳ���ȷ"
            };

        }


        /// <summary>
        /// 3����Զ�λ
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

                //2�ᶨλ
                result = Move2DRelative(new short[] { axis[0], axis[1] }, new float[] { vel[0], vel[1] }, new float[] { distance[0], distance[1] },

                    new float[] { velMin[0], velMin[1] }, new float[] { acc[0], acc[1] }, new float[] { dec[0], dec[1] }, new float[] { sramp[0], sramp[1] });
                if (!result.IsSuccess) return result;


                result = MoveRelative(axis[2], vel[2], distance[2], velMin[2], acc[2], dec[2], sramp[2]);
                if (!result.IsSuccess) return result;

                //�ȴ�ֹͣ
                result = WaitStop(axis[2]);
                if (!result.IsSuccess) return result;

                return OperationResult.CreateSuccessResult();
            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "���ݲ������Ȳ���ȷ"
            };

        }


        /// <summary>
        /// 3����Զ�λ
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

                //�ȶ�Z��
                result = MoveAbs(axis[2], vel[2], 0.0f, velMin[2], acc[2], dec[2], sramp[2]);
                if (!result.IsSuccess) return result;

                //�ȴ�ֹͣ
                result = WaitStop(axis[2]);
                if (!result.IsSuccess) return result;

                //2�ᶨλ
                result = Move2DAbs(new short[] { axis[0], axis[1] }, new float[] { vel[0], vel[1] }, new float[] { distance[0], distance[1] },

                    new float[] { velMin[0], velMin[1] }, new float[] { acc[0], acc[1] }, new float[] { dec[0], dec[1] }, new float[] { sramp[0], sramp[1] });
                if (!result.IsSuccess) return result;


                result = MoveAbs(axis[2], vel[2], distance[2], velMin[2], acc[2], dec[2], sramp[2]);
                if (!result.IsSuccess) return result;

                //�ȴ�ֹͣ
                result = WaitStop(axis[2]);
                if (!result.IsSuccess) return result;

                return OperationResult.CreateSuccessResult();
            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "���ݲ������Ȳ���ȷ"
            };

        }


        /// <summary>
        /// �������ֱ�߲岹
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

                //�ж�ÿ�����Ƿ�����Ҫ��
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //ѡ�� BASE ���б�
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
                ErrorMsg = "���ݲ�������ȷ"
            };
        }

        /// <summary>
        /// �������ֱ�߲岹
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

                //�ж�ÿ�����Ƿ�����Ҫ��
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //ѡ�� BASE ���б�
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
                ErrorMsg = "���ݲ�������ȷ"
            };
        }

        /// <summary>
        /// XYԲ����Բ岹(Բ�Ķ�λ)
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

                //�ж�ÿ�����Ƿ�����Ҫ��
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //ѡ�� BASE ���б�
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
                ErrorMsg = "���ݲ�������ȷ"
            };
        }


        /// <summary>
        /// XYԲ�����Բ岹(Բ�Ķ�λ)
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

                //�ж�ÿ�����Ƿ�����Ҫ��
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //ѡ�� BASE ���б�
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
                ErrorMsg = "���ݲ�������ȷ"
            };
        }


        /// <summary>
        /// XYԲ����Բ岹(�е㶨λ)
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

                //�ж�ÿ�����Ƿ�����Ҫ��
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //ѡ�� BASE ���б�
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
                ErrorMsg = "���ݲ�������ȷ"
            };
        }


        /// <summary>
        /// XYԲ�����Բ岹(�е㶨λ)
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

                //�ж�ÿ�����Ƿ�����Ҫ��
                foreach (var item in axis)
                {
                    result = CommonMotionValidate((short)item);
                    if (!result.IsSuccess) return result;
                }

                int error = 0;
                try
                {
                    //ѡ�� BASE ���б�
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
                ErrorMsg = "���ݲ�������ȷ"
            };
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="axis"></param>
        public override void ZeroAxisHome(uint axis)
        {
            zmcaux.ZAux_BusCmd_Datum(Handle, axis, 35);
        }

        /// <summary>
        /// ͨ�û������
        /// </summary>
        /// <param name="axis">���</param>
        /// <param name="vel">�ٶ�</param>
        /// <param name="creep">�����ٶ�</param>
        /// <param name="homeio">HomeIO</param>
        /// <param name="distance">ԭ�޾���</param>
        /// <param name="velMin">��С�ٶ�</param>
        /// <param name="acc">���ٶ�</param>
        /// <param name="dec">���ٶ�</param>
        /// <param name="sramp">s����ʱ��</param>
        /// <param name="homemode">����ģʽ</param>
        /// <returns>�������</returns>
        public override bool ZeroAxis(short axis, float vel, float creep, int homeio, int fwd_In, int rev_In, float distance, float velMin, float acc, float dec, float sramp)
        {
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);
            uint homeStatus = 1;
            uint revStatus = 1;
            int homemode = 0;
            zmcaux.ZAux_Direct_SetRevIn(Handle, axis, rev_In); //���ø���λ
            zmcaux.ZAux_Direct_SetFwdIn(Handle, axis, fwd_In); //��������λ
            if (!result.IsSuccess) return false;

            zmcaux.ZAux_Direct_GetIn(Handle, homeio, ref homeStatus);
            zmcaux.ZAux_Direct_GetIn(Handle, rev_In, ref revStatus);

            //����ԭ�㷽��һֱ�ߣ�ֱ��������λ
            if (homeStatus != 0 && revStatus != 0)
                result = MoveRelative(axis, vel, -3000, velMin, acc, dec, sramp);
            if (!result.IsSuccess) return false;
            if (resetStop)
                StopAxis(axis);
            //���㷽ʽ�ж�
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
                    //System.Windows.Forms.MessageBox.Show("����λ�źŴ���");
                    break;
                }
                if (resetStop)
                {
                    StopAxis(axis);
                    break;
                }
                Thread.Sleep(10);
            }

            //�ȴ�ֹͣ
            result = WaitStop(axis);
            if (!result.IsSuccess) return false;

            //ֱ�ӻ���
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

            //����
            if (resetStop)
                return false;
            result = DirectZeroAxis(axis, 1.6F, creep, homeio, 0.6F, 200, 200, 4);
            if (!result.IsSuccess) return false;
            //�ȴ�����ֹͣ
            //result = WaitHomeStop(axis);
            Thread.Sleep(100);
            result = WaitStop(axis);
            zmcaux.ZAux_Direct_SetMpos(Handle, axis, 0.0f);  //���ñ�����λ��
            ZeroAxisHome((uint)axis);
            //���سɹ�
            return result.IsSuccess;
        }
        public override bool ZeroAxis2(AxisPara axisPara, float vel, float creep, float distance, float velMin, float acc, float dec, float sramp)
        {
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axisPara.AxisNum);
            uint homeStatus = 1;
            uint revStatus = 1;
            int homemode = 0;
            zmcaux.ZAux_Direct_SetRevIn(Handle, axisPara.AxisNum, axisPara.Rev); //���ø���λ
            zmcaux.ZAux_Direct_SetFwdIn(Handle, axisPara.AxisNum, axisPara.Fwd); //��������λ
            if (!result.IsSuccess) return false;

            zmcaux.ZAux_Direct_GetIn(Handle, axisPara.homeIo, ref homeStatus);
            zmcaux.ZAux_Direct_GetIn(Handle, axisPara.Rev, ref revStatus);

            //����ԭ�㷽��һֱ�ߣ�ֱ��������λ
            if (homeStatus != 0 && revStatus != 0)
                result = MoveRelative(axisPara.AxisNum, vel, -3000, velMin, acc, dec, sramp);
            if (!result.IsSuccess) return false;
            if (resetStop)
                StopAxis(axisPara.AxisNum);
            //���㷽ʽ�ж�
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
                    //System.Windows.Forms.MessageBox.Show("����λ�źŴ���");
                    break;
                }
                if (resetStop)
                {
                    StopAxis(axisPara.AxisNum);
                    break;
                }
                Thread.Sleep(10);
            }

            //�ȴ�ֹͣ
            result = WaitStop(axisPara.AxisNum);
            if (!result.IsSuccess) return false;

            //ֱ�ӻ���
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

            //����
            if (resetStop)
                return false;
            result = DirectZeroAxis(axisPara.AxisNum, 1.6F, creep, axisPara.homeIo, 0.6F, 200, 200, 4);
            if (!result.IsSuccess) return false;
            //�ȴ�����ֹͣ
            //result = WaitHomeStop(axis);
            Thread.Sleep(100);
            result = WaitStop(axisPara.AxisNum);
            zmcaux.ZAux_Direct_SetMpos(Handle, axisPara.AxisNum, 0.0f);  //���ñ�����λ��
            ZeroAxisHome((uint)axisPara.AxisNum);
            //���سɹ�
            return result.IsSuccess;
        }

        /// <summary>
        /// ֱ�ӻ�ԭ��
        /// </summary>
        /// <param name="axis">���</param>
        /// <param name="vel">�ٶ�</param>
        /// <param name="creep">�����ٶ�</param>
        /// <param name="homeio">ԭ��IO��</param>
        /// <param name="velMin">��С�ٶ�</param>
        /// <param name="acc">���ٶ�</param>
        /// <param name="dec">���ٶ�</param>
        /// <param name="homemode">����ģʽ</param>
        /// <returns>����</returns>
        public override OperationResult DirectZeroAxis(short axis, float vel, float creep, int homeio, float velMin, float acc, float dec, int homemode = 3)
        {
            // �ж��Ƿ������˶�����
            var result = CommonMotionValidate(axis);
            if (!result.IsSuccess) return result;
            
            //����������
            int error = 0;

            try
            {
                //������С�ٶ�
                error = zmcaux.ZAux_Direct_SetLspeed(Handle, axis, velMin);
                ErrorHandler("ZAux_Direct_SetLspeed", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetSpeed(Handle, axis, vel);
                ErrorHandler("ZAux_Direct_SetSpeed", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetAccel(Handle, axis, acc);
                ErrorHandler("ZAux_Direct_SetAccel", error);

                //���ü��ٶ�
                error = zmcaux.ZAux_Direct_SetDecel(Handle, axis, dec);
                ErrorHandler("ZAux_Direct_SetDecel", error);

                //���������ٶ�
                error = zmcaux.ZAux_Direct_SetCreep(Handle, axis, creep);
                ErrorHandler("ZAux_Direct_SetCreep", error);

                //����HomeIO
                error = zmcaux.ZAux_Direct_SetDatumIn(Handle, axis, homeio);
                ErrorHandler("ZAux_Direct_SetDatumIn", error);

                //ִ�л�ԭ��
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
        /// ������λ����
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
        /// �ȴ�ֹͣ
        /// </summary>
        /// <param name="axis">���</param>
        /// <returns></returns>
        public override OperationResult WaitStop(short axis)
        {
            // �ж��Ƿ������ʼ������
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
        /// �ȴ��ᵽ���λ
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
        /// �ȴ�����ֹͣ
        /// </summary>
        /// <param name="axis">���</param>
        /// <returns></returns>
        public override OperationResult WaitHomeStop(short axis)
        {
            // �ж��Ƿ������ʼ������
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
        /// �ȴ��ź�
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
            DateTime timeB = DateTime.Now;	//��ȡ��ǰʱ��
            TimeSpan ts = timeB - recordTime;	//����ʱ���
            double time = Convert.ToDouble(ts.TotalSeconds);	//��ʱ���ת��Ϊ��
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
        /// ����ֹͣ����
        /// </summary>
        /// <param name="axis">���</param>
        /// <returns>�������</returns>
        public override OperationResult StopAxis(short axis)
        {
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //������
            int error = 0;

            try
            {
                /*         
                0 ��ȱʡ��ȡ����ǰ�˶�
                1 ȡ��������˶�
                2 ȡ����ǰ�˶��ͻ����˶�
                3 �����ж����巢��
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
        /// ֹͣ����
        /// </summary>
        /// <param name="axisList">���б�</param>
        /// <returns></returns>
        public override OperationResult StopMoreAxis(int[] axisList)
        {
            var result = CommonInitedValidate();
            if (!result.IsSuccess) return result;

            //������
            int error = 0;

            try
            {
                /*         
                    0ȡ����ǰ�˶� ��
                    1ȡ��������˶� ��
                    2ȡ����ǰ�˶��ͻ����˶� ��
                    3 ����ֹͣ��
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
        /// ֹͣ������
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
        /// ��ȡʵʱ�ٶ�
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override float GetVel(short axis)
        {
            //�ж��Ƿ������ʼ������
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return 0.0f;

            //�����ٶ�
            float vel = 0.0f;

            //���������
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
        /// ��ȡ������λ��
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override float GetMpos(short axis)
        {
            //�ж��Ƿ������ʼ������
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
        /// ��ȡ������λ��
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override float GetPos(short axis)
        {

            //�ж��Ƿ������ʼ������
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return 0.0f;

            //����λ��
            float pos = 0.0f;
            //���������
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
        /// λ������
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override OperationResult ZeroPos(short axis)
        {
            //�ж��Ƿ������ʼ������
            var result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //���������
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
        /// ͨ���˶���֤
        /// </summary>
        /// <param name="axis">���</param>
        /// <returns>�������</returns>
        private OperationResult CommonMotionValidate(short axis)
        {
            OperationResult result = CommonInitedValidate();
            //�ж��Ƿ��Ѿ���ʼ��
            if (!result.IsSuccess) return result;

            //�ж��Ƿ���������
            if (IsMoving(axis))
            {
                result.IsSuccess = false;
                result.ErrorMsg = "����������";
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// ͨ�ó�ʼ����֤
        /// </summary>
        /// <returns></returns>
        private OperationResult CommonInitedValidate()
        {
            OperationResult result = new OperationResult();
            //�ж��Ƿ��Ѿ���ʼ��
            if (!InitedOK)
            {
                result.IsSuccess = false;
                result.ErrorMsg = "������δ��ʼ��";
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="command">ִ������</param>
        /// <param name="error">������</param>
        private void ErrorHandler(string command, int error)
        {
            string result = string.Empty;
            switch (error)
            {
                case 0: break;
                default:
                    result = string.Format("{0}" + "ָ��ִ�д��󣬴�����Ϊ{1}", command, error);
                    break;
            }
            if (result.Length > 0)
            {
                //throw new Exception(result);
                //System.Windows.Forms.MessageBox.Show(result);
            }
        }


        /// <summary>
        /// �ж�ĳ�����Ƿ���������
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override bool IsMoving(short axis)
        {
            OperationResult result = CommonInitedValidate();
            //�ж��Ƿ��Ѿ���ʼ��
            if (!result.IsSuccess) return false;
            //��������״̬
            int runstate = -1;
            //���������
            int error = 0;
            try
            {
                //��ȡ��״̬
                error = zmcaux.ZAux_Direct_GetIfIdle(Handle, axis, ref runstate);
                //��������֤
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                return runstate == 0;
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// ��ȡ�����ź�
        /// </summary>
        /// <param name="ioNum">IO��</param>
        /// <param name="inSignal">�źŽ��</param>
        /// <returns></returns>
        public override void GetInSignal(int ioNum,ref bool inSignal)
        {
            int error = 0;
            UInt32 result=0;
            try
            {
                error = zmcaux.ZAux_Direct_GetIn(Handle, ioNum, ref result);
                //��������֤
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                inSignal = result == 0 ? false : true;
            }
            catch (Exception)
            {

            }
            
        }
        /// <summary>
        /// ��ȡ����ź�״̬
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
                //��������֤
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                inSignal = result == 0 ? false : true;
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// ��������ź�
        /// </summary>
        /// <param name="ioNum">IO��</param>
        /// <param name="ioValue">״ֵ̬</param>
        /// <returns></returns>
        public override void SetOutSignal(int ioNum,UInt32 ioValue)
        {
            int error = 0;
            try
            {
                error = zmcaux.ZAux_Direct_SetOp(Handle,ioNum, ioValue);
                //��������֤
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
            }
            catch (Exception)
            {
                
            }
        }
        /// <summary>
        /// ��ȡ�˶�����ֵ
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="planValue"></param>
        public override void GetPlanValue(short axis,ref float planValue)
        {
            int error = 0;
            try
            {
                error=zmcaux.ZAux_Direct_GetEndMove(Handle, axis, ref planValue);
                //��������֤
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
            //��������֤
            ErrorHandler("ZAux_Direct_GetIfIdle", error);
            return posVal;
        }
        /// <summary>
        /// ��ȡ���ڲ�������ֵ
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="encoderValue"></param>
        public override void GetEncoderValue(short axis, ref float encoderValue)
        {
            int error = 0;
            try
            {
                error = zmcaux.ZAux_Direct_GetEncoder(Handle, axis, ref encoderValue);

                //��������֤
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
        /// ������ʹ��
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
                //��������֤
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                return string.Empty;
            }
            catch (Exception)
            {
                return "ʹ������ʧ��!�������Ƿ�ͨ��...";
            }
        }
        /// <summary>
        ///��ȡ������
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="torqueValue"></param>
        public override string GetDriveTorque(UInt32 axis,ref int torqueValue)
        {
            int error = 0;
            try
            {
                zmcaux.ZAux_BusCmd_GetDriveTorque(Handle, axis, ref torqueValue);
                //��������֤
                ErrorHandler("ZAux_Direct_GetIfIdle", error);
                return string.Empty;
            }
            catch (Exception)
            {
                return "��ȡ������ʧ��!��������...";
            }
            
        }
        /// <summary>
        /// ��ȡ��״̬
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
            zmcaux.ZAux_Direct_GetIn(Handle, originValue, ref originSignal);  //��ԭ��
            zmcaux.ZAux_Direct_GetAxisStatus(Handle, axis, ref status);  //��״̬
            zmcaux.ZAux_Direct_GetAxisEnable(Handle, axis, ref enable);  //��ʹ��
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
            axisStatus.origin = originSignal == 1 ? 1 : 0;  //ԭ���ź�
            axisStatus.AxisEnadle = enable == 1 ? 1 : 0;    //�ŷ�ʹ��
        }

        /// <summary>
        /// ��ȡ��״̬
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
            zmcaux.ZAux_Direct_GetIn(Handle, originValue, ref originSignal);  //��ԭ��
            zmcaux.ZAux_Direct_GetIn(Handle, fwd, ref fwdSignal);  //������λ
            zmcaux.ZAux_Direct_GetIn(Handle, rev, ref revSignal);  //�Ḻ��λ
            zmcaux.ZAux_Direct_GetAxisStatus(Handle, axis, ref status);  //��״̬
            zmcaux.ZAux_Direct_GetAxisEnable(Handle, axis, ref enable);  //��ʹ��
           
            axisStatus.positLimit = fwdSignal == 0 ? 0 : 1;
            axisStatus.minusLimit = revSignal == 0 ? 0 : 1;
            axisStatus.servoError = status == 4194304 ? 1 : 0;
            axisStatus.servoError = status == 8 ? 1 : 0;
            axisStatus.origin = originSignal == 1 ? 1 : 0;  //ԭ���ź�
            axisStatus.AxisEnadle = enable == 1 ? 1 : 0;    //�ŷ�ʹ��
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="model"></param>
        public override void ClearAlarm(UInt32 axis,int model)
        {
            zmcaux.ZAux_BusCmd_DriveClear(Handle, axis, 0);
        }
    }

    /// <summary>
    /// ���������
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// �Ƿ�ɹ�
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// ������Ϣ
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
