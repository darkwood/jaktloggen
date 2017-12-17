﻿//using System;
//using UIKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;
//using Jaktloggen.iOS.Controls;
//using Jaktloggen.Cells;
////using Xamarin.iOS.CoreGraphics;

//[assembly: ExportRenderer(typeof(JTextCell), typeof(JTextCellRenderer))]
//namespace Jaktloggen.iOS.Controls
//{
//    public class JTextCellRenderer : TextCellRenderer
//    {
//        private static readonly Color DefaultDetailColor = new Color(0.32, 0.4, 0.57);
//        private static readonly Color DefaultTextColor = Color.Black;

//        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
//        {
//            var extendedCell = (JTextCell)item;

//            var textCell = (TextCell)item;
//            var style = extendedCell.DetailLocation == TextCellDetailLocation.Right ?
//                UITableViewCellStyle.Value1 :
//                UITableViewCellStyle.Subtitle;

//            var fullName = item.GetType().FullName;
//            var cell = tv.DequeueReusableCell(fullName) as CellTableViewCell;
//            if (cell == null)
//            {
//                cell = new CellTableViewCell(style, fullName);
//            }
//            else
//            {
//                cell.Cell.PropertyChanged -= cell.HandlePropertyChanged;
//            }

//            cell.Cell = textCell;
//            textCell.PropertyChanged += cell.HandlePropertyChanged;
//            cell.PropertyChanged = this.HandlePropertyChanged;
//            cell.TextLabel.Text = textCell.Text;
//            cell.DetailTextLabel.Text = textCell.Detail;
//            cell.TextLabel.TextColor = textCell.TextColor.ToUIColor(DefaultTextColor);
//            cell.DetailTextLabel.TextColor = textCell.DetailColor.ToUIColor(DefaultDetailColor);

//            UpdateBackground(cell, item);

//            cell.BackgroundColor = extendedCell.BackgroundColor.ToUIColor();
//            cell.SeparatorInset = new UIEdgeInsets(
//                (nfloat)extendedCell.SeparatorPadding.Top,
//                (nfloat)extendedCell.SeparatorPadding.Left,
//                (nfloat)extendedCell.SeparatorPadding.Bottom,
//                (nfloat)extendedCell.SeparatorPadding.Right);

//            if (extendedCell.ShowDisclousure)
//            {
//                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
//                if (!string.IsNullOrEmpty(extendedCell.DisclousureImage))
//                {
//                    var detailDisclosureButton = UIButton.FromType(UIButtonType.Custom);
//                    detailDisclosureButton.SetImage(UIImage.FromBundle(extendedCell.DisclousureImage), UIControlState.Normal);
//                    detailDisclosureButton.SetImage(UIImage.FromBundle(extendedCell.DisclousureImage), UIControlState.Selected);

//                    //detailDisclosureButton.Frame = new CGRect(0f, 0f, 30f, 30f);
//                    detailDisclosureButton.TouchUpInside += (sender, e) =>
//                    {
//                        var index = tv.IndexPathForCell(cell);
//                        tv.SelectRow(index, true, UITableViewScrollPosition.None);
//                        tv.Source.AccessoryButtonTapped(tv, index);
//                    };
//                    cell.AccessoryView = detailDisclosureButton;
//                }
//            }

//            if (!extendedCell.ShowSeparator)
//            {
//                tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;
//            }

//            tv.SeparatorColor = extendedCell.SeparatorColor.ToUIColor();

//            return cell;
//        }
//    }
//}
