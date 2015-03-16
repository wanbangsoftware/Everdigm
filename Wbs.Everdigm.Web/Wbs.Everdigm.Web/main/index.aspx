<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Wbs.Everdigm.Web.main.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Everdigm Terminal Control System</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="box">
                <div class="box-right">
                    <div>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="20px">
                                    <img alt="" src="../images/img_tab_left.png" /></td>
                                <td style="background: url(../images/img_tab_bg.png) repeat-x;">
                                    <table cellpadding="0" cellspacing="0" width="100%" style="text-align: left; font-weight: bolder; font-size: 14px;">
                                        <tr>
                                            <td width="20%">Equipment Type
                                                <select>
                                                    <option>EX.</option>
                                                    <option>LO.</option>
                                                    <option>RD.</option>
                                                </select>
                                            </td>
                                            <td width="16%">Model 
                                        <select name="">
                                            <option>DL215-9</option>
                                            <option>DL225-7</option>
                                            <option>DL225-9</option>
                                        </select>
                                            </td>
                                            <td width="11%">Status 
                                          <select name="">
                                              <option>O</option>
                                              <option>W</option>
                                              <option>I</option>
                                              <option>S</option>
                                          </select>
                                            </td>
                                            <td width="27%">Customer Number
                                                <select>
                                                    <option>Zhang San</option>
                                                    <option>Li Si</option>
                                                    <option>Wang Wu</option>
                                                    <option>Zhao Liu</option>
                                                </select>
                                            </td>
                                            <td width="26%">
                                                <img alt="" src="../images/btn_search.png" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <img alt="" src="../images/img_tab_right.png" /></td>
                            </tr>
                        </table>
                    </div>
                    <!--table-search-->
                    <div>
                        <table cellpadding="0" cellspacing="0" width="100%" class="box-table">
                            <thead>
                                <tr class="box-line">
                                    <td colspan="6" class="box-line-title-r">Equipment Information</td>
                                    <td colspan="5" class="box-line-title-r">Stock Information</td>
                                    <td colspan="6" class="box-line-title-r">Terminal Information</td>
                                    <td colspan="2" class="box-line-title-b">Customers</td>
                                </tr>
                                <tr class="box-line">
                                    <td class="box-line-title-r">Type</td>
                                    <td class="box-line-title-r">Model</td>
                                    <td class="box-line-title-r">Time</td>
                                    <td class="box-line-title-r">Oper.</td>
                                    <td class="box-line-title-r">Location</td>
                                    <td class="box-line-title-r">Status</td>
                                    <td class="box-line-title-r">In Date</td>
                                    <td class="box-line-title-r">In Type</td>
                                    <td class="box-line-title-r">Out Date</td>
                                    <td class="box-line-title-r">Out Type</td>
                                    <td class="box-line-title-r">Warehouse</td>
                                    <td class="box-line-title-r">Signel</td>
                                    <td class="box-line-title-r">Connect</td>
                                    <td class="box-line-title-r">Received</td>
                                    <td class="box-line-title-r">NO.</td>
                                    <td class="box-line-title-r">Satellite</td>
                                    <td class="box-line-title-r">SIM Card</td>
                                    <td class="box-line-title-r">Number</td>
                                    <td class="box-line-title-b">Contect</td>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td>脚注：分页插件</td>
                                </tr>
                            </tfoot>
                            <tbody>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                                <tr class="box-line-item">
                                    <td class="box-line-b">EX.</td>
                                    <td class="box-line-b">DL215-9-21442</td>
                                    <td class="box-line-b">2430</td>
                                    <td class="box-line-b">OFF</td>
                                    <td class="box-line-b">山东省烟台市</td>
                                    <td class="box-line-b-r">W</td>
                                    <td class="box-line-b">2014-09-21</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b">2014-10-22</td>
                                    <td class="box-line-b">S</td>
                                    <td class="box-line-b-r">Warehouse1</td>
                                    <td class="box-line-b">ON</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_gprs.png" /></td>
                                    <td class="box-line-b">2014-09-22</td>
                                    <td class="box-line-b">20140921221</td>
                                    <td class="box-line-b">
                                        <img alt="" src="../images/img_connect_install.png" /></td>
                                    <td class="box-line-b-r">23553523</td>
                                    <td class="box-line-b">Zhang San</td>
                                    <td class="box-line-b">23245672</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!--table-txt-->
                </div>
                <!--box-right-->
            </div>
            <!--box-->
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../scripts/main/index.js"></script>
</body>
</html>
