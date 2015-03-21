using System;
using System.Text;

using Wbs.Protocol.Items;
using Wbs.Protocol.TX300;
using Wbs.Protocol.WbsDateTime;

namespace Wbs.Protocol.TX300
{
    /// <summary>
    /// �����ֶε��������͡�
    /// </summary>
    public enum TX300DataTyeps
    {
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ byte �͡�
        /// </summary>
        BYTE = 1,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊʮ���������͡�
        /// </summary>
        UBYTE,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ 0xEE00 �д�������͡�
        /// </summary>
        ERROR_TYPE,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ ushort �͡�
        /// </summary>
        USHORT,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ uint �͡�
        /// </summary>
        UINT,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ int �ͣ���������
        /// </summary>
        INT,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ string �ͣ���Ҫת���� string �����
        /// </summary>
        STRING,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ datetime �ͣ���Ҫת���� TX300 Э���е�ʱ���ʽ����ʾ��
        /// </summary>
        DATETIME,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ IP �Σ���Ҫ����ת��Ϊ IP ���ַ�����ʽ����ʾ��
        /// </summary>
        IP,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ��ѹֵ����Ҫ���� 10 �õ���ȷֵ��
        /// </summary>
        VOLTAGE,
        /// <summary>
        /// ��ʾ���ֶ���������״̬��
        /// </summary>
        STATUS,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ��γ�ȱ�ʾ��ʽ��
        /// </summary>
        POSITION,
        /// <summary>
        /// ��ʾ���ֶ�������Ҫת����ʮ����֮���ٳ��� 100��
        /// </summary>
        DIV100,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ�������
        /// </summary>
        SECURITY_SINGAL,
        /// <summary>
        /// ��ʾ���ֶ�����ΪԶ�̿���״̬��
        /// </summary>
        CONTROL,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ����ԭ��
        /// </summary>
        CONNECT_STATUS,
        /// <summary>
        /// ��ʾ���ֶ�����ΪԶ������״̬��
        /// </summary>
        RESET,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ����״̬��
        /// </summary>
        ALARM,
        /// <summary>
        /// ��ʾ���ֶ�����Ϊ EPOS ��صĹ��ϴ��롣
        /// </summary>
        EPOS_TRUBLE,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EPOS �ı������롣
        /// </summary>
        EPOS_ALARM,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EPOS ����תʱ�䡣
        /// </summary>
        EPOS_RUNTIME,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EPOS �Ǳ�����Ϣ�е� PUMP ��Ϣ����Ҫ����ת����
        /// </summary>
        EPOS_EMD_PUMP,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EPOS �Ǳ�����Ϣ�е�ˮ����Ϣ����Ҫ����ת����
        /// </summary>
        EPOS_EMD_WATER_TEMP,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EPOS �Ǳ�����Ϣ�е�������Ϣ����Ҫ����ת����
        /// </summary>
        EPOS_EMD_OIL_TEMP,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EPOS �������ն˷��͸� EPOS ��״̬��
        /// </summary>
        EPOS_STATUS,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ 0xDD00 �еķ����� SMS ��ַ��
        /// </summary>
        SMS_SERVER_ADDRESS,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ Sim �����������롣
        /// </summary>
        SIM_LOCK_STATUS,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ EE00 �����еĲ��������ֶΡ�
        /// </summary>
        EE00_PARAM,
        /// <summary>
        /// ��ʾ Loader ��תʱ���е�ʱ�䡣
        /// </summary>
        LD_RUN_DATETIME,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ�������е������֡�
        /// </summary>
        RESP_COMMAND,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ��������״̬�롣
        /// </summary>
        RESP_STATUS,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ���ϱ����еĹ���������
        /// </summary>
        EPOS_ETF_ERR_COUNT,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ���ϱ����еĹ��ϴ��롣
        /// </summary>
        EPOS_ERR_CODE,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ���ϱ����й��ϵ�FMI�롣
        /// </summary>
        EPOS_ERR_FMI,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����е������ź�
        /// </summary>
        EPOS_MONITOR_INPUT,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����еĹ���ģʽ��Ϣ
        /// </summary>
        EPOS_MONITOR_MODULE,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����е�����ź�
        /// </summary>
        EPOS_MONITOR_OUTPUT,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����е��ȵ�������ź�
        /// </summary>
        EPOS_MONITOR_PILOT,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����еķ�������������ź�
        /// </summary>
        EPOS_MONITOR_ENG_CONTROL,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����еı���������
        /// </summary>
        EPOS_MONITOR_EEPRC,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����е� TPS ��ѹ
        /// </summary>
        EPOS_MONITOR_TPS,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����е�������ť��ѹ
        /// </summary>
        EPOS_MONITOR_DIALVOL,
        /// <summary>
        /// ��ʾ���Ӷ�Ϊ Monitor �����еı�����
        /// </summary>
        EPOS_MONITOR_TMPPRE,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ Monitor �����еĴ���͸�
        /// </summary>
        EPOS_MONITOR_BOOMPRE,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ Monitor �����е����ص�ѹ
        /// </summary>
        EPOS_MONITOR_BATTERY_VOLTAGE,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ Monitor �����е�ȼ��ʣ����
        /// </summary>
        EPOS_MONITOR_OIL_LEFT,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ Status report �е�������������Ϣ
        /// </summary>
        EPOS_STATUS_FILTER,
        /// <summary>
        /// ä���ֶ���������
        /// </summary>
        BLIND_UNLOCK_PASSWORD,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ TX10G �ն˵ı�����Ϣ
        /// </summary>
        TX10G_ALARMS,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ TX10G �ն˵Ķ�λ��Ϣ
        /// </summary>
        TX10G_GPS,
        /// <summary>
        /// ��ʾ���ֶ�Ϊ TX10G �ն˶�λ��Ϣ�еĶ�λ��Ϣ����
        /// </summary>
        TX10G_GPS_COUNT
    }

    /// <summary>
    /// ��λ��Ϣ���
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// ������Ϣ�е�GPS��Ϣ
        /// </summary>
        Alarm = 0,
        /// <summary>
        /// ����ظ��е�GPS��Ϣ
        /// </summary>
        Command,
        /// <summary>
        /// ���������ػ��е�GPS��Ϣ
        /// </summary>
        EngControl,
        /// <summary>
        /// ���ڱ����е�GPS��Ϣ
        /// </summary>
        Period,
        /// <summary>
        /// �ն˹ػ���Ϣ�е�GPS��Ϣ
        /// </summary>
        Shutdown,
        /// <summary>
        /// δ֪����ʱδ�����
        /// </summary>
        Unknown
    }

    /// <summary>
    /// ���ֶ���������Ĵ��䷽��
    /// </summary>
    public enum TX300CommandDirect
    { 
        /// <summary>
        /// S-T����ʾ���ֶ��������������ɷ����������ն˵ġ�
        /// </summary>
        SERVER,
        /// <summary>
        /// T-S����ʾ���ֶ����������������ն˷����������ġ�
        /// </summary>
        CLIENT
    }

    public enum doosan
    { 
        /// <summary>
        /// ��ʾ�������� DH �����ն˷��͵���Ϣ��
        /// </summary>
        DH,
        /// <summary>
        /// ��ʾ�������� DX �����ն˷��͵���Ϣ��
        /// </summary>
        DX,
        /// <summary>
        /// ��ʾ�������� Loader �����ն˷��͵���Ϣ��
        /// </summary>
        Loader,
        /// <summary>
        /// ��ʾ�������� TX10G �����ն˷��͵���Ϣ��
        /// </summary>
        TX10G,
        /// <summary>
        /// ��ʾ�������Ǹ��ͺ��ն�ͨ�õ���Ϣ��
        /// </summary>
        ALL
    }

    public enum TX300CommandTypes
    { 
        /// <summary>
        /// ��ʾ�������� TX �����ն����ã����� TX100 �� TX300��
        /// </summary>
        TX,
        /// <summary>
        /// ��ʾ�������� TX10G �����ն����á�
        /// </summary>
        TX10G,
        /// <summary>
        /// ��ʾ�������������ն˶����á�
        /// </summary>
        ALL
    }

    /// <summary>
    /// ���ֶλ������ԡ�
    /// </summary>
    public struct TX300CommandFiled
    {
        /// <summary>
        /// �ֶ����������롣
        /// </summary>
        public ushort command_id;
        /// <summary>
        /// �ֶε�������
        /// </summary>
        public string filed_name;
        /// <summary>
        /// �ֶ����ݳ��ȡ�
        /// </summary>
        public byte filed_len;
        /// <summary>
        /// ���ֶε��������͡�
        /// </summary>
        public TX300DataTyeps filed_type;
        /// <summary>
        /// ������Ĵ��䷽��
        /// </summary>
        public TX300CommandDirect direction;
        /// <summary>
        /// ����������ͣ�DH��DX����
        /// </summary>
        public doosan filed_style;
        /// <summary>
        /// ������ı�ע��
        /// </summary>
        public string memo;
        /// <summary>
        /// ���ֶ��ڷ����������Ƿ�Ҫ�ر���Ŀ����ʾ��
        /// </summary>
        public bool special;
        /// <summary>
        /// ����һ���µ��ֶ����������ṹ�塣
        /// </summary>
        /// <param name="cmd">�������</param>
        /// <param name="name">�ֶε�������</param>
        /// <param name="len">�ֶεĳ��ȡ�</param>
        public TX300CommandFiled(ushort cmd, string name, byte len, TX300DataTyeps type, TX300CommandDirect dir, doosan style, string mem, bool speci)
        {
            command_id = cmd;
            filed_name = name;
            filed_len = len;
            filed_type = type;
            direction = dir;
            filed_style = style;
            memo = mem;
            special = speci;
        }
    }
    /// <summary>
    /// TX300 �����ȫ��
    /// </summary>
    public struct TX300Command
    {
        /// <summary>
        /// �������ơ�
        /// </summary>
        public string command_name;
        /// <summary>
        /// ������롣
        /// </summary>
        public ushort command_id;
        /// <summary>
        /// �������
        /// </summary>
        public TX300CommandTypes command_type;
        /// <summary>
        /// ����һ���µ� TX300 ����ṹ�塣
        /// </summary>
        /// <param name="name">�������ơ�</param>
        /// <param name="id">�����롣</param>
        public TX300Command(string name, ushort id, TX300CommandTypes ctype)
        {
            command_name = name;
            command_id = id;
            command_type = ctype;
        }
    }

    /// <summary>
    /// TX300 Э���������ֶζ���
    /// </summary>
    public class TX300Fileds
    {
        /// <summary>
        /// Ҫ���͸��ն˵����
        /// </summary>
        public static TX300Command[] commands = new TX300Command[] 
        {
            new TX300Command("��λ��Ϣ(0x1000)", 0x1000, TX300CommandTypes.TX),
            new TX300Command("�°涨λ��Ϣ(0x1001)", 0x1001, TX300CommandTypes.TX),
            new TX300Command("Loader Զ�̿���(0x3000)", 0x3000, TX300CommandTypes.TX),
            new TX300Command("Զ�������ն�����(0x4000)", 0x4000, TX300CommandTypes.TX),
            new TX300Command("���ػ���Ϣ(0x5000)", 0x5000, TX300CommandTypes.TX),
            new TX300Command("�Ǳ�����Ϣ(0x6000)", 0x6000, TX300CommandTypes.TX),
            new TX300Command("���ϱ��(0x6001)", 0x6001, TX300CommandTypes.TX),
            new TX300Command("ͼ�η�������(0x6002)", 0x6002, TX300CommandTypes.TX),
            new TX300Command("������ʷ(0x6003)", 0x6003, TX300CommandTypes.TX),
            new TX300Command("����ʱ��(0x6004)", 0x6004, TX300CommandTypes.TX),
            new TX300Command("������Ϣ(0x6005)", 0x6005, TX300CommandTypes.TX),
            new TX300Command("״̬����(0x6006)", 0x6006, TX300CommandTypes.TX),
            new TX300Command("��������(0x6007)", 0x6007, TX300CommandTypes.TX),
            new TX300Command("DX ������Ϣ(0x6008)", 0x6008, TX300CommandTypes.TX),
            new TX300Command("DX ����ʱ��(0x6009)", 0x6009, TX300CommandTypes.TX),
            new TX300Command("DX Ĭ����Ϣ(0x600A)", 0x600A, TX300CommandTypes.TX),
            new TX300Command("Loader������תʱ��(0x600B)", 0x600B, TX300CommandTypes.TX),
            new TX300Command("Loaderÿ����תʱ��(0x600C)", 0x600C, TX300CommandTypes.TX),
            new TX300Command("Sim ������(0xBB00)", 0xBB00, TX300CommandTypes.TX),
            new TX300Command("ȫ������(0xDD00)", 0xDD00, TX300CommandTypes.TX),
            new TX300Command("ä������(0xDD01)", 0xDD01, TX300CommandTypes.TX),
            new TX300Command("ͨѶ��������(0x7010)", 0x7010, TX300CommandTypes.TX10G),
            new TX300Command("", 0x0000, TX300CommandTypes.ALL),
            new TX300Command("������Ϣ(0x2000)", 0x2000, TX300CommandTypes.TX),
            new TX300Command("TCP ������Ϣ(0xAA00)", 0xAA00, TX300CommandTypes.TX),
            new TX300Command("�ն�������Ϣ(0xCC00)", 0xCC00, TX300CommandTypes.TX),
            new TX300Command("�ն˴��������Ϣ(0xEE00)", 0xEE00, TX300CommandTypes.TX),
            new TX300Command("�ն˵�غľ��ػ���Ϣ(0xFF00)", 0xFF00, TX300CommandTypes.TX),
            new TX300Command("�ն˻�ȡ Sim ������(0xBB0F)", 0xBB0F, TX300CommandTypes.TX),
            new TX300Command("���ӷ�����(0x7000)", 0x7000, TX300CommandTypes.TX10G),
            new TX300Command("������Ϣ(0x7020)", 0x7020, TX300CommandTypes.TX10G),
            new TX300Command("��λ��Ϣ(0x7030)", 0x7030, TX300CommandTypes.TX10G),
            new TX300Command("������Ϣ(0x7040)", 0x7040, TX300CommandTypes.TX10G)
        };

        public static TX300CommandFiled[] fileds = new TX300CommandFiled[] 
        { 
            // 0xFFFF: TX Э���н��շ��ķ������ṹ
            new TX300CommandFiled(0xFFFF, "�������ܳ���", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xFFFF, "��������������", 2, TX300DataTyeps.RESP_COMMAND, TX300CommandDirect.SERVER, doosan.ALL, "�����е���������ͬ", false),
            new TX300CommandFiled(0xFFFF, "����������ˮ��", 2, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "�����е���ˮ����ͬ", false),
            new TX300CommandFiled(0xFFFF, "��������״̬��", 1, TX300DataTyeps.RESP_STATUS, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xFFFF, "��������֡����", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "�����е�֡������ͬ", false),
            // 0xAA00: T-S
            new TX300CommandFiled(0xAA00, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            // 0xBB00: T-S
            new TX300CommandFiled(0xBB00, "�ն�F/W�汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB00, "��������״̬", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB00, "��ǰ��������", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            // 0xBB00: S-T
            new TX300CommandFiled(0xBB00, "�����������", 1, TX300DataTyeps.SIM_LOCK_STATUS, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB00, "����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            // 0xBB0F: S-T
            new TX300CommandFiled(0xBB0F, "Sim������", 6, TX300DataTyeps.BYTE, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB0F, "��������", 4, TX300DataTyeps.BLIND_UNLOCK_PASSWORD, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            // 0xBB0F: T-S
            new TX300CommandFiled(0xBB0F, "ICCID����", 20, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            // 0xCC00: T-S
            new TX300CommandFiled(0xCC00, "�ն�F/W�汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xCC00, "�ƶ���վ����", 4, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xCC00, "����״̬", 1, TX300DataTyeps.CONNECT_STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "00|0B|10|20|40Ϊ���������������", true),
            new TX300CommandFiled(0xCC00, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xCC00, "�ն�ϵͳʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            // 0xCC00: S-T
            new TX300CommandFiled(0xCC00, "����ʱ����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "��λ����", false),
            new TX300CommandFiled(0xCC00, "������ϵͳʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            // 0xDD00: S-T
            new TX300CommandFiled(0xDD00, "�������", 1, TX300DataTyeps.STATUS, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xDD00, "���ڱ���ʱ����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "��λ����", false),
            new TX300CommandFiled(0xDD00, "SMS-GPRSʱ����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "��λ����", false),
            new TX300CommandFiled(0xDD00, "�ź�ǿ���������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xDD00, "�󱸵�ص͵�ѹ����", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.SERVER, doosan.ALL, "��λ��V" , false),
            new TX300CommandFiled(0xDD00, "���ʱ���趨", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "��λ��km/h" , false),
            new TX300CommandFiled(0xDD00, "Խ��뾶", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "��λ����" , false),
            new TX300CommandFiled(0xDD00, "Խ�����ĵ㾭��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "Խ�����ĵ�γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "���ڱ�������", 1, TX300DataTyeps.BYTE, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "�������˿�", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "��������ַ", 4, TX300DataTyeps.IP, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "����Ϣ��������ַ", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            // 0xDD01: S-T
            new TX300CommandFiled(0xDD01, "�趨ä������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "��ä��������", true),
            new TX300CommandFiled(0xDD01, "�ֶ���������", 4, TX300DataTyeps.BLIND_UNLOCK_PASSWORD, TX300CommandDirect.SERVER, doosan.ALL, "", true),
            // 0xDD01: T-S
            new TX300CommandFiled(0xDD01, "�趨ä������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "��ä��������", true),
            new TX300CommandFiled(0xDD01, "�ֶ���������", 4, TX300DataTyeps.BLIND_UNLOCK_PASSWORD, TX300CommandDirect.CLIENT, doosan.ALL, "", true),
            // 0xDD00: T-S
            new TX300CommandFiled(0xDD00, "�������", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "�ն�F/W�汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "��ǰ�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , true),
            new TX300CommandFiled(0xDD00, "���ڱ���ʱ����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����" , false),
            new TX300CommandFiled(0xDD00, "SMS-GPRSʱ����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����" , false),
            new TX300CommandFiled(0xDD00, "�ź�ǿ���������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , true),
            new TX300CommandFiled(0xDD00, "�󱸵�ص͵�ѹ����", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "��λ��V" , true),
            new TX300CommandFiled(0xDD00, "���ʱ���趨", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "��λ��km/h" , false),
            new TX300CommandFiled(0xDD00, "Խ��뾶", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����" , false),
            new TX300CommandFiled(0xDD00, "Խ�����ĵ㾭��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "$GPRMC��ʽ" , false),
            new TX300CommandFiled(0xDD00, "Խ�����ĵ�γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "$GPRMC��ʽ" , false),
            new TX300CommandFiled(0xDD00, "���ڱ�������", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "�������˿�", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "��������ַ", 4, TX300DataTyeps.IP, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "����Ϣ��������ַ", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "�ն�ϵͳʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            // 0xEE00
            new TX300CommandFiled(0xEE00, "�ն�F/W�汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xEE00, "��������", 1, TX300DataTyeps.ERROR_TYPE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xEE00, "�����������", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xEE00, "�����������������", 1, TX300DataTyeps.EE00_PARAM, TX300CommandDirect.CLIENT, doosan.ALL, "�ֽ���" , false),
            new TX300CommandFiled(0xEE00, "�����������������", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            // 0xFF00
            new TX300CommandFiled(0xFF00, "�󱸵�ص�ѹ", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "����ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "�ϱ�ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "��ǰ�ٶ�", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ��km/h" , false),
            new TX300CommandFiled(0xFF00, "�н�����", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����(����Ϊ0��)" , false),
            new TX300CommandFiled(0xFF00, "���θ߶�", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����" , false),
            // 0x1000: S-T
            new TX300CommandFiled(0x1000, "��������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "��" , false),
            // 0x1000: T-S
            new TX300CommandFiled(0x1000, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "��������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "====================" , false),
            new TX300CommandFiled(0x1000, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "����ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "�ϱ�ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "��ǰ�ٶ�", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ��km/h" , false),
            new TX300CommandFiled(0x1000, "�н�����", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����(����Ϊ0��)" , false),
            new TX300CommandFiled(0x1000, "���θ߶�", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����" , false),
            // 0x1001: T-S
            new TX300CommandFiled(0x1001, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "�ն�F/W�汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "�汾������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "��վ����", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "С������", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "ȼ��ʣ����", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "ʣ�����ݳ���", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            //new fileds(0x1000, "���θ߶�", 4, type.DIV100, direct.CLIENT, doosan.ALL, "��λ����" , false),
            //new fileds(0x1000, "���θ߶�", 4, type.DIV100, direct.CLIENT, doosan.ALL, "��λ����" , false),
            //new fileds(0x1000, "���θ߶�", 4, type.DIV100, direct.CLIENT, doosan.ALL, "��λ����" , false),
            // 0x2000: T-S
            new TX300CommandFiled(0x2000, "������Ϣ", 2, TX300DataTyeps.ALARM, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "����ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "�ϱ�ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "��ǰ�ٶ�", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ��km/h" , false),
            new TX300CommandFiled(0x2000, "��ǰ�н�����", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����(����Ϊ0��)" , false),
            new TX300CommandFiled(0x2000, "���θ߶�", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����" , false),
            
            // 0x3000: S-T
            new TX300CommandFiled(0x3000, "Զ�̿���״̬", 1, TX300DataTyeps.CONTROL, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "���ƶ���ʱ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "�ڴ�ʱ�����ƣ���λ����" , false),
            // 0x3000: T-S
            new TX300CommandFiled(0x3000, "Զ�̿���״̬", 1, TX300DataTyeps.CONTROL, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "����ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "�ϱ�ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "��ǰ�ٶ�", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "km/h" , false),
            new TX300CommandFiled(0x3000, "��ǰ�н�����", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����(����Ϊ0��)" , false),
            new TX300CommandFiled(0x3000, "���θ߶�", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "meter" , false),
            
            // 0x4000: S-T
            new TX300CommandFiled(0x4000, "����״̬", 1, TX300DataTyeps.RESET, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0x4000, "���ö���ʱ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "�ڴ�ʱ������ã���λ����" , false),
            // 0x5000: T-S
            new TX300CommandFiled(0x5000, "�������ѹ", 2, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "V" , true),
            new TX300CommandFiled(0x5000, "�󱸵�ص�ѹ", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "V" , true),
            new TX300CommandFiled(0x5000, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "����ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "�ϱ�ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "��ǰ�ٶ�", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "km/h" , false),
            new TX300CommandFiled(0x5000, "��ǰ�н�����", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "��λ����(����Ϊ0��)" , false),
            new TX300CommandFiled(0x5000, "���θ߶�", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "meter" , false),
            new TX300CommandFiled(0x5000, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.ALL, "meter" , false),
            // 0x5000: T-S(LD)
            new TX300CommandFiled(0x5000, "����תʱ�䣺Сʱ��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "����תʱ�䣺������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "��/�ر��", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "�������ѹ", 2, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.Loader, "V" , false),
            new TX300CommandFiled(0x5000, "���ص�ѹ", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.Loader, "V" , false),
            new TX300CommandFiled(0x5000, "ȼ��ʣ����", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "��ȴˮ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "����ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "�ϱ�ָʾ", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "��ǰ�ٶ�", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.Loader, "km/h" , false),
            new TX300CommandFiled(0x5000, "��ǰ�н�����", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.Loader, "��λ����(����Ϊ0��)" , false),
            new TX300CommandFiled(0x5000, "���θ߶�", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.Loader, "meter" , false),
            // 0x6000(DH)
            new TX300CommandFiled(0x6000, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "ת��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.DH, "rpm(1100��2100)" , false),
            new TX300CommandFiled(0x6000, "���ص�ѹ", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DH, "24��28.8" , false),
            new TX300CommandFiled(0x6000, "ǰ��ѹ��", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DH, "0��350" , false),
            new TX300CommandFiled(0x6000, "���ѹ��", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DH, "0��350" , false),
            new TX300CommandFiled(0x6000, "��ȴˮ��", 1, TX300DataTyeps.EPOS_EMD_WATER_TEMP, TX300CommandDirect.CLIENT, doosan.DH, "0��105" , false),
            new TX300CommandFiled(0x6000, "ȼ��ʣ����", 1, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DH, "5��100" , false),
            new TX300CommandFiled(0x6000, "����", 2, TX300DataTyeps.EPOS_MONITOR_INPUT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "ģʽ", 1, TX300DataTyeps.EPOS_MONITOR_MODULE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "���", 1, TX300DataTyeps.EPOS_MONITOR_OUTPUT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "����������", 2, TX300DataTyeps.EPOS_MONITOR_EEPRC, TX300CommandDirect.CLIENT, doosan.DH, "0��700" , false),
            new TX300CommandFiled(0x6000, "TPS��ѹ", 2, TX300DataTyeps.EPOS_MONITOR_TPS, TX300CommandDirect.CLIENT, doosan.DH, "4000��950" , false),
            new TX300CommandFiled(0x6000, "������ת��ѹ", 2, TX300DataTyeps.EPOS_MONITOR_DIALVOL, TX300CommandDirect.CLIENT, doosan.DH, "4000��950" , false),
            new TX300CommandFiled(0x6000, "�������������", 1, TX300DataTyeps.EPOS_MONITOR_ENG_CONTROL, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "����͸�ѹ��", 2, TX300DataTyeps.EPOS_MONITOR_BOOMPRE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "������", 2, TX300DataTyeps.EPOS_MONITOR_TMPPRE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "Һѹ���¶�", 1, TX300DataTyeps.EPOS_EMD_OIL_TEMP, TX300CommandDirect.CLIENT, doosan.DH, "0��95" , false),
            new TX300CommandFiled(0x6000, "�ȵ���", 1, TX300DataTyeps.EPOS_MONITOR_PILOT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6000(DX)
            new TX300CommandFiled(0x6000, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "δ֪����", 4, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6000, "������ת��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.DX, "rpm(1100��2100)" , false),
            new TX300CommandFiled(0x6000, "TPS ��ѹ", 2, TX300DataTyeps.EPOS_MONITOR_TPS, TX300CommandDirect.CLIENT, doosan.DX, "4000~950" , false),
            new TX300CommandFiled(0x6000, "ǰ��ѹ��", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DX, "0��350" , false),
            new TX300CommandFiled(0x6000, "���ѹ��", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DX, "0��350" , false),
            new TX300CommandFiled(0x6000, "δ֪����", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6000, "����������", 2, TX300DataTyeps.EPOS_MONITOR_EEPRC, TX300CommandDirect.CLIENT, doosan.DX, "0��700" , false),
            new TX300CommandFiled(0x6000, "δ֪����", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6000, "������ť��ѹ", 2, TX300DataTyeps.EPOS_MONITOR_DIALVOL, TX300CommandDirect.CLIENT, doosan.DX, "4000~950" , false),
            new TX300CommandFiled(0x6000, "δ֪����", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6000, "��ȴˮ�¶�", 2, TX300DataTyeps.EPOS_EMD_WATER_TEMP, TX300CommandDirect.CLIENT, doosan.DX, "0��105" , false),
            new TX300CommandFiled(0x6000, "������", 2, TX300DataTyeps.EPOS_MONITOR_TMPPRE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "���ص�ѹ", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DX, "24��28.8" , false),
            new TX300CommandFiled(0x6000, "����͸�", 2, TX300DataTyeps.EPOS_MONITOR_BOOMPRE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "ȼ��ʣ����", 2, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DX, "5��100" , false),
            new TX300CommandFiled(0x6000, "Һѹ���¶�", 2, TX300DataTyeps.EPOS_EMD_OIL_TEMP, TX300CommandDirect.CLIENT, doosan.DX, "0��95" , false),
            new TX300CommandFiled(0x6000, "δ֪����", 20, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6000, "����", 4, TX300DataTyeps.EPOS_MONITOR_INPUT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "�������������", 1, TX300DataTyeps.EPOS_MONITOR_ENG_CONTROL, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "���", 1, TX300DataTyeps.EPOS_MONITOR_OUTPUT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "ģʽ", 1, TX300DataTyeps.EPOS_MONITOR_MODULE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "�ȵ������", 1, TX300DataTyeps.EPOS_MONITOR_PILOT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6001(DH)
            new TX300CommandFiled(0x6001, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag1", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag2", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag3", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag4", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6001(DX)
            new TX300CommandFiled(0x6001, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "�������ݳ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "δ֪����", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "��������", 1, TX300DataTyeps.EPOS_ETF_ERR_COUNT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "���ϴ���", 1, TX300DataTyeps.EPOS_ERR_CODE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "����������Ϣ(FMI)", 1, TX300DataTyeps.EPOS_ERR_FMI, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6003(DH)
            new TX300CommandFiled(0x6003, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "��������", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "���ϴ���", 1, TX300DataTyeps.EPOS_TRUBLE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "���ϴ���", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "��תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6003(DX)
            new TX300CommandFiled(0x6003, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "δ֪����", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6003, "��������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "��������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DX, "--------------------" , false),
            new TX300CommandFiled(0x6003, "���ϴ���", 1, TX300DataTyeps.EPOS_ERR_CODE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "����FMI", 1, TX300DataTyeps.EPOS_ERR_FMI, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "���ϴ���", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "��������ʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6004(DH)
            new TX300CommandFiled(0x6004, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "driving time", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "working time", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "eng speed hrs 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "eng speed hrs 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "power mode hrs 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "power mode hrs 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "work mode 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "work mode 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "auto idle mode", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "travel speed 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "travel speed 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "travel speed 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 4", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 5", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 6", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 4", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 5", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 6", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6004(LD)
            new TX300CommandFiled(0x6004, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x6004, "����תʱ�䣺Сʱ��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x6004, "����תʱ�䣺������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            // 0x6004(DX)
            new TX300CommandFiled(0x6004, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "δ֪����", 3, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "û�з�������" , false),
            new TX300CommandFiled(0x6004, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "power md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "standard md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "digging md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "trenching_md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "travel_I_spd", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "travel_II_spd", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "auto idle md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "travle", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "work", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "press up", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "breaker", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "shear", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 2000", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1900", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "end speed 1800", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1700", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1600", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1200", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1200D", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 30", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 31", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 51", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 76", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 86", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 96", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 40", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 41", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 61", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 86", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 96", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 106", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "total fule", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "power fuel", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "standard fule", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6005(DH)
            new TX300CommandFiled(0x6005, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "ȼ��������", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "����������", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "��������������", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "����������", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "�ȵ�������", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "��������", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "Һѹ��", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "��ȴˮ", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6005(DX)
            new TX300CommandFiled(0x6005, "��Ϣ���", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "δ֪����", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "ȼ��������", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "����������", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "��������������", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "����������", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "�ȵ�������", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "��������", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "Һѹ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "��ȴˮ", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            /*
            new fileds(0x6005, "δ֪����", 16, type.BYTE, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "oil filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "air cleaner ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "eng oil filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "ret filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "pilot filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "eng oil ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "work oil ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "coolant ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "δ֪����", 2, type.BYTE, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "E-TУ���", 1, type.BYTE, direct.CLIENT, doosan.DX, "" , false),*/
            // 0x6006(DH)
            new TX300CommandFiled(0x6006, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "ȼ��ʣ����", 1, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DH, "�ٷֱ�ʣ����" , false),
            new TX300CommandFiled(0x6006, "���ص�ѹ", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DH, "V" , false),
            new TX300CommandFiled(0x6006, "����", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "ȼ��������", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "����������", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "��������������", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "����������", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "�ȵ�������", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "��������", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "Һѹ��", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "��ȴˮ", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6006(DX)
            new TX300CommandFiled(0x6006, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "ȼ��ʣ����", 2, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "���ص�ѹ", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "����", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "����������", 1, TX300DataTyeps.EPOS_STATUS_FILTER, TX300CommandDirect.CLIENT, doosan.DX, "8 bit" , false),
            new TX300CommandFiled(0x6006, "����תʱ��", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "E-TУ���", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6007(DH): T-S
            new TX300CommandFiled(0x6007, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6007, "��������", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6007(DH): S-T
            new TX300CommandFiled(0x6007, "��������", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.SERVER, doosan.DH, "" , false),
            // 0x6007(DX): T-S
            new TX300CommandFiled(0x6007, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6007, "��������", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6007, "������", 9, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6007(DX): S-T
            new TX300CommandFiled(0x6007, "ʱ�����", 5, TX300DataTyeps.BYTE, TX300CommandDirect.SERVER, doosan.DX, "������ʱ��" , false),
            new TX300CommandFiled(0x6007, "��������", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.SERVER, doosan.DX, "" , false),
            // 0x6008(DX)
            new TX300CommandFiled(0x6008, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6008, "������Ϣ", 10, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6009(DX)
            new TX300CommandFiled(0x6009, "��Ϣ����", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6009, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6009, "ÿ����תʱ��", 12, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x600A(DX)
            new TX300CommandFiled(0x600A, "T-E״̬", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x600A, "Ĭ����Ϣ", 19, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x600B
            new TX300CommandFiled(0x600B, "����תʱ�䣺Сʱ��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600B, "����תʱ�䣺������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600B, "����תʱ�䣺Сʱ��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600B, "����תʱ�䣺������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            // 0x600C
            new TX300CommandFiled(0x600C, "��Ϣ����", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600C, "ʱ�䣺������", 3, TX300DataTyeps.LD_RUN_DATETIME, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600C, "����תʱ�䣺Сʱ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600C, "����תʱ�䣺������", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            // 0x7000
            new TX300CommandFiled(0x7000, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7000, "F/W�汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7000, "��վ����", 4, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7000, "�ն��ڲ�ʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7010 T-S
            new TX300CommandFiled(0x7010, "����״̬", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "F/W �汾", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "���ڱ���ʱ��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.TX10G, "��" , false),
            new TX300CommandFiled(0x7010, "ͣ����ʱ����", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "��" , false),
            new TX300CommandFiled(0x7010, "����ʱ����", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "����ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS �ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "�󱸵�ص�ѹ", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS ��������ַ", 4, TX300DataTyeps.IP, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS �������˿�", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS �����ַ", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "�ն��ڲ�ʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7010 S-T
            new TX300CommandFiled(0x7010, "����״̬", 1, TX300DataTyeps.STATUS, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "���ڱ���ʱ��", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.TX10G, "��" , false),
            new TX300CommandFiled(0x7010, "ͣ����ʱ����", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "��" , false),
            new TX300CommandFiled(0x7010, "����ʱ����", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "����ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS �ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "�󱸵�ص�ѹ", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS ��������ַ", 4, TX300DataTyeps.IP, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS �������˿�", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS �����ַ", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            // 0x7020
            new TX300CommandFiled(0x7020, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7020, "��������", 1, TX300DataTyeps.TX10G_ALARMS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7020, "��λ��Ϣ", 1, TX300DataTyeps.TX10G_GPS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            //new TX300CommandFiled(0x7020, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            //new TX300CommandFiled(0x7020, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            //new TX300CommandFiled(0x7020, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7030
            new TX300CommandFiled(0x7030, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7030, "��λ��Ϣ����", 1, TX300DataTyeps.TX10G_GPS_COUNT, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7031 TX10G GPS ��Ϣ�����ն˷��͵���Ϣ��
            new TX300CommandFiled(0x7031, "����", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7031, "γ��", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7031, "��λʱ��", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7040
            new TX300CommandFiled(0x7040, "�ź�ǿ��", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "", false)
        };
        /// <summary>
        /// ͨ�� command_id ��ȡ��������������
        /// </summary>
        /// <param name="cmd">������롣</param>
        /// <returns>���ظ��������ϸ������</returns>
        public static string get_description(string cmd)
        {
            string str = "";
            for (int i = commands.GetLowerBound(0); i <= commands.GetUpperBound(0); i++)
            {
                if ((commands[i].command_name.IndexOf(cmd) >= 0))// && (commands[i].command_name == "")
                {
                    str = commands[i].command_name;
                    break;
                }
            }
            return str;
        }
        /// <summary>
        /// ���������롣
        /// </summary>
        /// <param name="descript">�������������</param>
        /// <returns>���������롣</returns>
        public static ushort get_command_id(string descript)
        {
            ushort cmd = 0x0000;
            for (int i = commands.GetLowerBound(0); i <= commands.GetUpperBound(0); i++)
            {
                if (commands[i].command_name == "")
                    break;
                if (commands[i].command_name.Equals(descript))
                {
                    cmd = commands[i].command_id;
                    break;
                }
            }
            return cmd;
        }
        
        /// <summary>
        /// ͨ���ֶ����ͺ��ֶ�ֵ��ȡ��Ӧ��������ֵ���ݡ�
        /// </summary>
        /// <param name="b">�ֶε�ֵ��16 ���ơ�</param>
        /// <param name="type">�ֶε����͡�</param>
        /// <returns>�����ֶζ�Ӧ��ֵ��</returns>
        public static string GetValues(byte[] b, TX300 tx300, TX300DataTyeps type)
        {
            string ss = "";
            switch (type)
            {
                case TX300DataTyeps.BYTE:
                    ss = ProtocolItems.GetHex(b);
                    break;
                case TX300DataTyeps.CONNECT_STATUS:
                    ss = ConnectStatus.GetConnectStatus(b[0]);
                    break;
                case TX300DataTyeps.UBYTE:
                    ss = b[0].ToString();
                    break;
                case TX300DataTyeps.DATETIME:
                    WbsDateTime.WbsDateTime wd = new WbsDateTime.WbsDateTime(b);
                    ss = wd.ByteToDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case TX300DataTyeps.STRING:
                    ss = Encoding.ASCII.GetString(b);
                    break;
                case TX300DataTyeps.UINT:
                    ss = BitConverter.ToUInt32(b, 0).ToString();
                    break;
                case TX300DataTyeps.USHORT:
                    ss = BitConverter.ToUInt16(b, 0).ToString();
                    break;
                case TX300DataTyeps.IP:
                    for (int k = 0; k < b.Length; k++)
                        ss += b[k].ToString() + ".";
                    ss = ss.Substring(0, ss.Length - 1);
                    break;
                case TX300DataTyeps.VOLTAGE:
                    int v = 0;
                    if (b.Length > 1)
                        v = BitConverter.ToUInt16(b, 0);
                    else
                        v = b[0];
                    double vol = v / 10.0;
                    ss = vol.ToString("0.0") + " V";
                    break;
                case TX300DataTyeps.STATUS:
                    if (tx300.CommandID == 0xDD00)
                    {
                        switch (b[0])
                        {
                            case 0x00:
                                ss = "�ɼ�ȫ���趨����";
                                break;
                            case 0xFF:
                                ss = "����ȫ���趨��Ϣ";
                                break;
                        }
                    }
                    else if (tx300.CommandID == 0x5000)
                    {
                        switch (b[0])
                        {
                            case 0x00:
                                ss = "ON��������";
                                break;
                            case 0xFF:
                                ss = "OFF���ػ���";
                                break;
                        }
                    }
                    else
                    {
                        switch (b[0])
                        {
                            case 0x00:
                                ss = "����ظ�";
                                break;
                            case 0xFF:
                                ss = "���ڱ���";
                                break;
                        }
                    }
                    break;
                case TX300DataTyeps.POSITION:
                    uint ii = BitConverter.ToUInt32(b, 0);
                    double p = ii / 10000.0;
                    ss = ProtocolItems.GPRMC2DDMMMM(p).ToString("00.000000");
                    break;
                case TX300DataTyeps.DIV100:
                    ushort iii = BitConverter.ToUInt16(b, 0);
                    double d = iii / 100.00;
                    ss = d.ToString("0.00");
                    break;
                case TX300DataTyeps.SECURITY_SINGAL:
                    ss = SecuritySignals.GetSecuritySignal(b[0]);
                    break;
                case TX300DataTyeps.CONTROL:
                    switch (b[0])
                    {
                        case 0x0F: ss = "Զ�̿��ƣ��ػ�"; break;
                        case 0xF0: ss = "Զ�̿��ƣ�����"; break;
                        default: ss = "N/A"; break;
                    }
                    break;
                case TX300DataTyeps.RESET:
                    ss = ResetTypes.GetResetTypes(b[0]);
                    break;
                case TX300DataTyeps.ALARM:
                    ss = AlarmsStatus.GetAlarmsValue(b);
                    break;
                case TX300DataTyeps.EPOS_TRUBLE:
                    ss = AlarmsDescription.GetDescription(b[0]);
                    break;
                case TX300DataTyeps.EPOS_ALARM:
                    ss = AlarmsStatus.GetAlarmsStatus(b[0]);
                    break;
                case TX300DataTyeps.EPOS_RUNTIME:
                    uint total;
                    if (b.Length == 2)
                        total = BitConverter.ToUInt16(b, 0);
                    else
                        total = BitConverter.ToUInt32(b, 0);
                    uint hours = total / 60;
                    uint minutes = total % 60;
                    ss = string.Format("{0:0} Сʱ {1:0} ��", hours, minutes);
                    break;
                case TX300DataTyeps.EPOS_EMD_PUMP:
                    ushort pump = BitConverter.ToUInt16(b, 0);
                    double r_pump = pump;
                    if (tx300.TerminalType == TerminalTypes.DH)
                    {
                        if (pump > 1070)
                            r_pump = (ushort)((r_pump - 1000) / 8.16);
                        ss = string.Format("{0:0}", r_pump);
                    }
                    else
                    {
                        r_pump = (ushort)((r_pump * 5000 / 1024 - 1000) / 8.16);
                        ss = string.Format("{0:0}", r_pump);
                    }
                    ss += " bar";
                    break;
                case TX300DataTyeps.EPOS_EMD_WATER_TEMP:
                    ss = tx300.TerminalType == TerminalTypes.DH ? WaterTempers.GetWaterTemp(b[0]) :
                        WaterTempers.GetWaterTempDX(BitConverter.ToUInt16(b, 0));
                    ss += " ��";
                    break;
                case TX300DataTyeps.EPOS_EMD_OIL_TEMP:
                    ss = tx300.TerminalType == TerminalTypes.DH ? OilTempers.GetOilTemp(b[0]) :
                        OilTempers.GetOilTempDX((ushort)(BitConverter.ToUInt16(b, 0) * 890 / 1024));
                    ss += " ��";
                    break;
                case TX300DataTyeps.EPOS_STATUS:
                    ss = EposStatus.GetEposStatus(b[0]);
                    break;
                case TX300DataTyeps.ERROR_TYPE:
                    ss = ErrorTypes.GetErrorTypes(b[0]);
                    break;
                case TX300DataTyeps.SMS_SERVER_ADDRESS:
                    ss = ProtocolItems.GetHex(b);
                    break;
                case TX300DataTyeps.SIM_LOCK_STATUS:
                    ss = SimLockStatus.GetLockStatus(b[0]);
                    break;
                case TX300DataTyeps.EE00_PARAM:
                    ss = b[0].ToString();
                    if (b[0] == 0x00)
                    {
                        ss = "�޲���(��������Ϊ0)";
                    }
                    break;
                case TX300DataTyeps.LD_RUN_DATETIME:
                    ss = (b[0] + 2000).ToString() + "-" + b[1].ToString("00") + "-" + b[2].ToString("00");
                    break;
                case TX300DataTyeps.RESP_COMMAND:
                    ss = "0x" + b[1].ToString("X2") + b[0].ToString("X2");
                    break;
                case TX300DataTyeps.RESP_STATUS:
                    ss = ResponseStatus.GetResponseStatus(b[0]);
                    break;
                case TX300DataTyeps.INT:
                    ss = BitConverter.ToInt32(b, 0).ToString();
                    break;
                case TX300DataTyeps.EPOS_ERR_CODE:
                    ss = AlarmsDescription.GetDescriptionDX(b[0]);
                    break;
                case TX300DataTyeps.EPOS_ERR_FMI:
                    ss = AlarmsDescription.GetFMIDescription(b[0]);
                    break;
                case TX300DataTyeps.EPOS_ETF_ERR_COUNT:
                    ss = b[0].ToString();
                    break;
                case TX300DataTyeps.EPOS_MONITOR_ENG_CONTROL:
                    // ֻ�� 1 ��byte
                    if (tx300.TerminalType == TerminalTypes.DH)
                        ss = MonitorEngineControl.GetEngControl(b[0]);
                    else
                        ss = MonitorEngineControl.GetEngControlDX(b[0]);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_INPUT:
                    ss = MonitorInput.GetInput(b);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_MODULE:
                    ss = MonitorModule.GetModule(b[0], tx300.TerminalType);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_OUTPUT:
                    ss = MonitorOutput.GetOutput(b[0]);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_PILOT:
                    ss = MonitorPilot.GetPilot(b[0], tx300.TerminalType);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_EEPRC:
                    ss = ((ushort)(BitConverter.ToUInt16(b, 0) * 890 / 1024)).ToString() + " mA";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_TPS:
                    ss = tx300.TerminalType == TerminalTypes.DH ? (BitConverter.ToUInt16(b, 0).ToString()) : (BitConverter.ToUInt16(b, 0) * 5000 / 1024).ToString();
                    ss += " mV";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_DIALVOL:
                    ss = tx300.TerminalType == TerminalTypes.DH ? BitConverter.ToUInt16(b, 0).ToString() : (BitConverter.ToUInt16(b, 0) * 5000 / 1024).ToString();
                    ss += " mV";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_TMPPRE:
                    ss = tx300.TerminalType == TerminalTypes.DH ? BitConverter.ToUInt16(b, 0).ToString() :
                        ((ushort)(((BitConverter.ToUInt16(b, 0) * 5000 / 1024 * 0.1275) - 63.74) / 10)).ToString();
                    ss += " w";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_BOOMPRE:
                    ss = tx300.TerminalType == TerminalTypes.DH ? BitConverter.ToUInt16(b, 0).ToString() :
                        ((ushort)((BitConverter.ToUInt16(b, 0) * 5000 / 1024 - 1000) / 8.16)).ToString();
                    ss += " bar";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE:
                    ss = tx300.TerminalType == TerminalTypes.DH ? (BitConverter.ToUInt16(b, 0) / 10.0).ToString() :
                        ((BitConverter.ToUInt16(b, 0) * 6336 / 16384 + 11) / 10.0).ToString();
                    ss += " V";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_OIL_LEFT:
                    ss = tx300.TerminalType == TerminalTypes.DH ? b[0].ToString() + "0" :
                        OilTempers.GetOilLeftDX((ushort)(BitConverter.ToUInt16(b, 0) * 500 / 1024)).ToString();
                    ss += "%";
                    break;
                case TX300DataTyeps.EPOS_STATUS_FILTER:
                    ss = StatusFilters.GetStatusDX(b[0]);
                    break;
                case TX300DataTyeps.BLIND_UNLOCK_PASSWORD:
                    ss = BitConverter.ToUInt32(b, 0).ToString("00000000");
                    break;
                case TX300DataTyeps.TX10G_ALARMS:
                    ss = tx10g_alarms.GetAlarms(b[0]);
                    break;
                case TX300DataTyeps.TX10G_GPS_COUNT:
                    ss = b[0].ToString();
                    break;
            }
            return ss;
        }
    }
}
