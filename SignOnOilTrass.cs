using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vector = AcadMain.Vector;
using AccessToDocument = AcadMain.AccessToDocument;
using System.Globalization;
using Autodesk.AutoCAD.EditorInput;
using System.Net;

namespace GlobalSign
{

    //Класс реализации расстановки знаков на одной стойке (в т.ч. их конструкции)
    public class SignModelOneRack : SignBase
    {
        //Конструктор
        public SignModelOneRack()
        {

        }

        //Командный метод для знака
        [CommandMethod("SignIden", CommandFlags.UsePickSet)]
        public static void SignIden()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //экземпляр объекта класса базы данных
                SignBase S = new SignBase();
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //Для SignIden i=1
                int i = 1;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//SI.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                              LocalValueAtt01, LocalTag01,
                                              LocalValueAtt02, LocalTag02,
                                              LocalValueAtt03, LocalTag03,
                                              LocalValueAtt04, LocalTag04,
                                              LocalValueAtt05, LocalTag05,
                                              LocalAngleBlock
                                              );
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignIdenUP", CommandFlags.UsePickSet)]
        public static void SignIdenUP()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для SignIdenUP i=2
                int i = 2;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                                           //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                          LocalValueAtt01, LocalTag01,
                                          LocalValueAtt02, LocalTag02,
                                          LocalValueAtt03, LocalTag03,
                                          LocalValueAtt04, LocalTag04,
                                          LocalValueAtt05, LocalTag05,
                                          LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignWaterStation", CommandFlags.UsePickSet)]
        public static void SignWaterStation()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для SignIdenUP i=53
                int i = 53;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                                           //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                          LocalValueAtt01, LocalTag01,
                                          LocalValueAtt02, LocalTag02,
                                          LocalValueAtt03, LocalTag03,
                                          LocalValueAtt04, LocalTag04,
                                          LocalValueAtt05, LocalTag05,
                                          LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignFrontier", CommandFlags.UsePickSet)]
        public static void SignFrontier()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для SignIdenUP i=54
                int i = 54;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                                           //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                          LocalValueAtt01, LocalTag01,
                                          LocalValueAtt02, LocalTag02,
                                          LocalValueAtt03, LocalTag03,
                                          LocalValueAtt04, LocalTag04,
                                          LocalValueAtt05, LocalTag05,
                                          LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignAP", CommandFlags.UsePickSet)]
        public static void SignAP()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=61
                int i = 61;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignDrive", CommandFlags.UsePickSet)]
        public static void SignDrive()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=68
                int i = 68;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignDriveG", CommandFlags.UsePickSet)]
        public static void SignDriveG()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //string LocalTypeSign = PK_Form.ComboBox1;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=69
                int i = 69;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignVantuz", CommandFlags.UsePickSet)]
        public static void SignVantuz()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=71
                int i = 71;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignValve", CommandFlags.UsePickSet)]
        public static void SignValve()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=72
                int i = 72;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignOD", CommandFlags.UsePickSet)]
        public static void SignOD()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для  i=73
                int i = 73;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                            LocalValueAtt01, LocalTag01,
                                            LocalValueAtt02, LocalTag02,
                                            LocalValueAtt03, LocalTag03,
                                            LocalValueAtt04, LocalTag04,
                                            LocalValueAtt05, LocalTag05,
                                            LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignSSOD", CommandFlags.UsePickSet)]
        public static void SignSSOD()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=74
                int i = 74;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignKPSOD", CommandFlags.UsePickSet)]
        public static void SignKPSOD()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=75
                int i = 75;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignUDZ", CommandFlags.UsePickSet)]
        public static void SignUDZ()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=76
                int i = 76;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignZS", CommandFlags.UsePickSet)]
        public static void SignZS()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=77
                int i = 77;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                            LocalValueAtt01, LocalTag01,
                                            LocalValueAtt02, LocalTag02,
                                            LocalValueAtt03, LocalTag03,
                                            LocalValueAtt04, LocalTag04,
                                            LocalValueAtt05, LocalTag05,
                                            LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignAmb", CommandFlags.UsePickSet)]
        public static void SignAmb()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=78
                int i = 78;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignPKU", CommandFlags.UsePickSet)]
        public static void SignPKU()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=79
                int i = 79;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignBBTM", CommandFlags.UsePickSet)]
        public static void SignBPKU()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для  i=80
                int i = 80;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
         }

        //Командный метод для знака
        [CommandMethod("SignVP", CommandFlags.UsePickSet)]
        public static void SignVP()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=81
                int i = 81;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignCrossGaz", CommandFlags.UsePickSet)]
        public static void SignCrossGaz()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=26
                int i = 26;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ,
                                        LocalNameSign,
                                        LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock,
                                        LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignHiPress", CommandFlags.UsePickSet)]
        public static void SignHiPress()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=18
                int i = 18;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignWarningRW", CommandFlags.UsePickSet)]
        public static void SignWarningRW()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=31
                int i = 31;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqOZ", CommandFlags.UsePickSet)]
        public static void SignEqOZ()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=36
                int i = 36;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqZNK", CommandFlags.UsePickSet)]
        public static void SignEqZNK()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=37
                int i = 37;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqVal", CommandFlags.UsePickSet)]
        public static void SignEqVal()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=38
                int i = 38;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqVan", CommandFlags.UsePickSet)]
        public static void SignEqVan()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=39
                int i = 39;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqOD", CommandFlags.UsePickSet)]
        public static void SignEqOD()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=40
                int i = 40;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqS", CommandFlags.UsePickSet)]
        public static void SignEqS()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=41
                int i = 41;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqKIP", CommandFlags.UsePickSet)]
        public static void SignEqKIP()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            //экземпляр формы для получения пикета установки знака и данных по знаку
            string LocalPiket = FormSignPK.TextBox1.Text;
            //Получение геометрии  точки вставки
            CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
            //экземпляр объекта класса базы данных
            SignModelOneRack S = new SignModelOneRack();
            //Для i=42
            int i = 42;
            // получение параметров блока для знака
            string LocalNameSign = S.GetParametrSign(i, 0);
            string LocalShortNameSign = S.GetParametrSign(i, 1);
            string LocalType = S.GetParametrSign(i, 2);
            string LocalTag01 = S.GetParametrSign(i, 3);
            string LocalPrompt01 = S.GetParametrSign(i, 4);
            string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
            string LocalTag02 = S.GetParametrSign(i, 6);
            string LocalPrompt02 = S.GetParametrSign(i, 7);
            string LocalValueAtt02 = S.GetParametrSign(i, 8);
            string LocalTag03 = S.GetParametrSign(i, 9);
            string LocalPrompt03 = S.GetParametrSign(i, 10);
            string LocalValueAtt03 = S.GetParametrSign(i, 11);
            string LocalTag04 = S.GetParametrSign(i, 12);
            string LocalPrompt04 = S.GetParametrSign(i, 13);
            string LocalValueAtt04 = S.GetParametrSign(i, 14);
            string LocalTag05 = S.GetParametrSign(i, 15);
            string LocalPrompt05 = S.GetParametrSign(i, 16);
            string LocalValueAtt05 = S.GetParametrSign(i, 17);
            double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                                       //Поиск блока новый/существующий
            if (IfExistBlock(LocalNameSign) == false)
            {
                CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                    LocalValueAtt01, LocalPrompt01, LocalTag01,
                                    LocalValueAtt02, LocalPrompt02, LocalTag02,
                                    LocalValueAtt03, LocalPrompt03, LocalTag03,
                                    LocalValueAtt04, LocalPrompt04, LocalTag04,
                                    LocalValueAtt05, LocalPrompt05, LocalTag05,
                                    LocalAngleBlock, LocalType
                                   );
            }
            else
            {
                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                    LocalValueAtt01, LocalTag01,
                                    LocalValueAtt02, LocalTag02,
                                    LocalValueAtt03, LocalTag03,
                                    LocalValueAtt04, LocalTag04,
                                    LocalValueAtt05, LocalTag05,
                                    LocalAngleBlock);
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqR", CommandFlags.UsePickSet)]
        public static void SignEqR()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=43
                int i = 43;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqKNU", CommandFlags.UsePickSet)]
        public static void SignEqKNU()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=44
                int i = 44;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqPO", CommandFlags.UsePickSet)]
        public static void SignEqPO()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=45
                int i = 45;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqPZ", CommandFlags.UsePickSet)]
        public static void SignEqPZ()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=46
                int i = 46;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                       LocalValueAtt01, LocalTag01,
                                       LocalValueAtt02, LocalTag02,
                                       LocalValueAtt03, LocalTag03,
                                       LocalValueAtt04, LocalTag04,
                                       LocalValueAtt05, LocalTag05,
                                       LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqOT", CommandFlags.UsePickSet)]
        public static void SignEqOT()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=47
                int i = 47;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                            LocalValueAtt01, LocalTag01,
                                            LocalValueAtt02, LocalTag02,
                                            LocalValueAtt03, LocalTag03,
                                            LocalValueAtt04, LocalTag04,
                                            LocalValueAtt05, LocalTag05,
                                            LocalAngleBlock
                                            );
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignZona", CommandFlags.UsePickSet)]
        public static void SignZona()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=86
                int i = 86;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        [CommandMethod("SignGZCCable", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignGZCCable()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=20
                int i = 20;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignZonaCable", CommandFlags.UsePickSet)]
        public static void SignZonaCable()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=21
                int i = 21;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 5);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }
    }

    //Класс реализации расстановки знаков на двух стойках (в т.ч. их конструкции)
    public class SignModelTwoRack : SignBase
    {
        //Конструктор
        public SignModelTwoRack()
        {

        }

        //Командный метод для знака (Аншлаг)
        [CommandMethod("SignWAD", CommandFlags.UsePickSet)]
        public static void SignWAD()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=66
                int i = 66;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака (Аншлаг)
        [CommandMethod("SignWADG", CommandFlags.UsePickSet)]
        public static void SignWADG()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=70
                int i = 70;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака (Аншлаг)
        [CommandMethod("SignPP", CommandFlags.UsePickSet)]
        public static void SignPP()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=56
                int i = 56;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака (Аншлаг)
        [CommandMethod("SignWP", CommandFlags.UsePickSet)]
        public static void SignWP()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=57
                int i = 57;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака (Аншлаг)
        [CommandMethod("SignNoDrRW", CommandFlags.UsePickSet)]
        public static void SignNoDrRW()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=32
                int i = 32;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                        );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака (Аншлаг)
        [CommandMethod("SignNoEarthZonaCable", CommandFlags.UsePickSet)]
        public static void SignNoEarthZonaCable()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=22
                int i = 22;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                        );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }
        
    }

    //Класс реализации расстановки треугольных знаков (в т.ч. их конструкции)
    public class SignModelTrangle : SignBase
    {
        //Конструктор
        public SignModelTrangle()
        {

        }

        //Командный метод для знака
        [CommandMethod("SignPR", CommandFlags.UsePickSet)]
        public static void SignPR()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelTrangle S = new SignModelTrangle();
                //Для i=82
                int i = 82;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.

                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                          LocalAngleBlock, LocalType);
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignCrossСomm", CommandFlags.UsePickSet)]
        public static void SignCrossComm()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=16
                int i = 16;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalAngleBlock,
                                            LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignCrossElCab", CommandFlags.UsePickSet)]
        public static void SignCrossElCab()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=17
                int i = 17;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                          LocalAngleBlock, LocalType
                                          );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignWarningGaz", CommandFlags.UsePickSet)]
        public static void SignWarningGaz()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=27
                int i = 27;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalAngleBlock,
                                            LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignCrossPipe", CommandFlags.UsePickSet)]
        public static void SignCrossPipe()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=19
                int i = 19;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalAngleBlock, LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

    }

    //Класс реализации расстановки круглых знаков (в т.ч. их конструкции)
    public class SignModelCircle : SignBase
    {
        //Конструктор
        public SignModelCircle()
        {

        }

        [CommandMethod("SignDM", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignDM()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=0
                int i = 0;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                string LocalTag06 = S.GetParametrSign(i, 18);
                string LocalPrompt06 = S.GetParametrSign(i, 19);
                string LocalValueAtt06 = S.GetParametrSign(i, 20);
                string LocalTag07 = S.GetParametrSign(i, 21);
                string LocalPrompt07 = S.GetParametrSign(i, 22);
                string LocalValueAtt07 = S.GetParametrSign(i, 23);

                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalValueAtt06, LocalPrompt06, LocalTag06,
                                            LocalValueAtt07, LocalPrompt07, LocalTag07,
                                            LocalAngleBlock, LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }



        [CommandMethod("SignMarker", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignMarker()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=6
                int i = 6;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                string LocalTag06 = S.GetParametrSign(i, 18);
                string LocalPrompt06 = S.GetParametrSign(i, 19);
                string LocalValueAtt06 = S.GetParametrSign(i, 20);
                string LocalTag07 = S.GetParametrSign(i, 21);
                string LocalPrompt07 = S.GetParametrSign(i, 22);
                string LocalValueAtt07 = S.GetParametrSign(i, 23);

                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalValueAtt06, LocalPrompt06, LocalTag06,
                                            LocalValueAtt07, LocalPrompt07, LocalTag07,
                                            LocalAngleBlock, LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        [CommandMethod("SignReper", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignReper()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=7
                int i = 7;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                string LocalTag06 = S.GetParametrSign(i, 18);
                string LocalPrompt06 = S.GetParametrSign(i, 19);
                string LocalValueAtt06 = S.GetParametrSign(i, 20);
                string LocalTag07 = S.GetParametrSign(i, 21);
                string LocalPrompt07 = S.GetParametrSign(i, 22);
                string LocalValueAtt07 = S.GetParametrSign(i, 23);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                          LocalValueAtt07, LocalPrompt07, LocalTag07,
                                          LocalAngleBlock, LocalType
                                          );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        [CommandMethod("SignSM", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignSM()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=25
                int i = 25;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                string LocalTag06 = S.GetParametrSign(i, 18);
                string LocalPrompt06 = S.GetParametrSign(i, 19);
                string LocalValueAtt06 = S.GetParametrSign(i, 20);
                string LocalTag07 = S.GetParametrSign(i, 21);
                string LocalPrompt07 = S.GetParametrSign(i, 22);
                string LocalValueAtt07 = S.GetParametrSign(i, 23);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalValueAtt06, LocalPrompt06, LocalTag06,
                                            LocalValueAtt07, LocalPrompt07, LocalTag07,
                                            LocalAngleBlock, LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }
    }

    //Класс реализации расстановки круглых знаков (в т.ч. их конструкции)
    public class SignModelStvor : SignBase
    {
        //Конструктор
        public SignModelStvor()
        {

        }

        [CommandMethod("SignWSR", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignWSR()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=50
                int i = 50;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignStvor(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        [CommandMethod("SignWNSR", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignWNSR()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=51
                int i = 51;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignStvor(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

    }

    //Класс реализации расстановки круглых знаков (в т.ч. их конструкции)
    public class SignModelKM : SignBase
    {
        //Конструктор
        public SignModelKM()
        {

        }

        [CommandMethod("SignКМ", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignКМ()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=11
                int i = 11;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                string LocalTag06 = S.GetParametrSign(i, 18);
                string LocalPrompt06 = S.GetParametrSign(i, 16);
                string LocalValueAtt06 = Convert.ToString(PKtoKM(LocalPiket));
                double LocalAngleBlock = 0;
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignKM(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                          LocalAngleBlock, LocalType
                                          );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign, 
                                          LocalValueAtt01, LocalTag01,
                                          LocalValueAtt02, LocalTag02,
                                          LocalValueAtt03, LocalTag03,
                                          LocalValueAtt04, LocalTag04,
                                          LocalValueAtt05, LocalTag05,
                                          LocalValueAtt06, LocalTag06,
                                          LocalAngleBlock
                                          );
                }
            }
        }

        [CommandMethod("SignКМM", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignКМM()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=12
                int i = 12;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                string LocalTag06 = S.GetParametrSign(i, 18);
                string LocalPrompt06 = S.GetParametrSign(i, 19);
                string LocalValueAtt06 = Convert.ToString(PKtoKM(LocalPiket));

                double LocalAngleBlock = 0;
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignKM(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                          LocalAngleBlock, LocalType
                                          );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                  LocalValueAtt01, LocalTag01,
                                                  LocalValueAtt02, LocalTag02,
                                                  LocalValueAtt03, LocalTag03,
                                                  LocalValueAtt04, LocalTag04,
                                                  LocalValueAtt05, LocalTag05,
                                                  LocalValueAtt06, LocalTag06,
                                                  LocalAngleBlock
                                                  );
                }
            }
        }

    }

    //Класс реализации расстановки прочих знаков (в т.ч. их конструкции)
    public class SignModelOther : SignBase
    {
        //Конструктор
        public SignModelOther()
        {

        }

        [CommandMethod("SignStop", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignStop()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=67
                int i = 67;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignDiff(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                            LocalAngleBlock, LocalType
                                            );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

        [CommandMethod("SignJ", CommandFlags.UsePickSet)]
        //Командный метод для знака
        public static void SignJ()
        {
            //экземпляр формы для получения пикета установки знака
            FormSignPK FormSignPK = new FormSignPK();
            FormSignPK.ShowDialog();
            if (FormSignPK.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = FormSignPK.TextBox1.Text;
                //Получение геометрии  точки вставки
                CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
                //экземпляр объекта класса базы данных
                SignModelOneRack S = new SignModelOneRack();
                //Для i=52
                int i = 52;
                // получение параметров блока для знака
                string LocalNameSign = S.GetParametrSign(i, 0);
                string LocalShortNameSign = S.GetParametrSign(i, 1);
                string LocalType = S.GetParametrSign(i, 2);
                string LocalTag01 = S.GetParametrSign(i, 3);
                string LocalPrompt01 = S.GetParametrSign(i, 4);
                string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(i, 4);
                string LocalTag02 = S.GetParametrSign(i, 6);
                string LocalPrompt02 = S.GetParametrSign(i, 7);
                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                string LocalTag03 = S.GetParametrSign(i, 9);
                string LocalPrompt03 = S.GetParametrSign(i, 10);
                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                string LocalTag04 = S.GetParametrSign(i, 12);
                string LocalPrompt04 = S.GetParametrSign(i, 13);
                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                string LocalTag05 = S.GetParametrSign(i, 15);
                string LocalPrompt05 = S.GetParametrSign(i, 16);
                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                //Поиск блока новый/существующий
                if (IfExistBlock(LocalNameSign) == false)
                {
                    CreateBlockSignDiff(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock,
                                        LocalType
                                       );
                }
                else
                {
                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                }
            }
        }

    }

    //Класс расстановки групповых знаков
    public class SignModelGroupSign : SignBase
    {
        //Конструктор
        public SignModelGroupSign()
        {

        }

        //Метод получения среднего угла для вставки блока
        public static double GetMiddleAngle(double angleBeg, double anglEnd)
        {
            double Angle = 0;

            //1 четверть
            if (0 < angleBeg && angleBeg <= Math.PI / 2)
            {
                if (0 < anglEnd && anglEnd <= Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (Math.PI / 2 < anglEnd && anglEnd <= Math.PI) { Angle = (angleBeg + anglEnd) / 2; }
                if (Math.PI < anglEnd && anglEnd <= 3 * Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (3 * Math.PI / 2 < anglEnd && anglEnd <= 2 * Math.PI) { Angle = (angleBeg + anglEnd) / 2 + Math.PI; }
            }
            //2 четверть
            if (Math.PI / 2 < angleBeg && angleBeg <= Math.PI)
            {
                if (0 < anglEnd && anglEnd <= Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (Math.PI / 2 < anglEnd && anglEnd <= Math.PI) { Angle = (angleBeg + anglEnd) / 2; }
                if (Math.PI < anglEnd && anglEnd <= 3 * Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (3 * Math.PI / 2 < anglEnd && anglEnd <= 2 * Math.PI) { Angle = (angleBeg + anglEnd) / 2 + Math.PI; }
            }
            //3 четверть
            if (Math.PI < angleBeg && angleBeg <= 3 * Math.PI / 2)
            {
                if (0 < anglEnd && anglEnd <= Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (Math.PI / 2 < anglEnd && anglEnd <= Math.PI) { Angle = (angleBeg + anglEnd) / 2; }
                if (Math.PI < anglEnd && anglEnd <= 3 * Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (3 * Math.PI / 2 < anglEnd && anglEnd <= 2 * Math.PI) { Angle = (angleBeg + anglEnd) / 2; }
            }
            //4 четверть
            if (3 * Math.PI / 2 < angleBeg && angleBeg <= 2 * Math.PI)
            {
                if (0 < anglEnd && anglEnd <= Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2 + Math.PI; }
                if (Math.PI / 2 < anglEnd && anglEnd <= Math.PI) { Angle = (angleBeg + anglEnd) / 2 + Math.PI; }
                if (Math.PI < anglEnd && anglEnd <= 3 * Math.PI / 2) { Angle = (angleBeg + anglEnd) / 2; }
                if (3 * Math.PI / 2 < anglEnd && anglEnd <= 2 * Math.PI) { Angle = (angleBeg + anglEnd) / 2; }
            }
            return Angle;
        }
        //Угол между векторами линий до и после
        public static double GetAngle(Point3d first, Point3d second, Point3d third)
        {
            Vector v1 = new Vector((first.X - second.X), (first.Y - second.Y),0);
            Vector v2 = new Vector((third.X - second.X), (third.Y - second.Y),0);
            return Vector.Angle(v1, v2);
        }

        //Метод получения смещения координаты дополнительных знаков на углах в начале - координаты Х
        public static double GetPXBeginLine(double coordinatepoint, double distugolsign, double anglevector)
        {
            if (0 <= anglevector && anglevector < Math.PI / 2)
            {
                return coordinatepoint - distugolsign * Math.Cos(anglevector); //- DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleLineBeg)
            }
            if (Math.PI / 2 <= anglevector && anglevector < Math.PI)
            {
                return coordinatepoint + distugolsign * Math.Cos(Math.PI - anglevector); //- DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleLineBeg)
            }
            if (Math.PI <= anglevector && anglevector < 3 * Math.PI / 2)
            {
                return coordinatepoint + distugolsign * Math.Cos(anglevector - Math.PI);// - DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleLineBeg)
            }
            if (3 * Math.PI / 2 < anglevector && anglevector <= 2 * Math.PI)
            {
                return coordinatepoint - distugolsign * Math.Cos(2 * Math.PI - anglevector); // - DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleLineBeg)
            }
            else
            {
                MessageBox.Show("Не верные данные для расчета знаков", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        //Метод получения смещения координаты дополнительных знаков на углах в начале - координаты Y
        public static double GetPYBeginLine(double coordinatepoint, double distugolsign, double anglevector)
        {
            if (0 <= anglevector && anglevector < Math.PI / 2)
            {
                return coordinatepoint - distugolsign * Math.Sin(anglevector);
            }
            if (Math.PI / 2 <= anglevector && anglevector < Math.PI)
            {
                return coordinatepoint - distugolsign * Math.Sin(Math.PI - anglevector);
            }
            if (Math.PI <= anglevector && anglevector < 3 * Math.PI / 2)
            {
                return coordinatepoint + distugolsign * Math.Sin(anglevector - Math.PI);
            }
            if (3 * Math.PI / 2 < anglevector && anglevector <= 2 * Math.PI)
            {
                return coordinatepoint + distugolsign * Math.Sin(2 * Math.PI - anglevector);
            }
            else
            {
                MessageBox.Show("Не верные данные для расчета знаков", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        //Метод получения смещения координаты дополнительных знаков на углах в конце - координаты Х
        public static double GetPXBeginEnd(double coordinatepoint, double distugolsign, double anglevector)
        {
            if (0 <= anglevector && anglevector < Math.PI / 2)
            {
                return coordinatepoint + distugolsign * Math.Cos(anglevector);
            }
            if (Math.PI / 2 <= anglevector && anglevector < Math.PI)
            {
                return coordinatepoint - distugolsign * Math.Cos(Math.PI - anglevector);
            }
            if (Math.PI <= anglevector && anglevector < 3 * Math.PI / 2)
            {
                return coordinatepoint - distugolsign * Math.Cos(anglevector - Math.PI);
            }
            if (3 * Math.PI / 2 < anglevector && anglevector <= 2 * Math.PI)
            {
                return coordinatepoint + distugolsign * Math.Cos(2 * Math.PI - anglevector);
            }
            else
            {
                MessageBox.Show("Не верные данные для расчета знаков", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        //Метод получения смещения координаты дополнительных знаков на углах в конце - координаты Y
        public static double GetPYBeginEnd(double coordinatepoint, double distugolsign, double anglevector)
        {
            if (0 <= anglevector && anglevector < Math.PI / 2)
            {
                return coordinatepoint + distugolsign * Math.Sin(anglevector);
            }
            if (Math.PI / 2 <= anglevector && anglevector < Math.PI)
            {
                return coordinatepoint + distugolsign * Math.Sin(Math.PI - anglevector);
            }
            if (Math.PI <= anglevector && anglevector < 3 * Math.PI / 2)
            {
                return coordinatepoint - distugolsign * Math.Sin(anglevector - Math.PI);
            }
            if (3 * Math.PI / 2 < anglevector && anglevector <= 2 * Math.PI)
            {
                return coordinatepoint - distugolsign * Math.Sin(2 * Math.PI - anglevector);
            }
            else
            {
                MessageBox.Show("Не верные данные для расчета знаков", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        //Проверка входящших параметров по начельному ПК
        public static bool CheckPK(string str, out string strPK)
        {

            str = str.Replace('.', ',');
            strPK = "";
            //MessageBox.Show($"str  {str}");

            string[] strinrPK = str.Split('+');
            if (strinrPK.Count() == 2)
            {
                //str = str.Replace(',', '.');
                bool b0 = double.TryParse(strinrPK[0], out double PK0);

                bool b1 = double.TryParse(strinrPK[1], out double PK1);


                if (b0 == true && b1 == true)
                {
                    //MessageBox.Show($"strinrPK[0]  {strinrPK[0]} значение bool {b0}");
                    //MessageBox.Show($"strinrPK[1]  {strinrPK[1]} значение bool {b1}");
                    //str = str.Replace(',', '.');
                    strPK = str;    
                    return true;
                }

            }
            //else
            //{

            MessageBox.Show($"Неверный формат ПК  {str}");
            return false;
            //}
        }

        //Метод расстановки групповых знаков
        [CommandMethod("InsertGroupSignTrassaCable", CommandFlags.UsePickSet)]
       
        //Метод расстановки групповых знаков
        public static void InsertGroupSignTrassaCable()
        {
            AccessToDocument AcToDraw = new AccessToDocument();
            //Получаем ссылку на документ
            Document AcadDoc = AcToDraw.Doc;
            //получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignTrassaCable GSAD = new FormGroupSignTrassaCable();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                //получение полилинии для расчета знаков
                bool checkPoly = GetPolyline(AcadDoc, AcadDB, out Polyline PolyTrassaMN);
                //Расстановка знаков
                if (checkPoly == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        double PK100 = PiketSto(BeginPiketString);
                        double PK000 = PiketPlus(BeginPiketString);
                        double BeginPiketDouble = PiketStringToDouble(BeginPiketString);
                        int i, j;

                        //Расстояние между знаками
                        double DistOpoznavatSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistOpoznavatSignConst = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaOpoznavatSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);

                        //21 - ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ НА КАБЕЛЬ
                        #region ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Переменные координаты вставки знака 
                            double LocalPX, LocalPY, LocalPZ;
                            //Начальный счетчик подсчета количества плюсовок
                            int n = 0;
                            //Расстояние от начала трассы до первого знака с учетом значения начального ПК
                            double BeginPiket = (PK100 * 100) % DistOpoznavatSignConst + PK000;
                            //Входные параметры для знака 
                            int k = 21; 
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            //расчет координат точек вставки знаков
                            for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 2); i++)
                            {
                                //Начальная и конечная точка отрезка на полилинии
                                Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                //Создаём ложную линию для определения угла направления начального
                                Line linePL = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                //Угол направления трассы
                                double AngleLenPL = linePL.Angle;
                                //Дистанция между вершинами
                                double LenPL = linePL.Length;
                                //Пересчет исходных координат
                                double LocalPXforLine = StartPoint.X - BeginPiket * Math.Cos(AngleLenPL) + DistOpoznavatSign * Math.Cos(AngleLenPL) - DeltaOpoznavatSign * Math.Cos(Math.PI / 2 - AngleLenPL);
                                double LocalPYforLine = StartPoint.Y - BeginPiket * Math.Sin(AngleLenPL) + DistOpoznavatSign * Math.Sin(AngleLenPL) + DeltaOpoznavatSign * Math.Sin(Math.PI / 2 - AngleLenPL);
                                double LocalPZforLine = 0;
                                //Расстановка опознавательных знаков через 500 м
                                if (LenPL >= DistOpoznavatSign - BeginPiket)
                                {
                                    LenPL = LenPL + BeginPiket - DistOpoznavatSign;
                                    for (j = 0; j <= (int)(LenPL / DistOpoznavatSignConst); j++)
                                    {
                                        //Расчет координат вставки знаков
                                        LocalPX = LocalPXforLine + j * DistOpoznavatSignConst * Math.Cos(AngleLenPL);
                                        LocalPY = LocalPYforLine + j * DistOpoznavatSignConst * Math.Sin(AngleLenPL);
                                        LocalPZ = LocalPZforLine;
                                        //Получение параметров блока для знака
                                        double distsign = (PK100 * 100 + DistOpoznavatSignConst - (PK100 * 100) % DistOpoznavatSignConst + DistOpoznavatSignConst * n);
                                        n++;
                                        string LocalValueAtt01 = Convert.ToString(KMtoPK(distsign));
                                        double LocalAngleBlock = AngleLenPL;
                                        //Поиск блока новый/существующий
                                        if (IfExistBlock(LocalNameSign) == false)
                                        {
                                            CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                        LocalAngleBlock, LocalType
                                                                        );
                                        }
                                        else
                                        {
                                            CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                        }
                                    }
                                    BeginPiket = 0;
                                    DistOpoznavatSign = DistOpoznavatSignConst - (LenPL - DistOpoznavatSignConst * (j - 1));
                                }
                                else
                                {
                                    DistOpoznavatSign -= LenPL;
                                }
                            }
                        }
                        #endregion ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки групповых знаков
        [CommandMethod("InsertGroupSignTrassa", CommandFlags.UsePickSet)]
        public static void InsertGroupSignTrassa()
        {
            AccessToDocument AcToDraw = new AccessToDocument();
            //Получаем ссылку на документ
            Document AcadDoc = AcToDraw.Doc;
            //получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignTrassa GSAD = new FormGroupSignTrassa();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();

            
            if (GSAD.ButtonWasClicked != false && CheckPK(GSAD.TextBox1.Text,out string strPK))
            {
                //получение полилинии для расчета знаков
                bool checkPoly = GetPolyline(AcadDoc, AcadDB, out Polyline PolyTrassaMN);
                //Расстановка знаков
                if (checkPoly == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = strPK;// GSF.TextBoxText(1);
                        double PK100 = PiketSto(BeginPiketString);
                        double PK000 = PiketPlus(BeginPiketString);
                        double BeginPiketDouble = PiketStringToDouble(BeginPiketString);
                        double ElasticRadius = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        int i, j;

                        #region ВХОДНЫЕ ДАННЫЕ
                        //Расстояние между знаками
                        double DistOpoznavatSign = Convert.ToDouble(GSAD.ComboBox1.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistOpoznavatSignConst = Convert.ToDouble(GSAD.ComboBox1.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaOpoznavatSign = Convert.ToDouble(GSAD.ComboBox2.Text, cultures);

                        //Количество знаков в угле
                        double CountUgolSign = Convert.ToDouble(GSAD.ComboBox3.Text, cultures);
                        //Расстояние между знаками
                        //double DistUgolSign = GSF.ComboBoxText(4);
                        //Смещение от оси трассы знака
                        double DeltaUgolSign = Convert.ToDouble(GSAD.ComboBox4.Text, cultures);

                        //Расстояние между знаками
                        double DistMarkerSign = Convert.ToDouble(GSAD.ComboBox5.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaMarkerSig = Convert.ToDouble(GSAD.ComboBox6.Text, cultures);

                        //Расстояние между знаками
                        double DistKMSign = Convert.ToDouble(GSAD.ComboBox7.Text, cultures);
                        //Расстояние между знаками
                        double DistDMSign = Convert.ToDouble(GSAD.ComboBox7.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistKMSignConst = Convert.ToDouble(GSAD.ComboBox7.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaKMSign = Convert.ToDouble(GSAD.ComboBox8.Text, cultures);

                        //Расстояние между знаками
                        double DistReperSign = Convert.ToDouble(GSAD.ComboBox9.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistReperSignConst = Convert.ToDouble(GSAD.ComboBox9.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaReperSign = Convert.ToDouble(GSAD.ComboBox10.Text, cultures);

                        //Расстояние между парными реперами
                        double DoubleReperMove = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        #endregion ВХОДНЫЕ ДАННЫЕ

                        #region ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Переменные координаты вставки знака 
                            double LocalPX, LocalPY, LocalPZ;
                            //Начальный счетчик подсчета количества плюсовок
                            int n = 0;
                            //Расстояние от начала трассы до первого знака с учетом значения начального ПК
                            double BeginPiket = (PK100 * 100) % DistOpoznavatSignConst + PK000;
                            //Входные параметры для знака 
                            int k = 1; //Для SignIden i=1
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            //расчет координат точек вставки знаков
                            for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 2); i++)
                            {
                                //Начальная и конечная точка отрезка на полилинии
                                Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                //Создаём ложную линию для определения угла направления начального
                                Line linePL = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                //Угол направления трассы
                                double AngleLenPL = linePL.Angle;
                                //Дистанция между вершинами
                                double LenPL = linePL.Length;
                                //Пересчет исходных координат
                                double LocalPXforLine = StartPoint.X - BeginPiket * Math.Cos(AngleLenPL) + DistOpoznavatSign * Math.Cos(AngleLenPL) - DeltaOpoznavatSign * Math.Cos(Math.PI / 2 - AngleLenPL);
                                double LocalPYforLine = StartPoint.Y - BeginPiket * Math.Sin(AngleLenPL) + DistOpoznavatSign * Math.Sin(AngleLenPL) + DeltaOpoznavatSign * Math.Sin(Math.PI / 2 - AngleLenPL);
                                double LocalPZforLine = 0;
                                //Расстановка опознавательных знаков через 500 м
                                if (LenPL >= DistOpoznavatSign - BeginPiket)
                                {
                                    LenPL = LenPL + BeginPiket - DistOpoznavatSign;
                                    for (j = 0; j <= (int)(LenPL / DistOpoznavatSignConst); j++)
                                    {
                                        //Расчет координат вставки знаков
                                        LocalPX = LocalPXforLine + j * DistOpoznavatSignConst * Math.Cos(AngleLenPL);
                                        LocalPY = LocalPYforLine + j * DistOpoznavatSignConst * Math.Sin(AngleLenPL);
                                        LocalPZ = LocalPZforLine;
                                        //Получение параметров блока для знака
                                        double distsign = (PK100 * 100 + DistOpoznavatSignConst - (PK100 * 100) % DistOpoznavatSignConst + DistOpoznavatSignConst * n);
                                        n++;
                                        //Если установка КМ знаков не требуется, то ставим все знаки
                                        if (GSAD.CheckBox4.IsChecked == false)
                                        {
                                            string LocalValueAtt01 = Convert.ToString(distsign / 100 + "+00");
                                            double LocalAngleBlock = AngleLenPL;
                                            //Поиск блока новый/существующий
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                        LocalAngleBlock, LocalType
                                                                        );
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                            }

                                        }
                                        //Если установка КМ знаков требуется, то пропускаем КМ
                                        if (GSAD.CheckBox4.IsChecked == true && distsign % DistKMSignConst != 0)
                                        {
                                            string LocalValueAtt01 = Convert.ToString(distsign / 100 + "+00");
                                            double LocalAngleBlock = AngleLenPL;
                                            //Поиск блока новый/существующий
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                        LocalAngleBlock, LocalType
                                                                        );
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                            }

                                        }
                                    }
                                    BeginPiket = 0;
                                    DistOpoznavatSign = DistOpoznavatSignConst - (LenPL - DistOpoznavatSignConst * (j - 1));
                                }
                                else
                                {
                                    DistOpoznavatSign -= LenPL;
                                }
                            }
                        }
                        #endregion ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ

                        #region ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ НА УГЛАХ ПОВОРОТА - ОБЯЗАТЕЛЬНЫЕ

                        //Ключ - индикатор необходимости расстановки знаков на углах поворота
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Сохранения ПК отчета
                            double BeginPiket = BeginPiketDouble;
                            for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 3); i++)
                            {
                                //Получение параметров блока для знака
                                int k = 2; //Для SignIdenUP i=2
                                string LocalNameSign = S.GetParametrSign(k, 0);
                                string LocalShortNameSign = S.GetParametrSign(k, 1);
                                string LocalType = S.GetParametrSign(k, 2);
                                string LocalTag01 = S.GetParametrSign(k, 3);
                                string LocalPrompt01 = S.GetParametrSign(k, 4);
                                //string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(k, 4);
                                string LocalTag02 = S.GetParametrSign(k, 6);
                                string LocalPrompt02 = S.GetParametrSign(k, 7);
                                string LocalValueAtt02 = S.GetParametrSign(k, 8);
                                string LocalTag03 = S.GetParametrSign(k, 9);
                                string LocalPrompt03 = S.GetParametrSign(k, 10);
                                string LocalValueAtt03 = S.GetParametrSign(k, 11);
                                string LocalTag04 = S.GetParametrSign(k, 12);
                                string LocalPrompt04 = S.GetParametrSign(k, 13);
                                string LocalValueAtt04 = S.GetParametrSign(k, 14);
                                string LocalTag05 = S.GetParametrSign(k, 15);
                                string LocalPrompt05 = S.GetParametrSign(k, 16);
                                string LocalValueAtt05 = S.GetParametrSign(k, 17);
                                //double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                                //Начальная и конечная точка отрезка на полилинии
                                Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                Point3d MiddlePoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 2);
                                //Создаём ложную линию для определения угла направления начального
                                Line LineBeg = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z));
                                //Создаём ложную линию для определения угла направления начального
                                Line LineEnd = new Line(new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                //Угол направления начала трассы
                                double AngleLineBeg = LineBeg.Angle;
                                //Угол направления следующего участка трассы
                                double AngleLineEnd = LineEnd.Angle;
                                //Угол вставки знака
                                double AngleMiddle = GetMiddleAngle(AngleLineBeg, AngleLineEnd);
                                //Дистанция между вершинами
                                double LenPL = LineBeg.Length;
                                //Пересчет исходных координат
                                double LocalPX = MiddlePoint.X - DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleMiddle);
                                double LocalPY = MiddlePoint.Y + DeltaUgolSign * Math.Sin(Math.PI / 2 - AngleMiddle);
                                double LocalPZ = 0;
                                //Расчет начального ПК трассы
                                BeginPiket += LenPL;
                                //Расчет параметров блока для знака
                                string LocalValueAtt01 = KMtoPK(BeginPiket);
                                double LocalAngleBlock = AngleMiddle; //Угол вставки блока - для одиночного 0 рад.
                                //Поиск блока новый/существующий                        
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                        LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ НА УГЛАХ ПОВОРОТА - ОБЯЗАТЕЛЬНЫЕ

                        #region ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ НА УГЛАХ ПОВОРОТА - ДОПОЛНИТЕЛЬНЫЕ НА КАТЕТАХ УПРУГИХ

                        //Ключ - индикатор необходимости расстановки знаков на углах поворота
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            if (CountUgolSign == 3)
                            {
                                //Сохранения ПК отчета
                                double BeginPiket = BeginPiketDouble;
                                for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 3); i++)
                                {
                                    //Получение параметров блока для знака
                                    int k = 1; //Для SignIden i=1
                                    string LocalNameSign = S.GetParametrSign(k, 0);
                                    string LocalShortNameSign = S.GetParametrSign(k, 1);
                                    string LocalType = S.GetParametrSign(k, 2);
                                    string LocalTag01 = S.GetParametrSign(k, 3);
                                    string LocalPrompt01 = S.GetParametrSign(k, 4);
                                    //string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(k, 4);
                                    string LocalTag02 = S.GetParametrSign(k, 6);
                                    string LocalPrompt02 = S.GetParametrSign(k, 7);
                                    string LocalValueAtt02 = S.GetParametrSign(k, 8);
                                    string LocalTag03 = S.GetParametrSign(k, 9);
                                    string LocalPrompt03 = S.GetParametrSign(k, 10);
                                    string LocalValueAtt03 = S.GetParametrSign(k, 11);
                                    string LocalTag04 = S.GetParametrSign(k, 12);
                                    string LocalPrompt04 = S.GetParametrSign(k, 13);
                                    string LocalValueAtt04 = S.GetParametrSign(k, 14);
                                    string LocalTag05 = S.GetParametrSign(k, 15);
                                    string LocalPrompt05 = S.GetParametrSign(k, 16);
                                    string LocalValueAtt05 = S.GetParametrSign(k, 17);
                                    //double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                                    //Начальная и конечная точка отрезка на полилинии
                                    Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                    Point3d MiddlePoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                    Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 2);
                                    //Получаем угол между двумя направлениями трассы до и после

                                    double AngleTrass = GetAngle(StartPoint, MiddlePoint, EndPoint);
                                    double DistUgolSign = ElasticRadius * Math.Tan((Math.PI - AngleTrass) / 2);
                                    //Создаём ложную линию для определения угла направления начального
                                    Line LineBeg = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z));
                                    //Создаём ложную линию для определения угла направления начального
                                    Line LineEnd = new Line(new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                    //Угол направления начала трассы
                                    double AngleLineBeg = LineBeg.Angle;
                                    //Угол направления следующего участка трассы
                                    double AngleLineEnd = LineEnd.Angle;
                                    //Дистанция между вершинами
                                    double LenPL = LineBeg.Length;
                                    //Расчет начального ПК трассы
                                    BeginPiket += LenPL;
                                    //Переменные координаты вставки знака 
                                    double AddLocalPX, AddLocalPY, AddLocalPZ;
                                    AddLocalPX = AddLocalPY = AddLocalPZ = 0;
                                    //расчет координат точек вставки знаков
                                    for (j = 0; j <= 1; j++)
                                    {
                                        string LocalValueAtt01 = "";
                                        double LocalAngleBlock = 0;
                                        if (j == 0) //Пересчет исходных координат в минус
                                        {
                                            AddLocalPX = GetPXBeginLine(MiddlePoint.X, DistUgolSign, AngleLineBeg) - DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleLineBeg);
                                            AddLocalPY = GetPYBeginLine(MiddlePoint.Y, DistUgolSign, AngleLineBeg) + DeltaUgolSign * Math.Sin(Math.PI / 2 - AngleLineBeg);
                                            AddLocalPZ = 0;
                                            LocalValueAtt01 = KMtoPK(BeginPiket - DistUgolSign);
                                            LocalAngleBlock = AngleLineBeg; //Угол вставки блока
                                        }
                                        if (j == 1) //Пересчет исходных координат в плюс
                                        {
                                            AddLocalPX = GetPXBeginEnd(MiddlePoint.X, DistUgolSign, AngleLineEnd) - DeltaUgolSign * Math.Cos(Math.PI / 2 - AngleLineEnd);
                                            AddLocalPY = GetPYBeginEnd(MiddlePoint.Y, DistUgolSign, AngleLineEnd) + DeltaUgolSign * Math.Sin(Math.PI / 2 - AngleLineEnd);
                                            AddLocalPZ = 0;
                                            LocalValueAtt01 = KMtoPK(BeginPiket + DistUgolSign);
                                            LocalAngleBlock = AngleLineEnd;

                                        }
                                        if (IfExistBlock(LocalNameSign) == false) //Поиск блока новый/существующий
                                        {
                                            CreateBlockSignIden(AddLocalPX, AddLocalPY, AddLocalPZ, LocalNameSign, LocalShortNameSign,
                                                                LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                LocalAngleBlock, LocalType);
                                        }
                                        else
                                        {

                                            CreateBlockSignIdenIfExist(AddLocalPX, AddLocalPY, AddLocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                        }
                                    }
                                }
                            }
                        
                        }
                        #endregion ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ НА УГЛАХ ПОВОРОТА - ДОПОЛНИТЕЛЬНЫЕ НА КАТЕТАХ УПРУГИХ

                        #region ЗНАКИ РЕПЕРА

                        //Ключ - индикатор необходимости расстановки реперов
                        if (GSAD.CheckBox5.IsChecked == true)
                        {
                            if (GSAD.CheckBox7.IsChecked == true)
                            {
                                BeginPiketString = GSAD.TextBox4.Text;
                                PK100 = PiketSto(BeginPiketString);
                                PK000 = PiketPlus(BeginPiketString);
                            }
                            //Начальный счетчик подсчета количества плюсовок
                            int n = 0;
                            //Расстояние от начала трассы до первого знака с учетом значения начального ПК
                            double BeginPiket = (PK100 * 100) % DistReperSignConst + PK000;
                            //Получение параметров блока для знака
                            int k = 7; //Для SignReper i=7
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            //string LocalValueAtt01 = LocalPiket;//S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);

                            //double LocalAngleBlock = 0;//Угол вставки блока - для одиночного 0 рад.
                            //расчет координат точек вставки знаков
                            for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 2); i++)
                            {
                                //Начальная и конечная точка отрезка на полилинии
                                Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                //Создаём ложную линию для определения угла направления начального
                                Line linePL = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                //Угол направления трассы
                                double AngleLenPL = linePL.Angle;
                                //Дистанция между вершинами
                                double LenPL = linePL.Length;
                                //Пересчет исходных координат
                                double LocalPXforLine = StartPoint.X - BeginPiket * Math.Cos(AngleLenPL) + DistReperSign * Math.Cos(AngleLenPL) - DeltaReperSign * Math.Cos(Math.PI / 2 - AngleLenPL);
                                double LocalPYforLine = StartPoint.Y - BeginPiket * Math.Sin(AngleLenPL) + DistReperSign * Math.Sin(AngleLenPL) + DeltaReperSign * Math.Sin(Math.PI / 2 - AngleLenPL);
                                double LocalPZforLine = 0;
                                //Расчет расстановки знаков КМ
                                if (LenPL >= DistReperSign - BeginPiket)
                                {
                                    LenPL = LenPL + BeginPiket - DistReperSign;
                                    for (j = 0; j <= (int)(LenPL / DistReperSignConst); j++)
                                    {
                                        int countReper = 1;
                                        if (GSAD.CheckBox6.IsChecked == true) { countReper = 2; }
                                        for (int t = 1; t <= countReper; t++)
                                        {
                                            //Расчет координат вставки знаков
                                            double LocalPX = LocalPXforLine + (j * (DistReperSignConst) + (t - 1) * DoubleReperMove) * Math.Cos(AngleLenPL);
                                            double LocalPY = LocalPYforLine + (j * (DistReperSignConst) + (t - 1) * DoubleReperMove) * Math.Sin(AngleLenPL);
                                            double LocalPZ = LocalPZforLine;
                                            //Расчет параметров блока для знака
                                            string LocalValueAtt01 = KMtoPK(PK100 * 100 + DistReperSignConst - (PK100 * 100) % DistReperSignConst + DistReperSignConst * n + (t - 1) * DoubleReperMove);
                                            double LocalAngleBlock = AngleLenPL; //Угол вставки блока - для одиночного 0 рад.
                                            //Поиск блока новый/существующий
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                            LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                            LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                            LocalAngleBlock, LocalType
                                                                            );
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                            LocalValueAtt01, LocalTag01,
                                                                            LocalValueAtt02, LocalTag02,
                                                                            LocalValueAtt03, LocalTag03,
                                                                            LocalValueAtt04, LocalTag04,
                                                                            LocalValueAtt05, LocalTag05,
                                                                            LocalAngleBlock);
                                            }
                                        }
                                        n++;
                                    }
                                    
                                    BeginPiket = 0;
                                    DistReperSign = DistReperSignConst - (LenPL - DistReperSignConst * (j - 1));
                                }
                                else
                                {
                                    DistReperSign -= LenPL;
                                }
                            }
                        }


                        #endregion ЗНАКИ РЕПЕРА

                        #region КИЛОМЕТРОВЫЕ ЗНАКИ

                        //Ключ - индикатор необходимости расстановки знаков
                        if (GSAD.CheckBox4.IsChecked == true)
                        {
                            if (GSAD.CheckBox6.IsChecked == true)
                            {
                                BeginPiketString = GSAD.TextBox4.Text;
                                PK100 = PiketSto(BeginPiketString);
                                PK000 = PiketPlus(BeginPiketString);
                            }
                            //Начальный счетчик подсчета количества плюсовок
                            int n = 0;
                            //Расстояние от начала трассы до первого знака с учетом значения начального ПК
                            double BeginPiket = (PK100 * 100) % DistKMSignConst + PK000;
                            //расчет координат точек вставки знаков
                            for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 2); i++)
                                {
                                    //Начальная и конечная точка отрезка на полилинии
                                    Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                    Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                    //Создаём ложную линию для определения угла направления начального
                                    Line linePL = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                    //Угол направления трассы
                                    double AngleLenPL = linePL.Angle;
                                    //Дистанция между вершинами
                                    double LenPL = linePL.Length;
                                    //Пересчет исходных координат
                                    double LocalPXforLine = StartPoint.X - BeginPiket * Math.Cos(AngleLenPL) + DistDMSign * Math.Cos(AngleLenPL) - DeltaKMSign * Math.Cos(Math.PI / 2 - AngleLenPL);
                                    double LocalPYforLine = StartPoint.Y - BeginPiket * Math.Sin(AngleLenPL) + DistDMSign * Math.Sin(AngleLenPL) + DeltaKMSign * Math.Sin(Math.PI / 2 - AngleLenPL);
                                    double LocalPZforLine = 0;
                                    //Расстановка километровых знаков
                                    if (LenPL >= DistDMSign - BeginPiket)
                                    {
                                        LenPL = LenPL + BeginPiket - DistDMSign;
                                        for (j = 0; j <= (int)(LenPL / DistKMSignConst); j++)
                                        {
                                        //Расчет координат вставки знаков
                                        double LocalPX = LocalPXforLine + j * DistKMSignConst * Math.Cos(AngleLenPL);
                                        double LocalPY = LocalPYforLine + j * DistKMSignConst * Math.Sin(AngleLenPL);
                                        double LocalPZ = LocalPZforLine;
                                            //Получение параметров блока для знака
                                            double distsign = (PK100 * 100 + DistKMSignConst - (PK100 * 100) % DistKMSignConst + DistKMSignConst * n);
                                            n += 1;

                                            //Расстановка знаков КМ - если нету вообще маркеров
                                            if (GSAD.CheckBox3.IsChecked  == false)
                                            {
                                                int k = 11; //Для SignКМ i=11
                                                string LocalNameSign = S.GetParametrSign(k, 0);
                                                string LocalShortNameSign = S.GetParametrSign(k, 1);
                                                string LocalType = S.GetParametrSign(k, 2);

                                                string LocalTag01 = S.GetParametrSign(k, 3);
                                                string LocalPrompt01 = S.GetParametrSign(k, 4);
                                                string LocalValueAtt01 = Convert.ToString(distsign / 100 + "+00");

                                                string LocalTag02 = S.GetParametrSign(k, 6);
                                                string LocalPrompt02 = S.GetParametrSign(k, 7);
                                                string LocalValueAtt02 = S.GetParametrSign(k, 8);

                                                string LocalTag03 = S.GetParametrSign(k, 9);
                                                string LocalPrompt03 = S.GetParametrSign(k, 10);
                                                string LocalValueAtt03 = S.GetParametrSign(k, 11);

                                                string LocalTag04 = S.GetParametrSign(k, 12);
                                                string LocalPrompt04 = S.GetParametrSign(k, 13);
                                                string LocalValueAtt04 = S.GetParametrSign(k, 14);

                                                string LocalTag05 = S.GetParametrSign(k, 15);
                                                string LocalPrompt05 = S.GetParametrSign(k, 16);
                                                string LocalValueAtt05 = S.GetParametrSign(k, 17);

                                                string LocalTag06 = S.GetParametrSign(k, 18);
                                                string LocalPrompt06 = S.GetParametrSign(k, 19);
                                                string LocalValueAtt06 = Convert.ToString(distsign / 1000);

                                                double LocalAngleBlock = AngleLenPL;//Угол вставки блока - для одиночного 0 рад.
                                                //Поиск блока новый/существующий
                                                if (IfExistBlock(LocalNameSign) == false)
                                                {
                                                    CreateBlockSignKM(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                      LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                      LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                      LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                      LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                      LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                      LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                      LocalAngleBlock, LocalType
                                                                      );
                                                }
                                                else
                                                {
                                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                                  LocalValueAtt01, LocalTag01,
                                                                                  LocalValueAtt02, LocalTag02,
                                                                                  LocalValueAtt03, LocalTag03,
                                                                                  LocalValueAtt04, LocalTag04,
                                                                                  LocalValueAtt05, LocalTag05,
                                                                                  LocalValueAtt06, LocalTag06,
                                                                                  LocalAngleBlock
                                                                                  );
                                                }

                                            }

                                            //Расстановка знаков КМ
                                            /*if (GSAD.CheckBox3.IsChecked == true && distsign % DistKMSignConst == 0 && distsign % DistMarkerSign != 0)
                                            {
                                                int k = 11; //Для SignКМM i=11
                                                string LocalNameSign = S.GetParametrSign(k, 0);
                                                string LocalShortNameSign = S.GetParametrSign(k, 1);
                                                string LocalType = S.GetParametrSign(k, 2);

                                                string LocalTag01 = S.GetParametrSign(k, 3);
                                                string LocalPrompt01 = S.GetParametrSign(k, 4);
                                                string LocalValueAtt01 = Convert.ToString(distsign / 100 + "+00");

                                                string LocalTag02 = S.GetParametrSign(k, 6);
                                                string LocalPrompt02 = S.GetParametrSign(k, 7);
                                                string LocalValueAtt02 = S.GetParametrSign(k, 8);

                                                string LocalTag03 = S.GetParametrSign(k, 9);
                                                string LocalPrompt03 = S.GetParametrSign(k, 10);
                                                string LocalValueAtt03 = S.GetParametrSign(k, 11);

                                                string LocalTag04 = S.GetParametrSign(k, 12);
                                                string LocalPrompt04 = S.GetParametrSign(k, 13);
                                                string LocalValueAtt04 = S.GetParametrSign(k, 14);

                                                string LocalTag05 = S.GetParametrSign(k, 15);
                                                string LocalPrompt05 = S.GetParametrSign(k, 16);
                                                string LocalValueAtt05 = S.GetParametrSign(k, 17);

                                                string LocalTag06 = S.GetParametrSign(k, 18);
                                                string LocalPrompt06 = S.GetParametrSign(k, 19);
                                                string LocalValueAtt06 = Convert.ToString(distsign / 1000);

                                                double LocalAngleBlock = AngleLenPL;//Угол вставки блока - для одиночного 0 рад.
                                                //Поиск блока новый/существующий
                                                if (IfExistBlock(LocalNameSign) == false)
                                                {
                                                    CreateBlockSignKM(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                          LocalAngleBlock, LocalType
                                                                          );
                                                }
                                                else
                                                {
                                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                                  LocalValueAtt01, LocalTag01,
                                                                                  LocalValueAtt02, LocalTag02,
                                                                                  LocalValueAtt03, LocalTag03,
                                                                                  LocalValueAtt04, LocalTag04,
                                                                                  LocalValueAtt05, LocalTag05,
                                                                                  LocalValueAtt06, LocalTag06,
                                                                                  LocalAngleBlock
                                                                                  );
                                                }

                                            }*/

                                            //Расстановка знаков КМ c маркером
                                            /*if (GSAD.CheckBox3.IsChecked == true && distsign % DistKMSignConst == 0 && distsign % DistMarkerSign == 0)
                                            {
                                                int k = 12; //Для SignКМM i=12
                                                string LocalNameSign = S.GetParametrSign(k, 0);
                                                string LocalShortNameSign = S.GetParametrSign(k, 1);
                                                string LocalType = S.GetParametrSign(k, 2);

                                                string LocalTag01 = S.GetParametrSign(k, 3);
                                                string LocalPrompt01 = S.GetParametrSign(k, 4);
                                                string LocalValueAtt01 = Convert.ToString(distsign / 100 + "+00");

                                                string LocalTag02 = S.GetParametrSign(k, 6);
                                                string LocalPrompt02 = S.GetParametrSign(k, 7);
                                                string LocalValueAtt02 = S.GetParametrSign(k, 8);

                                                string LocalTag03 = S.GetParametrSign(k, 9);
                                                string LocalPrompt03 = S.GetParametrSign(k, 10);
                                                string LocalValueAtt03 = S.GetParametrSign(k, 11);

                                                string LocalTag04 = S.GetParametrSign(k, 12);
                                                string LocalPrompt04 = S.GetParametrSign(k, 13);
                                                string LocalValueAtt04 = S.GetParametrSign(k, 14);

                                                string LocalTag05 = S.GetParametrSign(k, 15);
                                                string LocalPrompt05 = S.GetParametrSign(k, 16);
                                                string LocalValueAtt05 = S.GetParametrSign(k, 17);

                                                string LocalTag06 = S.GetParametrSign(k, 18);
                                                string LocalPrompt06 = S.GetParametrSign(k, 19);
                                                string LocalValueAtt06 = Convert.ToString(distsign / 1000);

                                                double LocalAngleBlock = AngleLenPL;//Угол вставки блока - для одиночного 0 рад.
                                                //Поиск блока новый/существующий
                                                if (IfExistBlock(LocalNameSign) == false)
                                                {
                                                    CreateBlockSignKM(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                      LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                      LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                      LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                      LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                      LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                      LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                      LocalAngleBlock, LocalType
                                                                      );
                                                }
                                                else
                                                {
                                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                                  LocalValueAtt01, LocalTag01,
                                                                                  LocalValueAtt02, LocalTag02,
                                                                                  LocalValueAtt03, LocalTag03,
                                                                                  LocalValueAtt04, LocalTag04,
                                                                                  LocalValueAtt05, LocalTag05,
                                                                                  LocalValueAtt06, LocalTag06,
                                                                                  LocalAngleBlock
                                                                                  );
                                                }
                                            }*/
                                        
                                    }
                                        BeginPiket = 0;
                                        DistDMSign = DistKMSignConst - (LenPL - DistKMSignConst * (j - 1));
                                    }
                                    else
                                    {
                                        DistDMSign -= LenPL;
                                    }
                                }
                            
                        }
                        #endregion КИЛОМЕТРОВЫЕ ЗНАКИ

                        #region ДЕФОРМАЦИОННЫЕ МАРКИ

                        //Ключ - индикатор необходимости расстановки знаков
                        if (GSAD.CheckBox8.IsChecked == true)
                        {
                            //Начальный счетчик подсчета количества плюсовок
                            int n = 0;
                            //Расстояние от начала трассы до первого знака с учетом значения начального ПК
                            double BeginPiket = (PK100 * 100) % DistKMSignConst + PK000;
                            //расчет координат точек вставки знаков
                            for (i = 0; i <= (PolyTrassaMN.NumberOfVertices - 2); i++)
                            {
                                //Начальная и конечная точка отрезка на полилинии
                                Point3d StartPoint = PolyTrassaMN.GetPoint3dAt(i);
                                Point3d EndPoint = PolyTrassaMN.GetPoint3dAt(i + 1);
                                //Создаём ложную линию для определения угла направления начального
                                Line linePL = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                                //Угол направления трассы
                                double AngleLenPL = linePL.Angle;
                                //Дистанция между вершинами
                                double LenPL = linePL.Length;
                                //Пересчет исходных координат
                                double LocalPXforLine = StartPoint.X - BeginPiket * Math.Cos(AngleLenPL) + DistKMSign * Math.Cos(AngleLenPL) - DeltaKMSign * Math.Cos(Math.PI / 2 - AngleLenPL);
                                double LocalPYforLine = StartPoint.Y - BeginPiket * Math.Sin(AngleLenPL) + DistKMSign * Math.Sin(AngleLenPL) + DeltaKMSign * Math.Sin(Math.PI / 2 - AngleLenPL);
                                double LocalPZforLine = 0;
                                //Расстановка километровых знаков
                                if (LenPL >= DistKMSign - BeginPiket)
                                {
                                    LenPL = LenPL + BeginPiket - DistKMSign;
                                    for (j = 0; j <= (int)(LenPL / DistKMSignConst); j++)
                                    {
                                        //Расчет координат вставки знаков
                                        double LocalPX = LocalPXforLine + j * DistKMSignConst * Math.Cos(AngleLenPL);
                                        double LocalPY = LocalPYforLine + j * DistKMSignConst * Math.Sin(AngleLenPL);
                                        double LocalPZ = LocalPZforLine;
                                        //Получение параметров блока для знака
                                        double distsign = (PK100 * 100 + DistKMSignConst - (PK100 * 100) % DistKMSignConst + DistKMSignConst * n);
                                        n += 1;
                                        int k = 0; //Для SignDM i=0

                                        string LocalNameSign = S.GetParametrSign(k, 0);
                                        string LocalShortNameSign = S.GetParametrSign(k, 1);
                                        string LocalType = S.GetParametrSign(k, 2);

                                        string LocalTag01 = S.GetParametrSign(k, 3);
                                        string LocalPrompt01 = S.GetParametrSign(k, 4);
                                        string LocalValueAtt01 = Convert.ToString(distsign / 100 + "+00");

                                        string LocalTag02 = S.GetParametrSign(k, 6);
                                        string LocalPrompt02 = S.GetParametrSign(k, 7);
                                        string LocalValueAtt02 = S.GetParametrSign(k, 8);

                                        string LocalTag03 = S.GetParametrSign(k, 9);
                                        string LocalPrompt03 = S.GetParametrSign(k, 10);
                                        string LocalValueAtt03 = S.GetParametrSign(k, 11);

                                        string LocalTag04 = S.GetParametrSign(k, 12);
                                        string LocalPrompt04 = S.GetParametrSign(k, 13);
                                        string LocalValueAtt04 = S.GetParametrSign(k, 14);

                                        string LocalTag05 = S.GetParametrSign(k, 15);
                                        string LocalPrompt05 = S.GetParametrSign(k, 16);
                                        string LocalValueAtt05 = S.GetParametrSign(k, 17);

                                        string LocalTag06 = ""; //S.GetParametrSign(k, 18);
                                        string LocalPrompt06 = ""; //S.GetParametrSign(k, 18);
                                        string LocalValueAtt06 = ""; //S.GetParametrSign(k, 18);

                                        string LocalTag07 = ""; //S.GetParametrSign(k, 18);
                                        string LocalPrompt07 = ""; //S.GetParametrSign(k, 18);
                                        string LocalValueAtt07 = ""; //S.GetParametrSign(k, 18);

                                        double LocalAngleBlock = AngleLenPL;//Угол вставки блока - для одиночного 0 рад.
                                                                            //Поиск блока новый/существующий
                                        if (IfExistBlock(LocalNameSign) == false)
                                        {
                                            CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                  LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                  LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                  LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                  LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                  LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                  LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                  LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                  LocalAngleBlock, LocalType
                                                                  );
                                        }
                                        else
                                        {
                                            CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                          LocalValueAtt01, LocalTag01,
                                                                          LocalValueAtt02, LocalTag02,
                                                                          LocalValueAtt03, LocalTag03,
                                                                          LocalValueAtt04, LocalTag04,
                                                                          LocalValueAtt05, LocalTag05,
                                                                          LocalValueAtt06, LocalTag06,
                                                                          LocalAngleBlock
                                                                          );
                                        }


                                    }
                                    BeginPiket = 0;
                                    DistKMSign = DistKMSignConst - (LenPL - DistKMSignConst * (j - 1));
                                }
                                else
                                {
                                    DistKMSign -= LenPL;
                                }
                            }

                        }
                        #endregion ДЕФОРМАЦИОННЫЕ МАРКИ


                        tr.Commit();
                    }
                }
            }

        }

        //Метод расстановки знаков на оврагах
        [CommandMethod("InsertGroupSignOvrag", CommandFlags.UsePickSet)]
        public static void InsertGroupSignOvrag()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignOvrag GSAD = new FormGroupSignOvrag();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();

            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние между знаками - не меняемое
                        double DistMarkerSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaMarkerSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Расстояние между знаками
                        double DistReperSign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaReperSign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку начала оврага по МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку окончания оврага по МН");
                        Point3d EndPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;
                        //Дистанция для расчета пикетов
                        double AngleLen = line.Length;
                        //Координаты вставки блоков
                        double LocalPX = 0, LocalPY = 0, LocalPZ = 0;
                        #endregion ВХОДНЫЕ ДАННЫЕ

                        //01 - МАРКЕРА - 2шт.
                        /*#region МАРКЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 6; //Для SignMarker i=6
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет пикета
                                if (i == -1)
                                {
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistMarkerSign);
                                    //Пересчет исходных координат
                                    LocalPX = StartPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = StartPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (i == 1)
                                {
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistMarkerSign + AngleLen);
                                    //Пересчет исходных координат
                                    LocalPX = EndPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = EndPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                              LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                              LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                              LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                              LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                              LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                              LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                              LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                              LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion МАРКЕР*/

                        //02 - РЕПЕР - 1шт.
                        #region РЕПЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 7; //Для SignReper i=7
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;//S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            LocalPX = StartPoint.X - DistReperSign * Math.Cos(AngleMN) - DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                            LocalPY = StartPoint.Y - DistReperSign * Math.Sin(AngleMN) + DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                            LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                        LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                        LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                        LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                            }
                        }
                        #endregion РЕПЕР

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на категорийных дорогах
        [CommandMethod("InsertGroupSignADIV", CommandFlags.UsePickSet)]
        public static void InsertGroupSignADIV()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignADIV GSAD = new FormGroupSignADIV();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние от оси пересечения до знака
                        double DistAnshlagSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistOZSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaOZSign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Расстояние между знаками
                        double DistSTOPSign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaSTOPSign = Convert.ToDouble(GSAD.TextBox6.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistMarkerSign = Convert.ToDouble(GSAD.TextBox7.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaMarkerSign = Convert.ToDouble(GSAD.TextBox8.Text, cultures);

                        //получение точки 1 границы дороги по оси МН
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку первой границы а/д по оси МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси а/д по оси МН");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку второй границы а/д по оси МН");
                        Point3d EndPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси а/д не на оси МН");
                        Point3d ADPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z), new Point3d(ADPoint.X, ADPoint.Y, ADPoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;
                        #endregion ВХОДНЫЕ ДАННЫЕ

                        //01 - ЗНАКИ НА АНШЛАГ - 2шт.
                        #region ЗНАКИ НА АНШЛАГ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 66; //Для SignWAD i=66
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                                                             //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Константа атрибута
                                string LocalValueAtt01;
                                //дельта смещения
                                double deltadist;
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    //Пересчет пикета 
                                    deltadist = Math.Sqrt(Math.Pow(MiddlePoint.X - StartPoint.X,2) + Math.Pow(MiddlePoint.Y - StartPoint.Y,2));
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * (DistAnshlagSign + deltadist));
                                    //Пересчет координат
                                    LocalPX = StartPoint.X + i * DistAnshlagSign * Math.Cos(AngleMN);
                                    LocalPY = StartPoint.Y + i * DistAnshlagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    //Пересчет пикета
                                    deltadist = Math.Pow(Math.Pow(EndPoint.X - MiddlePoint.X, 2) + Math.Pow(EndPoint.Y - MiddlePoint.Y, 2), 0.5);
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * (DistAnshlagSign + deltadist));
                                    //Пересчет координат
                                    LocalPX = EndPoint.X + i * DistAnshlagSign * Math.Cos(AngleMN);
                                    LocalPY = EndPoint.Y + i * DistAnshlagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //02 - ЗНАК ОХРАННАЯ ЗОНА - 4шт.
                        #region ЗНАК ОХРАННАЯ ЗОНА
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 1; //Для SignIden i=1
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    //Пересчет исходных координат
                                    double LocalPX; double LocalPY; double LocalPZ;
                                    if (j == -1)
                                    {
                                        LocalPX = StartPoint.X + i * DeltaOZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPY = StartPoint.Y + i * DeltaOZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    else
                                    {
                                        LocalPX = EndPoint.X + i * DeltaOZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPY = EndPoint.Y + i * DeltaOZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    if (IfExistBlock(LocalNameSign) == false)
                                    {
                                        CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                LocalAngleBlock, LocalType);
                                    }
                                    else
                                    {
                                        CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                    }
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //03 - ОСТАНОВКА ЗАПРЕЩЕНА - 2шт.
                        /*#region ОСТАНОВКА ЗАПРЕЩЕНА
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox3.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 67; //Для SignStop i=67
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                                                             //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2) // i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    LocalPX = EndPoint.X + i * DeltaSTOPSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPY = EndPoint.Y + i * DeltaSTOPSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    LocalPX = StartPoint.X + i * DeltaSTOPSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPY = StartPoint.Y + i * DeltaSTOPSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignDiff(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ОСТАНОВКА ЗАПРЕЩЕНА*/

                        //04 - МАРКЕРА - 2шт.
                        /*#region МАРКЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox4.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 6; //Для SignMarker i=6
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                                                            
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Константа атрибута
                                string LocalValueAtt01;
                                //дельта смещения
                                double deltadist;
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    //Пересчет пикета 
                                    deltadist = Math.Sqrt(Math.Pow(MiddlePoint.X - StartPoint.X, 2) + Math.Pow(MiddlePoint.Y - StartPoint.Y, 2));
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * (DistMarkerSign + deltadist));
                                    LocalPX = StartPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = StartPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    //Пересчет пикета
                                    deltadist = Math.Pow(Math.Pow(EndPoint.X - MiddlePoint.X, 2) + Math.Pow(EndPoint.Y - MiddlePoint.Y, 2), 0.5);
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * (DistMarkerSign + deltadist));
                                    LocalPX = EndPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = EndPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                            LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion МАРКЕР*/
                        tr.Commit();
                    }
                }
            }

        }

        //Метод расстановки знаков на категорийных дорогах
        [CommandMethod("InsertGroupSignADNoCategory", CommandFlags.UsePickSet)]
        public static void InsertGroupSignADNoCategory()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignADNoCategory GSAD = new FormGroupSignADNoCategory();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                { 
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                    #region ВХОДНЫЕ ДАННЫЕ
                    //Начальный ПК трассы для расстановки знаков
                    string BeginPiketString = GSAD.TextBox1.Text;
                    //Расстояние от оси пересечения до знака
                    double DistAnshlagSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                    //Расстояние между знаками - не меняемое
                    double DistPZSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                    //Смещение от оси трассы знака
                    double DeltaPZSign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        
                        //получение точки 1 границы дороги по оси МН
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку первой границы а/д по оси МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        
                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси а/д по оси МН");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку второй границы а/д по оси МН");
                        Point3d EndPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси а/д не на оси МН");
                        Point3d ADPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z), new Point3d(ADPoint.X, ADPoint.Y, ADPoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;
                        #endregion ВХОДНЫЕ ДАННЫЕ

                        //01 - ЗНАКИ НА АНШЛАГ - 2шт.
                        #region ЗНАКИ НА АНШЛАГ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 66; //Для SignWAD i=66
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Константа атрибута
                                string LocalValueAtt01;
                                //дельта смещения
                                double deltadist;
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    //Пересчет пикета 
                                    deltadist = Math.Sqrt(Math.Pow(MiddlePoint.X - StartPoint.X, 2) + Math.Pow(MiddlePoint.Y - StartPoint.Y, 2));
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * (DistAnshlagSign + deltadist));
                                    //Пересчет координат
                                    LocalPX = StartPoint.X + i * DistAnshlagSign * Math.Cos(AngleMN);
                                    LocalPY = StartPoint.Y + i * DistAnshlagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    //Пересчет пикета
                                    deltadist = Math.Pow(Math.Pow(EndPoint.X - MiddlePoint.X, 2) + Math.Pow(EndPoint.Y - MiddlePoint.Y, 2), 0.5);
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * (DistAnshlagSign + deltadist));
                                    //Пересчет координат
                                    LocalPX = EndPoint.X + i * DistAnshlagSign * Math.Cos(AngleMN);
                                    LocalPY = EndPoint.Y + i * DistAnshlagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden
                                        (LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                        LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //02 - ЗНАК ПРОЕЗД ЗДЕСЬ - 4шт.
                        #region ЗНАК ПРОЕЗД ЗДЕСЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Пересчет исходных координат
                            double LocalPX = 0 ; double LocalPY = 0; double LocalPZ = 0;
                            //Входные параметры для знака 
                            int k = 68; //Для SignDrive i=68
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    if (j == -1)
                                    {
                                        LocalPX = StartPoint.X + i * DeltaPZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistPZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPY = StartPoint.Y + i * DeltaPZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistPZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    if (j == 1)
                                    {
                                        LocalPX = EndPoint.X + i * DeltaPZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistPZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPY = EndPoint.Y + i * DeltaPZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistPZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    if (IfExistBlock(LocalNameSign) == false)
                                    {
                                        CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                LocalAngleBlock, LocalType);
                                    }
                                    else
                                    {
                                        CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                    }
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на железной дороге
        [CommandMethod("InsertGroupSignRW", CommandFlags.UsePickSet)]
        public static void InsertGroupSignRW()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignRW GSAD = new FormGroupSignRW();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние от оси пересечения до знака
                        double DistAnshlagSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistOZSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaOZSign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Расстояние между знаками
                        double DistSTOPSign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaSTOPSign = Convert.ToDouble(GSAD.TextBox6.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistMarkerSign = Convert.ToDouble(GSAD.TextBox7.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaMarkerSign = Convert.ToDouble(GSAD.TextBox8.Text, cultures);

                        //получение точки 1 границы дороги по оси МН
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку первой границы ж/д по оси МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси ж/д по оси МН");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку второй границы ж/д по оси МН");
                        Point3d EndPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси ж/д не на оси МН");
                        Point3d ADPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние

                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z), new Point3d(ADPoint.X, ADPoint.Y, ADPoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;

                        #endregion ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ

                        //01 - ЗНАКИ НА АНШЛАГ - 2шт.
                        #region ЗНАКИ НА АНШЛАГ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 32; //Для SignNoDrRW i=32
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет пикета
                                string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistAnshlagSign);
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    LocalPX = StartPoint.X + i * DistAnshlagSign * Math.Cos(AngleMN);
                                    LocalPY = StartPoint.Y + i * DistAnshlagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    LocalPX = EndPoint.X + i * DistAnshlagSign * Math.Cos(AngleMN);
                                    LocalPY = EndPoint.Y + i * DistAnshlagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                        LocalValueAtt01, LocalTag01,
                                        LocalValueAtt02, LocalTag02,
                                        LocalValueAtt03, LocalTag03,
                                        LocalValueAtt04, LocalTag04,
                                        LocalValueAtt05, LocalTag05,
                                        LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //02 - ЗНАК ОХРАННАЯ ЗОНА - 4шт.
                        #region ЗНАК ОХРАННАЯ ЗОНА
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 1; //Для SignIden i=1
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    //Пересчет исходных координат
                                    double LocalPX; double LocalPY; double LocalPZ;
                                    if (j == -1)
                                    {
                                        LocalPX = StartPoint.X + i * DeltaOZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPY = StartPoint.Y + i * DeltaOZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    else
                                    {
                                        LocalPX = EndPoint.X + i * DeltaOZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPY = EndPoint.Y + i * DeltaOZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + j * DistOZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    if (IfExistBlock(LocalNameSign) == false)
                                    {
                                        CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                LocalAngleBlock, LocalType);
                                    }
                                    else
                                    {
                                        CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                    }
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //03 - НЕФТЬ - 2шт.
                        #region ЗНАК НЕФТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox3.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 31; //Для SignWarningRW i=31
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    LocalPX = StartPoint.X + i * DeltaSTOPSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPY = StartPoint.Y + i * DeltaSTOPSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    LocalPX = EndPoint.X + i * DeltaSTOPSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPY = EndPoint.Y + i * DeltaSTOPSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) - i * DistSTOPSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗНАК НЕФТЬ

                        //04 - МАРКЕРА - 2шт.
                        /*#region МАРКЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox4.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 6; //Для SignMarker i=6
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет пикета
                                string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistMarkerSign);
                                //Пересчет исходных координат
                                double LocalPX; double LocalPY; double LocalPZ;
                                if (i == -1)
                                {
                                    LocalPX = StartPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = StartPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                else
                                {
                                    LocalPX = EndPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = EndPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                            LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion МАРКЕР*/
                        line.Dispose();
                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на пересечении с кабелем связи
        [CommandMethod("InsertGroupSignCrossCS", CommandFlags.UsePickSet)]
        public static void InsertGroupSignCrossCS()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupCrossCS GSAD = new FormGroupCrossCS();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;

                        //Расстояние от оси пересечения до знака
                        double DistCSSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaCSSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);

                        //Расстояние между знаками - не меняемое
                        double DistMCSign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaMCSign =Convert.ToDouble(GSAD.TextBox5.Text, cultures);

                        //Расстояние между знаками
                        double DistNonESign = Convert.ToDouble(GSAD.TextBox6.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaNonESign = Convert.ToDouble(GSAD.TextBox7.Text, cultures);

                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку пересечения кабеля связи с МН (на полилинии)");
                        Point3d StartPoint = new Point3d(PX, PY, 0);    //PZ - для приведения в компларное состояние
                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку на оси кабеля связи (не на полилинии МН)");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0);   //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку со стороны большего пикета МН (на полилинии)");
                        Point3d EndPoint = new Point3d(PX, PY, 0);      //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;

                        #endregion ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ

                        //01 - ЗНАКИ НА ПЕРЕСЕЧЕНИЕ - 1шт.
                        #region ЗНАКИ НА ПЕРЕСЕЧЕНИЕ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 16; //Для SignCrossСomm i=16
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Пересчет исходных координат
                            double LocalPX = StartPoint.X + DeltaCSSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCSSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPY = StartPoint.Y + DeltaCSSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCSSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                            }
                        }
                        #endregion ЗНАКИ НА ПЕРЕСЕЧЕНИЕ

                        //02 - СТОЛБИКИ ЗАМЕРНЫЕ - 2шт.
                        #region СТОЛБИКИ ЗАМЕРНЫЕ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 25; //Для SignSM i=25
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX = StartPoint.X + i * DeltaMCSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistMCSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPY = StartPoint.Y + i * DeltaMCSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistMCSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                              LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                              LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                              LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                              LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                              LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                              LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                              LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                              LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //03 - ЗЕМЛЮ НЕ КОПАТЬ - 2шт.
                        /*#region ЗЕМЛЮ НЕ КОПАТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox3.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 18; //Для SignHiPress i=18
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX = StartPoint.X + i * DeltaNonESign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPY = StartPoint.Y + i * DeltaNonESign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ОСТАНОВКА ЗАПРЕЩЕНА*/

                        //04 - ЗНАКИ ОХРАННОЙ ЗОНЫ ТАБЛИЧКА- 1шт.
                        #region ЗНАКИ НА ОХРАННОЙ ЗОНЫ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox4.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 20; //Для SignGZCCable i=20
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Пересчет исходных координат
                            double LocalPX = StartPoint.X + DeltaCSSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCSSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN) - 4 * S.HeigthTextSign * Math.Sin(AngleMN);
                            double LocalPY = StartPoint.Y + DeltaCSSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCSSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN) + 4 * S.HeigthTextSign * Math.Cos(AngleMN);
                            double LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                            }
                        }
                        #endregion ЗНАКИ НА ПЕРЕСЕЧЕНИЕ

                        tr.Commit();

                    }
                }
            }
        }

        //Метод расстановки знаков на пересечении с электрическим кабелем связи
        [CommandMethod("InsertGroupSignCrossCE", CommandFlags.UsePickSet)]
        public static void InsertGroupSignCrossCE()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupCrossCE GSAD = new FormGroupCrossCE();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние от оси пересечения до знака
                        double DistCESign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaCESign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Расстояние между знаками
                        double DistNonESign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaNonESign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку пересечения с электрического кабелем с МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0);    //PZ - для приведения в компларное состояние
                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси электрического кабелем (не на полилинии)");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0);   //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите трассы МН со стороны большего пикета");
                        Point3d EndPoint = new Point3d(PX, PY, 0);      //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;

                        #endregion ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ

                        //01 - ЗНАКИ НА ПЕРЕСЕЧЕНИЕ - 1шт.
                        #region ЗНАКИ НА ПЕРЕСЕЧЕНИЕ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 17; //Для SignCrossСomm i=16
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;//S.GetParametrSign(k, 4);
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Пересчет исходных координат
                            double LocalPX = StartPoint.X + DeltaCESign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCESign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPY = StartPoint.Y + DeltaCESign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCESign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                            }
                        }
                        #endregion ЗНАКИ НА ПЕРЕСЕЧЕНИЕ

                        //02 - ЗЕМЛЮ НЕ КОПАТЬ - 2шт.
                        /*#region ЗЕМЛЮ НЕ КОПАТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 18; //Для SignHiPress i=18
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX = StartPoint.X + i * DeltaNonESign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPY = StartPoint.Y + i * DeltaNonESign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ОСТАНОВКА ЗАПРЕЩЕНА*/

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на пересечении с подземным трубопроводом
        [CommandMethod("InsertGroupSignCrossEP", CommandFlags.UsePickSet)]
        public static void InsertGroupSignCrossEP()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignCrossEP GSAD = new FormGroupSignCrossEP();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние от оси пересечения до знака
                        double DistCPSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaCPSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Расстояние между знаками
                        double DistNonESign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaNonESign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку пересечения трубопровода с МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0);    //PZ - для приведения в компларное состояние
                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси трубопровода (не на полилинии)");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0);   //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите трассы МН со стороны большего пикета");
                        Point3d EndPoint = new Point3d(PX, PY, 0);      //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;

                        #endregion ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ

                        //01 - ЗНАКИ НА ПЕРЕСЕЧЕНИЕ - 1шт.
                        #region ЗНАКИ НА ПЕРЕСЕЧЕНИЕ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 19; //Для SignCrossPipe i=19
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Пересчет исходных координат
                            double LocalPX = StartPoint.X + DeltaCPSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCPSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPY = StartPoint.Y + DeltaCPSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCPSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                          LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                            }
                        }
                        #endregion ЗНАКИ НА ПЕРЕСЕЧЕНИЕ

                        //02 - ЗЕМЛЮ НЕ КОПАТЬ - 2шт.
                       /* #region ЗЕМЛЮ НЕ КОПАТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 18; //Для SignHiPress i=18
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX = StartPoint.X + i * DeltaNonESign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPY = StartPoint.Y + i * DeltaNonESign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗЕМЛЮ НЕ КОПАТЬ*/

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на пересечении с газопроводом
        [CommandMethod("InsertGroupSignCrossEGAZ", CommandFlags.UsePickSet)]
        public static void InsertGroupSignCrossEGAZ()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignCrossEGAZ GSAD = new FormGroupSignCrossEGAZ();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние от оси пересечения до знака
                        double DistCGAZSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaCGAZSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Расстояние между знаками
                        double DistNonESign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaNonESign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //Расстояние между знаками
                        double DistCMNSign = Convert.ToDouble(GSAD.TextBox6.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaCMNSign = Convert.ToDouble(GSAD.TextBox7.Text, cultures);
                        //Расстояние между знаками
                        double DistZGSign = Convert.ToDouble(GSAD.TextBox8.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaZGSign = Convert.ToDouble(GSAD.TextBox9.Text, cultures);
                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку пересечения газопровода с МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0);    //PZ - для приведения в компларное состояние
                        //получение точки направления а/д
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку оси газопровода (не на полилинии)");
                        Point3d MiddlePoint = new Point3d(PX, PY, 0);   //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите трассы МН со стороны большего пикета");
                        Point3d EndPoint = new Point3d(PX, PY, 0);      //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z));
                        //Угол направления трассы
                        double AngleCross = line.Angle;
                        //Создаём ложную линию для определения угла направления начального
                        line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;

                        #endregion ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ

                        //01 - ЗНАКИ НА ПЕРЕСЕЧЕНИЕ - 1шт.
                        #region ЗНАКИ НА ПЕРЕСЕЧЕНИЕ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 19; //Для SignCrossPipe i=19
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Пересчет исходных координат
                            double LocalPX = StartPoint.X + DeltaCGAZSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCGAZSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPY = StartPoint.Y + DeltaCGAZSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistCGAZSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                          LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                              LocalValueAtt01, LocalTag01,
                                                              LocalValueAtt02, LocalTag02,
                                                              LocalValueAtt03, LocalTag03,
                                                              LocalValueAtt04, LocalTag04,
                                                              LocalValueAtt05, LocalTag05,
                                                              LocalAngleBlock);
                            }
                        }
                        #endregion ЗНАКИ НА ПЕРЕСЕЧЕНИЕ

                        //02 - ЗЕМЛЮ НЕ КОПАТЬ - 2шт.
                        #region ЗЕМЛЮ НЕ КОПАТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox2.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 18; //Для SignHiPress i=18
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX = StartPoint.X + i * DeltaNonESign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPY = StartPoint.Y + i * DeltaNonESign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) + DistNonESign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                                double LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗЕМЛЮ НЕ КОПАТЬ

                        //03 - ЗНАКИ НА ПЕРЕСЕЧЕНИЕ СО СТОРОНЫ ГАЗОПРОВОДА - 1шт.
                        # region ЗНАКИ НА ПЕРЕСЕЧЕНИЕ СО СТОРОНЫ ГАЗОПРОВОДА
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox3.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 27; //Для SignWarningGaz i=27
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = BeginPiketString;
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Пересчет исходных координат
                            double LocalPX = StartPoint.X - DeltaCMNSign * Math.Cos(AngleCross) / Math.Sin(AngleCross - AngleMN) - DistCMNSign * Math.Cos(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPY = StartPoint.Y - DeltaCMNSign * Math.Sin(AngleCross) / Math.Sin(AngleCross - AngleMN) - DistCMNSign * Math.Sin(AngleMN) / Math.Sin(AngleCross - AngleMN);
                            double LocalPZ = 0;
                            if (IfExistBlock(LocalNameSign) == false)
                            {
                                CreateBlockSigTriangle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                          LocalAngleBlock, LocalType);
                            }
                            else
                            {
                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                            }
                        }
                        #endregion ЗНАКИ НА ПЕРЕСЕЧЕНИЕ СО СТОРОНЫ ГАЗОПРОВОДА

                        //04 - ЗАКРЕПЛЕНИЕ ТРАССЫ НЕФТЕПРОВОДА НА МЕСТНОСТИ - 2шт.
                        /*#region ЗЕМЛЮ НЕ КОПАТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox4.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 26; //Для SignCrossGaz i=26
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий

                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Пересчет исходных координат
                                double LocalPX = StartPoint.X + i * DistZGSign / Math.Sin(AngleCross - AngleMN) * Math.Cos(AngleMN) + DeltaZGSign / Math.Sin(AngleCross - AngleMN) * Math.Cos(AngleCross);
                                double LocalPY = StartPoint.Y + i * DistZGSign / Math.Sin(AngleCross - AngleMN) * Math.Sin(AngleMN) + DeltaZGSign / Math.Sin(AngleCross - AngleMN) * Math.Sin(AngleCross);
                                double LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗЕМЛЮ НЕ КОПАТЬ*/

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на временных и малых водотоках
        [CommandMethod("InsertGroupSignCrossLW", CommandFlags.UsePickSet)]
        public static void InsertGroupSignCrossLW()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignCrossLW GSAD = new FormGroupSignCrossLW();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние между знаками - не меняемое
                        double DistAnslagSign = Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistMarkerSign = Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaMarkerSign = Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Расстояние между знаками
                        double DistReperSign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaReperSign = Convert.ToDouble(GSAD.TextBox6.Text, cultures);
                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку начала водотока по МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку окончания водотока по МН");
                        Point3d EndPoint = new Point3d(PX, PY, 0);   //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;
                        //Дистанция для расчета пикетов
                        double AngleLen = line.Length;
                        //Координаты вставки блоков
                        double LocalPX = 0, LocalPY = 0, LocalPZ = 0;

                        #endregion ВХОДНЫЕ ДАННЫЕ

                        //01 - ЗНАКИ НА АНШЛАГ - 2шт.
                        #region ЗНАКИ НА АНШЛАГ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 57; //Для SignWP i=57
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                if (i == -1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistAnslagSign);
                                    //Пересчет исходных координат
                                    LocalPX = StartPoint.X + i * DistAnslagSign * Math.Cos(AngleMN);
                                    LocalPY = StartPoint.Y + i * DistAnslagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (i == 1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistAnslagSign + AngleLen);
                                    //Пересчет исходных координат
                                    LocalPX = EndPoint.X + i * DistAnslagSign * Math.Cos(AngleMN);
                                    LocalPY = EndPoint.Y + i * DistAnslagSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                            LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                            LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                            LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                            LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                            LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                            LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion ЗНАКИ НА АНШЛАГ

                        //02 - МАРКЕРА - 2шт.
                        /*#region МАРКЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 6; //Для SignMarker i=6
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                if (i == -1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistMarkerSign);
                                    //Пересчет исходных координат
                                    LocalPX = StartPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = StartPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (i == 1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistMarkerSign + AngleLen);
                                    //Пересчет исходных координат
                                    LocalPX = EndPoint.X + i * DistMarkerSign * Math.Cos(AngleMN) - DeltaMarkerSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = EndPoint.Y + i * DistMarkerSign * Math.Sin(AngleMN) + DeltaMarkerSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                              LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                              LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                              LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                              LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                              LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                              LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                              LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                              LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion МАРКЕР*/

                        //03 - РЕПЕР - 1шт.
                        #region РЕПЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox1.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 7; //Для SignReper i=7
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString));
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            for (int i = -1; i <= 1; i += 2)
                            {
                                //Поиск блока новый/существующий
                                LocalPX = StartPoint.X - DistReperSign * Math.Cos(AngleMN) - i * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                LocalPY = StartPoint.Y - DistReperSign * Math.Sin(AngleMN) + i * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                              LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                              LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                              LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                              LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                              LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                              LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                              LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                              LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion РЕПЕР

                        tr.Commit();
                    }
                }
            }
        }

        //Метод расстановки знаков на подводном переходе
        [CommandMethod("InsertGroupSignCrossPP", CommandFlags.UsePickSet)]
        public static void InsertGroupSignCrossPP()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            SignModelGroupSign S = new SignModelGroupSign();
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormGroupSignCrossPP GSAD = new FormGroupSignCrossPP();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();
            if (GSAD.ButtonWasClicked != false)
            {
                bool checkpoint = true;
                //Расстановка знаков
                if (checkpoint == true)
                {
                    using (Transaction tr = AcadDB.TransactionManager.StartTransaction()) //Старт транзакции
                    {
                        #region ВХОДНЫЕ ДАННЫЕ ЗАПРОСА С ФОРМЫ
                        //Начальный ПК трассы для расстановки знаков
                        string BeginPiketString = GSAD.TextBox1.Text;
                        //Расстояние между знаками - не меняемое
                        double DistStvorSign = 15; //Convert.ToDouble(GSAD.TextBox2.Text, cultures);
                        //Расстояние между знаками - не меняемое
                        double DistJSign = 15; // Convert.ToDouble(GSAD.TextBox3.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaJSign = 100; //Convert.ToDouble(GSAD.TextBox4.Text, cultures);
                        //Расстояние между знаками
                        double DistReperSign = Convert.ToDouble(GSAD.TextBox5.Text, cultures);
                        //Смещение от оси трассы знака
                        double DeltaReperSign = Convert.ToDouble(GSAD.TextBox6.Text, cultures);
                        //получение точки пересечения
                        CurrentPoint(out double PX, out double PY, out double PZ, "Введите точку начала водотока по МН");
                        Point3d StartPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        //получение точки трассе МН
                        CurrentPoint(out PX, out PY, out PZ, "Введите точку конца водотока по МН");
                        Point3d EndPoint = new Point3d(PX, PY, 0); //PZ - для приведения в компларное состояние
                        //Создаём ложную линию для определения угла направления начального
                        Line line = new Line(new Point3d(StartPoint.X, StartPoint.Y, StartPoint.Z), new Point3d(EndPoint.X, EndPoint.Y, EndPoint.Z));
                        //Угол направления трассы
                        double AngleMN = line.Angle;
                        //Дистанция для расчета пикетов
                        double AngleLen = line.Length;
                        //Координаты вставки блоков
                        double LocalPX = 0, LocalPY = 0, LocalPZ = 0;
                        #endregion ВХОДНЫЕ ДАННЫЕ

                        //01 - АНШЛАГ - 2шт.
                        #region АНШЛАГ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.ComboBox1.Text == SignBase.DataPP[0])
                        {
                            //Входные параметры для знака 
                            int k = 56; 
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                if (i == -1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistStvorSign);
                                    //Пересчет исходных координат
                                    LocalPX = StartPoint.X + i * DistStvorSign * Math.Cos(AngleMN);
                                    LocalPY = StartPoint.Y + i * DistStvorSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (i == 1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistStvorSign + AngleLen);
                                    //Пересчет исходных координат
                                    LocalPX = EndPoint.X + i * DistStvorSign * Math.Cos(AngleMN);
                                    LocalPY = EndPoint.Y + i * DistStvorSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                        LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                        LocalValueAtt01, LocalTag01,
                                                        LocalValueAtt02, LocalTag02,
                                                        LocalValueAtt03, LocalTag03,
                                                        LocalValueAtt04, LocalTag04,
                                                        LocalValueAtt05, LocalTag05,
                                                        LocalAngleBlock);
                                }
                            }
                        }
                        #endregion СТВОРНЫЙ ЗНАК

                        //01 - СТВОРНЫЙ ЗНАК - 2шт.
                        #region СТВОРНЫЙ ЗНАК
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.ComboBox1.Text == SignBase.DataPP[1] || GSAD.ComboBox1.Text == SignBase.DataPP[2] || GSAD.ComboBox1.Text == SignBase.DataPP[3])
                        {
                            //Входные параметры для знака 
                            int k = GSAD.IndexsignStvor; //Для SignWSR i=50 или SignWNSR i=51
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            //Поиск блока новый/существующий
                            for (int i = -1; i <= 1; i += 2)
                            {
                                if (i == -1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistStvorSign);
                                    //Пересчет исходных координат
                                    LocalPX = StartPoint.X + i * DistStvorSign * Math.Cos(AngleMN);
                                    LocalPY = StartPoint.Y + i * DistStvorSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (i == 1)
                                {
                                    //Пересчет пикета
                                    LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistStvorSign + AngleLen);
                                    //Пересчет исходных координат
                                    LocalPX = EndPoint.X + i * DistStvorSign * Math.Cos(AngleMN);
                                    LocalPY = EndPoint.Y + i * DistStvorSign * Math.Sin(AngleMN);
                                    LocalPZ = 0;
                                }
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignStvor(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                              LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                              LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                              LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                              LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                              LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                              LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                            LocalValueAtt01, LocalTag01,
                                                            LocalValueAtt02, LocalTag02,
                                                            LocalValueAtt03, LocalTag03,
                                                            LocalValueAtt04, LocalTag04,
                                                            LocalValueAtt05, LocalTag05,
                                                            LocalAngleBlock);
                                }
                            }
                        }
                        #endregion СТВОРНЫЙ ЗНАК

                        //02 - ЯКОРЬ НЕ БРОСАТЬ - 2шт.
                        #region ЯКОРЬ НЕ БРОСАТЬ
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.ComboBox1.Text == SignBase.DataPP[1] || GSAD.ComboBox1.Text == SignBase.DataPP[3])
                        {
                            //Входные параметры для знака 
                            int k = 52; //Для SignJ i=52
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.
                            for (int i = -1; i <= 1; i += 2)
                            {
                                for (int j = -1; j <= 1; j += 2)
                                {
                                    if (i == -1)
                                    {
                                        //Пересчет пикета
                                        LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistJSign);
                                        //Пересчет исходных координат
                                        LocalPX = StartPoint.X + i * DistJSign * Math.Cos(AngleMN) - j * DeltaJSign * Math.Cos(Math.PI / 2 - AngleMN);
                                        LocalPY = StartPoint.Y + i * DistJSign * Math.Sin(AngleMN) + j * DeltaJSign * Math.Sin(Math.PI / 2 - AngleMN);
                                        LocalPZ = 0;
                                    }
                                    if (i == 1)
                                    {
                                        //Пересчет пикета
                                        LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistJSign + AngleLen);
                                        //Пересчет исходных координат
                                        LocalPX = EndPoint.X + i * DistJSign * Math.Cos(AngleMN) - j * DeltaJSign * Math.Cos(Math.PI / 2 - AngleMN);
                                        LocalPY = EndPoint.Y + i * DistJSign * Math.Sin(AngleMN) + j * DeltaJSign * Math.Sin(Math.PI / 2 - AngleMN);
                                        LocalPZ = 0;

                                    }
                                    if (IfExistBlock(LocalNameSign) == false)
                                    {
                                        CreateBlockSignDiff(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                              LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                              LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                              LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                              LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                              LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                              LocalAngleBlock, LocalType);
                                    }
                                    //Поиск блока новый/существующий
                                    else
                                    {
                                        CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                    }
                                }
                            }
                        }
                        #endregion ЯКОРЬ НЕ БРОСАТЬ

                        //03 - РЕПЕР - 1шт.
                        #region РЕПЕР
                        //Ключ - индикатор необходимости расстановки опознавательных знаков
                        if (GSAD.CheckBox3.IsChecked == true)
                        {
                            //Входные параметры для знака 
                            int k = 7; //Для SignReper i=7
                            string LocalNameSign = S.GetParametrSign(k, 0);
                            string LocalShortNameSign = S.GetParametrSign(k, 1);
                            string LocalType = S.GetParametrSign(k, 2);
                            string LocalTag01 = S.GetParametrSign(k, 3);
                            string LocalPrompt01 = S.GetParametrSign(k, 4);
                            //string LocalValueAtt01 = "";
                            string LocalTag02 = S.GetParametrSign(k, 6);
                            string LocalPrompt02 = S.GetParametrSign(k, 7);
                            string LocalValueAtt02 = S.GetParametrSign(k, 8);
                            string LocalTag03 = S.GetParametrSign(k, 9);
                            string LocalPrompt03 = S.GetParametrSign(k, 10);
                            string LocalValueAtt03 = S.GetParametrSign(k, 11);
                            string LocalTag04 = S.GetParametrSign(k, 12);
                            string LocalPrompt04 = S.GetParametrSign(k, 13);
                            string LocalValueAtt04 = S.GetParametrSign(k, 14);
                            string LocalTag05 = S.GetParametrSign(k, 15);
                            string LocalPrompt05 = S.GetParametrSign(k, 16);
                            string LocalValueAtt05 = S.GetParametrSign(k, 17);
                            string LocalTag06 = S.GetParametrSign(k, 18);
                            string LocalPrompt06 = S.GetParametrSign(k, 19);
                            string LocalValueAtt06 = S.GetParametrSign(k, 20);
                            string LocalTag07 = S.GetParametrSign(k, 21);
                            string LocalPrompt07 = S.GetParametrSign(k, 22);
                            string LocalValueAtt07 = S.GetParametrSign(k, 23);
                            double LocalAngleBlock = AngleMN;//Угол вставки блока - для одиночного 0 рад.


                            if (GSAD.CountReper == 1)
                            {
                                int i = -1;
                                
                                //Пересчет пикета
                                string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistReperSign);
                                //Поиск блока новый/существующий
                                LocalPX = StartPoint.X - DistReperSign * Math.Cos(AngleMN) - i * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                LocalPY = StartPoint.Y - DistReperSign * Math.Sin(AngleMN) + i * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                LocalPZ = 0;
                                if (IfExistBlock(LocalNameSign) == false)
                                {
                                    CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                  LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                  LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                  LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                  LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                  LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                  LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                  LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                  LocalAngleBlock, LocalType);
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                }  
                            }

                            if (GSAD.CountReper == 2)
                            {
                                for (int i = -1; i <= 1; i += 2)
                                {
                                    //Пересчет пикета
                                    string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistReperSign);
                                    //Поиск блока новый/существующий
                                    LocalPX = StartPoint.X - DistReperSign * Math.Cos(AngleMN) - i * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                    LocalPY = StartPoint.Y - DistReperSign * Math.Sin(AngleMN) + i * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                    LocalPZ = 0;
                                    if (IfExistBlock(LocalNameSign) == false)
                                    {
                                        CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                  LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                  LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                  LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                  LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                  LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                  LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                  LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                  LocalAngleBlock, LocalType);
                                    }
                                    else
                                    {
                                        CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                LocalValueAtt01, LocalTag01,
                                                                LocalValueAtt02, LocalTag02,
                                                                LocalValueAtt03, LocalTag03,
                                                                LocalValueAtt04, LocalTag04,
                                                                LocalValueAtt05, LocalTag05,
                                                                LocalAngleBlock);
                                    }
                                }
                            }

                            if (GSAD.CountReper == 3)
                            {
                                for (int i = -1; i <= 1; i += 2)
                                {
                                    //Пересчет пикета
                                    if (i == -1)
                                    {
                                        string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistReperSign);
                                        for (int j = -1; j <= 1; j += 2)
                                        {
                                            //Поиск блока новый/существующий
                                            LocalPX = StartPoint.X + i * DistReperSign * Math.Cos(AngleMN) - j * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                            LocalPY = StartPoint.Y + i * DistReperSign * Math.Sin(AngleMN) + j * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                            LocalPZ = 0;
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                          LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                          LocalAngleBlock, LocalType);
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                            }
                                        }
                                    }
                                    if (i == 1)
                                    {
                                        string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistReperSign + AngleLen);
                                        for (int j = -1; j <= -1; j += 2)
                                        {
                                            //Поиск блока новый/существующий
                                            LocalPX = EndPoint.X + i * DistReperSign * Math.Cos(AngleMN) - j * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                            LocalPY = EndPoint.Y + i * DistReperSign * Math.Sin(AngleMN) + j * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                            LocalPZ = 0;
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                          LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                          LocalAngleBlock, LocalType);
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                            }
                                        }
                                    }
                                }
                            }

                            if (GSAD.CountReper == 4)
                            {
                                for (int i = -1; i <= 1; i += 2)
                                {
                                    //Пересчет пикета
                                    if (i == -1)
                                    {
                                        string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistReperSign);
                                        for (int j = -1; j <= 1; j += 2)
                                        {
                                            //Поиск блока новый/существующий
                                            LocalPX = StartPoint.X + i * DistReperSign * Math.Cos(AngleMN) - j * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                            LocalPY = StartPoint.Y + i * DistReperSign * Math.Sin(AngleMN) + j * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                            LocalPZ = 0;
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                          LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                          LocalAngleBlock, LocalType);
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                            }
                                        }
                                    }
                                    if (i == 1)
                                    {
                                        string LocalValueAtt01 = KMtoPK(PiketStringToDouble(BeginPiketString) + i * DistReperSign + AngleLen);
                                        for (int j = -1; j <= 1; j += 2)
                                        {
                                            //Поиск блока новый/существующий
                                            LocalPX = EndPoint.X + i * DistReperSign * Math.Cos(AngleMN) - j * DeltaReperSign * Math.Cos(Math.PI / 2 - AngleMN);
                                            LocalPY = EndPoint.Y + i * DistReperSign * Math.Sin(AngleMN) + j * DeltaReperSign * Math.Sin(Math.PI / 2 - AngleMN);
                                            LocalPZ = 0;
                                            if (IfExistBlock(LocalNameSign) == false)
                                            {
                                                CreateBlockSignCircle(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                                          LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                                          LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                                          LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                                          LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                                          LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                                          LocalValueAtt06, LocalPrompt06, LocalTag06,
                                                                          LocalValueAtt07, LocalPrompt07, LocalTag07,
                                                                          LocalAngleBlock, LocalType);
                                            }
                                            else
                                            {
                                                CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                                        LocalValueAtt01, LocalTag01,
                                                                        LocalValueAtt02, LocalTag02,
                                                                        LocalValueAtt03, LocalTag03,
                                                                        LocalValueAtt04, LocalTag04,
                                                                        LocalValueAtt05, LocalTag05,
                                                                        LocalAngleBlock);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion РЕПЕР

                        tr.Commit();
                    }
                }
            }
        }

        //Командный метод для знака
        [CommandMethod("SignEqup", CommandFlags.UsePickSet)]
        public static void SignEqup()
        {
            //Получение геометрии  точки вставки
            CurrentPoint(out double LocalPX, out double LocalPY, out double LocalPZ);
            //экземпляр объекта класса базы данных
            SignModelOneRack S = new SignModelOneRack();
            //экземпляр формы для получения пикета установки знака
            FormSignEqPK equpForm = new FormSignEqPK();
            equpForm.ShowDialog();
            if (equpForm.ButtonWasClicked != false)
            {
                //экземпляр формы для получения пикета установки знака и данных по знаку
                string LocalPiket = equpForm.TextBox1.Text;
                string LocalTypeSign = equpForm.ComboBox1.Text;
                //Все знаки на оборудование
                string[] LocalSignCount = new string[12];
                LocalSignCount[0] = equpForm.TextBox2.Text;
                LocalSignCount[1] = equpForm.TextBox3.Text;
                LocalSignCount[2] = equpForm.TextBox4.Text;
                LocalSignCount[3] = equpForm.TextBox5.Text;
                LocalSignCount[4] = equpForm.TextBox6.Text;
                LocalSignCount[5] = equpForm.TextBox7.Text;
                LocalSignCount[6] = equpForm.TextBox8.Text;
                LocalSignCount[7] = equpForm.TextBox9.Text;
                LocalSignCount[8] = equpForm.TextBox10.Text;
                LocalSignCount[9] = equpForm.TextBox11.Text;
                LocalSignCount[10] = equpForm.TextBox12.Text;
                LocalSignCount[11] = equpForm.TextBox13.Text;
                for (int i = 36; i < 48; i++)
                {
                    if (LocalSignCount[i - 36].Length != 0 && LocalSignCount[i - 36] != "0")
                    {
                        // получение параметров блока для знака
                        string LocalNameSign = S.GetParametrSign(i, 0);
                        string LocalShortNameSign = S.GetParametrSign(i, 1);
                        string LocalType = S.GetParametrSign(i, 2);
                        string LocalTag01 = S.GetParametrSign(i, 3);
                        string LocalPrompt01 = S.GetParametrSign(i, 4);
                        string LocalValueAtt01 = LocalPiket;
                        string LocalTag02 = S.GetParametrSign(i, 6);
                        string LocalPrompt02 = S.GetParametrSign(i, 7);
                        string LocalValueAtt02 = LocalTypeSign;
                        string LocalTag03 = S.GetParametrSign(i, 9);
                        string LocalPrompt03 = S.GetParametrSign(i, 10);
                        string LocalValueAtt03 = S.GetParametrSign(i, 11);
                        string LocalTag04 = S.GetParametrSign(i, 12);
                        string LocalPrompt04 = S.GetParametrSign(i, 13);
                        string LocalValueAtt04 = S.GetParametrSign(i, 14);
                        string LocalTag05 = S.GetParametrSign(i, 15);
                        string LocalPrompt05 = S.GetParametrSign(i, 16);
                        string LocalValueAtt05 = LocalSignCount[i - 36];
                        double LocalAngleBlock = 0;

                        if (IfExistBlock(LocalNameSign) == false)
                        {
                            CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                LocalAngleBlock, LocalType
                                               );
                        }
                        else
                        {
                            CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                    LocalValueAtt01, LocalTag01,
                                                    LocalValueAtt02, LocalTag02,
                                                    LocalValueAtt03, LocalTag03,
                                                    LocalValueAtt04, LocalTag04,
                                                    LocalValueAtt05, LocalTag05,
                                                    LocalAngleBlock
                                                    );
                        }
                        LocalPX += 5 * S.HeigthTextSign / 3;
                        //LocalPY = LocalPY;
                        //LocalPZ = LocalPZ;
                    }
                }
            }
        }

        #region Работа со знаками
        
        //Командный метод для знака
        [CommandMethod("ChangeSign", CommandFlags.UsePickSet)]
        public static void SignIden()
        {
            //ссылки на чертеж
            AccessToDocument adoc = new AccessToDocument();
            //Получаем ссылку на документ
            //Document ad = adoc.Doc;
            //получаем ссылку на БД
            Database db = adoc.DBase;
            //получаем ссылку на Ed
            //Editor ae = adoc.Ed;


            //экземпляр формы для получения пикета установки знака
            FormChangeSign FormChangeSign = new FormChangeSign();
            FormChangeSign.ShowDialog();
            if (FormChangeSign.ButtonWasClicked != false)
            {
                //Данные с формы
                string oldName = FormChangeSign.ComboBox1.Text;
                string newName = FormChangeSign.ComboBox2.Text;

                //Проверка на имен на пустоту
                if (String.IsNullOrEmpty(oldName))
                {
                    MessageBox.Show("Не задано имя старого блока", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (String.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Не задано имя нового блока", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    // замена блоков
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        // выбор всех вхождений
                        BlockTable blockTable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                        BlockTableRecord LocalBlockRef = blockTable[oldName].GetObject(OpenMode.ForWrite) as BlockTableRecord;
                        ObjectIdCollection C = LocalBlockRef.GetBlockReferenceIds(true, true);
                        if (C.Count > 0)
                        {
                            foreach (ObjectId id in C)
                            {
                                BlockReference LocalBlockRefSign = (BlockReference)tr.GetObject(id, OpenMode.ForWrite) as BlockReference;
                                Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = LocalBlockRefSign.AttributeCollection;

                                //Базовые значения для знака ПК
                                string LocalPk = (AtrCol[0].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;

                                //string LocalBase = (AtrCol[1].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                                //string LocalAccommodation = (AtrCol[2].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                                //string LocalDeepSign = (AtrCol[3].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                                //string LocalCountSign = (AtrCol[4].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                                double LocalPX = LocalBlockRefSign.Position.X;
                                double LocalPY = LocalBlockRefSign.Position.Y;
                                double LocalPZ = LocalBlockRefSign.Position.Z;



                                // экземпляр объекта класса базы данных
                                SignBase S = new SignBase();

                                // поиск индекса нового знака
                                int i = IndexSign(newName);

                                // получение параметров блока для знака
                                string LocalNameSign = S.GetParametrSign(i, 0);
                                string LocalShortNameSign = S.GetParametrSign(i, 1);
                                string LocalType = S.GetParametrSign(i, 2);
                                string LocalTag01 = S.GetParametrSign(i, 3);
                                string LocalPrompt01 = S.GetParametrSign(i, 4);
                                string LocalValueAtt01 = LocalPk;
                                string LocalTag02 = S.GetParametrSign(i, 6);
                                string LocalPrompt02 = S.GetParametrSign(i, 7);
                                string LocalValueAtt02 = S.GetParametrSign(i, 8);
                                string LocalTag03 = S.GetParametrSign(i, 9);
                                string LocalPrompt03 = S.GetParametrSign(i, 10);
                                string LocalValueAtt03 = S.GetParametrSign(i, 11);
                                string LocalTag04 = S.GetParametrSign(i, 12);
                                string LocalPrompt04 = S.GetParametrSign(i, 13);
                                string LocalValueAtt04 = S.GetParametrSign(i, 14);
                                string LocalTag05 = S.GetParametrSign(i, 15);
                                string LocalPrompt05 = S.GetParametrSign(i, 16);
                                string LocalValueAtt05 = S.GetParametrSign(i, 17);
                                double LocalAngleBlock = LocalBlockRefSign.Rotation;

                                //Удаление блока старого
                                LocalBlockRefSign.Erase(true);

                                if (IfExistBlock(newName) == false)
                                {
                                    CreateBlockSignIden(LocalPX, LocalPY, LocalPZ, LocalNameSign, LocalShortNameSign,
                                                        LocalValueAtt01, LocalPrompt01, LocalTag01,
                                                        LocalValueAtt02, LocalPrompt02, LocalTag02,
                                                        LocalValueAtt03, LocalPrompt03, LocalTag03,
                                                        LocalValueAtt04, LocalPrompt04, LocalTag04,
                                                        LocalValueAtt05, LocalPrompt05, LocalTag05,
                                                        LocalAngleBlock, LocalType
                                                       );
                                }
                                else
                                {
                                    CreateBlockSignIdenIfExist(LocalPX, LocalPY, LocalPZ, LocalNameSign,
                                                              LocalValueAtt01, LocalTag01,
                                                              LocalValueAtt02, LocalTag02,
                                                              LocalValueAtt03, LocalTag03,
                                                              LocalValueAtt04, LocalTag04,
                                                              LocalValueAtt05, LocalTag05,
                                                              LocalAngleBlock
                                                              );
                                }
                            }
                        }
                        tr.Commit();
                    }
                }
            }
        }













        //Командный метод отключения атрибутов

        [CommandMethod("OffAttribute")]
        public static void OffAttribute()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            //получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();

            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
            {
                //получаем таблицу блоков и проходим по всем записям таблицы блоков
                BlockTable blockTable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForRead);
                for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
                {
                    if (blockTable.Has(SB.GetParametrSign(i, 0))) //0 - индекс имя в базе
                    {
                        //получения переменной - таблицы блоков чертежа
                        BlockTableRecord LocalBlockRef = blockTable[SB.GetParametrSign(i, 0)].GetObject(OpenMode.ForRead) as BlockTableRecord;
                        //выбираем все объекты
                        ObjectIdCollection C = LocalBlockRef.GetBlockReferenceIds(true, true);
                        foreach (ObjectId id in C)
                        {
                            BlockReference LocalBlockRefSign = (BlockReference)tr.GetObject(id, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = LocalBlockRefSign.AttributeCollection;
                            for (int j = 0; j < AtrCol.Count; j++) (AtrCol[j].GetObject(OpenMode.ForWrite) as AttributeReference).Visible = false;
                        }
                    }
                }
                tr.Commit();
            }
        }
        //Командный метод включения атрибутов

        [CommandMethod("OnAttribute")]
        public static void OnAttribute()
        {
            //экземпляр класса доступа к чертежу
            AccessToDocument AcToDraw = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();

            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
            {
                //получаем таблицу блоков и проходим по всем записям таблицы блоков
                BlockTable blockTable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForRead);
                for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
                {
                    if (blockTable.Has(SB.GetParametrSign(i, 0))) //0 - индекс имя в базе
                    {
                        //получения переменной - таблицы блоков чертежа
                        BlockTableRecord LocalBlockRef = blockTable[SB.GetParametrSign(i, 0)].GetObject(OpenMode.ForRead) as BlockTableRecord;
                        //выбираем все объекты
                        ObjectIdCollection C = LocalBlockRef.GetBlockReferenceIds(true, true);
                        foreach (ObjectId id in C)
                        {
                            BlockReference LocalBlockRefSign = (BlockReference)tr.GetObject(id, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = LocalBlockRefSign.AttributeCollection;
                            for (int j = 0; j < AtrCol.Count; j++) (AtrCol[j].GetObject(OpenMode.ForWrite) as AttributeReference).Visible = true;
                        }
                    }
                }
                tr.Commit();
            }
        }

        //Командный метод переделки все ПК (изменение только пикет)
        [CommandMethod("MovePK")]
        public static void MovePK()
        {
            AccessToDocument AcToDraw = new AccessToDocument();
            //Получаем ссылку на документ
            Document AcadDoc = AcToDraw.Doc;
            //получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            //Пересчет ПК блоков
            FormCharacterOffset characterOffset = new FormCharacterOffset();
            //Открываем форму для исходных данных для расстановки знаков
            characterOffset.ShowDialog();
            if (characterOffset.ButtonWasClicked != false)
            {
                //Дистанция смещения
                double DeltaCharacter = Convert.ToDouble(characterOffset.TextBox1.Text);
                using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
                {
                    PromptSelectionResult acSSPrompt = AcadDoc.Editor.GetSelection();
                    if (acSSPrompt.Status == PromptStatus.OK)
                    {
                        SelectionSet acSSet = acSSPrompt.Value;
                        foreach (SelectedObject acSSObj in acSSet)
                        {
                            if (acSSObj != null)
                            {
                                Entity acEnt = tr.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;
                                if (acEnt.GetType() == typeof(BlockReference))
                                {
                                    BlockReference LocalBlockRefSign = (BlockReference)tr.GetObject(acSSObj.ObjectId, OpenMode.ForRead) as BlockReference;

                                    for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
                                    {
                                        if (LocalBlockRefSign.Name == SB.GetParametrSign(i, 0))
                                        {
                                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = ((BlockReference)acEnt).AttributeCollection;
                                            (AtrCol[0].GetObject(OpenMode.ForWrite) as AttributeReference).TextString = SignBase.KMtoPK(SignBase.PKtoKM((AtrCol[0].GetObject(OpenMode.ForWrite) as AttributeReference).TextString) * 1000 + DeltaCharacter);
                                        }
                                    }
                                }
                            }
                        }
                        tr.Commit();
                    }
                }
            }
        }

        #endregion Работа со знаками






















    }

    //Класс реализации расстановки знаков на одной стойке (в т.ч. их конструкции)
    public class Pos : SignBase
    {
        public Pos()
        {

        }

        //Командный метод для знака
        [CommandMethod("PosSign", CommandFlags.UsePickSet)]
        public static void PosSing()
        {
            // Получаем ссылку на документ
            AccessToDocument AcadDoc = new AccessToDocument();
            // получаем ссылку на БД
            Database AcadDB = AcadDoc.DBase;
            // Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            // Экземпляр объекта класса SignBase
            int index = 0;
            // начинаем транзакцию
            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
            {
                //получаем таблицу блоков и проходим по всем записям таблицы блоков
                BlockTable blockTable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForWrite);
                for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
                {
                    if (i!=67)
                    {
                        if (blockTable.Has(SB.GetParametrSign(i, 0))) //0 - индекс имя в базе
                        {
                            //получения переменной  - таблицы блоков чертежа
                            BlockTableRecord LocalBlockRef = blockTable[SB.GetParametrSign(i, 0)].GetObject(OpenMode.ForWrite) as BlockTableRecord;
                            //выбираем все объекты
                            ObjectIdCollection C = LocalBlockRef.GetBlockReferenceIds(true, true);
                            foreach (ObjectId id in C)
                            {
                                index++;
                                BlockReference LocalBlockRefSign = (BlockReference)tr.GetObject(id, OpenMode.ForWrite);
                                Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = LocalBlockRefSign.AttributeCollection;
                                //Базовые значения для знака ПК
                                string typeSign = (AtrCol[2].GetObject(OpenMode.ForRead) as AttributeReference).TextString;
                                if (typeSign == OneRack)
                                {
                                    if (IfExistBlock(posNameSign + OneRack))
                                    {
                                        CreateBlockPodSignIdenIfExist(LocalBlockRefSign, posNameSign + OneRack, index);
                                    }
                                    else
                                    {
                                        CreateBlockPodSignIdenOneRack(LocalBlockRefSign, index);
                                    }
                                }
                                if (typeSign == TwoRack)
                                {
                                    if (IfExistBlock(posNameSign + TwoRack))
                                    {
                                        CreateBlockPodSignIdenIfExist(LocalBlockRefSign, posNameSign + TwoRack, index);
                                    }
                                    else
                                    {
                                        CreateBlockPodSignIdenTwoRack(LocalBlockRefSign, index);
                                    }
                                }
                            }
                        }
                    }
                }
                tr.Commit();
            }
        }
    }
}