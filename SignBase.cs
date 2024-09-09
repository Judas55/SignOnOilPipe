using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using AcadMain;
using Color = Autodesk.AutoCAD.Colors.Color;
using System.Windows.Media.Animation;

namespace GlobalSign
{
    //Класса - БД по исходным данным по знакам
    public class SignBase
    {
        #region Константы видов знаков
        //Константа обыкновенного знака
        public const string ConstSignSimple = "SignSimple";
        //Константа аншлага
        public const string ConstSignAnshlag = "SignAnshlag";
        //Константа треугольного знака
        public const string ConstSignTriangle = "SignTriangle";
        //Константа знака крест на крест
        public const string ConstSignHiPress = "SignHiPress";
        //Константа знака на ЖД-НЕФТЬ
        public const string ConstSignWarningRW = "SignWarningRW";
        //Константа маркера  
        public const string ConstSignMarker = "SignMarker";
        //Константа репера
        public const string ConstSignReper = "SignReper";
        //Константа столбика замерного
        public const string ConstSignSM = "SignSM";
        //Константа несудоходного знака створного
        public const string ConstSignWNSR = "SignWNSR";
        //Константа судоходного знака створного
        public const string ConstSignWSR = "SignWSR";
        //Константа километрового знак с маркера
        public const string ConstSignКМM = "SignКМM";
        //Константа километрового знак без маркера
        public const string ConstSignКM = "SignКМ";
        //Константа знака якорь не бросать
        public const string ConstSignJ = "SignJ";
        //Константа знака стоп
        public const string ConstSignStop = "SignStop";
        //Константа знака оборудования
        public const string ConstSignEqup = "SignEqup";
        //Константа деформационная марка
        public const string ConstВeformationMark = "ConstВeformationMark";

        #endregion

        #region Константы материалов
        //Константа обыкновенного знака
        public const string ConstSignPolyMer = "пластик на основе полимерного композитного материала";
        //Константа треугольного знака
        public const string ConstGlassPlastic = "стеклопластик";
        //Константа треугольного знака
        public const string ConstMetal = "метал";
        //Константа треугольного знака
        public const string ConstBeton = "бетон";
        //Константа треугольного знака
        public const string ConstOther = "прочие";
        #endregion

        #region Константы типов стоек

        //Константа обыкновенного знака
        public const string OneRack = "на 1 стойке";
        //Константа аншлага
        public const string TwoRack = "на 2 стойках";
        //Константа на ограждении 
        public const string OnFencing = "на ограждении";
        //Константа на столбе 
        public const string OnPole = "на столбе";
        //Константа на столбе уже поставленного знака
        public const string OneRackExist = "на столбе знака";
        //Константа на столбе уже поставленного знака
        public const string OneRackPipe = "на трубопроводе";

        static public List<string> Rack { get; } = new List<string>()
        {
            OneRack,
            TwoRack,
            OnFencing,
            OnPole,
            OneRackExist,
            OneRackPipe
        };










        #endregion

        #region Константы делители

        //Константа делителя +
        public const Char DelimitelPlus = '+';
        //Константа делителя +
        public const Char DelimitelMinus = '-';
        //Константа делителя ПК
        public const string PK = "ПК";
        #endregion

        #region Константы поставщиков
        //Поставка подрядчика
        public const string Contractor = "Подрядчик";
        //Поставка Заказчика
        public const string Customer = "Заказчик";
        #endregion

        #region Константы ПОС
        //величина стороны квадрата для одностоечного знака
        public const double posLengthOne = 1;
        //public const string posNameSign = "ПОС_ПО_под_одностоечный_знак";
        public const string posNameSign = "ППО Знаки ЛО ";
        //Номер точки для блока;
        public const string number01 = "Номер точки 1";
        //Номер точки для блока;
        public const string number02 = "Номер точки 2";
        //Номер точки для блока;
        public const string number03 = "Номер точки 3";
        //Номер точки для блока;
        public const string number04 = "Номер точки 4";
        //Номер точки для блока;
        public const string numberBlock = "Номер блока";

        #endregion

        #region Константы климатического исполнения

        private const string climate01 = "-У1";
        private const string climate02 = "-УХЛ1";
        static public List<string> Сlimate { get; } = new List<string>()
        {
            climate01,
            climate02
        };

        #endregion

        #region Поля управления данными с формы 

        //Шаг расстановки знаков
        private const string BasisForEquipment01 = "УЗА" ;
        private const string BasisForEquipment02 = "Вантуз";
        private const string BasisForEquipment03 = "Колодец КИП";
        private const string BasisForEquipment04 = "Колодец КР";

        static public List<string> BasisForEquipment { get; } = new List<string>()
        {
            BasisForEquipment01,
            BasisForEquipment02,
            BasisForEquipment03,
            BasisForEquipment04
        };

        //Шаг расстановки знаков
        private const string DataPP01 = "до 70 м несудоходная";
        private const string DataPP02 = "до 70 м судоходная";
        private const string DataPP03 = "более 70 м несудоходная";
        private const string DataPP04 = "более 70 м судоходная";

        static public List<string> DataPP { get; } = new List<string>()
        {
            DataPP01,
            DataPP02,
            DataPP03,
            DataPP04
        };

        /*
        //Шаг расстановки знаков
        private const string DataRiver01 = "для несудоходных рек";
        private const string DataRiver02 = "для судоходных рек";

        static public List<string> DataRiver { get; } = new List<string>()
        {
            DataRiver01,
            DataRiver02
        };
        */

        #endregion Поля управления данными с формы 

        #region Поля определяющие геометрию блоков

        //Поле - параметр высоты текста
        private double localheigth;
        //Поле - параметр высоты текста атрибута
        private double textattribute;
        //Поле - параметр смещения текста атрибута
        private readonly double textAttributeDelta;
        //Поле - коэффициент сжатия текста сокращенного знака
        private double koefftextshortnamesign;
        #endregion

        #region Поля управления данными формы GroupSignTrassaData

        //Шаг расстановки знаков
        private readonly string[] step;
        //Шаг смещения знаков относительно трассы
        private readonly string[] delta;
        //Катет упругих изгибов
        private readonly string[] katet;
        //Схемы расстановки знаков
        private readonly string[] stepKM;
        //Количество знаков на упругих 1-3
        private readonly string[] countsign;

        #endregion

        //Поле (ссылка) на данные массива исходных знаков
        private readonly string[,] parametrs_sign;

        //Поле (ссылка) на данные массива исходных знаков
        private int countsignbaseRow;

        //Поле (ссылка) на данные массива исходных знаков
        private int countsignbaseColunm;

        //Конструктор
        public SignBase()
        {

            //Шаг расстановки знаков
            step = new string[5] { "500", "1000", "2000", "2500", "5000" };

            //Шаг смещения знаков относительно трассы
            delta = new string[7] { "2", "4", "6", "8", "10", "25", "50" };

            //Катет упругих изгибов
            katet = new string[7] { "5", "10", "15", "20", "25", "30", "35" };

            //Катет упругих изгибов
            countsign = new string[2] { "1", "10" };

            //Схемы расстановки знаковGirls Do Porn Episode 502
            stepKM = new string[5] { "1000", "2000", "3000", "4000", "5000" };

            localheigth = 3.0;
            textAttributeDelta = 3 * localheigth / 6;
            textattribute = 0.8 * textAttributeDelta;
            koefftextshortnamesign = 0.6;

            //ФОРМИРОВАНИЕ ОБЩЕГО МАССИВА ВСЕХ ЗНАКОВ
            parametrs_sign = new string[87, 36];

            //Длина массива базы данных знаков
            countsignbaseRow = parametrs_sign.GetLength(0);
            countsignbaseColunm = parametrs_sign.GetLength(1);

            #region Все знаки
            //ОПОЗНАВАТЕЛЬНЫЕ ЗНАКИ

            #region 00 - Деформационная марка
            int j = 0;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Деформационная марка";                      //Имя знака 
            parametrs_sign[j, 1] = "ДМ";                                       //Сокращение на щит на модель в автокаде
            parametrs_sign[j, 2] = ConstВeformationMark;                       //Имя команды - вызов из командной строки
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Деформационная марка";                     //Тег(Tag) 01 - Имя знака для таблицы ВПЗ
            parametrs_sign[j, 4] = "ПК";                                       //Промт(Prompt) 01 к тегу 01 - пикетаж установки знака
            parametrs_sign[j, 5] = "ХХХХ+ХХ";                                  //Значение(ValueValue) 01 к тегу 01 - значение пикетажа
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";                       //Тег(Tag) 02 - Имя знака для таблицы ВПЗ
            parametrs_sign[j, 7] = "Основание";                                //Промт(Prompt) 02 к тегу 02 - основание
            parametrs_sign[j, 8] = "Контроль ПВП";                             //Значение(ValueValue) 01 к тегу 01 - значение пикетажа
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";                          //Тег(Tag) 03 - Тип знака стока или щит
            parametrs_sign[j, 10] = "";                                        //Промт(Prompt) 03 к тегу 03 - основание
            parametrs_sign[j, 11] = OneRackPipe;                                   //Значение(ValueValue) 03 к тегу 03 - тип знак
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";                       //Тег(Tag) 04 - Тип знака стока или щит
            parametrs_sign[j, 13] = "м";                                       //Промт(Prompt) 04 к тегу 04 - основание
            parametrs_sign[j, 14] = "по трубе";                         //Значение(ValueValue) 04 к тегу 04 - тип знак
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";                               //Тег(Tag) 05 - Тип знака стока или щит
            parametrs_sign[j, 16] = "шт.";                                     //Промт(Prompt) 05 к тегу 05 - основание
            parametrs_sign[j, 17] = "1";                                       //Значение(ValueValue) 05 к тегу 05 - тип знак
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 19] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 20] = "";                                //Имя команды - вызов из командной строки
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 22] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 23] = "";                                //Имя команды - вызов из командной строки
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Устройство определения планово-высотного положения МН DN ХХХХ УОВ ХХХХ ";            //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "ХХХХ";                            //Код АСУНСИ
            parametrs_sign[j, 26] = "ХХХХ";                            //Код группы оборудования
            parametrs_sign[j, 27] = Contractor;                        //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "50";                              //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = "00.00.000-01 ТУ 1469-07104690510-2012";                        //Материал
            parametrs_sign[j, 31] = "";                                //Нормативный документ
            parametrs_sign[j, 32] = "-";                                //Резерв
            parametrs_sign[j, 33] = "-";                                //Резерв
            parametrs_sign[j, 34] = "-";                                //Резерв
            parametrs_sign[j, 35] = "-";                                //Резерв
            #endregion 0 - Деформационная марка

            #region 01 Знак опознавательный. Охранная зона МТ
            j = 1;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак опознавательный. Охранная зона МТ";    //Имя знака
            parametrs_sign[j, 1] = "ОЗ";                                       //Сокращение на щит на модель в автокаде
            parametrs_sign[j, 2] = ConstSignSimple;// "SignIden";              //Имя команды - вызов из командной строки
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак опознавательный. Охранная зона МТ";   //Тег(Tag) 01 - Имя знака для таблицы ВПЗ
            parametrs_sign[j, 4] = "ПК";                                       //Промт(Prompt) 01 к тегу 01 - пикетаж установки знака
            parametrs_sign[j, 5] = "ХХХХ+ХХ";                                  //Значение(ValueValue) 01 к тегу 01 - значение пикетажа
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";                       //Тег(Tag) 02 - Имя знака для таблицы ВПЗ
            parametrs_sign[j, 7] = "Основание";                                //Промт(Prompt) 02 к тегу 02 - основание
            parametrs_sign[j, 8] = "Охранная зона МТ";                         //Значение(ValueValue) 01 к тегу 01 - значение пикетажа
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";                          //Тег(Tag) 03 - Тип знака стока или щит
            parametrs_sign[j, 10] = "";                                        //Промт(Prompt) 03 к тегу 03 - основание
            parametrs_sign[j, 11] = OneRack;                                   //Значение(ValueValue) 03 к тегу 03 - тип знак
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";                        //Тег(Tag) 04 - Тип знака стока или щит
            parametrs_sign[j, 13] = "м";                                       //Промт(Prompt) 04 к тегу 04 - основание
            parametrs_sign[j, 14] = "0.7";                                     //Значение(ValueValue) 04 к тегу 04 - тип знак
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";                               //Тег(Tag) 05 - Тип знака стока или щит
            parametrs_sign[j, 16] = "шт.";                                     //Промт(Prompt) 05 к тегу 05 - основание
            parametrs_sign[j, 17] = "1";                                       //Значение(ValueValue) 05 к тегу 05 - тип знак
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";                                  //Имя команды - вызов из командной строки
            parametrs_sign[j, 19] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 20] = "";                                //Имя команды - вызов из командной строки
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 22] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 23] = "";                                //Имя команды - вызов из командной строки
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                          //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";       //Нормативный документ
            parametrs_sign[j, 32] = "ОО";                                
            parametrs_sign[j, 33] = climate01;                                
            parametrs_sign[j, 34] = climate02;                                
            parametrs_sign[j, 35] = "5346491";                               
            #endregion

            #region 02 Знак опознавательный. Охранная зона МТ с указателем поворота
            j = 2;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак опознавательный. Охранная зона МТ с указателем поворота";
            parametrs_sign[j, 1] = "ОЗУП";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак опознавательный. Охранная зона МТ с указателем поворота";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Указатель поворота";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ООУП-ТР-500х300;600х150";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329690";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "13.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ООУП";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346493";
            #endregion

            #region 03 - РЕЗЕРВ

            #endregion

            #region 04 - РЕЗЕРВ

            #endregion

            #region 05 - РЕЗЕРВ

            #endregion

            //ЗАКРЕПИТЕЛЬНЫЕ ЗНАКИ

            #region 06 Знак. Маркерный пункт
            j = 6;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Маркерный пункт";
            parametrs_sign[j, 1] = "МП";
            parametrs_sign[j, 2] = ConstSignMarker;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Маркерный пункт";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пункт контроля";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 22] = "";                                //Имя команды - вызов из командной строки
            parametrs_sign[j, 23] = "";                                //Имя команды - вызов из командной строки
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329688";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 07 Репер со знаком
            j = 7;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Репер со знаком";
            parametrs_sign[j, 1] = "РЕП";
            parametrs_sign[j, 2] = ConstSignReper;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Репер со знаком";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Закрепление местности";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки знака";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "Тип репера по ОТТ";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "Тип 1";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "Глубина установки репера";
            parametrs_sign[j, 22] = "м";
            parametrs_sign[j, 23] = "0.7";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329688";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 08 - РЕЗЕРВ

            #endregion

            #region 09 - РЕЗЕРВ

            #endregion

            #region 10 - РЕЗЕРВ

            #endregion

            //КИЛОМЕТРОВЫЕ ЗНАКИ

            #region 11 Знак километровый
            j = 11;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак километровый";
            parametrs_sign[j, 1] = "КМ";
            parametrs_sign[j, 2] = ConstSignКM;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак километровый";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Километр";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "Километр установки";
            parametrs_sign[j, 19] = "КМ";
            parametrs_sign[j, 20] = "ХХ";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-КМ-ТР-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329692";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "13.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "КМ";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346498";
            #endregion

            #region 12 Знак километровый с маркером
            j = 12;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак километровый с маркером";
            parametrs_sign[j, 1] = "КММ";
            parametrs_sign[j, 2] = ConstSignКМM;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак километровый с маркером";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Километр с маркером";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4                                                       
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "Километр установки";
            parametrs_sign[j, 19] = "КМ";
            parametrs_sign[j, 20] = "ХХ";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-КММ-ТР-500х300;500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329693";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "13.9";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "КММ";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346499";
            #endregion

            #region 13 - РЕЗЕРВ

            #endregion

            #region 14 - РЕЗЕРВ

            #endregion

            #region 15 - РЕЗЕРВ

            #endregion

            //ЗНАКИ НА ПЕРЕСЕЧЕНИЯХ

            #region 16 Знак. Указатель пересечения кабеля связи
            j = 16;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Указатель пересечения кабеля связи";
            parametrs_sign[j, 1] = "ПКС";
            parametrs_sign[j, 2] = ConstSignTriangle;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Указатель пересечения кабеля связи";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение кабеля связи";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОПК-ТР-400х400х400";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5322776";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОПК";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5341072";
            #endregion

            #region 17 Знак. Указатель пересечения электрического кабеля
            j = 17;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Указатель пересечения электрического кабеля";
            parametrs_sign[j, 1] = "ПКЭ";
            parametrs_sign[j, 2] = ConstSignTriangle;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Указатель пересечения электрического кабеля";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение электрического кабеля";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОПК-ТР-400х400х400";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5322776";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОПК";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5341072";
            #endregion

            #region 18 Знак. Огнеопасно! Высокое давление! Землю не копать!
            j = 18;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Высокое давление! Землю не копать!";
            parametrs_sign[j, 1] = "ЗНК";
            parametrs_sign[j, 2] = ConstSignHiPress;//"SignHiPress";
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Высокое давление! Землю не копать!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение коммуникации";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329688";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 19 Знак. Указатель пересечения трубопровода
            j = 19;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Указатель пересечения трубопровода";
            parametrs_sign[j, 1] = "ПТ";
            parametrs_sign[j, 2] = ConstSignTriangle;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Указатель пересечения трубопровода";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение трубопровода";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОПК-ТР-400х400х400";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5322776";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОПК";
            parametrs_sign[j, 33] = "-У1";
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5341072";
            #endregion

            #region 20 Щит-указатель Копать запрещается. Охранная зона кабеля
            j = 20;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель Копать запрещается. Охранная зона кабеля";
            parametrs_sign[j, 1] = "ЩОЗК";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель Копать запрещается. Охранная зона кабеля";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение кабеля связи";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRackExist;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-КЛС2-400х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329698";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "КЛС2";
            parametrs_sign[j, 33] = "-У1";
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346505";
            #endregion

            #region 21 - Знак. Охранная зона кабеля связи
            j = 21;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Охранная зона кабеля связи";
            parametrs_sign[j, 1] = "ОЗКС";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Охранная зона кабеля связи";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Кабель связи";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-КЛС-ТР-400х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5344908";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.5";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "КЛС";
            parametrs_sign[j, 33] = "-У1";
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5330064";
            #endregion

            #region 22 - Аншлаг. Копать запрещается. Охранная зона кабеля
            j = 22;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Аншлаг. Копать запрещается. Охранная зона кабеля";
            parametrs_sign[j, 1] = "АОЗК";
            parametrs_sign[j, 2] = ConstSignAnshlag;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Аншлаг. Копать запрещается. Охранная зона кабеля";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Кабель связи";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = TwoRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                              
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак П-образный Копать запрещается. Охранная зона кабеля 400х300мм";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "27093870";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "46";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "27093870";
            #endregion

            #region 23 - РЕЗЕРВ

            #endregion

            #region 24 - РЕЗЕРВ

            #endregion

            #region 25 Столбик замерный
            j = 25;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Столбик замерный";
            parametrs_sign[j, 1] = "СЗ";
            parametrs_sign[j, 2] = ConstSignSM;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Столбик замерный";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Кабель связи";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Столбик бетонный замерный";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "4705696";
            parametrs_sign[j, 27] = Contractor;
            parametrs_sign[j, 28] = "40";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstBeton;
            parametrs_sign[j, 31] = "";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            //ЗНАКИ НА ПЕРЕСЕЧЕНИЯХ C ГАЗОПРОВОДОМ

            #region 26 Знак. Закрепление трассы газопровода на местности
            j = 26;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Закрепление трассы газопровода на местности";
            parametrs_sign[j, 1] = "ЗГНМ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Закрепление трассы газопровода на местности";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение газопровода";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Охранная зона газопровода 600х450мм";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "2382456";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.9";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "2382456";
            #endregion

            #region 27 Знак. Осторожно газопровод!
            j = 27;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Осторожно газопровод!";
            parametrs_sign[j, 1] = "ОГ";
            parametrs_sign[j, 2] = ConstSignTriangle;// "SignWarningGaz";
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Осторожно газопровод!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение газопровода";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                            
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Осторожно! Газопровод! 710х710х710мм";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "2379964";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "11.9";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "2379964";
            #endregion

            #region 28 - РЕЗЕРВ

            #endregion

            #region 29 - РЕЗЕРВ

            #endregion

            #region 30 - РЕЗЕРВ

            #endregion

            //ЗНАКИ НА ПЕРЕСЕЧЕНИЯХ С ЖД

            #region 31 Знак сигнальный. Нефть
            j = 31;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак сигнальный. Нефть!";
            parametrs_sign[j, 1] = "Н";
            parametrs_sign[j, 2] = ConstSignWarningRW;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак сигнальный. Нефть!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение железной дороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnPole;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Нефть! 500х500мм металлический";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "4684189";
            parametrs_sign[j, 27] = Contractor;
            parametrs_sign[j, 28] = "11.9";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstMetal;
            parametrs_sign[j, 31] = "ГОСТ 8442-65";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 32 Аншлаг. Внимание нефтепровод! Движение техники запрещено!
            j = 32;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Аншлаг. Внимание нефтепровод! Движение техники запрещено! (ЖД)";
            parametrs_sign[j, 1] = "АДТЗ";
            parametrs_sign[j, 2] = ConstSignAnshlag;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Аншлаг. Внимание нефтепровод! Движение техники запрещено! (ЖД)";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение железной дороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = TwoRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                              
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-П-КВ-500х300-У1";
            parametrs_sign[j, 25] = "п23.01.026";
            parametrs_sign[j, 26] = "5330114";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "46";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "П";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5333289";
            #endregion

            #region 33 - РЕЗЕРВ

            #endregion

            #region 34 - РЕЗЕРВ

            #endregion

            #region 35 - РЕЗЕРВ

            #endregion

            //ЗНАКИ НА ОБОРУДОВАНИЕ

            #region 36 Щит-указатель. Охранная зона
            j = 36;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Охранная зона";
            parametrs_sign[j, 1] = "ЩОЗ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Охранная зона";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Оборудование";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Охранная зона МТ 500х300мм";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "1805952";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "2938985";
            #endregion

            #region 37 Щит-указатель. Огнеопасно! Высокое давление! Землю не копать!
            j = 37;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Огнеопасно! Высокое давление! Землю не копать!";
            parametrs_sign[j, 1] = "ЩЗН";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Огнеопасно! Высокое давление! Землю не копать!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Оборудование";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-ОВД-500х300";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "5329704";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОВД";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5332914";
            #endregion

            #region 38 Щит-указатель. Задвижка
            j = 38;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Задвижка";
            parametrs_sign[j, 1] = "ЩЗ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Задвижка";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Задвижка";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-З-500х300";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "5330392";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "З";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5332910";
            #endregion

            #region 39 Щит-указатель. Вантуз
            j = 39;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Вантуз";
            parametrs_sign[j, 1] = "ЩВ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Вантуз";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Вантуз";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-В-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5330401";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "В";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5332912";
            #endregion

            #region 40 Щит-указатель. Отбор давления
            j = 40;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Отбор давления";
            parametrs_sign[j, 1] = "ЩОД";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Отбор давления";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Отбор давления";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-ОД-500х300";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "5330421";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "ОД";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5332945";
            #endregion

            #region 41 Щит-указатель. Сигнализатор
            j = 41;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Сигнализатор";
            parametrs_sign[j, 1] = "ЩС";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Сигнализатор";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Сигнализатор";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-С-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5360993";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "С";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5360993";
            #endregion

            #region 42 Щит-указатель. Колодец КИПиА
            j = 42;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Колодец КИПиА";
            parametrs_sign[j, 1] = "ЩКК";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Колодец КИПиА";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Колодец КИП";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Колодец КИПиА МТ 500х300мм";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "1805945";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "1805945";
            #endregion

            #region 43 Щит-указатель. Расходомер
            j = 43;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Расходомер";
            parametrs_sign[j, 1] = "ЩР";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Расходомер";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Расходомер";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Расходомер МТ 500х300мм";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "1811681";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "1811681";
            #endregion

            #region 44 Щит-указатель. Категория наружной установки
            j = 44;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Категория наружной установки";
            parametrs_sign[j, 1] = "ЩКНУ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Категория наружной установки";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Оборудование";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель-ПЛ-КНУ-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5329706";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "КНУ";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5332917";
            #endregion

            #region 45 Знак пластиковый Пожароопасно. ЛВЖ
            j = 45;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак пластиковый Пожароопасно. ЛВЖ";
            parametrs_sign[j, 1] = "ЩЛВЖ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Знак пластиковый Пожароопасно. ЛВЖ";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Оборудование";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Пожароопасно! Легковоспламеняющиеся вещества 300х300х300мм";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "2363021";
            parametrs_sign[j, 27] = Contractor;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ГОСТ Р 12.4.026-2015";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 46 Щит-указатель. Запретная зона. Проход запрещен.
            j = 46;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Запретная зона. Проход запрещен.";
            parametrs_sign[j, 1] = "ЩПЗ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Запретная зона. Проход запрещен.";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Оборудование";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Запретная зона. Проход запрещен 500х250мм";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "1812543";
            parametrs_sign[j, 27] = Contractor;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstGlassPlastic;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "1812543";
            #endregion

            #region 47 Щит-указатель. Внимание! Охраняемая территория!
            j = 47;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Внимание! Охраняемая территория!";
            parametrs_sign[j, 1] = "ЩОТ";
            parametrs_sign[j, 2] = ConstSignEqup;
            //Данные по атрибуту №3
            parametrs_sign[j, 3] = "Щит-указатель. Внимание! Охраняемая территория!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №3
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Оборудование";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OnFencing;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "-";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Внимание! Охраняемая территория 500х250мм";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "1814182";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "0.4";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОЛ";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "1814182";
            #endregion

            #region 48 - РЕЗЕРВ

            #endregion

            #region 49 - РЕЗЕРВ

            #endregion

            //ЗНАКИ НА РЕКАХ И ВОДНЫХ ПРЕГРАДАХ

            #region 50 Знак створный для судоходных рек
            j = 50;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак створный для судоходных рек";
            parametrs_sign[j, 1] = "ЗССР";
            parametrs_sign[j, 2] = ConstSignWSR;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак створный для судоходных рек";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение водной преграды";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                               
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-СТС-ТР-500х300;1200х800";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5378445";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "250";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "СТС";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5703803";
            #endregion

            #region 51 Знак створный для не судоходных рек
            j = 51;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак створный для не судоходных рек";
            parametrs_sign[j, 1] = "ЗСНР";
            parametrs_sign[j, 2] = ConstSignWNSR;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак створный для не судоходных рек";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение водной преграды";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-СТН-ТР-500х300;600х600";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5378760";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "247";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "СТН";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5670514";
            #endregion

            #region 52 Знак. Якорь не бросать
            j = 52;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Якорь не бросать";
            parametrs_sign[j, 1] = "ЯК";
            parametrs_sign[j, 2] = ConstSignJ;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Якорь не бросать";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение водной преграды";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак. Якорь не бросать 2000х2000 стальной";
            parametrs_sign[j, 25] = "п.23.14";
            parametrs_sign[j, 26] = "4721340";
            parametrs_sign[j, 27] = Contractor;
            parametrs_sign[j, 28] = "50";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstMetal;
            parametrs_sign[j, 31] = "ГОСТ 26600-98";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 53 - Знак. Водомерый пост N
            j = 53;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Водомерый пост N";
            parametrs_sign[j, 1] = "ВП";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Водомерый пост ";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Водомерный пост";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Водомерный пост N 500х400";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5652990";
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5652990";                            //Резерв
            #endregion

            #region 54 - РЕЗЕРВ
            j = 54;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Рубеж ЛРН";
            parametrs_sign[j, 1] = "РЛРН";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Рубеж ЛРН";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Рубеж ЛРН";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Рубеж ЛРН N 300х500";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "2791289";
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "-";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "2791289";
            //Резерв
            #endregion

            #region 55 - РЕЗЕРВ

            #endregion

            #region 56 Аншлаг. Подводный переход
            j = 56;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Аншлаг. Подводный переход";
            parametrs_sign[j, 1] = "АПП";
            parametrs_sign[j, 2] = ConstSignAnshlag;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Аншлаг. Подводный переход";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение водной преграды";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = TwoRack;
            ///Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-П-КВ-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5330114";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "46";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "П";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5333289";
            #endregion

            #region 57 Аншлаг. Охранная зона МТ
            j = 57;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Аншлаг. Охранная зона";
            parametrs_sign[j, 1] = "АОЗ";
            parametrs_sign[j, 2] = ConstSignAnshlag;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Аншлаг. Охранная зона МТ";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение водной преграды";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = TwoRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                               
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-П-КВ-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5330114";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "46";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "П";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5333289";
            #endregion

            #region 58 - РЕЗЕРВ

            #endregion

            #region 59 - РЕЗЕРВ

            #endregion

            #region 60 - РЕЗЕРВ

            #endregion

            //ЗНАК НА ВОЗДУШНЫХ ПЕРЕХОДАХ

            #region 61 Знак. Проход и проезд запрещен! Воздушный переход!
            j = 61;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Щит-указатель. Проход и проезд запрещен! Воздушный переход!";
            parametrs_sign[j, 1] = "ЩВП";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Щит-указатель. Проход и проезд запрещен! Воздушный переход!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Воздушный переход";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Щит-указатель Проход и проезд запрещен! Воздушный переход! 500х300мм";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "2382491";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "";                                //Резерв
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 62 - РЕЗЕРВ

            #endregion

            #region 63 - РЕЗЕРВ

            #endregion

            #region 64 - РЕЗЕРВ

            #endregion

            #region 65 - РЕЗЕРВ

            #endregion

            //ЗНАКИ НА ПЕРЕСЕЧЕНИЯ С АВТОДОРОГАМИ

            #region 66 Аншлаг. Внимание нефтепровод! Движение техники запрещено
            j = 66;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Аншлаг. Внимание нефтепровод! Движение техники запрещено!";
            parametrs_sign[j, 1] = "АДТЗ";
            parametrs_sign[j, 2] = ConstSignAnshlag;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Аншлаг. Внимание нефтепровод! Движение техники запрещено!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение автодороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = TwoRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-П-КВ-500х300";
            parametrs_sign[j, 25] = "п23.01.026";
            parametrs_sign[j, 26] = "5330114";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "46";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "П";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5333289";
            #endregion

            #region 67 Знак. Остановка запрещена!
            j = 67;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Остановка запрещена!";
            parametrs_sign[j, 1] = "ЗОЗ";
            parametrs_sign[j, 2] = ConstSignStop;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Остановка запрещена!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение автодороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.8";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак дорожный 3.27 Остановка запрещена";
            parametrs_sign[j, 25] = "п23.14";
            parametrs_sign[j, 26] = "357668";
            parametrs_sign[j, 27] = Contractor;
            parametrs_sign[j, 28] = "865";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstMetal;
            parametrs_sign[j, 31] = "ГОСТ Р 52290-2004";
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 68 Знак. Внимание нефтепровод! Проезд здесь!
            j = 68;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Внимание нефтепровод! Проезд здесь!";
            parametrs_sign[j, 1] = "ПЗ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Внимание нефтепровод! Проезд здесь!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение автодороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 69 Знак. Внимание газопровод! Проезд здесь!
            j = 69;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Внимание газопровод! Проезд здесь!";
            parametrs_sign[j, 1] = "ПЗГ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Внимание газопровод! Проезд здесь!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение автодороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 70 Аншлаг. Внимание газопровод! Движение техники запрещено!
            j = 70;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Аншлаг. Внимание газопровод! Движение техники запрещено!";
            parametrs_sign[j, 1] = "ДТЗГ";
            parametrs_sign[j, 2] = ConstSignAnshlag;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Аншлаг. Внимание газопровод! Движение техники запрещено!";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Пересечение автодороги";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = TwoRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-П-КВ-500х300";
            parametrs_sign[j, 25] = "п23.01.02";
            parametrs_sign[j, 26] = "5330114";
            parametrs_sign[j, 27] = Customer;
            parametrs_sign[j, 28] = "46";
            parametrs_sign[j, 29] = "";
            parametrs_sign[j, 30] = ConstSignPolyMer;
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";
            parametrs_sign[j, 32] = "П";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5333289";
            #endregion

            //ЗНАКИ НА ОТДЕЛЬНОСТОЯЩЕЕ ОБОРУДОВАНИЕ

            # region 71 Знак. Огнеопасно! Нефтепровод! Вантуз
            j = 71;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! Вантуз";
            parametrs_sign[j, 1] = "ВАН";
            parametrs_sign[j, 2] = ConstSignSimple;//"SignVantuz";
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! Вантуз";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Вантуз";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 72 Знак. Огнеопасно! Нефтепровод! Задвижка
            j = 72;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! Задвижка";
            parametrs_sign[j, 1] = "ЗАД";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! Задвижка";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Задвижка";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";//Резерв
            #endregion

            #region 73 Знак. Огнеопасно! Нефтепровод! Отбор давления
            j = 73;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! Отбор давления";
            parametrs_sign[j, 1] = "ОД";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! Отбор давления";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Отбор давления";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 74 Знак. Огнеопасно! Нефтепровод! Сигнализатор
            j = 74;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! Сигнализатор";
            parametrs_sign[j, 1] = "СИГ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! Сигнализатор";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Сигнализатор СОД";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                 
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 75 Знак. Огнеопасно! Нефтепровод! КППСОД
            j = 75;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! КППСОД";
            parametrs_sign[j, 1] = "СОД";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! КППСОД";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "КППСОД";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";//Резерв
            #endregion

            #region 76 Знак. УДЗ
            j = 76;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. УДЗ";
            parametrs_sign[j, 1] = "УДЗ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. УДЗ";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "УДЗ";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";               //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                 //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                                 //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 77 Знак. Защитные сооружения
            j = 77;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Защитные сооружения";
            parametrs_sign[j, 1] = "ЗС";
            parametrs_sign[j, 2] = ConstSignSimple;//"SignZS";
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Защитные сооружения";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Защитные сооружения";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                            
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";//Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 78 Знак. Амбар
            j = 78;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Амбар";
            parametrs_sign[j, 1] = "А";
            parametrs_sign[j, 2] = ConstSignSimple;//"SignAmb";
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Амбар";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Амбар";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                           
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";            //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                 //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                                 //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 79 Знак. ПКУ
            j = 79;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. ПКУ";
            parametrs_sign[j, 1] = "ПКУ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. ПКУ";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "ПКУ";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                              
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";            //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                 //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                                 //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 80 Знак. Блок бокс ПКУ
            j = 80;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Блок бокс ПКУ";
            parametrs_sign[j, 1] = "БПКУ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Блок бокс ПКУ";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Блок бокс ПКУ";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                             
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                                //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 81 Знак. Вертолетная площадка
            j = 81;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Вертолетная площадка";
            parametrs_sign[j, 1] = "ВЕР";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Вертолетная площадка";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Вертолетная площадка";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                                //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 82 Знак. Протектор
            j = 82;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Протектор";
            parametrs_sign[j, 1] = "ПР";
            parametrs_sign[j, 2] = ConstSignTriangle;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Протектор";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Протектор";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                            
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак-ПЛ-ОО-ТР-500х300";  //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                //Код АСУНСИ
            parametrs_sign[j, 26] = "5329688";                                //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОТТ-75.200.00-КТН-0412-22";                              //Нормативный документ
            parametrs_sign[j, 32] = "ОО";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "5346491";
            #endregion

            #region 83 - Знак. Огнеопасно! Нефтепровод! Камера пуска СОД
            j = 83;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! Камера пуска СОД";
            parametrs_sign[j, 1] = "СОДЗ";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! Камера пуска СОД";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Камера пуска";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Камера пуска СОД 500х300мм";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "2369253";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 84 - Знак. Огнеопасно! Нефтепровод! Камера приема СОД
            j = 84;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Огнеопасно! Нефтепровод! Камера приема СОД";
            parametrs_sign[j, 1] = "СОДП";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Огнеопасно! Нефтепровод! Камера приема СОД";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Камера прием";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Камера приема СОД 500х300мм";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                         //Код АСУНСИ
            parametrs_sign[j, 26] = "1829132";                       //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.5";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = "";
            parametrs_sign[j, 34] = "";
            parametrs_sign[j, 35] = "";
            #endregion

            #region 85 - РЕЗЕРВ

            #endregion

            //ЗНАКИ РАЗГРАНИЧЕНИЯ

            #region 86 Знак. Разграничение зон ответственности
            j = 86;
            //Общие данные по знаку
            parametrs_sign[j, 0] = "Знак. Разграничение зон ответственности";
            parametrs_sign[j, 1] = "РЗО";
            parametrs_sign[j, 2] = ConstSignSimple;
            //Данные по атрибуту №1
            parametrs_sign[j, 3] = "Знак. Разграничение зон ответственности";
            parametrs_sign[j, 4] = "ПК";
            parametrs_sign[j, 5] = "ХХХХ+ХХ";
            //Данные по атрибуту №2
            parametrs_sign[j, 6] = "Информация для ВПЗ";
            parametrs_sign[j, 7] = "Основание";
            parametrs_sign[j, 8] = "Граница зон ответственности";
            //Данные по атрибуту №3
            parametrs_sign[j, 9] = "Метод установки";
            parametrs_sign[j, 10] = "";
            parametrs_sign[j, 11] = OneRack;
            //Данные по атрибуту №4
            parametrs_sign[j, 12] = "Глубина установки";
            parametrs_sign[j, 13] = "м";
            parametrs_sign[j, 14] = "0.7";
            //Данные по атрибуту №5
            parametrs_sign[j, 15] = "Количество";
            parametrs_sign[j, 16] = "шт.";
            parametrs_sign[j, 17] = "1";
            //Данные по атрибуту №6                                                                
            parametrs_sign[j, 18] = "";
            parametrs_sign[j, 19] = "";
            parametrs_sign[j, 20] = "";
            //Данные по атрибуту №7
            parametrs_sign[j, 21] = "";
            parametrs_sign[j, 22] = "";
            parametrs_sign[j, 23] = "";
            //Общие параметры по знакам
            parametrs_sign[j, 24] = "Знак Разграничение зон ответственности 1000х600мм";    //Наименование знака по АСУНСИ
            parametrs_sign[j, 25] = "п23.01.02";                                //Код АСУНСИ
            parametrs_sign[j, 26] = "1808540";                                //Код группы оборудования
            parametrs_sign[j, 27] = Customer;                          //Поставщик Customer Contractor
            parametrs_sign[j, 28] = "11.9";                            //Масса
            parametrs_sign[j, 29] = "";                                //Резерв
            parametrs_sign[j, 30] = ConstSignPolyMer;                  //Материал
            parametrs_sign[j, 31] = "ОЛ";                              //Нормативный документ
            parametrs_sign[j, 32] = "";
            parametrs_sign[j, 33] = climate01;
            parametrs_sign[j, 34] = climate02;
            parametrs_sign[j, 35] = "1808540";
            #endregion

            #endregion 
        }

        #region Свойства доступа к полям SignBase

        //Доступ к полю step
        public string[] Step
        {
            get { return step; }
        }

        //Доступ к полю delta
        public string[] Delta
        {
            get { return delta; }
        }

        //Доступ к полю katet
        public string[] Katet
        {
            get { return katet; }
        }

        //Доступ к полю stepKM
        public string[] StepKM
        {
            get { return stepKM; }
        }

        //Доступ к полю countSign
        public string[] CountSign
        {
            get { return countsign; }
        }

        #endregion

        #region Методы обработки данных

        //Доступ к полю 
        public int CountSignBaseRow
        {
            get { return countsignbaseRow; }
            private set { countsignbaseRow = value; }
        }

        //Доступ к полю 
        public int CountSignBaseColunm
        {
            get { return countsignbaseColunm; }
            private set { countsignbaseColunm = value; }
        }

        //Доступ к полю 
        public double HeigthTextSign
        {
            get { return localheigth; }
            private set { localheigth = value; }
        }

        //Доступ к полю 
        public double TextAttribute
        {
            get { return textattribute; }
            private set { textattribute = value; }
        }

        //Доступ к полю 
        public double TextAttributeDelta
        {
            get { return textAttributeDelta; }
            private set { TextAttributeDelta = value; }
        }

        //Доступ к полю 
        public double KoeffTextShortNameSign
        {
            get { return koefftextshortnamesign; }
            private set { koefftextshortnamesign = value; }

        }

        //метод получения текущей точки 0 перезагрузка
        public static void CurrentPoint(out double PX, out double PY, out double PZ)
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null)
            {
                MessageBox.Show("Чертеж не открыт", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Переменная получаемая из командной строки
            PromptPointResult PointResult;
            //Переменная получаемая из командной строки
            PromptPointOptions PointOption = new PromptPointOptions("") { Message = "\nВведите точку вставки блока:" };
            // Получение текущей точки
            PointResult = AcadDoc.Editor.GetPoint(PointOption);
            Point3d ReturnOutPoint = PointResult.Value;
            PX = ReturnOutPoint.X;
            PY = ReturnOutPoint.Y;
            PZ = ReturnOutPoint.Z;

            // Обработка исключения 
            if (PointResult.Status == PromptStatus.Cancel)
            {
                MessageBox.Show("Не верно задана точка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        //метод получения текущей точки 0 перезагрузка
        public static void CurrentPoint(out double PX, out double PY, out double PZ, string localQuestion)
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null)
            {
                MessageBox.Show("Чертеж не открыт", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Переменная получаемая из командной строки
            PromptPointResult PointResult;
            //Переменная получаемая из командной строки
            PromptPointOptions PointOption = new PromptPointOptions("") { Message = localQuestion };
            // Получение текущей точки
            PointResult = AcadDoc.Editor.GetPoint(PointOption);
            Point3d ReturnOutPoint = PointResult.Value;
            PX = ReturnOutPoint.X;
            PY = ReturnOutPoint.Y;
            PZ = ReturnOutPoint.Z;

            // Обработка исключения 
            if (PointResult.Status == PromptStatus.Cancel)
            {
                MessageBox.Show("Не верно задана точка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        //метод получения точки вставки

        public static void InsertPoint(out double PX, out double PY, out double PZ)
        {
            //получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null)
            {
                MessageBox.Show("Чертеж не открыт", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Переменная получаемая из командной строки
            PromptPointResult PointResult;
            //Переменная получаемая из командной строки
            PromptPointOptions PointOption = new PromptPointOptions("") { Message = "\nВведите точку вставки блока:" };
            // Получение текущей точки
            PointResult = AcadDoc.Editor.GetPoint(PointOption);
            Point3d ReturnOutPoint = PointResult.Value;
            PX = ReturnOutPoint.X;
            PY = ReturnOutPoint.Y;
            PZ = ReturnOutPoint.Z;

            // Обработка исключения 
            if (PointResult.Status == PromptStatus.Cancel)
            {
                MessageBox.Show("Не верно задана точка", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        // метод возвращаемый данные по квадратному запросу
        public string GetParametrSign(int c, int l)
        {
            return parametrs_sign[c, l];
        }

        // метод проверяет существует ли данный блок знака
        public static Boolean IfExistBlock(string LocalNameSignBlock)
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return false;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSignBlock))
                {
                    //MessageBox.Show("Блок с именем (" + LocalNameSignBlock + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                tr.Commit();
            }
            return false;
        }

        //метод получения разделенного пикета (отдельно 100)
        public static double PiketSto(string piket)
        {
            string[] Result = piket.Split(DelimitelPlus);
            return Convert.ToDouble(Result[0]);

        }

        //метод получения разделенного пикета (плюсовка)
        public static double PiketPlus(string piket)
        {
            piket = piket.Replace(',', '.');
            String[] Result = piket.Split(DelimitelPlus);
            return Convert.ToDouble(Result[1]);
        }

        //метод получения пикета
        public static double PiketStringToDouble(string piket)
        {
            string localpiket = piket.Replace(PK, "");
            return PiketSto(localpiket) * 100 + PiketPlus(localpiket);
        }

        //Метод преобразования пикетов в КМ
        public static double PKtoKM(string piket)
        {
            string localpiket = piket.Replace(PK, "");
            string[] Locallocalpiket = localpiket.Split('+');
            double LocalPiketSto = Convert.ToDouble(Locallocalpiket[0]);
            double LocalPiketPlus = Convert.ToDouble(Locallocalpiket[1]);
            return Convert.ToDouble((LocalPiketSto * 100 + LocalPiketPlus) / 1000);
        }

        //Метод преобразования КМ в пикеты
        public static string KMtoPK(double LocalKM)
        {
            double LocalPiketSto = (int)LocalKM / 100;
            double LocalPiketPlus = Math.Round(LocalKM - LocalPiketSto * 100, 2);
            return Convert.ToString(LocalPiketSto) + "+" + Convert.ToString(LocalPiketPlus);
        }

        //Метод доступа к полилинии в локальном документе и транзакции
        public static bool GetPolyline(Document LocalAcadDoc, Database LocalAcadDB, out Polyline LocalPoly)
        {
            using (Transaction trPoly = LocalAcadDB.TransactionManager.StartTransaction())
            {
                //Переменная получаемая из командной строки
                PromptEntityOptions EntityOption = new PromptEntityOptions("\n Выберите полилинию-трассу МТ");
                //Запрос в командную строку на указание полилинии
                EntityOption.SetRejectMessage(""); // \nВыберите полилинию-трассу МТ
                //проверка типа выбранного объекта
                EntityOption.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Polyline), true);
                //Запрос выбора объектов в области чертежа
                PromptEntityResult EntityResult = LocalAcadDoc.Editor.GetEntity(EntityOption);
                //проверка статуса правильности выбора
                if (EntityResult.Status == PromptStatus.OK)
                {
                    Polyline LocalPolyTrassaMN = trPoly.GetObject(EntityResult.ObjectId, OpenMode.ForRead) as Polyline;
                    if (LocalPolyTrassaMN.GetType() != typeof(Polyline))
                    {
                        MessageBox.Show("Полилиния трассы не выбрана. Расстановка знаков не выполнена", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LocalPoly = null;
                        return false;
                    }
                    else
                    {
                        LocalPoly = LocalPolyTrassaMN;
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Полилиния трассы не выбрана. Расстановка знаков не выполнена", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LocalPoly = null;
                    return false;
                }
            }
        }
        



        //Создание списка знаков для замены
        public static List<string> Sign()
        {
            // Список знаков
            List<string> _sign = new List<string>();

            // Ссылка на базовый класс
            SignBase SB = new SignBase();

            // Перебор блоков
            for (int i = 0; i <= SB.countsignbaseRow - 1; i++)
            {
                if (!String.IsNullOrEmpty(SB.GetParametrSign(i, 0)))
                {
                    _sign.Add(SB.GetParametrSign(i, 0));
                }
            }
            return _sign;
        }

        // поиск индекса знака по его имени
        public static int IndexSign(string nameSign)
        {
            int _i = -1;
            
            // Ссылка на базовый класс
            SignBase SB = new SignBase();

            // Перебор блоков
            for (int i = 0; i <= SB.countsignbaseRow - 1; i++)
            {
                if (SB.GetParametrSign(i, 0) == nameSign)
                {
                    _i = i;
                }
            }
            return _i;
        }
    














        #endregion

        #region Методы создания блоков
        public static void CreateBlockSignIden(double PX, double PY, double PZ, string LocalNameSign, string LocalShortNameSign,
                                        string LocalValueAtt01, string LocalPrompt01, string LocalTag01,
                                        string LocalValueAtt02, string LocalPrompt02, string LocalTag02,
                                        string LocalValueAtt03, string LocalPrompt03, string LocalTag03,
                                        string LocalValueAtt04, string LocalPrompt04, string LocalTag04,
                                        string LocalValueAtt05, string LocalPrompt05, string LocalTag05,
                                        double LocalAngleBlock, string marker01
                                       )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //ссылка на базу по блокам
                SignBase S = new SignBase();
                //ссылка на слои
                LayerWorks LocalDataSign = new LayerWorks();
                Point3d BasePoint = new Point3d(0, 0, 0);
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSign))
                {
                    MessageBox.Show("Блок с именем (" + LocalNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // создаем новое определение блока, задаем ему имя
                BlockTableRecord LocalBlock = new BlockTableRecord() { Name = LocalNameSign };
                // запоминаем ID созданного определения блока 
                ObjectId LocalBlockId = blocktable.Add(LocalBlock);
                tr.AddNewlyCreatedDBObject(LocalBlock, true);
                //Запоминание текущего слоя для возврата к нему
                string CurrentLayer = LocalDataSign.CurrentLayerData();
                //Подготавливаем слои
                LocalDataSign.PreparationLayer();
                //Слой знаков
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);
                // создаем линии
                Line line = new Line();
                if (marker01 == ConstSignSimple)
                #region Простой знак
                {
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Создаем однострочный текстовый объект
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Текста
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = LocalShortNameSign;
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);
                }
                #endregion

                if (marker01 == ConstSignAnshlag)
                #region Добавление элементов аншлага
                {
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                                    new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Создаем полилинию
                    Polyline poly01 = new Polyline();
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + 2 * S.HeigthTextSign), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + 3 * S.HeigthTextSign), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + 3 * S.HeigthTextSign), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + 2 * S.HeigthTextSign), 0, 0, 0);
                    poly01.Closed = true;
                    LocalBlock.AppendEntity(poly01);
                    tr.AddNewlyCreatedDBObject(poly01, true);

                    //Создаем штриховку полилинию
                    Hatch HatchRep = new Hatch();
                    HatchRep.SetDatabaseDefaults();
                    HatchRep.PatternScale = 0.3;
                    HatchRep.SetHatchPattern(HatchPatternType.PreDefined, "ANSI31");
                    ObjectIdCollection ObjIdColl = new ObjectIdCollection() { };
                    //HatchRep.Associative = true;
                    ObjIdColl.Add(poly01.ObjectId);
                    HatchRep.AppendLoop(HatchLoopTypes.Outermost, ObjIdColl);
                    HatchRep.EvaluateHatch(true);
                    LocalBlock.AppendEntity(HatchRep);
                    tr.AddNewlyCreatedDBObject(HatchRep, true);

                    //Создаем однострочный текстовый объект LocalDataSign
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Текста
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = LocalShortNameSign;
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);
                }
                #endregion

                if (marker01 == ConstSignWarningRW)
                #region Добавление элементов знака нефть
                {
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    // Создаем однострочный текстовый объект
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Текста
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 0.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = LocalShortNameSign;
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 0.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);
                }
                #endregion

                if (marker01 == ConstSignHiPress)
                #region Добавление элементов аншлага
                {
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Косые прямые линии №1
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Косые прямые линии №2
                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    // Создаем однострочный текстовый объект LocalDataSign
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Текста
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = LocalShortNameSign;
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);
                }
                #endregion

                if (marker01 == ConstSignEqup)
                #region Добавление элементов оборудование
                {
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign); //Слой атрибутов

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    // Создаем однострочный текстовый объект 
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Текста
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 0.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = LocalShortNameSign;
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 0.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);

                }
                #endregion




                #region Общие параметры для знаков

                //Добавляем атрибут №1 
                LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign); //Слой атрибутов
                BasePoint = new Point3d(BasePoint.X + S.HeigthTextSign, BasePoint.Y + 3 * S.HeigthTextSign - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt01,
                    Tag = LocalTag01,
                    TextString = LocalValueAtt01,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock01);
                tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                //Добавляем атрибут №2
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt02,
                    Tag = LocalTag02,
                    TextString = LocalValueAtt02,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock02);
                tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                //Добавляем атрибут №3
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt03,
                    Tag = LocalTag03,
                    TextString = LocalValueAtt03,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock03);
                tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                //Добавляем атрибут №4
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt04,
                    Tag = LocalTag04,
                    TextString = LocalValueAtt04,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock04);
                tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                //Добавляем атрибут №5
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt05,
                    Tag = LocalTag05,
                    TextString = LocalValueAtt05,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock05);
                tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                // открываем пространство модели на запись MyWorkLayer
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(InsPoint, LocalBlockId);

                // разворот вставки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                // добавляем экземпляр ссылки на объект
                AttributeReference AttributeRef01 = new AttributeReference();
                AttributeReference AttributeRef02 = new AttributeReference();
                AttributeReference AttributeRef03 = new AttributeReference();
                AttributeReference AttributeRef04 = new AttributeReference();
                AttributeReference AttributeRef05 = new AttributeReference();

                AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                AttributeRef01.TextString = LocalValueAtt01;
                AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                AttributeRef02.TextString = LocalValueAtt02;
                AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                AttributeRef03.TextString = LocalValueAtt03;
                AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                AttributeRef04.TextString = LocalValueAtt04;
                AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                AttributeRef05.TextString = LocalValueAtt05;

                // Добавляем AttributeReference к BlockReference
                br.AttributeCollection.AppendAttribute(AttributeRef01);
                tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                br.AttributeCollection.AppendAttribute(AttributeRef02);
                tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                br.AttributeCollection.AppendAttribute(AttributeRef03);
                tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                br.AttributeCollection.AppendAttribute(AttributeRef04);
                tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                br.AttributeCollection.AppendAttribute(AttributeRef05);
                tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                //восстанавливаем предыдущий текущий слой
                LocalDataSign.ActiveLayerSetup(CurrentLayer);
                #endregion

                tr.Commit();
                line.Dispose();
                tr.Dispose();
            }
        }
        public static void CreateBlockSigTriangle(double PX, double PY, double PZ, string LocalNameSign, string LocalShortNameSign,
                                           string LocalValueAtt01, string LocalPrompt01, string LocalTag01,
                                           string LocalValueAtt02, string LocalPrompt02, string LocalTag02,
                                           string LocalValueAtt03, string LocalPrompt03, string LocalTag03,
                                           string LocalValueAtt04, string LocalPrompt04, string LocalTag04,
                                           string LocalValueAtt05, string LocalPrompt05, string LocalTag05,
                                           double LocalAngleBlock, string marker01
                                           )
        {
            //получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            //получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //ссылка на базу
                SignBase S = new SignBase();
                //ссылка на слои
                LayerWorks LocalDataSign = new LayerWorks();
                Point3d BasePoint = new Point3d(0, 0, 0);
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSign))
                {
                    _ = MessageBox.Show("Блок с именем (" + LocalNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // создаем новое определение блока, задаем ему имя
                BlockTableRecord LocalBlock = new BlockTableRecord() { Name = LocalNameSign };
                // запоминаем ID созданного определения блока 
                ObjectId LocalBlockId = blocktable.Add(LocalBlock);
                tr.AddNewlyCreatedDBObject(LocalBlock, true);
                //Запоминание текущего слоя для возврата к нему
                string CurrentLayer = LocalDataSign.CurrentLayerData();
                //Подготавливаем слои
                LocalDataSign.PreparationLayer();
                //Слой знаков LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);
                // создаем линии
                if (marker01 == ConstSignTriangle)
                {
                    //Линия штока
                    Line line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Линия основания знака - половинка горизонтальная
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Линия основания щита нижнее 
                    line = new Line(new Point3d(BasePoint.X - S.HeigthTextSign / 2, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Линия левая треугольника 
                    line = new Line(new Point3d(BasePoint.X - S.HeigthTextSign / 2, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Линия правая треугольника
                    line = new Line(new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);
                }

                // Создаем однострочный текстовый объект
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Слой текста
                DBText Text = new DBText();
                Text.SetDatabaseDefaults();
                Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 3.5 * S.HeigthTextSign, BasePoint.Z);
                Text.Height = S.HeigthTextSign - 0.5 * 2;
                Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                Text.TextString = LocalShortNameSign;
                Text.HorizontalMode = TextHorizontalMode.TextCenter;
                Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 3.5 * S.HeigthTextSign, BasePoint.Z);
                Text.WidthFactor = S.KoeffTextShortNameSign;
                LocalBlock.AppendEntity(Text);
                tr.AddNewlyCreatedDBObject(Text, true);

                //Слой атрибута LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign);

                //Добавляем атрибут №1
                BasePoint = new Point3d(BasePoint.X + S.HeigthTextSign, BasePoint.Y + 3 * S.HeigthTextSign - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt01,
                    Tag = LocalTag01,
                    TextString = LocalValueAtt01,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock01);
                tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                //Добавляем атрибут №2
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt02,
                    Tag = LocalTag02,
                    TextString = LocalValueAtt02,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock02);
                tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                //Добавляем атрибут №3
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt03,
                    Tag = LocalTag03,
                    TextString = LocalValueAtt03,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock03);
                tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                //Добавляем атрибут №4
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt04,
                    Tag = LocalTag04,
                    TextString = LocalValueAtt04,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock04);
                tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                //Добавляем атрибут №5
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt05,
                    Tag = LocalTag05,
                    TextString = LocalValueAtt05,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock05);
                tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                // открываем пространство модели на запись LocalDataSign
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(InsPoint, LocalBlockId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                // добавляем экземпляр ссылки на объект
                AttributeReference AttributeRef01 = new AttributeReference();
                AttributeReference AttributeRef02 = new AttributeReference();
                AttributeReference AttributeRef03 = new AttributeReference();
                AttributeReference AttributeRef04 = new AttributeReference();
                AttributeReference AttributeRef05 = new AttributeReference();

                AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                AttributeRef01.TextString = LocalValueAtt01;
                AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                AttributeRef02.TextString = LocalValueAtt02;
                AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                AttributeRef03.TextString = LocalValueAtt03;
                AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                AttributeRef04.TextString = LocalValueAtt04;
                AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                AttributeRef05.TextString = LocalValueAtt05;

                // Добавляем AttributeReference к BlockReference
                br.AttributeCollection.AppendAttribute(AttributeRef01);
                tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                br.AttributeCollection.AppendAttribute(AttributeRef02);
                tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                br.AttributeCollection.AppendAttribute(AttributeRef03);
                tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                br.AttributeCollection.AppendAttribute(AttributeRef04);
                tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                br.AttributeCollection.AppendAttribute(AttributeRef05);
                tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                //восстанавливаем предыдущий текущий слой
                LocalDataSign.ActiveLayerSetup(CurrentLayer);
                tr.Commit();
            }
        }
        public static void CreateBlockSignCircle(double PX, double PY, double PZ, string LocalNameSign, string LocalShortNameSign,
                                                 string LocalValueAtt01, string LocalPrompt01, string LocalTag01,
                                                 string LocalValueAtt02, string LocalPrompt02, string LocalTag02,
                                                 string LocalValueAtt03, string LocalPrompt03, string LocalTag03,
                                                 string LocalValueAtt04, string LocalPrompt04, string LocalTag04,
                                                 string LocalValueAtt05, string LocalPrompt05, string LocalTag05,
                                                 string LocalValueAtt06, string LocalPrompt06, string LocalTag06,
                                                 string LocalValueAtt07, string LocalPrompt07, string LocalTag07,
                                                 double LocalAngleBlock, string marker01
                                                )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //ссылка на базу по блокам
                SignBase S = new SignBase();
                //ссылка на слои
                LayerWorks LocalDataSign = new LayerWorks();
                Point3d BasePoint = new Point3d(0, 0, 0);
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSign))
                {
                    MessageBox.Show("Блок с именем (" + LocalNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // создаем новое определение блока, задаем ему имя
                BlockTableRecord LocalBlock = new BlockTableRecord() { Name = LocalNameSign };
                // запоминаем ID созданного определения блока 
                ObjectId LocalBlockId = blocktable.Add(LocalBlock);
                tr.AddNewlyCreatedDBObject(LocalBlock, true);
                //Запоминание текущего слоя для возврата к нему
                string CurrentLayer = LocalDataSign.CurrentLayerData();
                //Подготавливаем слои
                LocalDataSign.PreparationLayer();
                //Слой знака LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);
                // Создаем окружность
                Vector3d normal = new Vector3d(0.0, 0.0, 1.0);
                double LocalRadius = S.HeigthTextSign / 2;
                Circle circle = new Circle(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z), normal, LocalRadius);
                LocalBlock.AppendEntity(circle);
                tr.AddNewlyCreatedDBObject(circle, true);
                // создаем линии
                if (marker01 == ConstSignReper)
                #region ConstSignReper
                {
                    Line line = new Line(new Point3d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z),
                                    new Point3d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z),
                                    new Point3d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Создаем полилинию
                    Polyline poly01 = new Polyline();
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 4)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4 + Math.PI / 12), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 4 + Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4 + 2 * Math.PI / 12), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 4 + 2 * Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X, BasePoint.Y + LocalRadius), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - Math.PI / 12), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 2 - Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 2 * Math.PI / 12), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 2 - 2 * Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y + LocalRadius * Math.Sin(Math.PI / 4)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X, BasePoint.Y), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 4)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4 + Math.PI / 12), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 4 + Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X - LocalRadius * Math.Cos(Math.PI / 4 + 2 * Math.PI / 12), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 4 + 2 * Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X, BasePoint.Y - LocalRadius), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - Math.PI / 12), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 2 - Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 2 * Math.PI / 12), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 2 - 2 * Math.PI / 12)), 0, 0, 0);
                    poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 4), BasePoint.Y - LocalRadius * Math.Sin(Math.PI / 4)), 0, 0, 0);
                    poly01.Closed = true;
                    LocalBlock.AppendEntity(poly01);
                    tr.AddNewlyCreatedDBObject(poly01, true);

                    //Создание штриховки по Polyline
                    Hatch HatchRep = new Hatch();
                    HatchRep.SetDatabaseDefaults();
                    HatchRep.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                    ObjectIdCollection ObjIdColl = new ObjectIdCollection { poly01.ObjectId };
                    //HatchRep.Associative = true;
                    HatchRep.AppendLoop(HatchLoopTypes.Outermost, ObjIdColl);
                    HatchRep.EvaluateHatch(true);
                    HatchRep.Normal = normal;
                    HatchRep.Elevation = 0.0;
                    HatchRep.PatternScale = 1.0;
                    LocalBlock.AppendEntity(HatchRep);
                    tr.AddNewlyCreatedDBObject(HatchRep, true);
                }
                #endregion

                if (marker01 == ConstSignMarker)
                #region ConstSignMarker
                {
                    // создаем линии
                    Line line = new Line(new Point3d(BasePoint.X - S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y - S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y - S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z),
                                    new Point3d(BasePoint.X - S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);
                }
                #endregion

                if (marker01 == ConstSignSM)
                #region ConstSignSM
                {
                    //Косая линия слева
                    Line line = new Line(new Point3d(BasePoint.X - LocalRadius / Math.Sin(Math.PI / 4), BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + LocalRadius / Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Косая линия справа
                    line = new Line(new Point3d(BasePoint.X + LocalRadius / Math.Sin(Math.PI / 4), BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + LocalRadius / Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //Вертикальная линия справа
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y + LocalRadius / Math.Sin(Math.PI / 4), BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + LocalRadius / Math.Sin(Math.PI / 4) + LocalRadius / 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);
                }
                #endregion

                if (marker01 == ConstВeformationMark)
                #region ConstSignSM
                {
                    //Текст
                    DBText text = new DBText
                    {
                        Position = new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                        Height = LocalRadius,
                        TextString = LocalShortNameSign,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        WidthFactor = 0.5,
                        AlignmentPoint = new Point3d(BasePoint.X - LocalRadius/2, BasePoint.Y - LocalRadius/2, BasePoint.Z),
                    };
                    LocalBlock.AppendEntity(text);
                    tr.AddNewlyCreatedDBObject(text, true);
                }
                #endregion

                #region Общие данные для всех знаков
                //Слой атрибута LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign);

                //Добавляем атрибут №1
                BasePoint = new Point3d(BasePoint.X + S.HeigthTextSign, BasePoint.Y + 3 * S.HeigthTextSign - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt01,
                    Tag = LocalTag01,
                    TextString = LocalValueAtt01,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock01);
                tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                //Добавляем атрибут №2
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt02,
                    Tag = LocalTag02,
                    TextString = LocalValueAtt02,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock02);
                tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                //Добавляем атрибут №3
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt03,
                    Tag = LocalTag03,
                    TextString = LocalValueAtt03,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock03);
                tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                //Добавляем атрибут №4
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt04,
                    Tag = LocalTag04,
                    TextString = LocalValueAtt04,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock04);
                tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                //Добавляем атрибут №5
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt05,
                    Tag = LocalTag05,
                    TextString = LocalValueAtt05,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock05);
                tr.AddNewlyCreatedDBObject(AttributeBlock05, true);


                // открываем пространство модели на запись LocalDataSign
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(InsPoint, LocalBlockId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                // добавляем экземпляр ссылки на объект
                AttributeReference AttributeRef01 = new AttributeReference();
                AttributeReference AttributeRef02 = new AttributeReference();
                AttributeReference AttributeRef03 = new AttributeReference();
                AttributeReference AttributeRef04 = new AttributeReference();
                AttributeReference AttributeRef05 = new AttributeReference();

                AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                AttributeRef01.TextString = LocalValueAtt01;
                AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                AttributeRef02.TextString = LocalValueAtt02;
                AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                AttributeRef03.TextString = LocalValueAtt03;
                AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                AttributeRef04.TextString = LocalValueAtt04;
                AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                AttributeRef05.TextString = LocalValueAtt05;

                // Добавляем AttributeReference к BlockReference
                br.AttributeCollection.AppendAttribute(AttributeRef01);
                tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                br.AttributeCollection.AppendAttribute(AttributeRef02);
                tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                br.AttributeCollection.AppendAttribute(AttributeRef03);
                tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                br.AttributeCollection.AppendAttribute(AttributeRef04);
                tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                br.AttributeCollection.AppendAttribute(AttributeRef05);
                tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                if (marker01 == ConstSignReper)
                {
                    //Слой атрибута LocalDataSign
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign);
                    //Добавляем атрибут №6
                    BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                    AttributeDefinition AttributeBlock06 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = LocalPrompt06,
                        Tag = LocalTag06,
                        TextString = LocalValueAtt06,
                        Height = S.TextAttribute,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = BasePoint
                    };
                    LocalBlock.AppendEntity(AttributeBlock06);
                    tr.AddNewlyCreatedDBObject(AttributeBlock06, true);

                    //Добавляем атрибут №7
                    BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                    AttributeDefinition AttributeBlock07 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = LocalPrompt07,
                        Tag = LocalTag07,
                        TextString = LocalValueAtt07,
                        Height = S.TextAttribute,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = BasePoint
                    };
                    LocalBlock.AppendEntity(AttributeBlock07);
                    tr.AddNewlyCreatedDBObject(AttributeBlock07, true);

                    AttributeReference AttributeRef06 = new AttributeReference();
                    AttributeReference AttributeRef07 = new AttributeReference();

                    AttributeRef06.SetAttributeFromBlock(AttributeBlock06, br.BlockTransform);
                    AttributeRef06.TextString = LocalValueAtt06;
                    AttributeRef07.SetAttributeFromBlock(AttributeBlock07, br.BlockTransform);
                    AttributeRef07.TextString = LocalValueAtt07;

                    br.AttributeCollection.AppendAttribute(AttributeRef06);
                    tr.AddNewlyCreatedDBObject(AttributeRef06, true);

                    br.AttributeCollection.AppendAttribute(AttributeRef07);
                    tr.AddNewlyCreatedDBObject(AttributeRef07, true);
                }

                //восстанавливаем предыдущий текущий слой
                LocalDataSign.ActiveLayerSetup(CurrentLayer);
                
                #endregion

                tr.Commit();
            }
        }

        public static void CreateBlockSignStvor(double PX, double PY, double PZ, string LocalNameSign, string LocalShortNameSign,
                                         string LocalValueAtt01, string LocalPrompt01, string LocalTag01,
                                         string LocalValueAtt02, string LocalPrompt02, string LocalTag02,
                                         string LocalValueAtt03, string LocalPrompt03, string LocalTag03,
                                         string LocalValueAtt04, string LocalPrompt04, string LocalTag04,
                                         string LocalValueAtt05, string LocalPrompt05, string LocalTag05,
                                         double LocalAngleBlock, string marker01
                                         )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //ссылка на базу
                SignBase S = new SignBase();
                //ссылка на слои
                LayerWorks LocalDataSign = new LayerWorks();
                Point3d BasePoint = new Point3d(0, 0, 0);
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSign))
                {
                    MessageBox.Show("Блок с именем (" + LocalNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // создаем новое определение блока, задаем ему имя
                BlockTableRecord LocalBlock = new BlockTableRecord() { Name = LocalNameSign };
                // запоминаем ID созданного определения блока 
                ObjectId LocalBlockId = blocktable.Add(LocalBlock);
                tr.AddNewlyCreatedDBObject(LocalBlock, true);
                //Запоминание текущего слоя для возврата к нему
                string CurrentLayer = LocalDataSign.CurrentLayerData();
                //Подготавливаем слои
                LocalDataSign.PreparationLayer();

                // создаем линии LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign); //Слой знака

                Line line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z),
                                new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 3, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 12, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 12, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                if (marker01 == ConstSignWNSR)
                {
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y + 3 * S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + 3.5 * S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);
                }

                // Создаем однострочный текстовый объект LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Слой текста
                DBText Text = new DBText();
                Text.SetDatabaseDefaults();
                Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                Text.Height = S.HeigthTextSign - 0.5 * 2;
                Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                Text.TextString = LocalShortNameSign;
                Text.HorizontalMode = TextHorizontalMode.TextCenter;
                Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 2.5 * S.HeigthTextSign, BasePoint.Z);
                Text.WidthFactor = S.KoeffTextShortNameSign;
                LocalBlock.AppendEntity(Text);
                tr.AddNewlyCreatedDBObject(Text, true);

                //Добавляем атрибут №1 LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign); //Слой атрибута
                BasePoint = new Point3d(BasePoint.X + S.HeigthTextSign, BasePoint.Y + 3 * S.HeigthTextSign - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt01,
                    Tag = LocalTag01,
                    TextString = LocalValueAtt01,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock01);
                tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                //Добавляем атрибут №2
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt02,
                    Tag = LocalTag02,
                    TextString = LocalValueAtt02,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock02);
                tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                //Добавляем атрибут №3
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt03,
                    Tag = LocalTag03,
                    TextString = LocalValueAtt03,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock03);
                tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                //Добавляем атрибут №4
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt04,
                    Tag = LocalTag04,
                    TextString = LocalValueAtt04,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock04);
                tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                //Добавляем атрибут №5
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt05,
                    Tag = LocalTag05,
                    TextString = LocalValueAtt05,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock05);
                tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                // открываем пространство модели на запись MyWorkLayer
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(InsPoint, LocalBlockId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                // добавляем экземпляр ссылки на объект
                AttributeReference AttributeRef01 = new AttributeReference();
                AttributeReference AttributeRef02 = new AttributeReference();
                AttributeReference AttributeRef03 = new AttributeReference();
                AttributeReference AttributeRef04 = new AttributeReference();
                AttributeReference AttributeRef05 = new AttributeReference();

                AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                AttributeRef01.TextString = LocalValueAtt01;
                AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                AttributeRef02.TextString = LocalValueAtt02;
                AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                AttributeRef03.TextString = LocalValueAtt03;
                AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                AttributeRef04.TextString = LocalValueAtt04;
                AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                AttributeRef05.TextString = LocalValueAtt05;

                // Добавляем AttributeReference к BlockReference
                br.AttributeCollection.AppendAttribute(AttributeRef01);
                tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                br.AttributeCollection.AppendAttribute(AttributeRef02);
                tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                br.AttributeCollection.AppendAttribute(AttributeRef03);
                tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                br.AttributeCollection.AppendAttribute(AttributeRef04);
                tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                br.AttributeCollection.AppendAttribute(AttributeRef05);
                tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                //восстанавливаем предыдущий текущий слой
                LocalDataSign.ActiveLayerSetup(CurrentLayer);
                tr.Commit();
            }
        }
        public static void CreateBlockSignKM(double PX, double PY, double PZ, string LocalNameSign, string LocalShortNameSign,
                                      string LocalValueAtt01, string LocalPrompt01, string LocalTag01,
                                      string LocalValueAtt02, string LocalPrompt02, string LocalTag02,
                                      string LocalValueAtt03, string LocalPrompt03, string LocalTag03,
                                      string LocalValueAtt04, string LocalPrompt04, string LocalTag04,
                                      string LocalValueAtt05, string LocalPrompt05, string LocalTag05,
                                      string LocalValueAtt06, string LocalPrompt06, string LocalTag06,
                                      double LocalAngleBlock, string marker01
                                      )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //ссылка на базу
                SignBase S = new SignBase();
                //ссылка на слои
                LayerWorks LocalDataSign = new LayerWorks();
                Point3d BasePoint = new Point3d(0, 0, 0);
                Point3d BasePointKM = new Point3d(0, 0, 0);
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSign))
                {
                    MessageBox.Show("Блок с именем (" + LocalNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // создаем новое определение блока, задаем ему имя
                BlockTableRecord LocalBlock = new BlockTableRecord() { Name = LocalNameSign };
                // запоминаем ID созданного определения блока 
                ObjectId LocalBlockId = blocktable.Add(LocalBlock);
                tr.AddNewlyCreatedDBObject(LocalBlock, true);
                //Запоминание текущего слоя для возврата к нему
                string CurrentLayer = LocalDataSign.CurrentLayerData();
                //Подготавливаем слои
                LocalDataSign.PreparationLayer();

                // Создаем окружность LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign); //Слой знака
                Vector3d normal = new Vector3d(0.0, 0.0, 1.0);
                double LocalRadius = S.HeigthTextSign / 2;
                Circle circle = new Circle(new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius, BasePoint.Z), normal, LocalRadius);
                LocalBlock.AppendEntity(circle);
                tr.AddNewlyCreatedDBObject(circle, true);
                // создаем линии
                Line line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + 2 * LocalRadius, BasePoint.Z));
                LocalBlock.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                //РАЗДЕЛЕНИЕ (ДОПОЛНЕНИЕ ЗНАКА) НА ОБЫКНОВЕННЫЙ И С МАРКЕРОМ
                if (marker01 == ConstSignКМM)
                {

                    // Создаем окружность
                    circle = new Circle(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z), normal, LocalRadius);
                    LocalBlock.AppendEntity(circle);
                    tr.AddNewlyCreatedDBObject(circle, true);

                    line = new Line(new Point3d(BasePoint.X - S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y - S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    line = new Line(new Point3d(BasePoint.X + S.HeigthTextSign / 2 * Math.Cos(Math.PI / 3), BasePoint.Y - S.HeigthTextSign / 2 * Math.Cos(Math.PI / 6), BasePoint.Z),
                                    new Point3d(BasePoint.X - S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);
                }

                //Создаем полилинию
                Polyline poly01 = new Polyline();
                poly01.AddVertexAt(0, new Point2d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + 2 * LocalRadius), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 1 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 2 - 1 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 2 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 2 - 2 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 3 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 2 - 3 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 4 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 2 - 4 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Cos(Math.PI / 2 - 5 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 2 - 5 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 2 - 1 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Cos(Math.PI / 2 - 1 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 2 - 2 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Cos(Math.PI / 2 - 2 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 2 - 3 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Cos(Math.PI / 2 - 3 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 2 - 4 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Cos(Math.PI / 2 - 4 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 2 - 5 * Math.PI / 12), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Cos(Math.PI / 2 - 5 * Math.PI / 12)), 0, 0, 0);
                poly01.AddVertexAt(0, new Point2d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign), 0, 0, 0);
                poly01.Closed = true;
                LocalBlock.AppendEntity(poly01);
                tr.AddNewlyCreatedDBObject(poly01, true);

                //Добавляем штриховку по полилинии
                Hatch HatchRep = new Hatch();
                HatchRep.SetDatabaseDefaults();
                HatchRep.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                ObjectIdCollection ObjIdColl = new ObjectIdCollection { poly01.ObjectId };
                //HatchRep.Associative = true;
                HatchRep.AppendLoop(HatchLoopTypes.Outermost, ObjIdColl);
                HatchRep.EvaluateHatch(true);
                HatchRep.Normal = normal;
                HatchRep.Elevation = 0.0;
                HatchRep.PatternScale = 1.0;
                LocalBlock.AppendEntity(HatchRep);
                tr.AddNewlyCreatedDBObject(HatchRep, true);

                //Слой атрибутов LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign);

                //Добавляем атрибут №1
                BasePoint = new Point3d(BasePoint.X + S.HeigthTextSign, BasePoint.Y + 3 * S.HeigthTextSign - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt01,
                    Tag = LocalTag01,
                    TextString = LocalValueAtt01,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock01);
                tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                //Добавляем атрибут №2
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt02,
                    Tag = LocalTag02,
                    TextString = LocalValueAtt02,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock02);
                tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                //Добавляем атрибут №3
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt03,
                    Tag = LocalTag03,
                    TextString = LocalValueAtt03,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock03);
                tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                //Добавляем атрибут №4
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt04,
                    Tag = LocalTag04,
                    TextString = LocalValueAtt04,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock04);
                tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                //Добавляем атрибут №5
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt05,
                    Tag = LocalTag05,
                    TextString = LocalValueAtt05,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock05);
                tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                //Переключение слоя  LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                //Добавляем атрибут №6
                BasePointKM = new Point3d(BasePointKM.X, BasePointKM.Y + 2 * S.HeigthTextSign + 2 * LocalRadius, BasePointKM.Z);
                AttributeDefinition AttributeBlock06 = new AttributeDefinition()
                {
                    Position = BasePointKM,
                    Prompt = LocalPrompt06,
                    Tag = LocalTag06,
                    TextString = LocalValueAtt06,
                    Height = S.HeigthTextSign,
                    HorizontalMode = TextHorizontalMode.TextCenter,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePointKM
                };
                LocalBlock.AppendEntity(AttributeBlock06);
                tr.AddNewlyCreatedDBObject(AttributeBlock06, true);

                // открываем пространство модели на запись MyWorkLayer
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(InsPoint, LocalBlockId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                // добавляем экземпляр ссылки на объект
                AttributeReference AttributeRef01 = new AttributeReference();
                AttributeReference AttributeRef02 = new AttributeReference();
                AttributeReference AttributeRef03 = new AttributeReference();
                AttributeReference AttributeRef04 = new AttributeReference();
                AttributeReference AttributeRef05 = new AttributeReference();
                AttributeReference AttributeRef06 = new AttributeReference();

                AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                AttributeRef01.TextString = LocalValueAtt01;
                AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                AttributeRef02.TextString = LocalValueAtt02;
                AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                AttributeRef03.TextString = LocalValueAtt03;
                AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                AttributeRef04.TextString = LocalValueAtt04;
                AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                AttributeRef05.TextString = LocalValueAtt05;
                AttributeRef06.SetAttributeFromBlock(AttributeBlock06, br.BlockTransform);
                AttributeRef06.TextString = LocalValueAtt06;

                // Добавляем AttributeReference к BlockReference
                br.AttributeCollection.AppendAttribute(AttributeRef01);
                tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                br.AttributeCollection.AppendAttribute(AttributeRef02);
                tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                br.AttributeCollection.AppendAttribute(AttributeRef03);
                tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                br.AttributeCollection.AppendAttribute(AttributeRef04);
                tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                br.AttributeCollection.AppendAttribute(AttributeRef05);
                tr.AddNewlyCreatedDBObject(AttributeRef05, true);
                br.AttributeCollection.AppendAttribute(AttributeRef06);
                tr.AddNewlyCreatedDBObject(AttributeRef06, true);

                //восстанавливаем предыдущий текущий слой
                LocalDataSign.ActiveLayerSetup(CurrentLayer);
                tr.Commit();
            }
        }
        public static void CreateBlockSignDiff(double PX, double PY, double PZ, string LocalNameSign, string LocalShortNameSign,
                                        string LocalValueAtt01, string LocalPrompt01, string LocalTag01,
                                        string LocalValueAtt02, string LocalPrompt02, string LocalTag02,
                                        string LocalValueAtt03, string LocalPrompt03, string LocalTag03,
                                        string LocalValueAtt04, string LocalPrompt04, string LocalTag04,
                                        string LocalValueAtt05, string LocalPrompt05, string LocalTag05,
                                        double LocalAngleBlock, string marker01
                                        )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //ссылка на базу
                SignBase S = new SignBase();
                //ссылка на слои
                LayerWorks LocalDataSign = new LayerWorks();
                Point3d BasePoint = new Point3d(0, 0, 0);
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
                // вначале проверяем, нет ли в таблице блока с таким именем если есть - выводим сообщение об ошибке и заканчиваем выполнение команды
                if (blocktable.Has(LocalNameSign))
                {
                    _=MessageBox.Show("Блок с именем (" + LocalNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // создаем новое определение блока, задаем ему имя
                BlockTableRecord LocalBlock = new BlockTableRecord() { Name = LocalNameSign };
                // запоминаем ID созданного определения блока 
                ObjectId LocalBlockId = blocktable.Add(LocalBlock);
                tr.AddNewlyCreatedDBObject(LocalBlock, true);
                //Запоминание текущего слоя для возврата к нему
                string CurrentLayer = LocalDataSign.CurrentLayerData();
                //Подготавливаем слои
                LocalDataSign.PreparationLayer();
                //Слой знака LocalDataSign
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                //РАЗДЕЛЕНИЕ ПО ТИПАМ ЗНАКОВ
                if (marker01 == ConstSignStop)
                #region STOP
                {
                    //Создаем круг
                    Vector3d normal = new Vector3d(0.0, 0.0, 1.0);
                    double LocalRadius = 5 * S.HeigthTextSign / 6;
                    Circle circle = new Circle(new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius, BasePoint.Z), normal, LocalRadius);
                    LocalBlock.AppendEntity(circle);
                    tr.AddNewlyCreatedDBObject(circle, true);

                    //линия стойка
                    Line line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //линия основание стойки
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //нижняя горизонталь прямоугольника
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //верхняя горизонталь прямоугольника
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //вертикаль №1 прямоугольника
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //вертикаль №2 прямоугольника
                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);
                    //снизу слева вверх  - косая
                    line = new Line(new Point3d(BasePoint.X - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z),
                                    new Point3d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //снизу справа вверх - косая
                    line = new Line(new Point3d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z),
                                    new Point3d(BasePoint.X - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    // Создаем однострочный текстовый объект  LocalDataSign
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Слой текста
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 1.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = "150м";
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 1.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);
                }
                #endregion
                if (marker01 == ConstSignJ)
                #region 
                {
                    //Создаем круг
                    Vector3d normal = new Vector3d(0.0, 0.0, 1.0);
                    double LocalRadius = 5 * S.HeigthTextSign / 6;
                    Circle circle = new Circle(new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius, BasePoint.Z), normal, LocalRadius);
                    LocalBlock.AppendEntity(circle);
                    tr.AddNewlyCreatedDBObject(circle, true);

                    // создаем линии
                    //линия стойка
                    Line line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //линия основание стойки
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y, BasePoint.Z),
                                    new Point3d(BasePoint.X + S.HeigthTextSign / 2, BasePoint.Y, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //нижняя горизонталь прямоугольника
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //верхняя горизонталь прямоугольника
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //вертикаль №1 прямоугольника
                    line = new Line(new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X - 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));

                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //вертикаль №1 прямоугольника
                    line = new Line(new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign, BasePoint.Z),
                                    new Point3d(BasePoint.X + 5 * S.HeigthTextSign / 6, BasePoint.Y + S.HeigthTextSign * 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //снизу слева вверх  - косая
                    line = new Line(new Point3d(BasePoint.X - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z),
                                    new Point3d(BasePoint.X + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //стойка якоря
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius / 2, BasePoint.Z),
                                    new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius + LocalRadius / 2, BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //левый якорь
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius / 2, BasePoint.Z),
                                    new Point3d(BasePoint.X - LocalRadius / 4 * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius / 2 + LocalRadius / 2 * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    //левый якорь
                    line = new Line(new Point3d(BasePoint.X, BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius / 2, BasePoint.Z),
                                    new Point3d(BasePoint.X + LocalRadius / 4 * Math.Sin(Math.PI / 4), BasePoint.Y + 2 * S.HeigthTextSign + LocalRadius - LocalRadius / 2 + LocalRadius / 2 * Math.Sin(Math.PI / 4), BasePoint.Z));
                    LocalBlock.AppendEntity(line);
                    tr.AddNewlyCreatedDBObject(line, true);

                    // Создаем однострочный текстовый объект LocalDataSign
                    LocalDataSign.ActiveLayerSetup(LayerWorks.layersigntext); //Слой текст
                    DBText Text = new DBText();
                    Text.SetDatabaseDefaults();
                    Text.Position = new Point3d(BasePoint.X, BasePoint.Y + 1.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.Height = S.HeigthTextSign - 0.5 * 2;
                    Text.Color = Color.FromColorIndex(ColorMethod.ByColor, 1);
                    Text.TextString = "200м";
                    Text.HorizontalMode = TextHorizontalMode.TextCenter;
                    Text.VerticalMode = TextVerticalMode.TextVerticalMid;
                    Text.AlignmentPoint = new Point3d(BasePoint.X, BasePoint.Y + 1.5 * S.HeigthTextSign, BasePoint.Z);
                    Text.WidthFactor = S.KoeffTextShortNameSign;
                    LocalBlock.AppendEntity(Text);
                    tr.AddNewlyCreatedDBObject(Text, true);

                }
                #endregion

                #region Общие параметры для знаков
                //Добавляем атрибут №1 
                LocalDataSign.ActiveLayerSetup(LayerWorks.layerattsign); //Слой атрибута
                BasePoint = new Point3d(BasePoint.X + S.HeigthTextSign, BasePoint.Y + 3 * S.HeigthTextSign - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt01,
                    Tag = LocalTag01,
                    TextString = LocalValueAtt01,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock01);
                tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                //Добавляем атрибут №2
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt02,
                    Tag = LocalTag02,
                    TextString = LocalValueAtt02,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock02);
                tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                //Добавляем атрибут №3
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt03,
                    Tag = LocalTag03,
                    TextString = LocalValueAtt03,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock03);
                tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                //Добавляем атрибут №4
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt04,
                    Tag = LocalTag04,
                    TextString = LocalValueAtt04,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock04);
                tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                //Добавляем атрибут №5
                BasePoint = new Point3d(BasePoint.X, BasePoint.Y - S.TextAttributeDelta, BasePoint.Z);
                AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                {
                    Position = BasePoint,
                    Prompt = LocalPrompt05,
                    Tag = LocalTag05,
                    TextString = LocalValueAtt05,
                    Height = S.TextAttribute,
                    HorizontalMode = TextHorizontalMode.TextLeft,
                    VerticalMode = TextVerticalMode.TextBottom,
                    Visible = true,
                    AlignmentPoint = BasePoint
                };
                LocalBlock.AppendEntity(AttributeBlock05);
                tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                // открываем пространство модели на запись LocalDataSign
                BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                LocalDataSign.ActiveLayerSetup(LayerWorks.layersign);

                // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                BlockReference br = new BlockReference(InsPoint, LocalBlockId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                // добавляем созданное вхождение блока на пространство модели и в транзакцию
                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                // добавляем экземпляр ссылки на объект
                AttributeReference AttributeRef01 = new AttributeReference();
                AttributeReference AttributeRef02 = new AttributeReference();
                AttributeReference AttributeRef03 = new AttributeReference();
                AttributeReference AttributeRef04 = new AttributeReference();
                AttributeReference AttributeRef05 = new AttributeReference();

                AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                AttributeRef01.TextString = LocalValueAtt01;
                AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                AttributeRef02.TextString = LocalValueAtt02;
                AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                AttributeRef03.TextString = LocalValueAtt03;
                AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                AttributeRef04.TextString = LocalValueAtt04;
                AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                AttributeRef05.TextString = LocalValueAtt05;

                // Добавляем AttributeReference к BlockReference
                br.AttributeCollection.AppendAttribute(AttributeRef01);
                tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                br.AttributeCollection.AppendAttribute(AttributeRef02);
                tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                br.AttributeCollection.AppendAttribute(AttributeRef03);
                tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                br.AttributeCollection.AppendAttribute(AttributeRef04);
                tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                br.AttributeCollection.AppendAttribute(AttributeRef05);
                tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                //восстанавливаем предыдущий текущий слой
                LocalDataSign.ActiveLayerSetup(CurrentLayer);
                #endregion

                tr.Commit();
            }
        }
        #endregion

        #region Методы проверок уже существующих знаков
        //Метод проверок на уже созданные обычные знаки
        public static void CreateBlockSignIdenIfExist(double PX, double PY, double PZ, string LocalNameSign,
                                               string LocalValueAtt01, string LocalTag01,
                                               string LocalValueAtt02, string LocalTag02,
                                               string LocalValueAtt03, string LocalTag03,
                                               string LocalValueAtt04, string LocalTag04,
                                               string LocalValueAtt05, string LocalTag05,
                                               double LocalAngleBlock
                                               )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //Получаем таблицу блоков текущего чертежа
                BlockTable blocktable = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                //Получаем прямую ссылку на блок
                BlockTableRecord LocalBlockRef = blocktable[LocalNameSign].GetObject(OpenMode.ForRead) as BlockTableRecord;

                // Открываем пространство модели – мы добавляем наш BlockReference к нему
                BlockTableRecord ms = blocktable[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;

                //Включаем слои 
                LayerWorks LocalMyWorkLayer = new LayerWorks();
                string CurrentLayer = LocalMyWorkLayer.CurrentLayerData();
                LocalMyWorkLayer.ActiveLayerSetup(LayerWorks.layersign);

                // получаем координаты точки вставки и вставляем экземпляр блока
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                BlockReference br = new BlockReference(InsPoint, LocalBlockRef.ObjectId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                foreach (ObjectId id in LocalBlockRef)
                {
                    DBObject obj = id.GetObject(OpenMode.ForRead);
                    if ((obj is AttributeDefinition attDef) && (!attDef.Constant))
                        using (AttributeReference attRef = new AttributeReference())
                        {
                            attRef.SetAttributeFromBlock(attDef, br.BlockTransform);
                            if (attRef.Tag == LocalTag01)
                            {
                                attRef.TextString = LocalValueAtt01;
                            }
                            if (attRef.Tag == LocalTag02)
                            {
                                attRef.TextString = LocalValueAtt02;
                            }
                            if (attRef.Tag == LocalTag03)
                            {
                                attRef.TextString = LocalValueAtt03;
                            }
                            if (attRef.Tag == LocalTag04)
                            {
                                attRef.TextString = LocalValueAtt04;
                            }
                            if (attRef.Tag == LocalTag05)
                            {
                                attRef.TextString = LocalValueAtt05;
                            }
                            // Добавляем AttributeReference к BlockReference
                            br.AttributeCollection.AppendAttribute(attRef);
                            tr.AddNewlyCreatedDBObject(attRef, true);
                        }
                }
                LocalMyWorkLayer.ActiveLayerSetup(CurrentLayer);
                tr.Commit();
            }
        }
        //Метод проверок на уже созданные километровые знаки
        public static void CreateBlockSignIdenIfExist(double PX, double PY, double PZ, string LocalNameSign,
                                               string LocalValueAtt01, string LocalTag01,
                                               string LocalValueAtt02, string LocalTag02,
                                               string LocalValueAtt03, string LocalTag03,
                                               string LocalValueAtt04, string LocalTag04,
                                               string LocalValueAtt05, string LocalTag05,
                                               string LocalValueAtt06, string LocalTag06,
                                               double LocalAngleBlock
                                               )
        {
            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                //Получаем таблицу блоков текущего чертежа
                BlockTable blocktable = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

                //Получаем прямую ссылку на блок
                BlockTableRecord LocalBlockRef = blocktable[LocalNameSign].GetObject(OpenMode.ForRead) as BlockTableRecord;

                // Открываем пространство модели – мы добавляем наш BlockReference к нему
                BlockTableRecord ms = blocktable[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;

                //Включаем слои 
                LayerWorks LocalMyWorkLayer = new LayerWorks();
                string CurrentLayer = LocalMyWorkLayer.CurrentLayerData();
                LocalMyWorkLayer.ActiveLayerSetup(LayerWorks.layersign);

                // получаем координаты точки вставки и вставляем экземпляр блока
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                BlockReference br = new BlockReference(InsPoint, LocalBlockRef.ObjectId);

                //Разворот ссылки блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(LocalAngleBlock, Vector3d.ZAxis, InsPoint));

                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                foreach (ObjectId id in LocalBlockRef)
                {
                    DBObject obj = id.GetObject(OpenMode.ForRead);
                    if ((obj is AttributeDefinition attDef) && (!attDef.Constant))
                        using (AttributeReference attRef = new AttributeReference())
                        {
                            attRef.SetAttributeFromBlock(attDef, br.BlockTransform);
                            if (attRef.Tag == LocalTag01)
                            {
                                attRef.TextString = LocalValueAtt01;
                            }
                            if (attRef.Tag == LocalTag02)
                            {
                                attRef.TextString = LocalValueAtt02;
                            }
                            if (attRef.Tag == LocalTag03)
                            {
                                attRef.TextString = LocalValueAtt03;
                            }
                            if (attRef.Tag == LocalTag04)
                            {
                                attRef.TextString = LocalValueAtt04;
                            }
                            if (attRef.Tag == LocalTag05)
                            {
                                attRef.TextString = LocalValueAtt05;
                            }
                            if (attRef.Tag == LocalTag06)
                            {
                                attRef.TextString = LocalValueAtt06;
                            }
                            // Добавляем AttributeReference к BlockReference
                            br.AttributeCollection.AppendAttribute(attRef);
                            tr.AddNewlyCreatedDBObject(attRef, true);
                        }
                }
                LocalMyWorkLayer.ActiveLayerSetup(CurrentLayer);
                tr.Commit();
            }
        }
        #endregion

        #region Методы создания блоков ПОС
        // public static void CreateBlockPodSignIden(double PX, double PY, double PZ, double AngleBlock, int index)
        public static void CreateBlockPodSignIdenOneRack(BlockReference blockReference, int index)
        {
            // геометрия блока
            double PX = blockReference.Position.X;
            double PY = blockReference.Position.Y;
            double PZ = blockReference.Position.Z;
            double AngleBlock = blockReference.Rotation;
            //ссылка на атрибуты блока
            //Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = blockReference.AttributeCollection;
            // доступ к папку
            AccessToDocument AcToDraw = new AccessToDocument();
            //получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            //ссылка на слои
            LayerWorks layer = new LayerWorks();
            //начинаем транзакцию
            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
            {
                string posNameSign = SignBase.posNameSign + OneRack;
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForWrite);
                if (blocktable.Has(posNameSign))
                    {
                        MessageBox.Show("Блок с именем (" + posNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // создаем новое определение блока, задаем ему имя
                    BlockTableRecord block = new BlockTableRecord() { Name = posNameSign };
                    // запоминаем ID созданного определения блока 
                    ObjectId blockId = blocktable.Add(block);
                    tr.AddNewlyCreatedDBObject(block, true);
                    //Запоминаем текущий слой
                    string CurrentLayer = layer.CurrentLayerData();
                    //Подготавливаем слои
                    layer.PreparationLayer();
                    //Слой знака
                    layer.ActiveLayerSetup(LayerWorks.layerPOSconstEarth);

                    //Базовые координаты
                    Point3d BasePoint = new Point3d(0, 0, 0);
                    Point3d InsPoint = new Point3d(PX, PY, PZ);

                    //Создаем полилинию
                    Polyline poly = new Polyline();

                    poly.AddVertexAt(0, new Point2d(BasePoint.X - posLengthOne / 2, BasePoint.Y + posLengthOne / 2), 0, 0, 0);
                    poly.AddVertexAt(0, new Point2d(BasePoint.X + posLengthOne / 2, BasePoint.Y + posLengthOne / 2), 0, 0, 0);
                    poly.AddVertexAt(0, new Point2d(BasePoint.X + posLengthOne / 2, BasePoint.Y - posLengthOne / 2), 0, 0, 0);
                    poly.AddVertexAt(0, new Point2d(BasePoint.X - posLengthOne / 2, BasePoint.Y - posLengthOne / 2), 0, 0, 0);
                    poly.AddVertexAt(0, new Point2d(BasePoint.X - posLengthOne / 2, BasePoint.Y + posLengthOne / 2), 0, 0, 0);

                    poly.ConstantWidth = 0.15;
                    poly.Closed = false;
                    block.AppendEntity(poly);
                    tr.AddNewlyCreatedDBObject(poly, true);

                    #region Атрибуты для заполения углов - нумерация углов
                    layer.ActiveLayerSetup(LayerWorks.layerattPOSconstEarth);

                    //Добавляем атрибут №1
                    AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number01,
                        TextString = (4 * index - 3).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X - posLengthOne / 2, BasePoint.Y + posLengthOne / 2, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock01);
                    tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                    //Добавляем атрибут №2
                    AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number02,
                        TextString = (4 * index - 2).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X + posLengthOne / 2, BasePoint.Y + posLengthOne / 2, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock02);
                    tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                    //Добавляем атрибут №3
                    AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number03,
                        TextString = (4 * index - 1).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X + posLengthOne / 2, BasePoint.Y - posLengthOne / 2, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock03);
                    tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                    //Добавляем атрибут №4
                    AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number04,
                        TextString = (4 * index - 0).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X - posLengthOne / 2, BasePoint.Y - posLengthOne / 2, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock04);
                    tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                    //Добавляем атрибут №5
                    AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = numberBlock,
                        TextString = (index).ToString(),
                        Height = posLengthOne / 2,
                        HorizontalMode = TextHorizontalMode.TextMid,
                        VerticalMode = TextVerticalMode.TextVerticalMid,
                        Visible = true,
                        AlignmentPoint = BasePoint
                    };
                    block.AppendEntity(AttributeBlock05);
                    tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                    #endregion Атрибуды для заполения углов - нумерация углов


                    // открываем пространство модели на запись LocalDataSign
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    //Слой знака
                    layer.ActiveLayerSetup(LayerWorks.layerPOSconstEarth);
                    // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                    BlockReference br = new BlockReference(InsPoint, blockId);

                    // разворот вставки блока на заданный радиус
                    br.TransformBy(Matrix3d.Rotation(AngleBlock, Vector3d.ZAxis, InsPoint));

                    // добавляем созданное вхождение блока на пространство модели и в транзакцию
                    ms.AppendEntity(br);
                    tr.AddNewlyCreatedDBObject(br, true);

                    // добавляем экземпляр ссылки на объект
                    AttributeReference AttributeRef01 = new AttributeReference();
                    AttributeReference AttributeRef02 = new AttributeReference();
                    AttributeReference AttributeRef03 = new AttributeReference();
                    AttributeReference AttributeRef04 = new AttributeReference();
                    AttributeReference AttributeRef05 = new AttributeReference();

                    AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                    AttributeRef01.TextString = (4 * index - 3).ToString();
                    AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                    AttributeRef02.TextString = (4 * index - 2).ToString();
                    AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                    AttributeRef03.TextString = (4 * index - 1).ToString();
                    AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                    AttributeRef04.TextString = (4 * index - 0).ToString();
                    AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                    AttributeRef05.TextString = (index).ToString();

                    // Добавляем AttributeReference к BlockReference
                    br.AttributeCollection.AppendAttribute(AttributeRef01);
                    tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef02);
                    tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef03);
                    tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef04);
                    tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef05);
                    tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                    layer.ActiveLayerSetup(CurrentLayer);
                    tr.Commit();
            }
        }

        public static void CreateBlockPodSignIdenTwoRack(BlockReference blockReference, int index)
        {
            // геометрия блока
            double PX = blockReference.Position.X;
            double PY = blockReference.Position.Y;
            double PZ = blockReference.Position.Z;
            double AngleBlock = blockReference.Rotation;
            //ссылка на атрибуты блока
            //Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = blockReference.AttributeCollection;
            // доступ к папку 
            AccessToDocument AcToDraw = new AccessToDocument();  
            // получаем ссылку на БД
            Database AcadDB = AcToDraw.DBase;
            // ссылка на слои
            LayerWorks layer = new LayerWorks();  
            // начинаем транзакцию 
            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())  
            {
                string posNameSign = SignBase.posNameSign + TwoRack;
                // открываем таблицу блоков на запись
                BlockTable blocktable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForWrite);
                    if (blocktable.Has(posNameSign))
                    {
                        MessageBox.Show("Блок с именем (" + posNameSign + ") уже существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // создаем новое определение блока, задаем ему имя
                    BlockTableRecord block = new BlockTableRecord() { Name = posNameSign };
                    // запоминаем ID созданного определения блока 
                    ObjectId blockId = blocktable.Add(block);
                    tr.AddNewlyCreatedDBObject(block, true);
                    //Запоминаем текущий слой
                    string CurrentLayer = layer.CurrentLayerData();
                    //Подготавливаем слои
                    layer.PreparationLayer();
                    //Слой знака
                    layer.ActiveLayerSetup(LayerWorks.layerPOSconstEarth);

                    //Базовые координаты
                    Point3d BasePoint = new Point3d(0, 0, 0);
                    Point3d InsPoint = new Point3d(PX, PY, PZ);

                    //Создаем полилинию
                    Polyline poly = new Polyline();

                        
                poly.AddVertexAt(0, new Point2d(BasePoint.X - posLengthOne / 2, BasePoint.Y + posLengthOne), 0, 0, 0);   
                poly.AddVertexAt(0, new Point2d(BasePoint.X + posLengthOne / 2, BasePoint.Y + posLengthOne), 0, 0, 0); 
                poly.AddVertexAt(0, new Point2d(BasePoint.X + posLengthOne / 2, BasePoint.Y - posLengthOne), 0, 0, 0);  
                poly.AddVertexAt(0, new Point2d(BasePoint.X - posLengthOne / 2, BasePoint.Y - posLengthOne), 0, 0, 0);    
                poly.AddVertexAt(0, new Point2d(BasePoint.X - posLengthOne / 2, BasePoint.Y + posLengthOne), 0, 0, 0);

                    poly.ConstantWidth = 0.15;
                    poly.Closed = false;
                    block.AppendEntity(poly);
                    tr.AddNewlyCreatedDBObject(poly, true);

                    #region Атрибуты для заполения углов - нумерация углов
                    layer.ActiveLayerSetup(LayerWorks.layerattPOSconstEarth);

                    //Добавляем атрибут №1
                    AttributeDefinition AttributeBlock01 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number01,
                        TextString = (4 * index - 3).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X - posLengthOne / 2, BasePoint.Y + posLengthOne, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock01);
                    tr.AddNewlyCreatedDBObject(AttributeBlock01, true);

                    //Добавляем атрибут №2
                    AttributeDefinition AttributeBlock02 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number02,
                        TextString = (4 * index - 2).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X + posLengthOne / 2, BasePoint.Y + posLengthOne, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock02);
                    tr.AddNewlyCreatedDBObject(AttributeBlock02, true);

                    //Добавляем атрибут №3
                    AttributeDefinition AttributeBlock03 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number03,
                        TextString = (4 * index - 1).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X + posLengthOne / 2, BasePoint.Y - posLengthOne, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock03);
                    tr.AddNewlyCreatedDBObject(AttributeBlock03, true);

                    //Добавляем атрибут №4
                    AttributeDefinition AttributeBlock04 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = number04,
                        TextString = (4 * index - 0).ToString(),
                        Height = posLengthOne / 3,
                        HorizontalMode = TextHorizontalMode.TextLeft,
                        VerticalMode = TextVerticalMode.TextBottom,
                        Visible = true,
                        AlignmentPoint = new Point3d(BasePoint.X - posLengthOne / 2, BasePoint.Y - posLengthOne, BasePoint.Z)
                    };
                    block.AppendEntity(AttributeBlock04);
                    tr.AddNewlyCreatedDBObject(AttributeBlock04, true);

                    //Добавляем атрибут №5
                    AttributeDefinition AttributeBlock05 = new AttributeDefinition()
                    {
                        Position = BasePoint,
                        Prompt = "",
                        Tag = numberBlock,
                        TextString = (index).ToString(),
                        Height = posLengthOne / 2,
                        HorizontalMode = TextHorizontalMode.TextMid,
                        VerticalMode = TextVerticalMode.TextVerticalMid,
                        Visible = true,
                        AlignmentPoint = BasePoint
                    };
                    block.AppendEntity(AttributeBlock05);
                    tr.AddNewlyCreatedDBObject(AttributeBlock05, true);

                    #endregion Атрибуды для заполения углов - нумерация углов


                    // открываем пространство модели на запись LocalDataSign
                    BlockTableRecord ms = (BlockTableRecord)tr.GetObject(blocktable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                    //Слой знака
                    layer.ActiveLayerSetup(LayerWorks.layerPOSconstEarth);
                    // создаем новое вхождение блока, используя ранее сохраненный ID определения блока
                    BlockReference br = new BlockReference(InsPoint, blockId);

                    // разворот вставки блока на заданный радиус
                    br.TransformBy(Matrix3d.Rotation(AngleBlock, Vector3d.ZAxis, InsPoint));

                    // добавляем созданное вхождение блока на пространство модели и в транзакцию
                    ms.AppendEntity(br);
                    tr.AddNewlyCreatedDBObject(br, true);

                    // добавляем экземпляр ссылки на объект
                    AttributeReference AttributeRef01 = new AttributeReference();
                    AttributeReference AttributeRef02 = new AttributeReference();
                    AttributeReference AttributeRef03 = new AttributeReference();
                    AttributeReference AttributeRef04 = new AttributeReference();
                    AttributeReference AttributeRef05 = new AttributeReference();

                    AttributeRef01.SetAttributeFromBlock(AttributeBlock01, br.BlockTransform);
                    AttributeRef01.TextString = (4 * index - 3).ToString();
                    AttributeRef02.SetAttributeFromBlock(AttributeBlock02, br.BlockTransform);
                    AttributeRef02.TextString = (4 * index - 2).ToString();
                    AttributeRef03.SetAttributeFromBlock(AttributeBlock03, br.BlockTransform);
                    AttributeRef03.TextString = (4 * index - 1).ToString();
                    AttributeRef04.SetAttributeFromBlock(AttributeBlock04, br.BlockTransform);
                    AttributeRef04.TextString = (4 * index - 0).ToString();
                    AttributeRef05.SetAttributeFromBlock(AttributeBlock05, br.BlockTransform);
                    AttributeRef05.TextString = (index).ToString();

                    // Добавляем AttributeReference к BlockReference
                    br.AttributeCollection.AppendAttribute(AttributeRef01);
                    tr.AddNewlyCreatedDBObject(AttributeRef01, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef02);
                    tr.AddNewlyCreatedDBObject(AttributeRef02, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef03);
                    tr.AddNewlyCreatedDBObject(AttributeRef03, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef04);
                    tr.AddNewlyCreatedDBObject(AttributeRef04, true);
                    br.AttributeCollection.AppendAttribute(AttributeRef05);
                    tr.AddNewlyCreatedDBObject(AttributeRef05, true);

                    layer.ActiveLayerSetup(CurrentLayer);
                    tr.Commit();

            }
        }

        public static void CreateBlockPodSignIdenIfExist(BlockReference blockReference, string nameBlock, int index)
        {
            // геометрия блока
            double PX = blockReference.Position.X;
            double PY = blockReference.Position.Y;
            double PZ = blockReference.Position.Z;
            double AngleBlock = blockReference.Rotation;

            // получаем ссылку на документ
            Document AcadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            if (AcadDoc == null) return;
            // получаем ссылку на БД
            Database db = AcadDoc.Database;
            // ссылка на слои
            LayerWorks layer = new LayerWorks();
            // начинаем транзакцию
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // Получаем таблицу блоков текущего чертежа
                BlockTable blocktable = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                //Получаем прямую ссылку на блок
                BlockTableRecord LocalBlockRef = blocktable[nameBlock].GetObject(OpenMode.ForRead) as BlockTableRecord;
                // Открываем пространство модели – мы добавляем наш BlockReference к нему
                BlockTableRecord ms = blocktable[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;

                // Запоминаем текущий слой
                string CurrentLayer = layer.CurrentLayerData();
                // Подготавливаем слои
                layer.PreparationLayer();
                // Слой знака
                layer.ActiveLayerSetup(LayerWorks.layerPOSconstEarth);

                // получаем координаты точки вставки и вставляем экземпляр блока
                Point3d InsPoint = new Point3d(PX, PY, PZ);
                BlockReference br = new BlockReference(InsPoint, LocalBlockRef.ObjectId);

                // Разворот блока на заданный радиус
                br.TransformBy(Matrix3d.Rotation(AngleBlock, Vector3d.ZAxis, InsPoint));

                ms.AppendEntity(br);
                tr.AddNewlyCreatedDBObject(br, true);

                foreach (ObjectId id in LocalBlockRef)
                {
                    DBObject obj = id.GetObject(OpenMode.ForRead);
                    if ((obj is AttributeDefinition attDef) && (!attDef.Constant))
                        using (AttributeReference attRef = new AttributeReference())
                        {
                            attRef.SetAttributeFromBlock(attDef, br.BlockTransform);
                            if (attRef.Tag == number01)
                            {
                                attRef.TextString = (4 * index - 3).ToString();
                            }
                            if (attRef.Tag == number02)
                            {
                                attRef.TextString = (4 * index - 2).ToString();
                            }
                            if (attRef.Tag == number03)
                            {
                                attRef.TextString = (4 * index - 1).ToString();
                            }
                            if (attRef.Tag == number04)
                            {
                                attRef.TextString = (4 * index - 0).ToString();
                            }
                            if (attRef.Tag == numberBlock)
                            {
                                attRef.TextString = (index).ToString();
                            }
                            // Добавляем AttributeReference к BlockReference
                            br.AttributeCollection.AppendAttribute(attRef);
                            tr.AddNewlyCreatedDBObject(attRef, true);
                        }
                }
                layer.ActiveLayerSetup(CurrentLayer);                
                tr.Commit();
            }
        }
        #endregion Методы создания блоков ПОС
    }
}
