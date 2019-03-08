﻿using Novacode;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace PMSClient.ReportsHelper
{
    public class WordDeliverySheet : ReportBase
    {
        private string prefix = "发货清单";
        public WordDeliverySheet()
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "DeliverySheet.docx");
            tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "DeliverySheet_Temp.docx");
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
        }
        public WordDeliverySheet(string deliverySheetType)
        {
            var targetName = $"{prefix}{ReportHelper.TimeNameDocx}";
            if (deliverySheetType == "English")
            {
                sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "DeliverySheet.docx");
                tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "DeliverySheet_Temp.docx");
            }
            else
            {
                sourceFile = Path.Combine(ReportHelper.ReportsTemplateFolder, "DeliverySheet_zh_cn.docx");
                tempFile = Path.Combine(ReportHelper.ReportsTemplateTempFolder, "DeliverySheet_zh_cn_Temp.docx");
            }
            targetFile = Path.Combine(ReportHelper.DesktopFolder, targetName);
            sheetType = deliverySheetType;
        }
        public void SetModel(DcDelivery model)
        {
            if (model != null)
            {
                this.model = model;
                CreateFolderOnDesktop();
                var targetName = $"{prefix}_{model.DeliveryName}.docx";
                targetFile = Path.Combine(targetDir, targetName);
            }
        }
        private string sheetType = "";
        private DcDelivery model;
        public override void Output()
        {
            try
            {
                if (model == null)
                {
                    return;
                }
                ReportHelper.FileCopy(sourceFile, tempFile);
                //写入数据到文件
                #region 创建文档
                using (DocX document = DocX.Load(tempFile))
                {
                    #region 基本字段
                    document.ReplaceText("[CreateTime]", DateTime.Now.ToString("yyyy-MM-dd"));
                    document.ReplaceText("[ShipTime]", model.ShipTime.ToString("yyyy-MM-dd"));
                    document.ReplaceText("[Country]", model.Country ?? "");
                    document.ReplaceText("[DeliveryName]", model.DeliveryName ?? "");
                    document.ReplaceText("[DeliveryNumber]", model.DeliveryNumber ?? "");
                    document.ReplaceText("[InvoiceNumber]", model.InvoiceNumber ?? "");

                    if (document.Tables[0] != null)
                    {
                        Table mainTable = document.Tables[1];

                        using (var service = new DeliveryServiceClient())
                        {
                            var result = service.GetDeliveryItemByDeliveryID(model.ID)
                                .OrderBy(i => i.OrderNumber)
                                .ThenBy(i=>i.PackNumber)
                                .ThenBy(i=>i.ProductID);
                            int rownumber = 1;
                            int datanumber = 1;

                            foreach (var item in result)
                            {
                                mainTable.Rows[rownumber].Cells[0].Paragraphs[0].Append(datanumber.ToString()).FontSize(10).Alignment = Alignment.center;
                                mainTable.Rows[rownumber].Cells[1].Paragraphs[0].Append(item.ProductID).FontSize(10);
                                string itemType = "";
                                if (sheetType == "English")
                                {
                                    if (item.ProductType.Contains("靶材"))
                                    {
                                        itemType = "Target";

                                    }
                                    else if(item.ProductType.Contains("背板"))
                                    {
                                        itemType = "BP";

                                    }
                                    else if (item.ProductType.Contains("绑定"))
                                    {
                                        itemType = "Bonding";

                                    }
                                    else
                                    {
                                        itemType = item.ProductType;
                                    }
                                }
                                else
                                {
                                    itemType = item.ProductType;
                                }
                                mainTable.Rows[rownumber].Cells[2].Paragraphs[0].Append(itemType).FontSize(10);
                                mainTable.Rows[rownumber].Cells[3].Paragraphs[0].Append(item.Composition).FontSize(10);
                                mainTable.Rows[rownumber].Cells[4].Paragraphs[0].Append(item.Customer).FontSize(10);
                                mainTable.Rows[rownumber].Cells[5].Paragraphs[0].Append(item.PO).FontSize(10);
                                mainTable.Rows[rownumber].Cells[6].Paragraphs[0].Append(item.Dimension).FontSize(10);
                                mainTable.Rows[rownumber].Cells[7].Paragraphs[0].Append(item.PackNumber.ToString())
                                    .FontSize(10).Alignment = Alignment.center;

                                //查找230mm靶材的绑定记录
                                if (item.Dimension.Trim().StartsWith("230"))
                                {
                                    try
                                    {
                                        using (var s_bonding = new RecordBondingServiceClient())
                                        {
                                            var bonding = s_bonding.GetRecordBondingByProductID(item.ProductID)
                                                .FirstOrDefault();
                                            if (bonding != null)
                                            {
                                                string bp_lot = bonding.PlateLot;
                                                mainTable.Rows[rownumber].Cells[8].Paragraphs[0]
                                                    .Append(bp_lot)
                                                    .FontSize(10).Alignment = Alignment.left;

                                                if (bp_lot.EndsWith("A"))
                                                {
                                                    mainTable.Rows[rownumber].Cells[9].Paragraphs[0]
                                                        .Append("old")
                                                        .FontSize(10).Alignment = Alignment.left;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }

                                mainTable.InsertRow();
                                datanumber++;
                                rownumber++;
                            }
                        }

                    }
                    document.Save();
                    #endregion
                }
                #endregion
                //复制到临时文件
                ReportHelper.FileCopy(tempFile, targetFile);
                PMSDialogService.Show("原材料报告创建成功，即将打开");
                System.Diagnostics.Process.Start(targetFile);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
