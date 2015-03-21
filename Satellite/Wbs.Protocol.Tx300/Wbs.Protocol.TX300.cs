using System;
using System.Runtime.InteropServices;
using Wbs.Protocol;
using Wbs.Protocol.Items;
using Wbs.Protocol.WbsDateTime;

namespace Wbs.Protocol.TX300
{
    /// <summary>
    /// TX300 Э����һЩԼ���ĳ�����
    /// </summary>
    public class TX300Items
    {
        /// <summary>
        /// ��ǰ���õ�����Э��汾���롣
        /// </summary>
        public static byte protocol_version = 0x10;
        /// <summary>
        /// terminal_id �ֶγ��ȣ��̶�ֵ��
        /// </summary>
        public static int terminal_id_length = 6;
        /// <summary>
        /// TX300 Э�����ݰ�ͷ���ȡ�
        /// </summary>
        public static int header_length = 17;
        /// <summary>
        /// TX300 Э�鷴�����ĳ��ȡ�
        /// </summary>
        public static int response_length = 14;
        /// <summary>
        /// �������DXϵ�У���ʱ������ĳ��ȡ�
        /// </summary>
        public static int security_time_stamp_length = 5;
        /// <summary>
        /// ��������ַ��ռ���ȡ�
        /// </summary>
        public static int server_address_length = 4;
        /// <summary>
        /// SMS ��������ַ��ռ���ȡ�
        /// </summary>
        public static int sms_server_length = 10;
        /// <summary>
        /// �ַ�����ʽ�� SMS ��������ַ���ȡ�
        /// </summary>
        public static int sms_server_len = 12;
    }

    
    /// <summary>
    /// TX300 Э���� DX ״̬�������������Ϣ
    /// </summary>
    public class StatusFilters
    {
        /// <summary>
        /// ��ȡ DX ϵ��״̬�����е���������Ϣ
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetStatusDX(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string str = "";
            str += bin[0] == '1' ? "ȼ����������������" : "";
            str += bin[1] == '1' ? "������������������" : "";
            str += bin[2] == '1' ? "����������������������" : "";
            str += bin[3] == '1' ? "������������������" : "";
            str += bin[4] == '1' ? "�ȵ���������������" : "";
            str += bin[5] == '1' ? "�������ͣ�������" : "";
            str += bin[6] == '1' ? "Һѹ�ͣ�������" : "";
            str += bin[7] == '1' ? "��ȴˮ��������" : "";
            if ("" != str)
                str = str.Substring(0, str.Length - 1);
            return str;
        }
    }

    
    /// <summary>
    /// TX10G �ı�������
    /// </summary>
    public enum TX10GAlarms : byte
    {
        /// <summary>
        /// û�б����򱨾������
        /// </summary>
        NoAlarm = 0x00,
        /// <summary>
        /// ����Դ�ϱ�����
        /// </summary>
        ChargingOff = 0x20,
        /// <summary>
        /// ͣ����ʱ������
        /// </summary>
        StopTimeout = 0x40,
        /// <summary>
        /// ��ص����ͱ�����
        /// </summary>
        BatteryLow = 0x80
    }
    /// <summary>
    /// TX10G �ı�������
    /// </summary>
    public class tx10g_alarms
    {
        /// <summary>
        /// ��ȡ TX10G �ն˵ı�����Ϣ
        /// </summary>
        /// <param name="b">�������ݡ�</param>
        /// <returns>���ر���������Ϣ��</returns>
        public static string GetAlarms(byte b)
        {
            string ret = "";
            if ((b & (byte)TX10GAlarms.BatteryLow) == (byte)TX10GAlarms.BatteryLow)
                ret += "��ص����͡�";
            if ((b & (byte)TX10GAlarms.ChargingOff) == (byte)TX10GAlarms.ChargingOff)
                ret += "����Դ�ϡ�";
            if ((b & (byte)TX10GAlarms.StopTimeout) == (byte)TX10GAlarms.StopTimeout)
                ret += "ͣ����ʱ��";
            if (ret != "")
                ret = ret.Substring(0, ret.Length - 1);
            return ret;
        }
    }
    

    /// <summary>
    /// TX300 Э���б�����������͡�
    /// </summary>
    public class SecuritySignals
    {
        /// <summary>
        /// ����/����������
        /// </summary>
        public const byte ENABLE = 0x00;
        /// <summary>
        /// ���ñ�������ܡ�
        /// </summary>
        public const byte DISABLE = 0x10;
        /// <summary>
        /// �����̵ı������
        /// </summary>
        public const byte AGENT = 0x20;
        /// <summary>
        /// ���磨��ɽ���ı������
        /// </summary>
        public const byte DOOSAN = 0x40;
        /// <summary>
        /// ��ȡ�ַ��������ı����������͡�
        /// </summary>
        /// <param name="b">�����Ʊ�ʾ�ı���������롣</param>
        /// <returns>�ַ��������ı�������ܡ�</returns>
        public static string GetSecuritySignal(byte b)
        {
            string s = "N/A";
            int index = DePow(b / 0x10 * 2);
            int ubd = GetSecuritySignal().GetUpperBound(0);
            if (index > ubd)
                return s;
            else
                return GetSecuritySignal()[index];
        }
        /// <summary>
        /// ��ȡ���ֱ������
        /// </summary>
        /// <returns>���ظ��ֱ���������������</returns>
        public static string[] GetSecuritySignal()
        {
            string[] s = new string[] { "����/���", "���ñ�������", "����������", "��������" };
            return s;
        }
        /// <summary>
        /// ͨ��ָ����ֵ��ȡ��Ϊ 2 ��ָ����
        /// </summary>
        /// <param name="i">ָ������ֵ��</param>
        /// <returns>���� 2 ��ָ����</returns>
        private static int DePow(int i)
        {
            int ret = 0;
            while (Math.Truncate(Math.Pow(2, ret)) < i)
            {
                ret++;
            }
            return ret;
        }
    }

    /// <summary>
    /// EPOS ������Ϣ
    /// </summary>
    public class AlarmsStatus
    {
        /// <summary>
        /// ��ȡ������Ϣ��״̬��
        /// </summary>
        /// <param name="b">������Ϣ��</param>
        /// <returns>�����ַ�����ʽ�ı���������</returns>
        public static string GetAlarmsStatus(byte b)
        {
            string s = "";
            switch (b)
            {
                case 0x00:
                case 0x30: s = "��ȫ"; break;
                case 0x01:
                case 0x31: s = "Σ��"; break;
                default: s = "N/A"; break;
            }
            return s;
        }
        /// <summary>
        /// ��ȡ�ն˵ı�����Ϣֵ��
        /// </summary>
        /// <param name="b">������Ϣ���ݡ�</param>
        /// <returns>���ر�����Ϣ��</returns>
        public static string GetAlarmsValue(byte[] b)
        {
            string bin = ProtocolItems.IntToDigit(b[0], 2, 8) + ProtocolItems.IntToDigit(b[1], 2, 8);
            //int alarm = b;
            string ss = "";
            ss += bin[0] == '1' ? "�ھ������ϡ�" : "";
            ss += bin[4] == '1' ? "GSM ä����" : "";
            ss += bin[5] == '1' ? "�ն���Ǵ򿪡�" : "";
            ss += bin[6] == '1' ? "GSM ���߶�·��" : "";
            ss += bin[7] == '1' ? "GSM ���߽ӵء�" : "";
            ss += bin[8] == '1' ? "GSM ���߶ϡ�" : "";
            ss += bin[9] == '1' ? "EPOS ͨѶ�쳣��" : "";
            ss += bin[10] == '1' ? "Sim ���γ���" : "";
            ss += bin[11] == '1' ? "Խ�硢" : "";
            ss += bin[12] == '1' ? "�������߶ϡ�" : "";
            ss += bin[13] == '1' ? "���١�" : "";
            ss += bin[14] == '1' ? "GPS ���߶ϡ�" : "";
            ss += bin[15] == '1' ? "�ն���ӵ�Դ�ϡ�" : "";
            if (ss.Length > 0)
                ss = ss.Substring(0, ss.Length - 1);
            else
                ss = "�ޱ���";
            return ss;
        }
    }
    /// <summary>
    /// EPOS ����ִ��״̬��
    /// </summary>
    public class EposStatus
    {
        /// <summary>
        /// ��ȡ EPOS ����ִ��״ֵ̬��
        /// </summary>
        /// <param name="b">EPOS �����ִ��״̬�롣</param>
        /// <returns>���� EPOS ִ��״ֵ̬��</returns>
        public static string GetEposStatus(byte b)
        {
            string ss = "";
            switch (b)
            {
                case 0x00: ss = "EPOS ����ʧ��"; break;
                case 0x01: ss = "EPOS ����ɹ�"; break;
                default: ss = "N/A"; break;
            }
            return ss;
        }
    }
    
    /// <summary>
    /// 0xEE00 �еĴ������͡�
    /// </summary>
    public class ErrorTypes
    {
        /// <summary>
        /// F/W ���µ����ն���û�д��������Ĺ��ܡ�
        /// </summary>
        public static byte f_w_upgrade = 0x10;
        /// <summary>
        /// EPOS û�������������ݡ�
        /// </summary>
        public static byte epos_not_resp = 0x20;
        /// <summary>
        /// EPOS û������ʱ���͵� EPOS ���
        /// </summary>
        public static byte epos_not_start = 0x30;
        /// <summary>
        /// ͨ�� 0xEE00 �����еĴ������ȡ�������͡�
        /// </summary>
        /// <param name="b">�����롣</param>
        /// <returns>���ش������͡�</returns>
        public static string GetErrorTypes(byte b)
        {
            string ss = "";
            switch (b)
            {
                case 0x10: ss = "F/W �汾����"; break;
                case 0x20: ss = "EPOS �޷���"; break;
                case 0x30: ss = "��û������ EPOS ����"; break;
                default: ss = "N/A"; break;
            }
            return ss;
        }
    }
    /// <summary>
    /// Զ�̿����ն˵����ͣ�0x3000��
    /// </summary>
    public class RemoteControls
    {
        /// <summary>
        /// Զ�̿�����
        /// </summary>
        public const byte remote_on = 0xF0;
        /// <summary>
        /// Զ�̹ػ���
        /// </summary>
        public const byte remote_off = 0x0F;
        /// <summary>
        /// ��ȡԶ�̿������
        /// </summary>
        /// <param name="b">Զ�̿��ƴ��롣</param>
        /// <returns>����Զ�̿������</returns>
        public static string GetRemoteControls(byte b)
        {
            string s = "N/A";
            if (b == remote_off)
                s = "Զ�̿��ƣ��ػ�";
            else if (b == remote_on)
                s = "Զ�̿��ƣ�����";
            return s;
        }/*
        /// <summary>
        /// ��ȡԶ�̿���������롣
        /// </summary>
        /// <param name="s">Զ�̿������</param>
        /// <returns>����Զ�̿���������롣</returns>
        public static byte GetRemoteControls(string s)
        {
            byte ret = 0x00;
            if (s.CompareTo(GetRemoteControls()[0]) == 0)
                ret = remote_on;
            else if (s.CompareTo(GetRemoteControls()[1]) == 0)
                ret = remote_off;
            return ret;
        }*/
        /// <summary>
        /// ��ȡ����Զ�̿������
        /// </summary>
        /// <returns>�������е�Զ�̿������</returns>
        public static string[] GetRemoteControls()
        {
            string[] s = new string[] { "Զ�̿��ƣ�����", "Զ�̿��ƣ��ػ�" };
            return s;
        }
    }
    /// <summary>
    /// Զ�������ն˵����͡�
    /// </summary>
    public class ResetTypes
    {
        /// <summary>
        /// Ӳ�����ն˲���Ĭ�ϣ�UDP����ʽ���ӷ�������
        /// </summary>
        public const byte hw_reset = 0x00;
        /// <summary>
        /// �����ն˵� UDP ͨѶģʽ��
        /// </summary>
        public const byte reset_to_udp = 0x10;
        /// <summary>
        /// �����ն˵� SMS ͨѶģʽ��
        /// </summary>
        public const byte reset_to_sms = 0x20;
        /// <summary>
        /// �����ն˵�����ͨѶģʽ��
        /// </summary>
        public const byte reset_to_satellite = 0x30;
        /// <summary>
        /// �����ն˵� TCP ͨѶģʽ��
        /// </summary>
        public const byte reset_to_tcp = 0x40;
        /// <summary>
        /// ��ȡԶ��������ʽ��
        /// </summary>
        /// <param name="b">Զ���������͡�</param>
        /// <returns>����Զ���������͵�������</returns>
        public static string GetResetTypes(byte b)
        {
            string ss = "N/A";
            int index = b / 0x10;
            if (index > 4)
                return ss;
            else
                return GetResetTypes()[index];
        }
        /// <summary>
        /// ��ȡ����Զ���������
        /// </summary>
        /// <returns>�������е�Զ���������͡�</returns>
        public static string[] GetResetTypes()
        {
            string[] s = new string[] { "��Ĭ����������", "����Ϊ UDP ��ʽ", "����Ϊ SMS ��ʽ", "����Ϊ����ͨѶ��ʽ", "����Ϊ TCP ��ʽ" };
            return s;
        }
    }

    /// <summary>
    /// Sim ������������״̬�롣
    /// </summary>
    public class SimLockStatus
    {
        /// <summary>
        /// ѯ������״̬��
        /// </summary>
        public const byte status_ask = 0x00;
        /// <summary>
        /// ���� Sim ����
        /// </summary>
        public const byte status_lock = 0x10;
        /// <summary>
        /// ���� Sim ����
        /// </summary>
        public const byte status_unlock = 0x20;
        /// <summary>
        /// �޸Ľ������롣
        /// </summary>
        public const byte status_passwd = 0x30;
        /// <summary>
        /// Sim����������
        /// </summary>
        public const byte sim_locked = 0x0F;
        /// <summary>
        /// Sim��δ������
        /// </summary>
        public const byte sim_unlocked = 0xF0;
        /// <summary>
        /// ��ȡ Sim ��������������״̬��
        /// </summary>
        /// <param name="b">����������״̬��</param>
        /// <returns>��������������״̬������</returns>
        public static string GetLockStatus(byte b)
        {
            string ss = "N/A";
            if (b == 0x0F)
                ss = "Sim��������";
            else if (b == 0xF0)
                ss = "Sim��δ����";
            else
            {
                int index = b / 0x10;
                if (index <= 3)
                    ss = GetLockStatus()[index];
            }
            return ss;
        }
        /// <summary>
        /// ��ȡ Sim �������ĸ���״̬��
        /// </summary>
        /// <returns>���ظ��� Sim ��������״̬��</returns>
        public static string[] GetLockStatus()
        {
            string[] s = new string[] { "�ɼ�����״̬", "���� Sim ��", "���� Sim ��", "���Ľ�������" };
            return s;
        }
    }

    /// <summary>
    /// ��ȡ�Ǳ���Ϣ�е�ˮ�����ݡ�
    /// </summary>
    public class WaterTempers
    {
        /// <summary>
        /// ת�� EPOS �Ǳ���Ϣ EMD �е�ˮ�����ݡ�
        /// </summary>
        /// <param name="b">�����ƴ��롣</param>
        /// <returns>����ת�������ˮ�·�Χֵ��</returns>
        public static string GetWaterTemp(byte b)
        {
            string s = "";
            switch (b)
            {
                case 1: s = "41��60"; break;
                case 2: s = "61��71"; break;
                case 3: s = "72��78"; break;
                case 4: s = "79��85"; break;
                case 5: s = "86��94"; break;
                case 6: s = "95��98"; break;
                case 7: s = "99��101"; break;
                case 8: s = "102��104"; break;
                case 9: s = "105��107"; break;
                case 10: s = "> 108"; break;
                default: s = "< 40"; break;
            }
            return s;
        }
        /// <summary>
        /// ת�� EPOS �Ǳ���Ϣ EMD �е�ˮ�����ݡ�
        /// </summary>
        /// <param name="value">ˮ�����ݡ�</param>
        /// <returns>����ת�������ˮ�·�Χֵ��</returns>
        public static string GetWaterTempDX(ushort value)
        {
            string s = "";
            if (value == 0)
                value = 500;
            if (value <= 71)
                s = "108";
            else if (value < 88)
                s = "105";
            else if (value < 99)
                s = "102";
            else if (value < 107)
                s = "99";
            else if (value < 120)
                s = "95";
            else if (value < 163)
                s = "86";
            else if (value < 245)
                s = "79";
            else if (value < 314)
                s = "72";
            else if (value < 393)
                s = "61";
            else if (value < 462)
                s = "41";
            else
                s = "40";

            return s;
        }
    }

    /// <summary>
    /// ��ȡ EPOS ����������
    /// </summary>
    public class AlarmsDescription
    {
        /// <summary>
        /// ��ȡ TX Э�����ն˱�����������
        /// </summary>
        /// <param name="code">TX �ն˵ı������롣</param>
        /// <returns>���� TX �ն˵ı���������</returns>
        public static string GetAlarmDescription(ushort code)
        {
            uint tmp = code;
            string s = "";
            if (tmp / 32 > 0)
            {
                s += "�����Ρ�";
                tmp = tmp % 32;
            }
            if (tmp / 16 > 0)
            {
                s += "Խ�硢";
                tmp = tmp % 16;
            }
            if (tmp / 8 > 0)
            {
                s += "�������߶ϡ�";
                tmp = tmp % 8;
            }
            if (tmp / 4 > 0)
            {
                s += "���١�";
                tmp = tmp % 4;
            }
            if (tmp / 2 > 0)
            {
                s += "GPS���߶ϡ�";
                tmp = tmp % 2;
            }
            if (tmp / 1 > 0)
            {
                s += "����ϡ�";
            }
            return s.Substring(0, s.Length - 1);
        }
        /// <summary>
        /// ��ȡ T100 Э���е��ն˱���������
        /// </summary>
        /// <param name="arm">�ն˱������ݡ�</param>
        /// <returns>�ն˱���������</returns>
        public static string GetAlarmDescription(string arm)
        {
            return arm.Replace("M", "�����").Replace("G", "GPS���߶�").Replace("A", "����ϡ�GPS���߶�");
        }
        /// <summary>
        /// ��ȡ DH �ͺ� EPOS ����������
        /// </summary>
        /// <param name="code">���ϴ��롣</param>
        /// <returns>����������</returns>
        public static string GetDescription(byte code)
        {
            string s = "";
            switch (code)
            {
                case 1:
                    s = "��ű�������·"; break;
                case 2:
                    s = "��ѹ��ŷ���·"; break;
                case 3:
                    s = "��ת���ȵ�ŷ���·"; break;
                case 11:
                    s = "��ű�������·"; break;
                case 12:
                    s = "��ѹ��ŷ���·"; break;
                case 13:
                    s = "��ת���ȵ�ŷ���·"; break;
                case 21:
                    s = "������ť����쳣(H)"; break;
                case 22:
                    s = "������ť����쳣(L)"; break;
                case 23:
                    s = "TPS����쳣(H)"; break;
                case 24:
                    s = "TPS����쳣(L)"; break;
                case 25:
                    s = "ǰ��ѹ���������쳣(H)"; break;
                case 26:
                    s = "ǰ��ѹ���������쳣(L)"; break;
                case 27:
                    s = "���ѹ���������쳣(H)"; break;
                case 28:
                    s = "���ѹ���������쳣(L)"; break;
                case 29:
                    s = "Speed����������쳣"; break;
                case 30:
                    s = "T/Mѹ������쳣"; break;
                case 31:
                    s = "ȼ�ʹ������ӵ�"; break;
                case 32:
                    s = "ȼ�ʹ�������·"; break;
                case 33:
                    s = "�������ѹ����"; break;
                case 34:
                    s = "�������ѹ����"; break;
                case 41:
                    s = "��������������"; break;
                case 42:
                    s = "��������ѹ��"; break;
                case 43:
                    s = "��ȴˮ����"; break;
                case 44:
                    s = "Һѹ�͹���"; break;
                case 82:
                    s = "ͨ���쳣"; break;
                default:
                    s = "δ֪"; break;
            }
            return s;
        }
        /// <summary>
        /// ��ȡ DX �ͺŹ��ϱ����е� FMI ��Ϣ������
        /// </summary>
        /// <param name="code">FMI ���롣</param>
        /// <returns>FMI ��Ϣ������</returns>
        public static string GetFMIDescription(byte code)
        {
            string str = "";
            switch (code)
            {
                case 0:
                    str = "Above normal Range(DATA VALID but ABOVE NORMAL OPERATIONAL RANGE)";
                    break;
                case 1:
                    str = "Below normal Range(DATA VALID but BELOW NORMAL OPERATIONAL RANGE)";
                    break;
                case 2:
                    str = "Incorrect signal(DATA ERRATIC, INTERMITTENT OR INCORRECT)";
                    break;
                case 3:
                    str = "Voltage above nomal(VOLTAGE ABOVE NORMAL, OR SHORTED TO HIGH SOURCE)";
                    break;
                case 4:
                    str = "Voltage below nomal(VOLTAGE BELOW NORMAL, OR SHORTED TO LOW SOURCE)";
                    break;
                case 5:
                    str = "Current below nomal(CURRENT BELOW NORMAL OR OPEN CIRCUIT)";
                    break;
                case 6:
                    str = "Current above nomal(CURRENT ABOVE NORMAL OR GROUNDED CIRCUIT)";
                    break;
                case 7:
                    str = "SIGNAL ERROR";
                    break;
                case 8:
                    str = "Abnomal signal(ABNORMAL FRQUENCY OR PULSE WHDTH OR PERIOD)";
                    break;
                case 9:
                    str = "ABNORMAL UPDATE(ABNORMAL UPDATE RATE)";
                    break;
                case 10:
                    str = "ABNORMAL RATE OF CHANGE(ABNORMAL RATE OF CHANGE)";
                    break;
                case 11:
                    str = "Failure mode not identifiable(ROOT CAUSE NOT KNOWN - Malfunction)";
                    break;
                case 12:
                    str = "FAILUER";
                    break;
                case 13:
                    str = "OUT OF CALIBRATION";
                    break;
                case 14:
                    str = "SPECIAL INSTRUCTIONS";
                    break;
                case 15:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - LEAST SEVERSE LEVEL";
                    break;
                case 16:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - MODERATELY SEVERE LEVEL";
                    break;
                case 17:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - LEAST SEVERSE LEVEL";
                    break;
                case 18:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - MODERATELY SEVERE LEVEL";
                    break;
                case 19:
                    str = "RECEIVED NETWORK DATA IN ERROR";
                    break;
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    str = "RESERVED FOR SAE ASSIGNMENT";
                    break;
                case 31:
                    str = "NOT AVAILABLE OR CONDITION EXISTS";
                    break;
                default:
                    str = "Unknow";
                    break;
            }
            return str;
        }
        /// <summary>
        /// ��ȡ DX �ͺ� EPOS ����������
        /// </summary>
        /// <param name="code">���ϴ��롣</param>
        /// <returns>����������</returns>
        public static string GetDescriptionDX(byte code)
        {
            string s = "";
            switch (code)
            {
                case 1: s = "V210 PUMP P/V"; break;
                case 2: s = "V211 COOLING FAN P/V"; break;
                case 3: s = "V212 FLOW CONTROL P/V"; break;
                case 4: s = "V213 RELIEF PRESSURE UP S/V"; break;
                case 5: s = "V214 HIGH SPEED S/V"; break;
                case 6: s = "V215 SWING PRIORITY S/V"; break;
                case 7: s = "V216 REVERSE FAN SPEED S/V"; break;
                case 8: s = "V217 STARTER RELAY"; break;
                case 9: s = "V218 AFTER HEAT RELAY"; break;
                case 10: s = "V227 DIAL"; break;
                case 11: s = "V230 E/G CONTROL MOTOR SENSOR"; break;
                case 12: s = "V228 ACCEL. PEDAL SENSOR ERROR"; break;
                case 13: s = "V225 FUEL LEVEL SENSOR"; break;
                case 14: s = "V222 HYD. OIL TEMP. SENSOR"; break;
                case 15: s = "V223 WATER TEMPERATURE SENSOR"; break;
                case 16: s = "V224 ENGINE SPEED SENSOR"; break;
                case 17: s = "V220 FRONT PUMP PRESS. SENSOR"; break;
                case 18: s = "V221 REAL PUMP PRESS. SENSOR"; break;
                case 20: s = "V229 PARK BRAKE PRESS. SENSOR"; break;
                case 21: s = "V226 ALTERNATOR POTENTIAL"; break;
                case 23: s = "V203 COMMUNICATION ERROR J1939"; break;
                case 24: s = "V202 ECU COMMUNICATION ERROR"; break;
                case 25: s = "V201 CAUGE PANEL COMMUNICATION ERROR"; break;
                case 26: s = "V231 ACCEL PEDAL SWITCH ERROR"; break;
                case 30: s = "E011 COOLANT TEMPERATURE SENSOR"; break;
                case 31: s = "E012 FUEL TEMPERATURE SENSOR"; break;
                case 32: s = "E013 BOOST AIR TEMPERATURE SENSOR"; break;
                case 33: s = "E014 BOOST AIR PRESSURE SENSOR"; break;
                case 34: s = "E017 E/G OIL TEMPERATURE SENSOR"; break;
                case 35: s = "E018 E/G OIL PRESSURE SENSOR"; break;
                case 36: s = "E021 BATTERY VOLTAGE"; break;
                case 37: s = "E022 FUEL PRESSURE SENSOR"; break;
                case 38: s = "E032 FUEL PRESSURE MONITORING MPROP"; break;
                case 39: s = "E037 CAN-B LINE"; break;
                case 40: s = "E038 ENGINE OVERSPEED"; break;
                case 41: s = "E039 MAIN RELAY SCG"; break;
                case 42: s = "E041 REDUNDANT SHUTOFF PATH"; break;
                case 43: s = "E042 E/G SPEED SENSOR (CRANKSHAFT)"; break;
                case 44: s = "E043 E/G SPEED SENSOR (CAMSHAFT)"; break;
                case 45: s = "E044 E/G SPEED SENSOR"; break;
                case 46: s = "E045 EEPROM"; break;
                case 47: s = "E046 RECOVERY"; break;
                case 48: s = "E047 MONITORING OF PRV"; break;
                case 49: s = "E048 POWER SUPPLY"; break;
                case 50: s = "E049 MAIN RELAY SCB"; break;
                case 51: s = "E051 MAIN RELAY ECU"; break;
                case 52: s = "E091 CAN-A LINE"; break;
                case 53: s = "E058 SOLENOID POWERSTAGE 1"; break;
                case 54: s = "E059 SOLENOID POWERSTAGE 2"; break;
                case 55: s = "E061 SOLENOID POWERSTAGE 3"; break;
                case 56: s = "E062 SOLENOID POWERSTAGE 4"; break;
                case 57: s = "E063 SOLENOID POWERSTAGE 5"; break;
                case 58: s = "E064 SOLENOID POWERSTAGE 6"; break;
                case 59: s = "E083 PREHEAT LAMP"; break;
                case 60: s = "E068 PREHEAT RELAY"; break;
                case 61: s = "E065 FUEL HI PRESSURE PUMP"; break;
                case 62: s = "E097 MONITORING OF MISFIRE CYLINDER 1"; break;
                case 63: s = "E098 MONITORING OF MISFIRE CYLINDER 2"; break;
                case 64: s = "E099 MONITORING OF MISFIRE CYLINDER 3"; break;
                case 65: s = "E101 MONITORING OF MISFIRE CYLINDER 4"; break;
                case 66: s = "E102 MONITORING OF MISFIRE CYLINDER 5"; break;
                case 67: s = "E103 MONITORING OF MISFIRE CYLINDER 6"; break;
                case 68: s = "E104 MONITORING OF MISFIRE MULTIPLE CYLINDER"; break;
                case 69: s = "E105 MONITORING OF OVERRUN"; break;
                case 70: s = "E106 ENGINE SPEED REDUNDANT"; break;
                case 71: s = "E104 FAILURE OF RECEIVING ECU ERROR LOG"; break;
                default: s = "Unknow"; break;
            }
            return s;
        }
    }
    /// <summary>
    /// ��������������ź�
    /// </summary>
    public class MonitorEngineControl
    {
        /// <summary>
        /// ��ȡ DH �ͺŵķ��������������Ϣ
        /// </summary>
        /// <param name="bin">����</param>
        /// <returns></returns>
        public static string GetEngControl(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "��׼��" : "";
            tmp += bin[6] == '1' ? "�Զ����١�" : "";
            tmp += bin[5] == '1' ? "���ȡ�" : "";
            tmp += bin[4] == '1' ? "����ģʽ��" : "";
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
        /// <summary>
        /// ��ȡ DX �ͺŵķ��������������Ϣ
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        public static string GetEngControlDX(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "��׼��" : "";
            tmp += bin[6] == '1' ? "�Զ����١�" : "";
            tmp += bin[5] == '1' ? "����ģʽ��" : "";
            tmp += bin[4] == '1' ? "���ȡ�" : "";
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// �����ź�
    /// </summary>
    public class MonitorInput
    {
        /// <summary>
        /// ��ȡ�����ź�
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetInput(byte[] b)
        {
            int len = b.Length;
            string bin_1, bin_2, bin_3, bin_4;
            bin_1 = ProtocolItems.IntToDigit(b[0], 2, 8);
            bin_2 = ProtocolItems.IntToDigit(b[1], 2, 8);
            bin_3 = len == 2 ? "" : ProtocolItems.IntToDigit(b[2], 2, 8);
            bin_4 = len == 2 ? "" : ProtocolItems.IntToDigit(b[3], 2, 8);
            string tmp = "";
            tmp += len == 2 ? (bin_1[7] == '1' ? "�������" : "") : (bin_4[4] == '1' ? "�������" : "");
            tmp += len == 2 ? (bin_1[6] == '1' ? "��ѹ��" : "") : (bin_1[7] == '1' ? "��ѹ��" : "");
            tmp += len == 2 ? (bin_1[5] == '1' ? "����ѡ��" : "") : (bin_1[5] == '1' ? "����ѡ��" : "");
            tmp += len == 2 ? (bin_1[4] == '1' ? "�������ߡ�" : "") : (bin_1[4] == '1' ? "�������ߡ�" : "");
            tmp += len == 2 ? (bin_1[3] == '1' ? "�Զ����ߡ�" : "") : (bin_1[3] == '1' ? "�Զ����ߡ�" : "");
            tmp += len == 2 ? (bin_1[2] == '1' ? "��ҵ�ơ�" : "") : (bin_1[2] == '1' ? "��ҵ�ơ�" : "");
            tmp += len == 2 ? (bin_2[7] == '1' ? "����ѹ����" : "") : (bin_3[7] == '1' ? "����ѹ����" : "");
            tmp += len == 2 ? (bin_2[6] == '1' ? "��ҵѹ����" : "") : (bin_3[6] == '1' ? "��ҵѹ����" : "");
            tmp += len == 2 ? (bin_2[5] == '1' ? "��������ѹ��" : "") : (bin_3[1] == '1' ? "��������ѹ��" : "");
            tmp += len == 2 ? (bin_2[4] == '1' ? "������������" : "") : (bin_3[5] == '1' ? "������������" : "");
            tmp += len == 2 ? (bin_2[3] == '1' ? "������������" : "") : (bin_3[3] == '1' ? "������������" : "");
            tmp += len == 2 ? (bin_2[2] == '1' ? "�ȵ���������" : "") : (bin_3[4] == '1' ? "�ȵ���������" : "");
            tmp += len == 2 ? (bin_2[1] == '1' ? "������ѡ��" : "") : (bin_3[2] == '1' ? "������ѡ��" : "");
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// ģʽѡ��
    /// </summary>
    public class MonitorModule
    {
        /// <summary>
        /// ��ȡģʽѡ����Ϣ
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetModule(byte b, byte tt)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += "Power��" + (bin[7] == '1' ? "Power��" : "��׼��");
            tmp += "��ҵ��" + (tt == TerminalTypes.DH ? (bin[6] == '1' ? "�ڹ���" : "�ھ�") : (bin[5] == '1' ? "�ڹ���" : "�ھ�"));
            tmp += tt == TerminalTypes.DH ? (bin[5] == '1' ? "�Զ����١�" : "") : (bin[3] == '1' ? "�Զ����١�" : "");
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// �����Ϣ
    /// </summary>
    public class MonitorOutput
    {
        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetOutput(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "��ѹ��" : "";
            tmp += bin[6] == '1' ? "�������ߡ�" : "";
            tmp += bin[5] == '1' ? "��ת���ȡ�" : "";
            tmp += bin[4] == '1' ? "��ת��" : "";
            tmp += bin[3] == '1' ? "�������桢" : "";
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// �ȵ��������Ϣ��
    /// </summary>
    public class MonitorPilot
    {
        /// <summary>
        /// ��ȡ�ȵ��������Ϣ
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetPilot(byte b,byte tt)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "��硢" : "";
            tmp += bin[6] == '1' ? "��������ѹ��" : "";
            tmp += tt == TerminalTypes.DH ? (bin[5] == '1' ? "���������ȡ�" : "") : (bin[4] == '1' ? "����������" : "");
            tmp += tt == TerminalTypes.DH ? (bin[3] == '1' ? "�����ơ�" : "") : (bin[1] == '1' ? "������" : "");
            tmp += tt == TerminalTypes.DH ? (bin[1] == '1' ? "�����ɱ�����" : "") : (bin[2] == '1' ? "�����ɱ���" : "");
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// Һѹ��ѹ������
    /// </summary>
    public class MonitorPump
    {
        /// <summary>
        /// ��ȡҺѹ��ѹ����С
        /// </summary>
        /// <param name="pump"></param>
        /// <returns></returns>
        public static string GetPump(ushort pump, doosan type)
        {
            double p = pump * 1.0;
            if (type == doosan.DH)
            {
                if (pump > 1070)
                {
                    p = (p - 1000) / 8.16;
                }
            }
            else
            {
                p = (p * 5000 / 1024 - 1000) / 8.16;
            }
            return string.Format("{0:0}", p);
        }
    }
    /// <summary>
    /// ��ȡ EPOS ��Ϣ���Ǳ�����Ϣ���������ݡ�
    /// </summary>
    public class OilTempers
    {
        /// <summary>
        /// ת�� EPOS �������Ǳ� EMD ��Ϣ�е��������ݡ�
        /// </summary>
        /// <param name="b">����������</param>
        /// <returns>����ת��������������ݡ�</returns>
        public static string GetOilTemp(byte b)
        {
            string s = "";
            switch (b)
            {
                case 1: s = "< 30"; break;
                case 2: s = "30��50"; break;
                case 3: s = "50��75"; break;
                case 4: s = "75��85"; break;
                case 5: s = "85��95"; break;
                case 6: s = "> 96"; break;
                default: s = "0"; break;
            }
            return s;
        }
        /// <summary>
        /// ת�� EPOS ������ monitor ��Ϣ�е��������ݡ�
        /// </summary>
        /// <param name="value">�������ݡ�</param>
        /// <returns>����������</returns>
        public static string GetOilTempDX(ushort value)
        {
            if (value == 0)
                value = 500;
            string s = "";
            if (value < 114)
                s = "100";
            else if (value < 120)
                s = "95";
            else if (value < 127)
                s = "93";
            else if (value < 143)
                s = "90";
            else if (value < 163)
                s = "86";
            else if (value < 235)
                s = "80";
            else if (value < 330)
                s = "70";
            else if (value < 392)
                s = "60";
            else if (value < 434)
                s = "50";
            else if (value < 465)
                s = "40";
            else
                s = "40";

            return s;
        }
        /// <summary>
        /// ת�� DX ȼ��ʣ������
        /// </summary>
        /// <param name="value">EPOS ���ݡ�</param>
        /// <returns>ת�����ȼ��ʣ������0~100��</returns>
        public static byte GetOilLeftDX(ushort value)
        {
            int tmp = (0 == value) ? 400 : value;
            byte ret = 0;
            if (117 > tmp)
                ret = 100;
            else if (142 > tmp && 117 <= tmp)
                ret = 90;
            else if (189 > tmp && 142 <= tmp)
                ret = 80;
            else if (226 > tmp && 189 <= tmp)
                ret = 70;
            else if (255 > tmp && 226 <= tmp)
                ret = 60;
            else if (278 > tmp && 255 <= tmp)
                ret = 50;
            else if (297 > tmp && 278 <= tmp)
                ret = 40;
            else if (313 > tmp && 297 <= tmp)
                ret = 30;
            else if (327 > tmp && 313 <= tmp)
                ret = 20;
            else if (339 > tmp && 327 <= tmp)
                ret = 10;
            else if (339 <= tmp)
                ret = 0;

            return ret;
        }
    }

    /// <summary>
    /// TX300 Э���� 0xCC00 ����������״̬�ֶΡ�
    /// </summary>
    public class ConnectStatus
    {
        /// <summary>
        /// ��ȡ�ַ����������������͡�
        /// </summary>
        /// <param name="b">����������ʾ���������͡�</param>
        /// <returns>�ַ�����ʾ���������͡�</returns>
        public static string GetConnectStatus(byte b)
        {
            string s = "";
            switch (b)
            {
                case 0x00:
                case 0x10:
                case 0x20:
                case 0x40:
                    s = "��������������"; break;
                case 0x01: s = "��������ʱ����"; break;
                case 0x02: s = "��״̬����ɾ������"; break;
                case 0x03: s = "ģ���޻ظ�"; break;
                case 0x04: s = "SMS ���Ź��̴���"; break;
                case 0x05: s = "CPU ��ģ�� 7 ����û��ͨѶ"; break;
                case 0x06: s = "CPU ���� 12 ����û�м������"; break;
                case 0x07: s = "SMS ������ 5 ������û������"; break;
                case 0x08: s = "��ʼ��ʱ�䳬�� 2 ��������"; break;
                case 0x09: s = "ģ���ʼ��ʱ������������׶λظ�"; break;
                case 0x0A: s = "�������ڱ���ʱ�䵫û���ϴ�����"; break;
                case 0x0B: s = "�ն˵͵�ѹ����ʱ"; break;
                case 0x0C: s = "���� 0xCC00 ʱû�� Sim ������"; break;
                case 0x0D: s = "5 Сʱ�ڷ�������һ�ζ�û�ɹ�"; break;
                case 0x0E: s = "5 ������ CSQ > limit ��û������"; break;
                case 0x0F: s = "Sim ��������������"; break;
                case 0x11: s = "��ͼ���� GPRS ʱ���Ӵ���"; break;
                case 0x12: s = "������ģ���Դ�ر�"; break;
                case 0x13: s = "GPS ��������� GPS ��Ȼ��������"; break;
                case 0x14: s = "���ݹ����г�����������"; break;
                case 0x15: s = "�������洫�Ͷ��и���"; break;
                case 0x16: s = "���� EPOS ���Ͷ��и���"; break;
                case 0x17: s = "���ͱ���ʱ��������������������"; break;
                case 0x18: s = "���� EPOS ���ʹ���ʱ�䣨20�룩"; break;
                case 0x19: s = "�������ݽ��д���ʱ�䣨5�֣�"; break;
                case 0x1A: s = "ģ�����ų�ʱ������10�룩"; break;
                case 0x1B: s = "EPOS ���ų�ʱ������10�룩"; break;
                case 0x1C: s = "ģ�����Ź����д������������1�֣�"; break;
                case 0x1D: s = "EPOS ���Ź����д������������1�֣�"; break;
                case 0x1E: s = "����������д���1�֣�"; break;
                default: s = "unknow"; break;
            }
            return s;
        }
    }

    /// <summary>
    /// TX300 ͨѶЭ���ͷ�ṹ��
    /// </summary>
    public class TX300
    {
        /// <summary>
        /// �����ܳ��ȡ�
        /// </summary>
        protected ushort total_length;
        /// <summary>
        /// ͨѶ���õ�Э�顣
        /// </summary>
        private byte protocol_type;
        /// <summary>
        /// �ն����͡�
        /// </summary>
        private byte terminal_type;
        /// <summary>
        /// �����֡�ÿһ�����ݰ���Ψһ��ʶ��
        /// </summary>
        private ushort command_id;
        /// <summary>
        /// �����������õ�ͨѶЭ��汾�����е������汾���¼��ݡ�
        /// </summary>
        private byte protocol_version;
        /// <summary>
        /// �ն˷�����Ϣ����ˮ�š�
        /// </summary>
        private ushort sequence_id;
        /// <summary>
        /// �ն˺��롣
        /// </summary>
        private byte[] terminal_id = new byte[TX300Items.terminal_id_length];
        /// <summary>
        /// ֡ ID��
        /// </summary>
        private byte package_id;
        /// <summary>
        /// ֡����
        /// </summary>
        private byte total_package;
        /// <summary>
        /// ���� TX300 Э�������Ķ���������
        /// </summary>
        protected byte[] content;
        /// <summary>
        /// ȥ�� TX300 Э���ͷ֮�����Ϣ���ݡ�
        /// </summary>
        private byte[] messages;
        protected int iIndex = 0;
        /// <summary>
        /// ����һ���µİ��� TX300 ����ͨѶЭ�����õ����ݰ���
        /// </summary>
        public TX300()
        {
            total_length = 0;
            protocol_version = TX300Items.protocol_version;
            package_id = 1;
            total_package = 1;
            sequence_id = 0xFFFF;
        }
        /// <summary>
        /// ͨ��һ�� 16 ���Ƶ��ַ�����ʾ�Ķ�������������һ�����ݰ���
        /// </summary>
        /// <param name="data_hex_string">�� 16 �����ַ�����ʾ�Ķ���������</param>
        public TX300(string data_hex_string)
        {
            content = ProtocolItems.GetBytes(data_hex_string);
        }
        /// <summary>
        /// ͨ��һ������������������һ�����ݰ���
        /// </summary>
        /// <param name="b"></param>
        public TX300(byte[] b)
        {
            content = new byte[b.Length];
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// ��ȡ���ݰ����ܳ��ȡ�
        /// </summary>
        public ushort TotalLength
        {
            get { return total_length; }
        }
        /// <summary>
        /// ��ȡ���������ݰ���ͨѶЭ�����͡�
        /// </summary>
        public byte ProtocolType
        {
            get { return protocol_type; }
            set { protocol_type = value; }
        }
        /// <summary>
        /// ��ȡ���������ݰ����ն����͡�
        /// </summary>
        public byte TerminalType
        {
            get { return terminal_type; }
            set { terminal_type = value; }
        }
        /// <summary>
        /// ��ȡ�����������֡�
        /// </summary>
        public ushort CommandID
        {
            get { return command_id; }
            set { command_id = value; }
        }
        /// <summary>
        /// ��ȡ������ͨѶЭ��汾�š�
        /// </summary>
        public byte ProtocolVersion
        {
            get { return protocol_version; }
            set { protocol_version = value; }
        }
        /// <summary>
        /// ��ȡ��������ˮ�š�
        /// </summary>
        public ushort SequenceID
        {
            get { return sequence_id; }
            set { sequence_id = value; }
        }
        /// <summary>
        /// ��ȡ�������ն� ID��
        /// </summary>
        public string TerminalID
        {
            get 
            {
                string t = ProtocolItems.GetHex(terminal_id);
                // ȥ����ǰ��� 0 ������ն˵� Sim �����롣
                return t.Substring(1); 
            }
            set
            {
                byte[] b = ProtocolItems.GetBytes(value);
                Buffer.BlockCopy(b, 0, terminal_id, 0, b.Length);
                b = null;
            }
        }
        /// <summary>
        /// ��ȡ���������ݰ���֡������
        /// </summary>
        public byte PackageID
        {
            get { return package_id; }
            set { package_id = value; }
        }
        /// <summary>
        /// ��ȡ��������֡����
        /// </summary>
        public byte TotalPackage
        {
            get { return total_package; }
            set { total_package = value; }
        }
        /// <summary>
        /// ��ȡ���������ݰ��Ķ���������
        /// </summary>
        public byte[] Content
        {
            get { return content; }
            set { Buffer.BlockCopy(value, 0, content, 0, value.Length); }
        }
        /// <summary>
        /// �� 16 ���Ƶ��ַ����������������������á�
        /// </summary>
        public string SetContent
        {
            set { content = ProtocolItems.GetBytes(value); }
        }
        /// <summary>
        /// ��ȡ���������ݰ����ݣ�ȥ�� TX300 Э�����ݰ�ͷ���ֵ����ݣ���
        /// </summary>
        public byte[] MsgContent
        {
            get { return messages; }
            set { Buffer.BlockCopy(value, 0, messages, 0, value.Length); }
        }

        /// <summary>
        /// ������ݰ��и��ֶε�ֵ�Ա������趨�ʹ����
        /// </summary>
        public virtual void Clear()
        {
            content = null;
            messages = null;
            terminal_id.Initialize();
            command_id = 0;
            sequence_id = 0;
        }
        /// <summary>
        /// �������ֶε�ֵ���� TX300 ����ͨѶЭ������
        /// </summary>
        public virtual void msg_to_package()
        {
            // ���� total_length �ֶΣ��ȵ��������ٴ���
            iIndex = Marshal.SizeOf(total_length);
            // ��� protocol_type �ֶ�
            iIndex += Marshal.SizeOf(protocol_type);
            content = new byte[iIndex];
            content[content.Length - Marshal.SizeOf(protocol_type)] = protocol_type;
            // ��� terminal_type �ֶ�
            iIndex += Marshal.SizeOf(terminal_type);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(terminal_type)] = terminal_type;
            // ��� command_id �ֶ�
            byte[] b = BitConverter.GetBytes(command_id);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, content.Length - b.Length, b.Length);
            // ��� protocol_version �ֶ�
            iIndex += Marshal.SizeOf(protocol_version);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(protocol_version)] = protocol_version;
            // ��� sequence_id �ֶ�
            b = BitConverter.GetBytes(sequence_id);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, content.Length - b.Length, b.Length);
            // ��� terminal_id �ֶ�
            iIndex += terminal_id.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(terminal_id, 0, content, content.Length - terminal_id.Length, terminal_id.Length);
            // ��� package_id �ֶ�
            iIndex += Marshal.SizeOf(package_id);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(package_id)] = package_id;
            // ��� total_package �ֶ�
            iIndex += Marshal.SizeOf(total_package);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(total_package)] = total_package;
            // ������� total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// �������ֶΰ������µ� TX300 ͨѶЭ�����������
        /// </summary>
        /// <returns></returns>
        public virtual bool package_to_msg()
        {
            iIndex = 0;
            // ��� total_length �ֶ�
            total_length = BitConverter.ToUInt16(content, iIndex);
            iIndex += Marshal.SizeOf(total_length);
            // ��� protocol_type �ֶ�
            protocol_type = content[iIndex];
            iIndex += Marshal.SizeOf(protocol_type);
            // ��� terminal_type �ֶ�
            terminal_type = content[iIndex];
            iIndex += Marshal.SizeOf(terminal_type);
            // ��� command_id �ֶ�
            command_id = BitConverter.ToUInt16(content, iIndex);
            iIndex += Marshal.SizeOf(command_id);
            // ��� protocol_version �ֶ�
            protocol_version = content[iIndex];
            iIndex += Marshal.SizeOf(protocol_version);
            // ��� sequence_id �ֶ�
            sequence_id = BitConverter.ToUInt16(content, iIndex);
            iIndex += Marshal.SizeOf(sequence_id);
            // ��� terminal_id �ֶ�
            Buffer.BlockCopy(content, iIndex, terminal_id, 0, terminal_id.Length);
            iIndex += terminal_id.Length;
            // ��� package_id �ֶ�
            package_id = content[iIndex];
            iIndex += Marshal.SizeOf(package_id);
            // ��� total_package �ֶ�
            total_package = content[iIndex];
            iIndex += Marshal.SizeOf(total_package);
            // ��� messages �ֶ�
            if (total_length > iIndex)
            {
                messages = new byte[total_length - iIndex];
                Buffer.BlockCopy(content, iIndex, messages, 0, messages.Length);
            }
            return iIndex == total_length;
        }
    }
    /// <summary>
    /// 0x1000 ��λ��Ϣ������
    /// </summary>
    public class TX300_1000 : TX300
    {
        public TX300_1000()
        {
            CommandID = 0x1000;
        }
        /// <summary>
        /// ����λ��Ϣ������
        /// </summary>
        private byte request_no;
        /// <summary>
        /// ��������Ķ�λ��Ϣ������
        /// </summary>
        public byte RequestNo
        {
            set { request_no = value; }
            //get { return request_no; }
        }
        /// <summary>
        /// �� 0x1000 �����еĸ����ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� request_no
            iIndex += Marshal.SizeOf(request_no);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = request_no;
            // ��� total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x3000 Զ�̿��ػ����
    /// </summary>
    public class TX300_3000 : TX300
    {
        public TX300_3000()
        {
            CommandID = 0x3000;
        }
        /// <summary>
        /// Զ�̿���״̬��
        /// </summary>
        private byte control_status;
        /// <summary>
        /// ���ƶ���ʱ�䣺�ڴ�ʱ��֮��ʼ���ơ�
        /// </summary>
        private byte action_time;
        /// <summary>
        /// ����Զ�̿���״̬���͡�
        /// </summary>
        public byte ControlStatus
        {
            set { control_status = value; }
        }
        /// <summary>
        /// ����Զ�̿��ƶ���ʱ�䡣
        /// </summary>
        public byte ActionTime
        {
            set { action_time = value; }
        }
        /// <summary>
        /// �� 0x3000 �����еĸ����ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� control_status
            iIndex += Marshal.SizeOf(control_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(control_status)] = control_status;
            // ��� action_time
            iIndex += Marshal.SizeOf(action_time);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(action_time)] = action_time;
            // ��� total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x4000 Զ������������
    /// </summary>
    public class TX300_4000 : TX300
    {
        public TX300_4000()
        {
            CommandID = 0x4000;
        }
        /// <summary>
        /// ����״̬��
        /// </summary>
        private byte reset_status;
        /// <summary>
        /// ���ö���ʱ�䣺�ڴ�ʱ������á�
        /// </summary>
        private byte action_time;
        /// <summary>
        /// ��������״̬�롣
        /// </summary>
        public byte ResetStatus
        {
            set { reset_status = value; }
        }
        /// <summary>
        /// �������ö���ʱ�䡣
        /// </summary>
        public byte ActionTime
        {
            set { action_time = value; }
        }
        /// <summary>
        /// �� 0x4000 �����еĸ����ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� reset_status
            iIndex += Marshal.SizeOf(reset_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = reset_status;
            // ��� action_time
            iIndex += Marshal.SizeOf(action_time);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = action_time;
            // ��� total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x6002 EPOS ��ת��Ϣͼ������������
    /// </summary>
    public class TX300_6002 : TX300
    {
        public TX300_6002()
        {
            CommandID = 0x6002;
        }
        /// <summary>
        /// �����ͼ�η������ݵ�ʱ�䣬��λ���ӡ�
        /// </summary>
        private byte request_times = 1;
        /// <summary>
        /// ��������ķ������ݵ�ʱ�䣬��λ���ӡ�
        /// </summary>
        public byte RequestTimes
        {
            set { request_times = value; }
        }
        /// <summary>
        /// �� 0x6002 �����еĸ����ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� request_times
            iIndex += Marshal.SizeOf(request_times);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = request_times;
            // ��� total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x6007 ����������ء�
    /// </summary>
    public class TX300_6007 : TX300
    {
        public TX300_6007()
        {
            CommandID = 0x6007;
        }
        /// <summary>
        /// ���������롣
        /// </summary>
        private byte security_code;
        /// <summary>
        /// DX ϵ�б��������ʱ�����YYMMDDHHMM��
        /// </summary>
        private byte[] time_stamp = new byte[TX300Items.security_time_stamp_length];
        /// <summary>
        /// ���ñ��������롣
        /// </summary>
        public byte SecurityCode
        {
            set { security_code = value; }
        }
        /// <summary>
        /// �� 0x6007 �����еĸ����ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // �ж� DX �� DH �ͺŵ��ն�ͨѶ����
            // DX ϵ�б���������ʱ�����
            if (TerminalType == TerminalTypes.DX)
            {
                string stamp = DateTime.Now.ToString("yyMMddHHmm");
                Buffer.BlockCopy(ProtocolItems.GetBytes(stamp), 0, time_stamp, 0, TX300Items.security_time_stamp_length);
                iIndex += TX300Items.security_time_stamp_length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(time_stamp, 0, content, iIndex - TX300Items.security_time_stamp_length, TX300Items.security_time_stamp_length);
            }
            // ��� security_code
            iIndex += Marshal.SizeOf(security_code);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = security_code;
            // ��� total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x600B ���� DH80G/DX60/Loader ��תʱ��������
    /// </summary>
    public class TX300_600B : TX300
    {
        public TX300_600B()
        {
            CommandID = 0x600B;
        }
        /// <summary>
        /// ��תʱ�䣺Сʱ��
        /// </summary>
        private ushort run_hours;
        /// <summary>
        /// ��תʱ�䣺���ӡ�
        /// </summary>
        private byte run_minutes;
        /// <summary>
        /// ���� DH80G/DX60/Loader ��תʱ�䣺Сʱ��
        /// </summary>
        public ushort Hours
        {
            set { run_hours = value; }
        }
        /// <summary>
        /// ���� DH80G/DX60/Loader ��תʱ�䣺���ӡ�
        /// </summary>
        public byte Minutes
        {
            set { run_minutes = value; }
        }
        /// <summary>
        /// �� 0x600B ������ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� run_hours
            byte[] b = BitConverter.GetBytes(run_hours);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
            // ��� run_minutes
            iIndex += Marshal.SizeOf(run_minutes);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(run_minutes)] = run_minutes;
            // ��� total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// TX10G �Ĳ����������
    /// </summary>
    public class TX10G_7010 : TX300
    {
        public TX10G_7010()
        {
            CommandID = 0x7010;
        }
        /// <summary>
        /// ����״̬��0x00���ɼ���0xFF�����á�
        /// </summary>
        private byte msg_status;
        /// <summary>
        /// ���ڱ���ʱ������
        /// </summary>
        private ushort period;
        /// <summary>
        /// ͣ����ʱʱ�䡣
        /// </summary>
        private byte stoped;
        /// <summary>
        /// ����ʱ������
        /// </summary>
        private byte heartbeat;
        /// <summary>
        /// ����ź�ǿ�ȣ�ä������
        /// </summary>
        private byte csq_low;
        /// <summary>
        /// SMS ͨѶ�ź�ǿ�����á�
        /// </summary>
        private byte csq_sms;
        /// <summary>
        /// �󱸵�ص�ѹ��
        /// </summary>
        private byte battery;
        /// <summary>
        /// ������ IP ��ַ��
        /// </summary>
        private byte[] server_address = new byte[TX300Items.server_address_length];
        /// <summary>
        /// �������˿ڡ�
        /// </summary>
        private ushort server_port;
        /// <summary>
        /// SMS ��������ַ��
        /// </summary>
        private byte[] sms_address;
        /// <summary>
        /// ��ȡ����������״̬��
        /// </summary>
        public byte MsgStatus
        {
            get { return msg_status; }
            set { msg_status = value; }
        }
        /// <summary>
        /// ��ȡ�����ö��ڱ���ʱ������
        /// </summary>
        public ushort PeriodInteval
        {
            get { return period; }
            set { period = value; }
        }
        /// <summary>
        /// ��ȡ������ͣ����ʱʱ�䡣
        /// </summary>
        public byte StopTimeout
        {
            get { return stoped; }
            set { stoped = value; }
        }
        /// <summary>
        /// ��ȡ����������ʱ������
        /// </summary>
        public byte Heartbeat
        {
            get { return heartbeat; }
            set { heartbeat = value; }
        }
        /// <summary>
        /// ��ȡ����������ź�ǿ�ȡ�
        /// </summary>
        public byte LowCSQ
        {
            get { return csq_low; }
            set { csq_low = value; }
        }
        /// <summary>
        /// ��ȡ������ SMS ͨѶ�ź�ǿ�ȡ�
        /// </summary>
        public byte SMSCSQ
        {
            get { return csq_sms; }
            set { csq_sms = value; }
        }
        /// <summary>
        /// ��ȡ�����ú󱸵����͵�ѹ�趨��
        /// </summary>
        public byte Battery
        {
            get { return battery; }
            set { battery = value; }
        }
        /// <summary>
        /// ��ȡ������ GPRS ������ IP ��ַ��
        /// </summary>
        public string ServerIP
        {
            get
            {
                string ip = "";
                for (int i = 0; i < server_address.Length; i++)
                {
                    ip += server_address[i].ToString() + ".";
                }
                // ����ȥ������.��IP��ַ��
                return ip.Substring(0, ip.Length - 1); ;
            }
            set
            {
                string[] s = value.Split(new char[] { '.' });
                server_address = new byte[TX300Items.server_address_length];
                for (int i = 0; i < s.Length; i++)
                {
                    server_address[i] = byte.Parse(s[i]);
                }
            }
        }
        /// <summary>
        /// ��ȡ������ GPRS �������˿ں��롣
        /// </summary>
        public ushort ServerPort
        {
            get { return server_port; }
            set { server_port = value; }
        }
        /// <summary>
        /// ��ȡ������ SMS �����ַ��
        /// </summary>
        public string SmsAddress
        {
            get { return ProtocolItems.GetHex(sms_address); }
            set
            {
                if (value.Length % 2 != 0)
                    value = "0" + value;
                byte[] b = ProtocolItems.GetBytes(value);
                sms_address = new byte[b.Length + 1];
                Buffer.BlockCopy(b, 0, sms_address, 1, b.Length);
                sms_address[0] = (byte)b.Length;
            }
        }
        /// <summary>
        /// �� 0x7010 ����ĸ����ֶΰ���Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            byte[] b = null;
            // ��� msg_status
            iIndex += Marshal.SizeOf(msg_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = msg_status;
            if (msg_status == 0xFF)
            {
                // ��� period
                b = BitConverter.GetBytes(period);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� stoped
                iIndex += Marshal.SizeOf(stoped);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = stoped;
                // ��� heartbeat
                iIndex += Marshal.SizeOf(heartbeat);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = heartbeat;
                // ��� csq_low
                iIndex += Marshal.SizeOf(csq_low);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = csq_low;
                // ��� csq_sms
                iIndex += Marshal.SizeOf(csq_sms);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = csq_sms;
                // ��� battery
                iIndex += Marshal.SizeOf(battery);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = battery;
                // ��� server_address
                iIndex += server_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(server_address, 0, content, iIndex - server_address.Length, server_address.Length);
                // ��� server_port
                b = BitConverter.GetBytes(server_port);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� sms_address
                iIndex += sms_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(sms_address, 0, content, iIndex - sms_address.Length, sms_address.Length);
            }
            // ���´�� total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0xBB00 Sim����������������������
    /// </summary>
    public class TX300_BB00 : TX300
    {
        public TX300_BB00()
        {
            CommandID = 0xBB00;
        }
        /// <summary>
        /// ����״̬��
        /// </summary>
        private byte lock_status;
        /// <summary>
        /// �������롣
        /// </summary>
        private ushort password;
        /// <summary>
        /// ��������״̬��
        /// </summary>
        public byte LockStatus
        {
            set { lock_status = value; }
        }
        /// <summary>
        /// ���ý������룬4λ���֣�����4λʱȡ����ߵ�4λ���֡�
        /// </summary>
        public string PassWrod
        {
            set 
            {
                string pw = value;
                if (pw.Length > 4)
                    pw = pw.Substring(0, 4);
                try
                {
                    password = ushort.Parse(pw);
                }
                catch
                {
                    throw new Exception("�������벻����Ч�����ָ�ʽ��");
                }
            }
        }
        /// <summary>
        /// �� 0xBB00 �����еĸ����ֶΰ��� TX300 Э������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� lock_status
            iIndex += Marshal.SizeOf(lock_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = lock_status;
            // ��� password
            if ((lock_status == SimLockStatus.status_ask) || (lock_status == SimLockStatus.status_unlock))
                password = 0;
            byte[] b = BitConverter.GetBytes(password);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
            // ��� total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0xDD00 ȫ������������
    /// </summary>
    public class TX300_DD00 : TX300
    {
        public TX300_DD00()
        {
            CommandID = 0xDD00;
        }
        /// <summary>
        /// ����״̬��0x00ʱΪ��������ѯ����Щ���ã�0xFFʱΪ�������������Щ���á�
        /// </summary>
        private byte msg_status;
        /// <summary>
        /// �ն�F/W�汾���룬ֻ���ն˻ظ����������������б��ӶΡ�
        /// </summary>
        //private string fw_version;
        /// <summary>
        /// ��ǰ�ź�ǿ�ȣ�ֻ���ն˻ظ����������������б��ӶΡ�
        /// </summary>
        //private byte csq;
        /// <summary>
        /// ���ڱ���ʱ��������λ���ӡ�
        /// </summary>
        private ushort inteval_period;
        /// <summary>
        /// SMS-GPRSת��ʱ�������������ˣ���
        /// </summary>
        private ushort inteval_sms_gprs;
        /// <summary>
        /// ����Ⱥ�ǿ�����ơ�
        /// </summary>
        private byte limit_csq;
        /// <summary>
        /// ��ͺ󱸵�ص�ѹ���ơ�
        /// </summary>
        private byte limit_battery;
        /// <summary>
        /// ���ʱ�����ơ�
        /// </summary>
        private byte limit_speed;
        /// <summary>
        /// Խ��뾶��
        /// </summary>
        private byte limit_radius;
        /// <summary>
        /// Խ�����ĵ㾭�ȡ�
        /// </summary>
        private uint limit_longitude;
        /// <summary>
        /// Խ�����ĵ�γ�ȡ�
        /// </summary>
        private uint limit_latitude;
        /// <summary>
        /// ���ڱ����趨�������趨GPRS��SMS��Satelliteģʽ���Ƿ������ڱ��档
        /// </summary>
        private byte limit_period;
        /// <summary>
        /// ���������ŵĶ˿ڡ�
        /// </summary>
        private ushort server_port;
        /// <summary>
        /// ��������ַ��
        /// </summary>
        private byte[] server_address = new byte[TX300Items.server_address_length];
        /// <summary>
        /// SMS��������ַ��
        /// </summary>
        private byte[] sms_address = new byte[TX300Items.sms_server_length];
        /// <summary>
        /// �ն�ϵͳʱ�䡣
        /// </summary>
        private WbsDateTime.WbsDateTime wdt;
        /// <summary>
        /// ��ȡ��������Ϣ״̬��
        /// </summary>
        public byte MsgStatus
        {
            get { return msg_status; }
            set { msg_status = value; }
        }
        /// <summary>
        /// ��ȡ�ն�FW�汾��ֻ���ն˻ظ�����������Ϣ�����С�
        /// </summary>
        /*public string FW_Version
        {
            get { return fw_version; }
            //set { fw_version = value; }
        }
        /// <summary>
        /// ��ȡ��ǰ�ź�ǿ�ȡ�
        /// </summary>
        public byte CSQ
        {
            get { return csq; }
        }*/
        /// <summary>
        /// ��ȡ�����ö��ڱ���ʱ������
        /// </summary>
        public ushort IntevalPeriod
        {
            get { return inteval_period; }
            set { inteval_period = value; }
        }
        /// <summary>
        /// ��ȡ������SMS-GPRSת����ʱ������
        /// </summary>
        public ushort IntevalSmsGprs
        {
            get { return inteval_sms_gprs; }
            set { inteval_sms_gprs = value; }
        }
        /// <summary>
        /// ��ȡ����������ź�ǿ�����ơ�
        /// </summary>
        public byte LimitCSQ
        {
            get { return limit_csq; }
            set { limit_csq = value; }
        }
        /// <summary>
        /// ��ȡ��������͵�ѹ���ơ�
        /// </summary>
        public byte LimitBattery
        {
            get { return limit_battery; }
            set { limit_battery = value; }
        }
        /// <summary>
        /// ��ȡ���������ʱ�����ơ�
        /// </summary>
        public byte LimitSpeed
        {
            get { return limit_speed; }
            set { limit_speed = value; }
        }
        /// <summary>
        /// ��ȡ������Խ��뾶��
        /// </summary>
        public byte LimitRadius
        {
            get { return limit_radius; }
            set { limit_radius = value; }
        }
        /// <summary>
        /// ��ȡ������Խ�����ĵ㾭�ȡ�
        /// </summary>
        public uint LimitLongitude
        {
            get { return limit_longitude; }
            set { limit_longitude = value; }
        }
        /// <summary>
        /// ��ȡ������Խ�����ĵ�γ�ȡ�
        /// </summary>
        public uint LimitLatitude
        {
            get { return limit_latitude; }
            set { limit_latitude = value; }
        }
        /// <summary>
        /// ��ȡ�����ö��ڱ����趨��
        /// </summary>
        public byte LimitPeriod
        {
            get { return limit_period; }
            set { limit_period = value; }
        }
        /// <summary>
        /// ��ȡ�����÷������˿ں���
        /// </summary>
        public ushort ServerPort
        {
            get { return server_port; }
            set { server_port = value; }
        }
        /// <summary>
        /// ��ȡ�����÷�����IP��ַ��
        /// </summary>
        public string ServerIP
        {
            get
            {
                string ip = "";
                for (int i = 0; i < server_address.Length; i++)
                {
                    ip += server_address[i].ToString() + ".";
                }
                // ����ȥ������.��IP��ַ��
                return ip.Substring(0, ip.Length - 1); ;
            }
            set
            {
                string[] s = value.Split(new char[] { '.' });
                server_address = new byte[TX300Items.server_address_length];
                for (int i = 0; i < s.Length; i++)
                {
                    server_address[i] = byte.Parse(s[i]);
                }
            }
        }
        /// <summary>
        /// ��ȡ������SMS��������ַ��
        /// </summary>
        public string SmsAddress
        {
            get { return ProtocolItems.GetHex(sms_address); }
            set 
            {
                if (value.Length % 2 != 0)
                    value = "0" + value;
                byte[] b = ProtocolItems.GetBytes(value);
                sms_address = new byte[b.Length + 1];
                Buffer.BlockCopy(b, 0, sms_address, 1, b.Length);
                sms_address[0] = (byte)b.Length;
            }
        }
        /// <summary>
        /// ��ȡ�������ն�ϵͳʱ�䣬ֻ���ն˻ظ����������С�
        /// </summary>
        public DateTime TerminalTime
        {
            get { return wdt.ByteToDateTime; }
            set { wdt = new WbsDateTime.WbsDateTime(value); }
        }
        /// <summary>
        /// ���������·�����������
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            byte[] b = null;
            // ��� msg_status
            iIndex += Marshal.SizeOf(msg_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(msg_status)] = msg_status;
            // �������������Ǹ����ն���������Ҫ���������ֶ�ֵ��
            if (msg_status == 0xFF)
            { 
                // ��� inteval_period
                b = BitConverter.GetBytes(inteval_period);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� inteval_sms_gprs
                b = BitConverter.GetBytes(inteval_sms_gprs);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� limit_csq
                iIndex += Marshal.SizeOf(limit_csq);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_csq;
                // ��� limit_battery
                iIndex += Marshal.SizeOf(limit_battery);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_battery;
                // ��� limit_speed
                iIndex += Marshal.SizeOf(limit_speed);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_speed;
                // ��� limit_radius
                b = BitConverter.GetBytes(limit_radius);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� limit_longitude
                b = BitConverter.GetBytes(limit_longitude);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� limit_latitude
                b = BitConverter.GetBytes(limit_latitude);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� limit_period
                iIndex += Marshal.SizeOf(limit_period);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_period;
                // ��� server_port
                b = BitConverter.GetBytes(server_port);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // ��� server_address
                iIndex += server_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(server_address, 0, content, iIndex - server_address.Length, server_address.Length);
                // ��� sms_address
                iIndex += sms_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(sms_address, 0, content, iIndex - sms_address.Length, sms_address.Length);
            }
            // ���´�� total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// ���ն˷��ͻ��������ݽ����ɸ��ֶΡ�
        /// </summary>
        /// <returns></returns>
        public override bool package_to_msg()
        {
            return base.package_to_msg();
            // ��� 
        }
    }
    /// <summary>
    /// 0xDD01 ȫ�������������
    /// </summary>
    public class TX300_DD01 : TX300
    {
        /// <summary>
        /// M days
        /// </summary>
        byte days;
        /// <summary>
        /// Manual unlock password
        /// </summary>
        uint pwd = 1234;
        public TX300_DD01()
        {
            CommandID = 0xDD01;
        }
        /// <summary>
        /// ���û��ȡ M ������
        /// </summary>
        public byte MDays
        {
            get { return days; }
            set { days = value; }
        }
        /// <summary>
        /// ���û��ȡ�������룬����趨��������� 99999999 �����뽫�ᱻֱ����Ϊ 0��
        /// </summary>
        public uint Password
        {
            get { return pwd; }
            set 
            {
                if (value > 99999999)
                    pwd = 0;
                else
                    pwd = value;
            }
        }
        /// <summary>
        /// �� 0xDD01 �ĸ����ֶΰ��� TX300 Э����
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // ��� lock_status
            iIndex += Marshal.SizeOf(days);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = days;
            // ��� password
            byte[] b = BitConverter.GetBytes(pwd);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
            // ��� total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// �� 0xDD01 �ĸ����ֶδ������н�������
        /// </summary>
        /// <returns></returns>
        public override bool package_to_msg()
        {
            base.package_to_msg();
            // ��� days
            days = content[iIndex];
            iIndex += Marshal.SizeOf(days);
            // ��� pwd
            pwd = BitConverter.ToUInt32(content, iIndex);
            iIndex += Marshal.SizeOf(pwd);
            // ��� checksum
            iIndex += 1;
            return iIndex == total_length;
        }
    }
}

