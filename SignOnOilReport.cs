using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using AccessToDocument = AcadMain.AccessToDocument;
using PK = AcadMain.PK;


namespace GlobalSign
{
   
    //Класс работы с реперами    
    public class DataReper
    {
        #region Константы заполнения репер тип 1
        //Константа репера тип1 база металл
        public const double Type01ReperMetalMain = 87.67;
        //Константа репера тип1 переменный металл
        public const double Type01ReperMetalDiff57 = 5.28;
        //Константа репера тип1 переменный металл
        public const double Type01ReperMetalDiff89 = 0.0;
        //Константа репера тип1 бетон
        public const double Type01ReperBeton = 0.054;
        //Константа репера тип1 песок
        public const double Type01ReperSand = 0.2;
        //Константа репера тип1 смазка
        public const double Type01ReperGrease = 0;
        //Константа репера тип1 диаметр бурения
        public const double Type01ReperDiam = 500;
        //АКЗ подземный репера
        public const double Type01ReperAKZEarth = 1.87;
        //АКЗ подземный репера переменный
        public const double Type01ReperAKZEarthDiff = 0.20;
        //АКЗ блока бетонного
        public const double Type01ReperAKZBeton = 0.81;
        //АКЗ надземный репера блока бетонного
        public const double Type01ReperAKZUnder = 1.58;

        //Константа репера тип2 база металл
        public const double Type02ReperMetalMain = 87.67;
        //Константа репера тип2 переменный металл
        public const double Type02ReperMetalDiff57 = 5.28;
        //Константа репера тип2 переменный металл
        public const double Type02ReperMetalDiff89 = 0.0;
        //Константа репера тип2 бетон
        public const double Type02ReperBeton = 0.054;
        //Константа репера тип2 песок
        public const double Type02ReperSand = 0.2;
        //Константа репера тип2 смазка
        public const double Type02ReperGrease = 0;
        //Константа репера тип2 диаметр бурения
        public const double Type02ReperDiam = 500;
        //АКЗ подземный репера
        public const double Type02ReperAKZEarth = 1.87;
        //АКЗ подземный репера переменный
        public const double Type02ReperAKZEarthDiff = 0.20;
        //АКЗ блока бетонного
        public const double Type02ReperAKZBeton = 0.81;
        //АКЗ надземный репера блока бетонного
        public const double Type02ReperAKZUnder = 1.58;

        //Константа репера тип3 база металл
        public const double Type03ReperMetalMain = 88.67;
        //Константа репера тип3 переменный металл
        public const double Type03ReperMetalDiff57 = 5.28;
        //Константа репера тип3 переменный металл
        public const double Type03ReperMetalDiff89 = 9.47;
        //Константа репера тип3 бетон
        public const double Type03ReperBeton = 0.054;
        //Константа репера тип3 песок
        public const double Type03ReperSand = 0.2;
        //Константа репера тип3 смазка
        public const double Type03ReperGrease = 0.0025;
        //Константа репера тип3 диаметр бурения
        public const double Type03ReperDiam = 500;
        //АКЗ подземный репера
        public const double Type03ReperAKZEarth = 1.87;
        //АКЗ подземный репера переменный
        public const double Type03ReperAKZEarthDiff = 0.50;
        //АКЗ блока бетонного
        public const double Type03ReperAKZBeton = 0.81;
        //АКЗ надземный репера блока бетонного
        public const double Type03ReperAKZUnder = 1.58;

        //Константа репера тип4 база металл
        public const double Type04ReperMetalMain = 87.67;
        //Константа репера тип4 переменный металл
        public const double Type04ReperMetalDiff57 = 5.28;
        //Константа репера тип3 переменный металл
        public const double Type04ReperMetalDiff89 = 9.47;
        //Константа репера тип4 бетон
        public const double Type04ReperBeton = 0.054;
        //Константа репера тип4 песок
        public const double Type04ReperSand = 0.2;
        //Константа репера тип4 смазка
        public const double Type04ReperGrease = 0.0025;
        //Константа репера тип4 диаметр бурения
        public const double Type04ReperDiam = 500;
        //АКЗ подземный репера
        public const double Type04ReperAKZEarth = 1.87;
        //АКЗ подземный репера переменный
        public const double Type04ReperAKZEarthDiff = 0.50;
        //АКЗ блока бетонного
        public const double Type04ReperAKZBeton = 0.81;
        //АКЗ надземный репера блока бетонного
        public const double Type04ReperAKZUnder = 1.58;

        //Константа репера тип6 база металл
        public const double Type06ReperMetalMain = 83.88;
        //Константа репера тип6 переменный металл
        public const double Type06ReperMetalDiff57 = 5.28;
        //Константа репера тип6 переменный металл
        public const double Type06ReperMetalDiff89 = 0;
        //Константа репера тип6 бетон
        public const double Type06ReperBeton = 0;
        //Константа репера тип6 песок
        public const double Type06ReperSand = 0.06;
        //Константа репера тип6 смазка
        public const double Type06ReperGrease = 0;
        //Константа репера тип4 диаметр бурения
        public const double Type06ReperDiam = 57;
        //АКЗ подземный репера
        public const double Type06ReperAKZEarth = 1.78;
        //АКЗ подземный репера переменный
        public const double Type06ReperAKZEarthDiff = 0.31;
        //АКЗ блока бетонного
        public const double Type06ReperAKZBeton = 0.00;
        //АКЗ надземный репера блока бетонного
        public const double Type06ReperAKZUnder = 1.58;

        //Константа репера тип71 база металл
        public const double Type71ReperMetalMain = 87.67;
        //Константа репера тип71 переменный металл
        public const double Type71ReperMetalDiff57 = 5.28;
        //Константа репера тип71 переменный металл
        public const double Type71ReperMetalDiff89 = 0;
        //Константа репера тип71 бетон
        public const double Type71ReperBeton = 0.054;
        //Константа репера тип71 песок
        public const double Type71ReperSand = 0.2;
        //Константа репера тип71 смазка
        public const double Type71ReperGrease = 0;
        //Константа репера тип71 диаметр бурения
        public const double Type71ReperDiam = 500;
        //АКЗ подземный репера
        public const double Type71ReperAKZEarth = 1.87;
        //АКЗ подземный репера переменный
        public const double Type71ReperAKZEarthDiff = 0.20;
        //АКЗ блока бетонного
        public const double Type71ReperAKZBeton = 0.81;
        //АКЗ надземный репера блока бетонного
        public const double Type71ReperAKZUnder = 1.58;

        //Константа репера тип72 база металл
        public const double Type72ReperMetalMain = 30.28;
        //Константа репера тип72 переменный металл
        public const double Type72ReperMetalDiff57 = 0.0;
        //Константа репера тип71 переменный металл
        public const double Type72ReperMetalDiff89 = 52.28;         //Фактически это 273 труба
        //Константа репера тип72 бетон
        public const double Type72ReperBeton = 0.021;
        //Константа репера тип72 песок
        public const double Type72ReperSand = 0.0;
        //Константа репера тип72 смазка
        public const double Type72ReperGrease = 0;
        //Константа репера тип72 диаметр бурения
        public const double Type72ReperDiam = 500;
        //АКЗ подземный репера
        public const double Type72ReperAKZEarth = 1.46;
        //АКЗ подземный репера переменный
        public const double Type72ReperAKZEarthDiff = 0.20;
        //АКЗ блока бетонного
        public const double Type72ReperAKZBeton = 0.42;
        //АКЗ надземный репера блока бетонного
        public const double Type72ReperAKZUnder = 1.66;

        #endregion

        #region Поля
        //Поле массы
        private double mass;
        //Поле бетон
        private double beton;
        //Поле песок
        private double sand;
        //Поле песок
        private double grease;
        //Поле диаметр бурения
        private double diam;
        //Поле типов реперов
        private readonly string[] typeReper;
        //Поле АКЗрозионного покрытия
        private double akzEarth;
        //Поле АКЗрозионного покрытия для бетона
        private double akzBeton;
        //Поле АКЗрозионного покрытия для бетона
        private double akzUnder;

        #endregion

        public DataReper(string localTypeReper, string localDeep)
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ".",
            };


            typeReper = new string[7] { "Тип 1", "Тип 2", "Тип 3", "Тип 4", "Тип 6", "Тип 7.1", "Тип 7.2" };
            //Исходные данные
            mass = 0; beton = 0; sand = 0; sand = 0; diam = 500;
            //Выборка по массе
            if (localTypeReper == typeReper[0]) mass = Type01ReperMetalMain + Type01ReperMetalDiff57 * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1);
            if (localTypeReper == typeReper[1]) mass = Type02ReperMetalMain + Type02ReperMetalDiff57 * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1);
            if (localTypeReper == typeReper[2]) mass = Type03ReperMetalMain + Type03ReperMetalDiff57 * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.5) + Type03ReperMetalDiff89 * (Convert.ToDouble(localDeep) - 1.0 + 0.2);
            if (localTypeReper == typeReper[3]) mass = Type04ReperMetalMain + Type04ReperMetalDiff57 * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.5) + Type04ReperMetalDiff89 * (Convert.ToDouble(localDeep) - 3.0 + 0.2);
            if (localTypeReper == typeReper[4]) mass = Type06ReperMetalMain + Type06ReperMetalDiff57 * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3);
            if (localTypeReper == typeReper[5]) mass = Type71ReperMetalMain + Type71ReperMetalDiff57 * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1);
            if (localTypeReper == typeReper[6]) mass = Type72ReperMetalMain + Type03ReperMetalDiff89 * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3);
            //Выборка по бетону
            if (localTypeReper == typeReper[0]) beton = Type01ReperBeton;
            if (localTypeReper == typeReper[1]) beton = Type02ReperBeton;
            if (localTypeReper == typeReper[2]) beton = Type03ReperBeton;
            if (localTypeReper == typeReper[3]) beton = Type04ReperBeton;
            //if (localTypeReper == typeReper[4]) return beton = Type06ReperBeton;
            if (localTypeReper == typeReper[5]) beton = Type71ReperBeton;
            if (localTypeReper == typeReper[6]) beton = Type72ReperBeton;
            //Выборка по песку
            if (localTypeReper == typeReper[0]) sand = Type01ReperSand * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3);
            if (localTypeReper == typeReper[1]) sand = Type02ReperSand * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3);
            if (localTypeReper == typeReper[2]) sand = Type03ReperSand * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3);
            if (localTypeReper == typeReper[3]) sand = Type04ReperSand * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3);
            if (localTypeReper == typeReper[4]) sand = Type06ReperSand * (Convert.ToDouble(localDeep, numberFormatInfo));
            if (localTypeReper == typeReper[5]) sand = Type71ReperSand * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3);
            //Выборка по смазке
            if (localTypeReper == typeReper[2]) grease = Type03ReperGrease * (Convert.ToDouble(localDeep, numberFormatInfo) - 1.0 + 0.2);
            if (localTypeReper == typeReper[3]) grease = Type04ReperGrease * (Convert.ToDouble(localDeep, numberFormatInfo) - 3.0 + 0.2);
            //Выборка по диаметру бурения
            if (localTypeReper == typeReper[4]) diam = Type06ReperDiam;
            if (localTypeReper == typeReper[6]) diam = Type72ReperDiam;

            //Выборка АКЗ подземный 
            if (localTypeReper == typeReper[0]) akzEarth = 2 * (Type01ReperAKZEarth + Type01ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1)); //2 - слоя
            if (localTypeReper == typeReper[1]) akzEarth = 2 * (Type02ReperAKZEarth + Type02ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1)); //2 - слоя
            if (localTypeReper == typeReper[2]) akzEarth = 2 * (Type03ReperAKZEarth + Type03ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1)); //2 - слоя
            if (localTypeReper == typeReper[3]) akzEarth = 2 * (Type04ReperAKZEarth + Type04ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1)); //2 - слоя
            if (localTypeReper == typeReper[4]) akzEarth = 2 * (Type06ReperAKZEarth + Type06ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3));       //2 - слоя
            if (localTypeReper == typeReper[5]) akzEarth = 2 * (Type71ReperAKZEarth + Type71ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) + 0.3 - 0.1)); //2 - слоя
            if (localTypeReper == typeReper[6]) akzEarth = 2 * (Type72ReperAKZEarth + Type72ReperAKZEarthDiff * (Convert.ToDouble(localDeep, numberFormatInfo) - 0.3));       //2 - слоя

            //Выборка АКЗ блока бетонного
            if (localTypeReper == typeReper[0]) akzBeton = 2 * Type01ReperAKZBeton; //2 - слоя
            if (localTypeReper == typeReper[1]) akzBeton = 2 * Type02ReperAKZBeton; //2 - слоя
            if (localTypeReper == typeReper[2]) akzBeton = 2 * Type03ReperAKZBeton; //2 - слоя
            if (localTypeReper == typeReper[3]) akzBeton = 2 * Type04ReperAKZBeton; //2 - слоя
            if (localTypeReper == typeReper[4]) akzBeton = 2 * Type06ReperAKZBeton; //2 - слоя
            if (localTypeReper == typeReper[5]) akzBeton = 2 * Type71ReperAKZBeton; //2 - слоя
            if (localTypeReper == typeReper[6]) akzBeton = 2 * Type72ReperAKZBeton; //2 - слоя

            //Выборка АКЗ надземный
            if (localTypeReper == typeReper[0]) akzUnder = Type01ReperAKZUnder;
            if (localTypeReper == typeReper[1]) akzUnder = Type02ReperAKZUnder;
            if (localTypeReper == typeReper[2]) akzUnder = Type03ReperAKZUnder;
            if (localTypeReper == typeReper[3]) akzUnder = Type04ReperAKZUnder;
            if (localTypeReper == typeReper[4]) akzUnder = Type06ReperAKZUnder;
            if (localTypeReper == typeReper[5]) akzUnder = Type71ReperAKZUnder;
            if (localTypeReper == typeReper[6]) akzUnder = Type72ReperAKZUnder;

        }
        public DataReper()
        {
            typeReper = new string[7] { "Тип 1", "Тип 2", "Тип 3", "Тип 4", "Тип 6", "Тип 7.1", "Тип 7.2" };
        }

        #region Доступ к полям
        //Поле массы металлоконструкции реперов
        public double Mass
        {
            get { return mass; }
            set { mass = value; }
        }
        //Поле потребности в бетоне
        public double Beton
        {
            get { return beton; }
            set { beton = value; }
        }
        //Поле потребности в песке
        public double Sand
        {
            get { return sand; }
            set { sand = value; }
        }
        //Поле потребности в смазке
        public double Grease
        {
            get { return grease; }
            set { grease = value; }
        }
        //Поле диаметр бурения
        public double Diam
        {
            get { return diam; }
            set { diam = value; }
        }
        //Поле типов реперов
        public string[] TypeReper
        {
            get { return typeReper; }
        }
        //Поле АКЗрозионного покрытия
        public double AkzEarth
        {
            get { return akzEarth; }
            set { akzEarth = value; }
        }
        //Поле АКЗрозионного покрытия
        public double AkzBeton
        {
            get { return akzBeton; }
            set { akzBeton = value; }
        }
        //Поле АКЗрозионного покрытия
        public double AkzUnder
        {
            get { return akzUnder; }
            set { akzUnder = value; }
        }
        #endregion
    }

    //Класс - ОБЪЕКТ(ЗНАКИ)
    public class Sign
    {
        #region Константы заполнения
        //Константа пустота для строк
        public const string EmptyString = "-";
        //Константа пустота для десятичных
        public const double EmptyDouble = 0;
        //Константа пустота для целых
        public const int EmptyInt = 0;
        #endregion

        #region Поля класса - параметры знаков
        //00-Значение километра - КМ
        private double km;
        //01-Значение пикета - ХХ+ХХ
        private string pk;
        //02-Имя блока
        private string nameSign;
        //03-Основание для установки знака
        private string baseSign;
        //04-Количество знаков
        private int countSign;
        //05-Место установки знака OneRack, TwoRack, OnFencing, OnPole
        private string accommodationSign;
        //06-Глубина установки знака
        private string deepSign;
        //07-Координата X
        private double xSign;
        //08-Координата Y
        private double ySign;
        //09-Координата Z
        private double zSign;
        //10-Слой блока
        private string layerSign;
        //11-Тип репера
        private string typeOfRep;
        //12-Глубина репера - бурения
        private string deepOfRep;
        //13-Наименование знака для спецификации
        private string nameSignSpecp;
        //14-Код АСУНСИ
        private string asunsi;
        //15-Подгруппа
        private string groupEq;
        //16-Поставщик
        private string typeProvider;
        //17-Масса
        private double mass;
        //18-Тип согласно ОТТ ОТТ-75.200.00-КТН-0412-22
        private string typeOtt;
        //18-Тип условный
        private string typeConditional;

        #endregion

        public Sign()
        {
            km = EmptyDouble;
            pk = EmptyString;
            nameSign = EmptyString;
            baseSign = EmptyString;
            countSign = EmptyInt;
            accommodationSign = EmptyString;
            deepSign = EmptyString;
            xSign = EmptyDouble;
            ySign = EmptyDouble;
            zSign = EmptyDouble;
            layerSign = EmptyString;
            typeOfRep = EmptyString;
            deepOfRep = EmptyString;
            nameSignSpecp = EmptyString;
            asunsi = EmptyString;
            groupEq = EmptyString;
            typeProvider = EmptyString;
            typeOtt = EmptyString;
            mass = 0;
        }

        #region Доступ к полям
        //00-Значение километра - КМ
        public double KM
        {
            get { return km; }
            set { km = value; }
        }
        //01-Значение пикета - ХХ+ХХ
        public string PK
        {
            get { return pk; }
            set { pk = value; }
        }
        //02-Имя блока
        public string NameSign
        {
            get { return nameSign; }
            set { nameSign = value; }
        }
        //03-Основание для установки знака
        public string BaseSign
        {
            get { return baseSign; }
            set { baseSign = value; }
        }
        //04-Количество знаков
        public int CountSign
        {
            get { return countSign; }
            set { countSign = value; }
        }
        //05-Место установки знака OneRack, TwoRack, OnFencing, OnPole
        public string AccommodationSign
        {
            get { return accommodationSign; }
            set { accommodationSign = value; }
        }
        //06-Глубина установки знака
        public string DeepSign
        {
            get { return deepSign; }
            set { deepSign = value; }
        }
        //07-Координата X
        public double XSign
        {
            get { return xSign; }
            set { xSign = value; }
        }
        //08-Координата Y
        public double YSign
        {
            get { return ySign; }
            set { ySign = value; }
        }
        //09-Координата Z
        public double ZSign
        {
            get { return zSign; }
            set { zSign = value; }
        }
        //10-Слой блока
        public string LayerSign
        {
            get { return layerSign; }
            set { layerSign = value; }
        }
        //11-Тип репера
        public string TypeOfRep
        {
            get { return typeOfRep; }
            set { typeOfRep = value; }
        }
        //12-Глубина репера - бурения
        public string DeepOfRep
        {
            get { return deepOfRep; }
            set { deepOfRep = value; }
        }
        //13-Наименование знака для спецификации
        public string NameSignSpecp
        {
            get { return nameSignSpecp; }
            set { nameSignSpecp = value; }
        }
        //14-Код АСУНСИ
        public string AsuNsi
        {
            get { return asunsi; }
            set { asunsi = value; }
        }
        //15-Подгруппа
        public string GroupEq
        {
            get { return groupEq; }
            set { groupEq = value; }

        }
        //16-Поставщик
        public string TypeProvider
        {
            get { return typeProvider; }
            set { typeProvider = value; }
        }
        //14-Масса
        public double Mass
        {
            get { return mass; }
            set { mass = value; }
        }
        //18-18-Тип согласно ОТТ ОТТ-75.200.00-КТН-0412-22
        public string TypeOtt
        {
            get { return typeOtt; }
            set { typeOtt = value; }
        }
        public string TypeConditional
        {
            get { return typeConditional; }
            set { typeConditional = value; }
        }


        #endregion
    }

    //Класс - СПЕЦИФИКАЦИИ
    public class Specp
    {
        #region Поля класса - параметры знаков
        //00-Наименование знака
        private string techName01;
        //01-Наименование знака
        private string techName02;
        //02-Наименование знака
        private string techName03;
        //03-Опросный лист
        private string opList01;
        //04-Опросный лист
        private string opList02;
        //05-Код АСУНСИ
        private string asunsi;
        //06-Код оборудования
        private string groupEq;
        //07-Единицы измерения
        private string initN;
        //08-количество
        private string countSpecp;
        //09-масса
        private string mass;
        //10-Примечание
        private string prim;
        //10-Примечание
        private string addPrim;
        //11-Глубина для ВР
        private string deepSign;
        //12-Поставщик
        private string custom;
        //13-Место установки
        private string accommodationSign;
        //14-Глубина установки репера
        private string deepReper;
        //15-Диаметр бурения репера
        private string diamReper;
        #endregion

        //пустой конструктор т.к. заполнять его будем при инициализации
        public Specp()
        {
            techName01 = "";
            techName02 = "";
            techName03 = "";
            opList01 = "";
            opList02 = "";
            asunsi = "";
            groupEq = "";
            initN = "";
            countSpecp = "";
            mass = "";
            prim = "";
            addPrim = "";
            deepSign = "";
            custom = "";
            accommodationSign = "";
            deepReper = "";
            diamReper = "";

        }

        #region Доступ к полям
        //00-Наименование знака
        public string TechName01
        {
            get { return techName01; }
            set { techName01 = value; }
        }
        //01-Наименование знака
        public string TechName02
        {
            get { return techName02; }
            set { techName02 = value; }
        }
        //02-Наименование знака
        public string TechName03
        {
            get { return techName03; }
            set { techName03 = value; }
        }
        //03-Опросный лист
        public string OpList01
        {
            get { return opList01; }
            set { opList01 = value; }
        }
        //04-Опросный лист
        public string OpList02
        {
            get { return opList02; }
            set { opList02 = value; }
        }
        //05-Код АСУНСИ
        public string AsuNsi
        {
            get { return asunsi; }
            set { asunsi = value; }
        }
        //06-Код оборудования
        public string GroupEq
        {
            get { return groupEq; }
            set { groupEq = value; }
        }
        //07-Единицы измерения
        public string InitN
        {
            get { return initN; }
            set { initN = value; }
        }
        //08-количество
        public string CountSpecp
        {
            get { return countSpecp; }
            set { countSpecp = value; }
        }
        //09-Координата Z
        public string Mass
        {
            get { return mass; }
            set { mass = value; }
        }
        //10-Слой блока
        public string Prim
        {
            get { return prim; }
            set { prim = value; }

            /*
            set
            {
                if (double.TryParse(value, out double Num) == true) prim = "Н=" + Num + "м";
                else prim = value;
            }*/
        }
        //Примечание дополнительное
        public string AddPrim
        {
            get { return addPrim; }
            set { addPrim = value; }
        }
        //11-Слой блока
        public string DeepSign
        {
            get { return deepSign; }
            set { deepSign = value; }
        }
        //12-Поставщик
        public string Custom
        {
            get { return custom; }
            set { custom = value; }
        }
        //13-Место установки
        public string AccommodationSign
        {
            get { return accommodationSign; }
            set { accommodationSign = value; }
        }
        //14-Глубина установки репера
        public string DeepReper
        {
            get { return deepReper; }
            set { deepReper = value; }
        }
        //15-Глубина установки репера
        public string DiamReper
        {
            get { return diamReper; }
            set { diamReper = value; }
        }

        #endregion
    }

    //Класс - ОБЪЕМОВ РАБОТ
    public class VolumeWork
    {
        #region Поля класса - параметры знаков
        //00-Наименование работы
        private string techName01;
        //01-Наименование работы
        private string techName02;
        //02-Наименование работы
        private string techName03;
        //03-Наименование работы
        private string techName04;
        //04-Наименование работы
        private string techName05;
        //05-Наименование работы
        private string techName06;
        //06-Наименование работы
        private string techName07;
        //07-Наименование работы
        private string techName08;
        //08-Наименование работы
        private string techName09;
        //09-Наименование работы
        private string techName10;
        //10-Наименование работы
        private string techName11;
        //11-Наименование работы
        private string techName12;
        //12-Наименование работы
        private string techName13;
        //13-Наименование работы
        private string techName14;
        //14-Наименование работы
        private string techName15;
        //15-Наименование работы
        private string techName16;
        //16-Наименование работы
        private string techName17;
        //17-Наименование работы
        private string techName18;
        //18-Наименование работы
        private string techName19;
        //19-Наименование работы
        private string techName20;

        //01-Единицы измерения
        private string initN01;
        //02-Единицы измерения
        private string initN02;
        //03-Единицы измерения
        private string initN03;
        //4-Единицы измерения
        private string initN04;
        //05-Единицы измерения
        private string initN05;
        //06-Единицы измерения
        private string initN06;
        //07-Единицы измерения
        private string initN07;
        //08-Единицы измерения
        private string initN08;
        //09-Единицы измерения
        private string initN09;
        //10-Единицы измерения
        private string initN10;
        //11-Единицы измерения
        private string initN11;
        //12-Единицы измерения
        private string initN12;
        //13-Единицы измерения
        private string initN13;
        //14-Единицы измерения
        private string initN14;
        //15-Единицы измерения
        private string initN15;
        //16-Единицы измерения
        private string initN16;
        //17-Единицы измерения
        private string initN17;
        //18-Единицы измерения
        private string initN18;
        //19-Единицы измерения
        private string initN19;
        //20-Единицы измерения
        private string initN20;

        //01-Единицы измерения
        private string countWV01;
        //02-Единицы измерения
        private string countWV02;
        //03-Единицы измерения
        private string countWV03;
        //04-Единицы измерения
        private string countWV04;
        //05-Единицы измерения
        private string countWV05;
        //06-Единицы измерения
        private string countWV06;
        //07-Единицы измерения
        private string countWV07;
        //08-Единицы измерения
        private string countWV08;
        //09-Единицы измерения
        private string countWV09;
        //10-Единицы измерения
        private string countWV10;
        //11-Единицы измерения
        private string countWV11;
        //12-Единицы измерения
        private string countWV12;
        //13-Единицы измерения
        private string countWV13;
        //14-Единицы измерения
        private string countWV14;
        //15-Единицы измерения
        private string countWV15;
        //16-Единицы измерения
        private string countWV16;
        //17-Единицы измерения
        private string countWV17;
        //18-Единицы измерения
        private string countWV18;
        //19-Единицы измерения
        private string countWV19;
        //20-Единицы измерения
        private string countWV20;
        #endregion

        //конструктор
        public VolumeWork()
        {
            techName01 = ""; initN01 = ""; countWV01 = "";
            techName02 = ""; initN02 = ""; countWV02 = "";
            techName03 = ""; initN03 = ""; countWV03 = "";
            techName04 = ""; initN04 = ""; countWV04 = "";
            techName05 = ""; initN05 = ""; countWV05 = "";
            techName06 = ""; initN06 = ""; countWV06 = "";
            techName07 = ""; initN07 = ""; countWV07 = "";
            techName08 = ""; initN08 = ""; countWV08 = "";
            techName09 = ""; initN09 = ""; countWV09 = "";
            techName10 = ""; initN10 = ""; countWV10 = "";
            techName11 = ""; initN11 = ""; countWV11 = "";
            techName12 = ""; initN12 = ""; countWV12 = "";
            techName13 = ""; initN13 = ""; countWV13 = "";
            techName14 = ""; initN14 = ""; countWV14 = "";
        }

        #region Доступ к полям
        //00-Наименование работы
        public string TechName01
        {
            get { return techName01; }
            set { techName01 = value; }
        }
        //01-Наименование работы
        public string TechName02
        {
            get { return techName02; }
            set { techName02 = value; }
        }
        //02-Наименование работы
        public string TechName03
        {
            get { return techName03; }
            set { techName03 = value; }
        }
        //03-Наименование работы
        public string TechName04
        {
            get { return techName04; }
            set { techName04 = value; }
        }
        //04-Наименование работы
        public string TechName05
        {
            get { return techName05; }
            set { techName05 = value; }
        }
        //05-Наименование работы
        public string TechName06
        {
            get { return techName06; }
            set { techName06 = value; }
        }
        //06-Наименование работы
        public string TechName07
        {
            get { return techName07; }
            set { techName07 = value; }
        }
        //07-Наименование работы
        public string TechName08
        {
            get { return techName08; }
            set { techName08 = value; }
        }
        //08-Наименование работы
        public string TechName09
        {
            get { return techName09; }
            set { techName09 = value; }
        }
        //09-Наименование работы
        public string TechName10
        {
            get { return techName10; }
            set { techName10 = value; }
        }
        //10-Наименование работы
        public string TechName11
        {
            get { return techName11; }
            set { techName11 = value; }
        }
        //11-Наименование работы
        public string TechName12
        {
            get { return techName12; }
            set { techName12 = value; }
        }
        //12-Наименование работы
        public string TechName13
        {
            get { return techName13; }
            set { techName13 = value; }
        }
        //13-Наименование работы
        public string TechName14
        {
            get { return techName14; }
            set { techName14 = value; }
        }
        //14-Наименование работы
        public string TechName15
        {
            get { return techName15; }
            set { techName15 = value; }
        }
        //15-Наименование работы
        public string TechName16
        {
            get { return techName16; }
            set { techName16 = value; }
        }
        //16-Наименование работы
        public string TechName17
        {
            get { return techName17; }
            set { techName17 = value; }
        }
        //17-Наименование работы
        public string TechName18
        {
            get { return techName18; }
            set { techName18 = value; }
        }
        //18-Наименование работы
        public string TechName19
        {
            get { return techName19; }
            set { techName19 = value; }
        }
        //19-Наименование работы
        public string TechName20
        {
            get { return techName20; }
            set { techName20 = value; }
        }
        //00-Единицы измерения
        public string InitN01
        {
            get { return initN01; }
            set { initN01 = value; }
        }
        //01-Единицы измерения
        public string InitN02
        {
            get { return initN02; }
            set { initN02 = value; }
        }
        //02-Единицы измерения
        public string InitN03
        {
            get { return initN03; }
            set { initN03 = value; }
        }
        //03-Единицы измерения
        public string InitN04
        {
            get { return initN04; }
            set { initN04 = value; }
        }
        //04-Единицы измерения
        public string InitN05
        {
            get { return initN05; }
            set { initN05 = value; }
        }
        //05-Единицы измерения
        public string InitN06
        {
            get { return initN06; }
            set { initN06 = value; }
        }
        //06-Единицы измерения
        public string InitN07
        {
            get { return initN07; }
            set { initN07 = value; }
        }
        //07-Единицы измерения
        public string InitN08
        {
            get { return initN08; }
            set { initN08 = value; }
        }
        //08-Единицы измерения
        public string InitN09
        {
            get { return initN09; }
            set { initN09 = value; }
        }
        //09-Единицы измерения
        public string InitN10
        {
            get { return initN10; }
            set { initN10 = value; }
        }
        //10-Единицы измерения
        public string InitN11
        {
            get { return initN11; }
            set { initN11 = value; }
        }
        //11-Единицы измерения
        public string InitN12
        {
            get { return initN12; }
            set { initN12 = value; }
        }
        //12-Единицы измерения
        public string InitN13
        {
            get { return initN13; }
            set { initN13 = value; }
        }
        //13-Единицы измерения
        public string InitN14
        {
            get { return initN14; }
            set { initN14 = value; }
        }
        //14-Единицы измерения
        public string InitN15
        {
            get { return initN15; }
            set { initN15 = value; }
        }
        //15-Единицы измерения
        public string InitN16
        {
            get { return initN16; }
            set { initN16 = value; }
        }
        //16-Единицы измерения
        public string InitN17
        {
            get { return initN17; }
            set { initN17 = value; }
        }
        //17-Единицы измерения
        public string InitN18
        {
            get { return initN18; }
            set { initN18 = value; }
        }
        //18-Единицы измерения
        public string InitN19
        {
            get { return initN19; }
            set { initN19 = value; }
        }
        //19-Единицы измерения
        public string InitN20
        {
            get { return initN20; }
            set { initN20 = value; }
        }

        //00-Единицы измерения
        public string CountWV01
        {
            get { return countWV01; }
            set { countWV01 = value; }
        }
        //01-Единицы измерения
        public string CountWV02
        {
            get { return countWV02; }
            set { countWV02 = value; }
        }
        //02-Единицы измерения
        public string CountWV03
        {
            get { return countWV03; }
            set { countWV03 = value; }
        }
        //03-Единицы измерения
        public string CountWV04
        {
            get { return countWV04; }
            set { countWV04 = value; }
        }
        //04-Единицы измерения
        public string CountWV05
        {
            get { return countWV05; }
            set { countWV05 = value; }
        }
        //05-Единицы измерения
        public string CountWV06
        {
            get { return countWV06; }
            set { countWV06 = value; }
        }
        //06-Единицы измерения
        public string CountWV07
        {
            get { return countWV07; }
            set { countWV07 = value; }
        }
        //07-Единицы измерения
        public string CountWV08
        {
            get { return countWV08; }
            set { countWV08 = value; }
        }
        //08-Единицы измерения
        public string CountWV09
        {
            get { return countWV09; }
            set { countWV09 = value; }
        }
        //09-Единицы измерения
        public string CountWV10
        {
            get { return countWV10; }
            set { countWV10 = value; }
        }
        //10-Единицы измерения
        public string CountWV11
        {
            get { return countWV11; }
            set { countWV11 = value; }
        }
        //11-Единицы измерения
        public string CountWV12
        {
            get { return countWV12; }
            set { countWV12 = value; }
        }
        //12-Единицы измерения
        public string CountWV13
        {
            get { return countWV13; }
            set { countWV13 = value; }
        }
        //13-Единицы измерения
        public string CountWV14
        {
            get { return countWV14; }
            set { countWV14 = value; }
        }
        //14-Единицы измерения
        public string CountWV15
        {
            get { return countWV15; }
            set { countWV15 = value; }
        }
        //15-Единицы измерения
        public string CountWV16
        {
            get { return countWV16; }
            set { countWV16 = value; }
        }
        //16-Единицы измерения
        public string CountWV17
        {
            get { return countWV17; }
            set { countWV17 = value; }
        }
        //17-Единицы измерения
        public string CountWV18
        {
            get { return countWV18; }
            set { countWV18 = value; }
        }
        //18-Единицы измерения
        public string CountWV19
        {
            get { return countWV19; }
            set { countWV19 = value; }
        }
        //19-Единицы измерения
        public string CountWV20
        {
            get { return countWV20; }
            set { countWV20 = value; }
        }

        #endregion

        public int GetCount(VolumeWork volumeWork)
        {
            if (volumeWork != null)
            {
                int n = 0;
                if (volumeWork.TechName01.Length != 0) n += 1;
                if (volumeWork.TechName02.Length != 0) n += 1;
                if (volumeWork.TechName03.Length != 0) n += 1;
                if (volumeWork.TechName04.Length != 0) n += 1;
                if (volumeWork.TechName05.Length != 0) n += 1;
                if (volumeWork.TechName06.Length != 0) n += 1;
                if (volumeWork.TechName07.Length != 0) n += 1;
                if (volumeWork.TechName08.Length != 0) n += 1;
                if (volumeWork.TechName09.Length != 0) n += 1;
                if (volumeWork.TechName10.Length != 0) n += 1;
                if (volumeWork.TechName11.Length != 0) n += 1;
                if (volumeWork.TechName12.Length != 0) n += 1;
                return n;
            }
            return 0;

        }
    }

    //Класс обработки данных чертежа
    public class CreatTable
    {
        #region Константы заполнения
        //Константа пустота для строк
        private const string Report = "Сводка";
        //Константа пустота для десятичных
        private const string Specp = "Спецификация";
        //Константа пустота для целых
        private const string VolumeWork = "Объемы работ";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork02 = "Установка пластиковых знаков, в том числе:";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork03 = " - разметка места установки";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork04_1 = " - бурение ям в грунтах 2гр. на глубину (";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork04_2 = " м для каждого знака)";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork05 = " - раскладка знаков по местам установки";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork06 = " - установка знака с монтажом щитков и трамбовкой грунта";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork10 = "установка щита-указателя на ограждении";

        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork20 = "Установка металлических знаков, в том числе:";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork22 = " - рытье ям в ручную грунта 2гр. с последующей";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork23 = "ручной трамбовкой грунта основания";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork24 = " - устройство подушки из песка";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork25 = " - устройство фундамента Ф1";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork26 = " - установка металлических стоек с креплением щитков";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork27 = " - бетонирование установленной стойки";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork28 = " - окрашивание стоек с фундаментом";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork29 = "  - доставка грунта автотраспортом";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork30 = "(плотностью 1600 кг/м3) с расстояния";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork31 = "  - устройство бермы экскаватором с емк. ковша 0,65м3";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork32 = " - уплотнение пневмотрамбовками";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork39 = " - установка щита-указателя на столбе";

        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork40 = "Репер";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork41 = "1.Монтаж м/к репера с бурением скважин в грунтах 2гр.";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork42 = "диаметром ";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork43 = " мм глубиной до ";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork44 = " м (для каждого репер)";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork45 = "2.Устройство фундамента из бетона";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork46 = "3.Засыпка репера песком";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork47 = "4.Заполнение смазкой";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork48 = "5.Покрытие фундамента битумной мастикой на 2 раза";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork49 = "по огрунтовой праймером поверхности в один слой";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork50 = "6.Покрытие подземных металлоконструкции";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork51 = "мастикой МБР-65 в два слоя по битумной грунтовке";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork52 = "БНИ-IV в один слой";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork53 = "(в т.ч. очистка, обезжиривание, сушка)";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork54 = "7.Покрытие надземных металлоконструкции";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork55 = "покрытием АКП С4(II) по ОТТ-25.220.01-КТН-097-16";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork56 = "(в т.ч. очистка, обезжиривание, сушка)";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork57 = "8.Плановая и высотная привязка репера";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork58 = "(закрепление репера на местности)";
        //Константа для реперов
        private const string NameVolumeWork60 = "Репер постоянный";
        //Константа для реперов
        private const string NameVolumeWork61 = "(металлоконструкции) ";
        //Константа для дифмарок
        private const string NameVolumeWork62 = " - сборка и монтаж металлического ";
        //Константа для дифмарок
        private const string NameVolumeWork63 = "устройства планово-высотного положения";
        //Константа для дифмарок
        private const string NameVolumeWork64 = "на трубопроводе";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork70 = "Установка";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork71 = "столбика бетонного замерного";
        //Константа для объема работ - наименование объекта
        private const string NameVolumeWork80 = "Грунт глинистый";

        //Константа единицы измерения
        private const string UnitsSign = "знаков";
        //Константа единицы измерения
        private const string UnitsMetr = "м";
        //Константа единицы измерения
        private const string UnitsTonn = "т";
        //Константа единицы измерения
        private const string UnitsKg = "кг";
        //Константа единицы измерения
        private const string UnitsEmpty = "";
        //Константа единицы измерения
        private const string UnitsMetrK = "м3";
        //Константа единицы измерения
        private const string UnitsMetrKv = "м2";
        //Константа единицы измерения
        private const string UnitsOne = "к-т";
        //Константа единицы измерения
        private const string UnitsMetrKV = "м2";
        //Константа единицы измерения
        private const string UnitsStolb = "столбик";
        //Константа единицы измерения
        private const string UnitsReper = "репер";
        //Константа единицы измерения
        private const string UnitsКМ = "км";
        //Константа единицы измерения
        private const string UnitsMark = "марок";
        //Номинальный объем подушки грунта по знак остановка запрещена
        private const double SignStopMassFundation = 850;
        //Номинальный объем подушки грунта по знак остановка запрещена
        private const double SignStopMassBeton = 184;
        //Номинальный объем разработки грунта по знак остановка запрещена
        private const double SignStopEarth = 1.1 * 0.7 * 0.8;
        //Номинальный объем подушки грунта по знак остановка запрещена
        private const double SignStopSand = 0.17;
        //Номинальный объем бермы для Остановки запрещено
        private const double SignStopSandBerma = 35;
        #endregion

        #region Поля CreatTable
        //Поле - коэффициент сжатия высоты текста атрибута
        private string franklingothicbook;
        //Поле - размер текст заполнения
        private int textsize;
        //Поле - номер строки для заполнения таблицы эксел
        private int startrows;

        //Поле - размер текст титула сводки
        private int titleReporttextsize;
        //Поле - ширина титула сводки
        private int titleReportrowheight;
        //Шапка сводки
        private string[] titleReport;
        //Разметка столбцов сводки
        private int[] titleReportSize;

        //Поле - размер текст титула спецификации
        private int titleSpecptextsize;
        //Поле - ширина титула спецификации
        private int titleSpecprowheight;
        //Шапка спецификации
        private string[] titleSpecp;
        //Разметка столбцов спецификации
        private int[] titleSpecpSize;

        //Поле - размер текст титула объемов работ
        private int titleVolumeWorktextsize;
        //Поле - ширина титула объемов работ
        private int titleVolumeWorkrowheight;
        //Шапка объемов работ
        private string[] titleVolumeWork;
        //Разметка объемов работ
        private int[] titleVolumeWorkSize;

        #endregion Поля CreatTable

        //Конструктор
        public CreatTable()
        {
            franklingothicbook = "Franklin Gothic Book";
            textsize = 12;
            startrows = 2;

            titleReporttextsize = 12;
            titleReportrowheight = 40;
            titleReport = new string[16]
            {
                "п/п",
                "КМ",
                "ПК",
                "Наименование знака",
                "Основание для установки знака",
                "Кол.",
                "Тип ОТТ",
                "Условное обозначение",
                "Глубина, м",
                "Тип",
                "X",
                "Y",
                "Z",
                "Слой",
                "Примечание 1",
                "Примечание 2"
            };
            titleReportSize = new int[16]
            {
                5,
                10,
                10,
                60,
                30,
                8,
                12,
                15,
                15,
                18,
                12,
                12,
                15,
                15,
                22,
                22
            };

            titleSpecptextsize = 12;
            titleSpecprowheight = 40;
            titleSpecp = new string[8]
            {
                "п/п",
                "Наименование и техническая характеристика",
                "Тип, марка, обозначение, документа, опросного листа",
                "Код оборудования, изделия, материала",
                "Единица измерения",
                "Количество",
                "Масса",
                "Примечание"
            };
            titleSpecpSize = new int[8]
            {
                5,
                75,
                30,
                30,
                15,
                11,
                11,
                18
            };

            titleVolumeWorktextsize = 12;
            titleVolumeWorkrowheight = 40;
            titleVolumeWork = new string[4]
            {
                "п/п",
                "Наименование работ",
                "Ед.изм.",
                "Значение"
            };
            titleVolumeWorkSize = new int[4]
            {
                5,
                85,
                15,
                15
            };

        }

        #region Доступ к общим полям 
        //Доступ к полю 
        public string FranklinGothicBook
        {
            get { return franklingothicbook; }
            set { franklingothicbook = value; }
        }
        //Доступ к полю 
        public int TextSize
        {
            get { return textsize; }
            set { textsize = value; }
        }
        //Доступ к полю
        public int StartRows
        {
            get { return startrows; }
            set { startrows = value; }
        }
        #endregion

        #region Доступ к полям сводки
        //Доступ к полю titleReporttextsize
        public int TitleReportTextSize
        {
            get { return titleReporttextsize; }

            set { titleReporttextsize = value; }
        }
        //Доступ к полю titleReportrowheight
        public int TitleReportRowHeight
        {
            get { return titleReportrowheight; }
            set { titleReportrowheight = value; }
        }
        //Доступ к полю titleReport
        public string[] TitleReport
        {
            get { return titleReport; }
            set { titleReport = value; }
        }
        //Доступ к полю titleReportSize
        public int[] TitleReportSize
        {
            get { return titleReportSize; }
            set { titleReportSize = value; }
        }
        #endregion

        #region Доступ к полям спецификации
        //Доступ к полю titleSpecptextsize
        public int TitleSpecpTextSize
        {
            get { return titleSpecptextsize; }
            set { titleSpecptextsize = value; }
        }
        //Доступ к полю titleSpecprowheight
        public int TitleSpecpRowHeight
        {
            get { return titleSpecprowheight; }
            set { titleSpecprowheight = value; }
        }
        //Доступ к полю titleSpecp
        public string[] TitleSpecp
        {
            get { return titleSpecp; }
            set { titleSpecp = value; }
        }
        //Доступ к полю titleSpecpSize
        public int[] TitleSpecpSize
        {
            get { return titleSpecpSize; }
            set { titleSpecpSize = value; }
        }
        #endregion

        #region Доступ к полям объемов работ
        //Доступ к полю titleVolumeWorktextsize
        public int TitleVolumeWorkTextSize
        {
            get { return titleVolumeWorktextsize; }
            set { titleVolumeWorktextsize = value; }
        }
        //Доступ к полю titleVolumeWorkrowheight
        public int TitleVolumeWorkrowheight
        {
            get { return titleVolumeWorkrowheight; }
            set { titleVolumeWorkrowheight = value; }
        }
        //Доступ к полю titleVolumeWork
        public string[] TitleVolumeWork
        {
            get { return titleVolumeWork; }
            set { titleVolumeWork = value; }
        }
        //Доступ к полю titleVolumeWorkSize
        public int[] TitleVolumeWorkSize
        {
            get { return titleVolumeWorkSize; }
            set { titleVolumeWorkSize = value; }
        }
        #endregion

        //Метод формирования шапки сводки
        public void TitleTableReport(Excel.Worksheet LocalWorkSheet)
        {
            //Установка ширины титульной строки
            LocalWorkSheet.Rows[StartRows - 1].RowHeight = TitleReportRowHeight;

            for (int i = 0; i <= TitleReport.Length - 1; i++)
            {
                //Значение ячейки
                LocalWorkSheet.Cells[StartRows - 1, i + 1] = TitleReport[i];
                //Ширина столбца
                LocalWorkSheet.Columns[i + 1].ColumnWidth = TitleReportSize[i];
            }
            //Форматирование ячеек - просто оформление
            Excel.Range rangeAO = LocalWorkSheet.get_Range("A" + Convert.ToString(StartRows - 1), "P" + Convert.ToString(StartRows - 1));
            //тип линии таблицы
            rangeAO.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //Жирность
            rangeAO.Font.Bold = true;
            //размер шрифта
            rangeAO.Font.Size = TitleReportTextSize;
            //название шрифта
            //rangeAO.Font.Name = FranklinGothicBook;
            //выравнивание по горизонтали
            rangeAO.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeAO.WrapText = true;
        }

        //Метод формирования шапки спецификации
        public void TitleTableSpecp(Excel.Worksheet LocalWorkSheet)
        {
            //Установка ширины титульной строки
            LocalWorkSheet.Rows[StartRows - 1].RowHeight = TitleSpecpRowHeight;

            for (int i = 0; i <= TitleSpecp.Length - 1; i++)
            {
                //Значение ячейки
                LocalWorkSheet.Cells[StartRows - 1, i + 1] = TitleSpecp[i];
                //Ширина столбца
                LocalWorkSheet.Columns[i + 1].ColumnWidth = TitleSpecpSize[i];
            }
            //Форматирование ячеек - просто оформление
            Excel.Range rangeAO = LocalWorkSheet.get_Range("A" + Convert.ToString(StartRows - 1), "H" + Convert.ToString(StartRows - 1));
            //тип линии таблицы
            rangeAO.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //Жирность
            rangeAO.Font.Bold = true;
            //размер шрифта
            rangeAO.Font.Size = TitleSpecpTextSize;
            //название шрифта
            //rangeAO.Font.Name = FranklinGothicBook;
            //выравнивание по горизонтали
            rangeAO.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeAO.WrapText = true;
        }

        //Метод формирования шапки объемы работ
        public void TitleTableVolumeWork(Excel.Worksheet LocalWorkSheet)
        {
            //Установка ширины титульной строки
            LocalWorkSheet.Rows[StartRows - 1].RowHeight = TitleReportRowHeight;

            for (int i = 0; i <= TitleVolumeWork.Length - 1; i++)
            {
                //Значение ячейки
                LocalWorkSheet.Cells[StartRows - 1, i + 1] = TitleVolumeWork[i];
                //Ширина столбца
                LocalWorkSheet.Columns[i + 1].ColumnWidth = TitleVolumeWorkSize[i];
            }
            //Форматирование ячеек - просто оформление
            Excel.Range rangeAO = LocalWorkSheet.get_Range("A" + Convert.ToString(StartRows - 1), "D" + Convert.ToString(StartRows - 1));
            //тип линии таблицы
            rangeAO.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //Жирность
            rangeAO.Font.Bold = true;
            //размер шрифта
            rangeAO.Font.Size = TitleVolumeWorkSize;
            //название шрифта
            //rangeAO.Font.Name = FranklinGothicBook;
            //выравнивание по горизонтали
            rangeAO.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeAO.WrapText = true;
        }

        //Статически метод формирования списка знаков
        static List<Sign> ListSignReport()
        {
            //Получаем ссылку на документ
            AccessToDocument AcadDoc = new AccessToDocument();
            //получаем ссылку на БД
            Database AcadDB = AcadDoc.DBase;
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            //Список знаков
            List<Sign> signReport = new List<Sign>();

            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
            {
                //получаем таблицу блоков и проходим по всем записям таблицы блоков
                BlockTable blockTable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForRead);
                for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
                {
                    if (blockTable.Has(SB.GetParametrSign(i, 0))) //0 - индекс имя в базе
                    {
                        //получения переменной  - таблицы блоков чертежа
                        BlockTableRecord LocalBlockRef = blockTable[SB.GetParametrSign(i, 0)].GetObject(OpenMode.ForRead) as BlockTableRecord;
                        //выбираем все объекты
                        ObjectIdCollection C = LocalBlockRef.GetBlockReferenceIds(true, true);
                        foreach (ObjectId id in C)
                        {
                            BlockReference LocalBlockRefSign = (BlockReference)tr.GetObject(id, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = LocalBlockRefSign.AttributeCollection;
                            //Базовые значения для знака ПК
                            string LocalPk = (AtrCol[0].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                            //Базовые значения для знака Основание
                            string LocalBase = (AtrCol[1].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                            //Базовые значения для знака размещение
                            string LocalAccommodation = (AtrCol[2].GetObject(OpenMode.ForWrite) as AttributeReference).TextString;
                            //Обработка километровых знаков
                            if (LocalBlockRefSign.Name == SB.GetParametrSign(7, 0))
                            {
                                signReport.Add(new Sign()
                                {
                                    KM = SignBase.PKtoKM(PK.CommaToPoint(LocalPk)),
                                    PK = LocalPk,
                                    NameSign = LocalBlockRefSign.Name,
                                    BaseSign = LocalBase,
                                    CountSign = Convert.ToInt32((AtrCol[4].GetObject(OpenMode.ForWrite) as AttributeReference).TextString),
                                    AccommodationSign = LocalAccommodation,
                                    DeepSign = (AtrCol[3].GetObject(OpenMode.ForWrite) as AttributeReference).TextString,
                                    XSign = LocalBlockRefSign.Position.X,
                                    YSign = LocalBlockRefSign.Position.Y,
                                    ZSign = LocalBlockRefSign.Position.Z,
                                    LayerSign = LocalBlockRefSign.Layer,
                                    TypeOfRep = (AtrCol[5].GetObject(OpenMode.ForWrite) as AttributeReference).TextString,
                                    DeepOfRep = (AtrCol[6].GetObject(OpenMode.ForWrite) as AttributeReference).TextString,
                                    NameSignSpecp = SB.GetParametrSign(i, 24),
                                    AsuNsi = SB.GetParametrSign(i, 25),
                                    GroupEq = SB.GetParametrSign(i, 26),
                                    TypeProvider = SB.GetParametrSign(i, 20),
                                    TypeOtt = SB.GetParametrSign(i, 32),
                                    TypeConditional = SB.GetParametrSign(i, 1)
                                });
                            }
                            //Обработка километровых знаков
                            if (LocalBlockRefSign.Name != SB.GetParametrSign(7, 0))
                            {
                                signReport.Add(new Sign()
                                {
                                    KM = SignBase.PKtoKM(PK.CommaToPoint(LocalPk)),
                                    PK = LocalPk,
                                    NameSign = LocalBlockRefSign.Name,
                                    BaseSign = LocalBase,
                                    CountSign = Convert.ToInt32((AtrCol[4].GetObject(OpenMode.ForWrite) as AttributeReference).TextString),
                                    AccommodationSign = LocalAccommodation,
                                    DeepSign = (AtrCol[3].GetObject(OpenMode.ForWrite) as AttributeReference).TextString,
                                    XSign = LocalBlockRefSign.Position.X,
                                    YSign = LocalBlockRefSign.Position.Y,
                                    ZSign = LocalBlockRefSign.Position.Z,
                                    LayerSign = LocalBlockRefSign.Layer,
                                    TypeOfRep = "-",
                                    DeepOfRep = "-",
                                    NameSignSpecp = SB.GetParametrSign(i, 24),
                                    AsuNsi = SB.GetParametrSign(i, 25),
                                    GroupEq = SB.GetParametrSign(i, 26),
                                    TypeProvider = SB.GetParametrSign(i, 27),
                                    TypeOtt = SB.GetParametrSign(i, 32),
                                    TypeConditional = SB.GetParametrSign(i, 1)
                                });
                            }
                        }
                    }
                }
                tr.Commit();
            }

            #region Сортировка списка
            signReport.Sort(delegate (Sign x, Sign y) { return x.KM.CompareTo(y.KM); });
            #endregion

            return signReport;
        }

        //Статически метод формирования спецификации
        static List<Specp> ListSignSpecp(string climate)
        {
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            //Список глубин знаков
            List<string> deepString = new List<string>();
            foreach (Sign sign in ListSignReport())
            {
                deepString.Add(sign.DeepSign);
            }
            //Удаление повторяющих элементов сток
            List<string> newdeepString = new List<string>(deepString.Distinct());
            //Список знаков
            List<Specp> signSpecp = new List<Specp>();
            //Настройка формата
            CultureInfo cultures = new CultureInfo("ru-RU");

            //перебираем поставку Заказчика
            for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
            {
                foreach (string dS in newdeepString)
                {
                    int n = 0;
                    foreach (Sign Sign in ListSignReport())
                    {
                        if (Sign.NameSign == SB.GetParametrSign(i, 0) && Sign.DeepSign == dS && SB.GetParametrSign(i, 27) == SignBase.Customer)
                        {
                            n += Sign.CountSign;
                        }
                    }
                    if (n != 0) //|| specp.TechName02 == SignBase.ConstOther
                    {
                        string techName01;
                        string asuNsi;

                        if (climate == SignBase.Сlimate[0])
                        {
                            techName01 = SB.GetParametrSign(i, 24) + SB.GetParametrSign(i, 33);
                            asuNsi = SB.GetParametrSign(i, 26);
                        }
                        else
                        {
                            techName01 = SB.GetParametrSign(i, 24) + SB.GetParametrSign(i, 34);
                            asuNsi = SB.GetParametrSign(i, 35);
                        }

                        if (SB.GetParametrSign(i, 30) == SignBase.ConstBeton)
                        {
                            signSpecp.Add(new Specp()
                            {
                                TechName01 = techName01,
                                TechName02 = "", //SB.GetParametrSign(i, 30)
                                TechName03 = "",
                                OpList01 = SB.GetParametrSign(i, 31),
                                OpList02 = "",
                                AsuNsi = asuNsi,
                                GroupEq = SB.GetParametrSign(i, 25),
                                InitN = SB.GetParametrSign(i, 16),
                                CountSpecp = Convert.ToString(n, cultures),
                                Mass = SB.GetParametrSign(i, 28),
                                //Prim = dS,
                                Prim = SB.GetParametrSign(i, 1),
                                AddPrim = "Не в реестре ОВП",
                                DeepSign = dS,
                                Custom = SB.GetParametrSign(i, 27),
                                AccommodationSign = SB.GetParametrSign(i, 11),
                            });
                        }
                        if (SB.GetParametrSign(i, 30) != SignBase.ConstBeton)
                        { 
                            signSpecp.Add(new Specp()
                            {
                                TechName01 = techName01,
                                TechName02 = SB.GetParametrSign(i, 30),
                                TechName03 = "",
                                OpList01 = SB.GetParametrSign(i, 31),
                                OpList02 = "",
                                AsuNsi = asuNsi,
                                GroupEq = SB.GetParametrSign(i, 25),
                                InitN = SB.GetParametrSign(i, 16),
                                CountSpecp = Convert.ToString(n, cultures),
                                Mass = SB.GetParametrSign(i, 28),
                                //Prim = dS,
                                Prim = SB.GetParametrSign(i, 1),
                                AddPrim = "Не в реестре ОВП",
                                DeepSign = dS,
                                Custom = SB.GetParametrSign(i, 27),
                                AccommodationSign = SB.GetParametrSign(i, 11),
                            });
                        }
                    }
                }
            }

            //перебираем поставку Подрядчик
            for (int i = 0; i <= SB.CountSignBaseRow - 1; i++)
            {
                foreach (string dS in newdeepString)
                {
                    int n = 0;
                    foreach (Sign Sign in ListSignReport())
                    {
                        if (Sign.NameSign == SB.GetParametrSign(i, 0) && Sign.DeepSign == dS && SB.GetParametrSign(i, 27) == SignBase.Contractor)
                        {
                            n += Sign.CountSign;
                        }
                    }
                    if (n != 0)
                    {
                        string techName01;
                        string asuNsi;

                        if (climate == SignBase.Сlimate[0])
                        {
                            techName01 = SB.GetParametrSign(i, 24) + SB.GetParametrSign(i, 33);
                            asuNsi = SB.GetParametrSign(i, 26);
                        }
                        else
                        {
                            techName01 = SB.GetParametrSign(i, 24) + SB.GetParametrSign(i, 34);
                            asuNsi = SB.GetParametrSign(i, 35);
                        }

                        signSpecp.Add(new Specp()
                        {
                            TechName01 = techName01,
                            TechName02 = SB.GetParametrSign(i, 30),
                            TechName03 = "",
                            OpList01 = SB.GetParametrSign(i, 31),
                            OpList02 = "",
                            AsuNsi = asuNsi,
                            GroupEq = SB.GetParametrSign(i, 25),
                            InitN = SB.GetParametrSign(i, 16),
                            CountSpecp = Convert.ToString(n, cultures),
                            Mass = SB.GetParametrSign(i, 28),
                            //Prim = dS,
                            Prim = SB.GetParametrSign(i, 1),
                            AddPrim = "Не в реестре ОВП",
                            DeepSign = dS,
                            Custom = SB.GetParametrSign(i, 27),
                            AccommodationSign = SB.GetParametrSign(i, 11),
                        });
                    }
                }
            }

            return signSpecp;
        }

        //Статически метод формирования металлоконструкции репера
        static List<Specp> ListSignSpecpStop()
        {
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            //Список знаков
            List<Specp> signSpecpStop = new List<Specp>();
            int n = 0;
            foreach (Sign sign in ListSignReport())
            {
                if (sign.NameSign == SB.GetParametrSign(67, 0))
                {
                     n += 1;
                }
            }
            if (n != 0)
            {
                signSpecpStop.Add(new Specp()
                {
                    TechName01 = NameVolumeWork80,
                    TechName02 = "с учетом потерь 1%", //NameVolumeWork60
                    TechName03 = "",
                    OpList01 = "",
                    OpList02 = "",
                    AsuNsi = "4735081",
                    GroupEq = "п21.11.18",
                    InitN = UnitsMetrK,
                    CountSpecp = Convert.ToString(n*35*1.01),
                    Mass = Convert.ToString(1700),
                    Prim = "для дорожных берм",
                    DeepSign = "",
                    Custom = "",
                    AccommodationSign = "",
                });
            }
            return signSpecpStop;
        }

        //Статически метод формирования объемов работ Металлоконструкций реперов
        static List<VolumeWork> ListSignWorkVolumeReperMK()
        {
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            //Экземпляр объекта класса SignBase
            DataReper TREmpty = new DataReper();
            //Список глубин знаков
            List<string> deepString = new List<string>();
            foreach (Sign sign in ListSignReport())
            {
                deepString.Add(sign.DeepOfRep);
            }
            //Удаление повторяющих элементов сток
            List<string> newdeepString = new List<string>(deepString.Distinct());
            //Список знаков
            List<VolumeWork> signWorkVolumeReperMK = new List<VolumeWork>();

            foreach (string dTR in TREmpty.TypeReper)
            {
                foreach (string dS in newdeepString)
                {
                    int n = 0;
                    foreach (Sign Sign in ListSignReport())
                    {
                        if (Sign.NameSign == SB.GetParametrSign(7, 0) && Sign.DeepOfRep == dS && Sign.TypeOfRep == dTR)
                        {
                            n += Sign.CountSign;
                        }
                    }
                    if (n != 0)
                    {
                        DataReper TR = new DataReper(dTR, dS);
                        signWorkVolumeReperMK.Add(new VolumeWork()
                        {
                            TechName01 = NameVolumeWork40 + " " + dTR,
                            InitN01 = UnitsSign,
                            CountWV01 = Convert.ToString(n),

                            TechName02 = NameVolumeWork41,
                            InitN02 = UnitsKg,
                            CountWV02 = Convert.ToString(n * TR.Mass),

                            TechName03 = NameVolumeWork42 + Convert.ToString(TR.Diam) + NameVolumeWork43 + dS + NameVolumeWork44,
                            InitN03 = UnitsMetr,
                            CountWV03 = Convert.ToString(n * Convert.ToDouble(dS)),

                            TechName04 = NameVolumeWork45,
                            InitN04 = UnitsMetrK,
                            CountWV04 = Convert.ToString(n * TR.Beton),

                            TechName05 = NameVolumeWork46,
                            InitN05 = UnitsMetrK,
                            CountWV05 = Convert.ToString(n * TR.Sand),

                            TechName06 = NameVolumeWork47,
                            InitN06 = UnitsMetrK,
                            CountWV06 = Convert.ToString(n * TR.Grease),

                            TechName07 = NameVolumeWork48,
                            InitN07 = UnitsMetrKV,
                            CountWV07 = Convert.ToString(n * TR.AkzBeton),

                            TechName08 = NameVolumeWork49,
                            InitN08 = "",
                            CountWV08 = "",

                            TechName09 = NameVolumeWork50,
                            InitN09 = UnitsMetrKV,
                            CountWV09 = Convert.ToString(n * TR.AkzEarth),

                            TechName10 = NameVolumeWork51,
                            InitN10 = "",
                            CountWV10 = "",

                            TechName11 = NameVolumeWork52,
                            InitN11 = "",
                            CountWV11 = "",

                            TechName12 = NameVolumeWork53,
                            InitN12 = "",
                            CountWV12 = "",

                            TechName13 = NameVolumeWork54,
                            InitN13 = UnitsMetrKV,
                            CountWV13 = Convert.ToString(n * TR.AkzUnder),

                            TechName14 = NameVolumeWork55,
                            InitN14 = "",
                            CountWV14 = "",

                            TechName15 = NameVolumeWork56,
                            InitN15 = "",
                            CountWV15 = "",

                            TechName16 = NameVolumeWork57,
                            InitN16 = UnitsReper,
                            CountWV16 = Convert.ToString(n),

                            TechName17 = NameVolumeWork58,
                            InitN17 = "",
                            CountWV17 = "",
                            /*
                            TechName18 = NameVolumeWork49,
                            InitN18 = UnitsMetrKV,
                            CountWV18 = Convert.ToString(n * TR.Beton),

                            TechName19 = NameVolumeWork49,
                            InitN19 = UnitsMetrKV,
                            CountWV19 = Convert.ToString(n * TR.Beton),

                            TechName20 = NameVolumeWork49,
                            InitN20 = UnitsMetrKV,
                            CountWV20 = Convert.ToString(n * TR.Beton),
                            */
                        });
                    }
                }
            }
            return signWorkVolumeReperMK;
        }

        //Статически метод формирования металлоконструкции репера
        static List<Specp> ListSignSpecpReperMK()
        {
            //Экземпляр объекта класса SignBase
            SignBase SB = new SignBase();
            //Экземпляр объекта класса SignBase
            DataReper TREmpty = new DataReper();
            //Список глубин знаков
            List<string> deepString = new List<string>();
            foreach (Sign sign in ListSignReport())
            {
                deepString.Add(sign.DeepOfRep);
            }
            //Удаление повторяющих элементов сток
            List<string> newdeepString = new List<string>(deepString.Distinct());
            //Список знаков
            List<Specp> signSpecpeReperMK = new List<Specp>();

            foreach (string dTR in TREmpty.TypeReper)
            {
                foreach (string dS in newdeepString)
                {
                    int n = 0;
                    foreach (Sign Sign in ListSignReport())
                    {
                        if (Sign.NameSign == SB.GetParametrSign(7, 0) && Sign.DeepOfRep == dS && Sign.TypeOfRep == dTR)
                        {
                            n += Sign.CountSign;
                        }
                    }
                    if (n != 0)
                    {
                        DataReper TR = new DataReper(dTR, dS);
                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = NameVolumeWork60 ,
                            TechName02 = "(" + dTR + " " + "глубиной " + dS + " м)", //NameVolumeWork60
                            TechName03 = "",
                            OpList01 = "ОТТ-23.040.00-КТН-100-15",
                            OpList02 = "",
                            AsuNsi = "4610833",
                            GroupEq = "п14.24",
                            InitN = UnitsOne,
                            CountSpecp = Convert.ToString(n),
                            Mass = Convert.ToString(TR.Mass),
                            Prim = UnitsKg,
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });
                        //АКЗ репера - добавилд в ручную, т.к. смысла нету делать ссылки - используется 1 раз 
                        
                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Праймер битумный",
                            TechName02 = "",
                            TechName03 = "",
                            OpList01 = "ГОСТ 30693-2000",
                            OpList02 = "",
                            AsuNsi = "2393357",
                            GroupEq = "п21.11.16",
                            InitN = UnitsKg,
                            CountSpecp = Convert.ToString(Math.Round(n * 0.5 * TR.AkzBeton,2)),
                            Mass = Convert.ToString("0,5кг/м2"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });
                        
                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Мастика битумная",
                            TechName02 = "",
                            TechName03 = "",
                            OpList01 = "ГОСТ 30693-2000",
                            OpList02 = "",
                            AsuNsi = "959943",
                            GroupEq = "п21.11.16",
                            InitN = UnitsKg,
                            CountSpecp = Convert.ToString(Math.Round(n * 3.8 * TR.AkzBeton,2)),
                            Mass = Convert.ToString("3,8кг/м2"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });
                        
                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Грунтовка битумная для ремонта трубопровода",
                            TechName02 = "",
                            TechName03 = "",
                            OpList01 = "ГОСТ 9812-74",
                            OpList02 = "",
                            AsuNsi = "1258059",
                            GroupEq = "п07.01",
                            InitN = UnitsTonn,
                            CountSpecp = Convert.ToString(Math.Round(n * 1 * 0.4 * TR.AkzEarth/1000, 3)),
                            Mass = Convert.ToString("3,8кг/м2"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });

                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Мастика битумно-резиновая МБР-65 ГОСТ 15836-79",
                            TechName02 = "",
                            TechName03 = "",
                            OpList01 = "ГОСТ 15836-79",
                            OpList02 = "",
                            AsuNsi = "354215",
                            GroupEq = "п21.11.16",
                            InitN = UnitsKg,
                            CountSpecp = Convert.ToString(Math.Round(n * 2 * 1.5 * TR.AkzEarth, 2)),
                            Mass = Convert.ToString("1,5кг/м2"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });

                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Покрытие антикоррозионное для строящихся и реконструируемых",
                            TechName02 = "надземных трубопроводов, контрукции и оборудования С4(II)",
                            TechName03 = "цвет белый RAL 9003 ОТТ-25.220.01-КТН-097-16",
                            OpList01 = "ОТТ-25.220.01-КТН-097-16",
                            OpList02 = "",
                            AsuNsi = "4069276",
                            GroupEq = "п07.03.03",
                            InitN = UnitsMetrKv,
                            CountSpecp = Convert.ToString(Math.Round(n * TR.AkzUnder, 2)),
                            Mass = Convert.ToString("0,4кг/м2"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });

                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Песок строительный средний фр.0,7-0,2 ГОСТ 8736-2014",
                            TechName02 = "",
                            TechName03 = "",
                            OpList01 = "ГОСТ 8736-2014",
                            OpList02 = "",
                            AsuNsi = "2376376",
                            GroupEq = "п21.11.01",
                            InitN = UnitsMetrK,
                            CountSpecp = Convert.ToString(Math.Round(n * TR.Sand*1.01, 2)),
                            Mass = Convert.ToString("1500"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });

                        signSpecpeReperMK.Add(new Specp()
                        {
                            TechName01 = "Бетон тяжелый В15 П1 F100 W6 D2400 ГОСТ 25192-2012",
                            TechName02 = "",
                            TechName03 = "",
                            OpList01 = "ГОСТ 25192-2012",
                            OpList02 = "",
                            AsuNsi = "1752922",
                            GroupEq = "п08.01.12.01",
                            InitN = UnitsMetrK,
                            CountSpecp = Convert.ToString(Math.Round(n * TR.Beton*1.01, 2)),
                            Mass = Convert.ToString("2400"),
                            Prim = "Не в реестре ОВП",
                            DeepSign = "",
                            Custom = "",
                            AccommodationSign = "",
                        });
                    }   
                }
            }
            return signSpecpeReperMK;
        }

        //Статически метод формирования объемов работ по знакам
        static List<VolumeWork> ListSignWorkVolume(string climate)
        {
            //Список знаков
            List<VolumeWork> signWorkVolume = new List<VolumeWork>();
            SignBase S = new SignBase();

            foreach (Specp specp in ListSignSpecp(climate))
            {
                if (specp.Custom == SignBase.Customer)
                {
                    //ПЛАСТИКОВЫЕ ЗНАКИ
                    //Пластик на 1 стойки
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OneRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsEmpty,
                            CountWV02 = UnitsEmpty,
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + specp.DeepSign + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsTonn,
                            CountWV06 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Пластик на 2 стойках
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.TwoRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsEmpty,
                            CountWV02 = UnitsEmpty,
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + Convert.ToString(2 * Convert.ToDouble(specp.DeepSign)) + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(2 * Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsTonn,
                            CountWV06 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Пластик на ограждении
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OnFencing)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork10,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Пластик на столбе
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OnPole)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Пластик на стойке знака
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OneRackExist)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }

                    //МЕТАЛЛИЧЕСКИЕ ЗНАКИ
                    //Металл на 1 стойки
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OneRack && specp.TechName01 == S.GetParametrSign(52,24))
                    {
                        //количество знаков
                        int ns = Convert.ToInt32(specp.CountSpecp);
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,

                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = UnitsEmpty,

                            TechName03 = NameVolumeWork22,
                            InitN03 = UnitsMetrK,
                            CountWV03 = Convert.ToString(Math.Round(SignStopEarth * ns, 3)),

                            TechName04 = NameVolumeWork23,
                            InitN04 = UnitsEmpty,
                            CountWV04 = UnitsEmpty,

                            TechName05 = NameVolumeWork24,
                            InitN05 = UnitsMetrK,
                            CountWV05 = Convert.ToString(Math.Round(SignStopSand * ns, 3)),

                            TechName06 = NameVolumeWork25,
                            InitN06 = UnitsTonn,
                            CountWV06 = Convert.ToString(Math.Round(SignStopMassFundation / 1000 * ns, 3)),

                            TechName07 = NameVolumeWork26,
                            InitN07 = UnitsTonn,
                            CountWV07 = Convert.ToString(Math.Round((8.2 + 3 + 1.0 + 12.0 * (0.07 + 0.0005 + 0.0005) + 6 * 0.0005) / 1000 * ns, 3)),

                            TechName08 = NameVolumeWork27,
                            InitN08 = UnitsTonn,
                            CountWV08 = Convert.ToString(Math.Round(SignStopMassBeton / 1000 * ns, 3)),

                            TechName09 = NameVolumeWork28,
                            InitN09 = UnitsEmpty,
                            CountWV09 = UnitsEmpty,

                            TechName10 = NameVolumeWork31,
                            InitN10 = UnitsMetrK,
                            CountWV10 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                            TechName11 = NameVolumeWork32,
                            InitN11 = UnitsMetrK,
                            CountWV11 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                        });

                    }
                    //Металл на 1 стойки
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OneRack && specp.TechName01 != S.GetParametrSign(52, 24))
                    {
                        //количество знаков
                        int ns = Convert.ToInt32(specp.CountSpecp);
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,

                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = UnitsEmpty,

                            TechName03 = NameVolumeWork22,
                            InitN03 = UnitsMetrK,
                            CountWV03 = Convert.ToString(Math.Round(SignStopEarth * ns, 3)),

                            TechName04 = NameVolumeWork23,
                            InitN04 = UnitsEmpty,
                            CountWV04 = UnitsEmpty,

                            TechName05 = NameVolumeWork24,
                            InitN05 = UnitsMetrK,
                            CountWV05 = Convert.ToString(Math.Round(SignStopSand * ns, 3)),

                            TechName06 = NameVolumeWork25,
                            InitN06 = UnitsTonn,
                            CountWV06 = Convert.ToString(Math.Round(SignStopMassFundation / 1000 * ns, 3)),

                            TechName07 = NameVolumeWork26,
                            InitN07 = UnitsTonn,
                            CountWV07 = Convert.ToString(Math.Round((8.2 + 3 + 1.0 + 12.0 * (0.07 + 0.0005 + 0.0005) + 6 * 0.0005) / 1000 * ns, 3)),

                            TechName08 = NameVolumeWork27,
                            InitN08 = UnitsTonn,
                            CountWV08 = Convert.ToString(Math.Round(SignStopMassBeton / 1000 * ns, 3)),

                            TechName09 = NameVolumeWork28,
                            InitN09 = UnitsEmpty,
                            CountWV09 = UnitsEmpty,

                            TechName10 = NameVolumeWork31,
                            InitN10 = UnitsMetrK,
                            CountWV10 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                            TechName11 = NameVolumeWork32,
                            InitN11 = UnitsMetrK,
                            CountWV11 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                        });
  
                    }
                    //Металл на 2 стойках
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.TwoRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + Convert.ToString(2 * Convert.ToDouble(specp.DeepSign)) + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(2 * Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Металл на ограждении
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OnFencing)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork10,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Металл на столбе
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OnPole)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Металл на существующей стойке 
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OneRackExist)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }

                    //СТЕКЛОПЛАСТИК
                    //Стеклопластик на 1 стойки
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OneRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + specp.DeepSign + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Стеклопластик на 2 стойках
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.TwoRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + Convert.ToString(2 * Convert.ToDouble(specp.DeepSign)) + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(2 * Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Стеклопластик на ограждении
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OnFencing)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork10,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Стеклопластик на столбе
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OnPole)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Стеклопластик на стойке знака
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OneRackExist)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }

                    //Металл на трубе (дифмарки)
                    if (specp.AccommodationSign == SignBase.OneRackPipe && specp.TechName01 == S.GetParametrSign(25, 24))
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsMark,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = "",
                            TechName03 = NameVolumeWork62,
                            InitN03 = UnitsTonn,
                            CountWV03 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName04 = NameVolumeWork63,
                            InitN04 = UnitsEmpty,
                            CountWV04 = "",
                            TechName05 = NameVolumeWork64,
                            InitN05 = UnitsEmpty,
                            CountWV05 = "",
                        });
                    }

                    //БЕТОН
                    //Бетон на 1 стойки
                    if (specp.TechName02 == SignBase.ConstBeton && specp.AccommodationSign == SignBase.OneRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = NameVolumeWork70,
                            InitN01 = UnitsStolb,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork71,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000)
                        });
                    }
                }

                if (specp.Custom == SignBase.Contractor)
                {
                    //ПЛАСТИКОВЫЕ ЗНАКИ
                    //Пластик на 1 стойки
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OneRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + specp.DeepSign + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Пластик на 2 стойках
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.TwoRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + Convert.ToString(2 * Convert.ToDouble(specp.DeepSign)) + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(2 * Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Пластик на ограждении
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OnFencing)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork10,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Пластик на столбе
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OnPole)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Пластик на стойке знака
                    if (specp.TechName02 == SignBase.ConstSignPolyMer && specp.AccommodationSign == SignBase.OneRackExist)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }

                    //МЕТАЛЛИЧЕСКИЕ ЗНАКИ
                    //Металл на 1 стойки
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OneRack && specp.TechName01 == S.GetParametrSign(52, 24))
                    {
                        //количество знаков
                        int ns = Convert.ToInt32(specp.CountSpecp);
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,

                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = UnitsEmpty,

                            TechName03 = NameVolumeWork22,
                            InitN03 = UnitsMetrK,
                            CountWV03 = Convert.ToString(Math.Round(SignStopEarth * ns, 3)),

                            TechName04 = NameVolumeWork23,
                            InitN04 = UnitsEmpty,
                            CountWV04 = UnitsEmpty,

                            TechName05 = NameVolumeWork24,
                            InitN05 = UnitsMetrK,
                            CountWV05 = Convert.ToString(Math.Round(SignStopSand * ns, 3)),

                            TechName06 = NameVolumeWork25,
                            InitN06 = UnitsTonn,
                            CountWV06 = Convert.ToString(Math.Round(SignStopMassFundation / 1000 * ns, 3)),

                            TechName07 = NameVolumeWork26,
                            InitN07 = UnitsTonn,
                            CountWV07 = Convert.ToString(Math.Round((8.2 + 3 + 1.0 + 12.0 * (0.07 + 0.0005 + 0.0005) + 6 * 0.0005) / 1000 * ns, 3)),

                            TechName08 = NameVolumeWork27,
                            InitN08 = UnitsTonn,
                            CountWV08 = Convert.ToString(Math.Round(SignStopMassBeton / 1000 * ns, 3)),

                            TechName09 = NameVolumeWork28,
                            InitN09 = UnitsEmpty,
                            CountWV09 = UnitsEmpty,

                            TechName10 = NameVolumeWork31,
                            InitN10 = UnitsMetrK,
                            CountWV10 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                            TechName11 = NameVolumeWork32,
                            InitN11 = UnitsMetrK,
                            CountWV11 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                        });
                    }
                    //Металл на 1 стойки
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OneRack && specp.TechName01 != S.GetParametrSign(52, 24))
                    {
                        //количество знаков
                        int ns = Convert.ToInt32(specp.CountSpecp);
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,

                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = UnitsEmpty,

                            TechName03 = NameVolumeWork22,
                            InitN03 = UnitsMetrK,
                            CountWV03 = Convert.ToString(Math.Round(SignStopEarth * ns, 3)),

                            TechName04 = NameVolumeWork23,
                            InitN04 = UnitsEmpty,
                            CountWV04 = UnitsEmpty,

                            TechName05 = NameVolumeWork24,
                            InitN05 = UnitsMetrK,
                            CountWV05 = Convert.ToString(Math.Round(SignStopSand * ns, 3)),

                            TechName06 = NameVolumeWork25,
                            InitN06 = UnitsTonn,
                            CountWV06 = Convert.ToString(Math.Round(SignStopMassFundation / 1000 * ns, 3)),

                            TechName07 = NameVolumeWork26,
                            InitN07 = UnitsTonn,
                            CountWV07 = Convert.ToString(Math.Round((8.2 + 3 + 1.0 + 12.0 * (0.07 + 0.0005 + 0.0005) + 6 * 0.0005) / 1000 * ns, 3)),

                            TechName08 = NameVolumeWork27,
                            InitN08 = UnitsTonn,
                            CountWV08 = Convert.ToString(Math.Round(SignStopMassBeton / 1000 * ns, 3)),

                            TechName09 = NameVolumeWork28,
                            InitN09 = UnitsEmpty,
                            CountWV09 = UnitsEmpty,

                            TechName10 = NameVolumeWork31,
                            InitN10 = UnitsMetrK,
                            CountWV10 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),

                            TechName11 = NameVolumeWork32,
                            InitN11 = UnitsMetrK,
                            CountWV11 = Convert.ToString(Math.Round(SignStopSandBerma * ns, 3)),
                        });
                    }
                    //Металл на 2 стойках
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.TwoRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + Convert.ToString(2 * Convert.ToDouble(specp.DeepSign)) + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(2 * Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Металл на ограждении
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OnFencing)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork10,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Металл на столбе
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OnPole)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Металл на существующей стойке
                    if (specp.TechName02 == SignBase.ConstMetal && specp.AccommodationSign == SignBase.OneRackExist)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Металл на трубе (дифмарки)
                    if (specp.TechName02 != SignBase.ConstOther && specp.AccommodationSign == SignBase.OneRackPipe)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsMark,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = "",
                            TechName03 = NameVolumeWork62,
                            InitN03 = UnitsTonn,
                            CountWV03 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName04 = NameVolumeWork63,
                            InitN04 = UnitsEmpty,
                            CountWV04 = "",
                            TechName05 = NameVolumeWork64,
                            InitN05 = UnitsEmpty,
                            CountWV05 = "",
                        });
                    }

                    //СТЕКЛОПЛАСТИК
                    //Стеклопластик на 1 стойки
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OneRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + specp.DeepSign + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Стеклопластик на 2 стойках
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.TwoRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork02,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName03 = NameVolumeWork03,
                            InitN03 = UnitsEmpty,
                            CountWV03 = "",
                            TechName04 = NameVolumeWork04_1 + Convert.ToString(2 * Convert.ToDouble(specp.DeepSign)) + NameVolumeWork04_2,
                            InitN04 = UnitsMetr,
                            CountWV04 = Convert.ToString(2 * Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.DeepSign)),
                            TechName05 = NameVolumeWork05,
                            InitN05 = UnitsEmpty,
                            CountWV05 = UnitsEmpty,
                            TechName06 = NameVolumeWork06,
                            InitN06 = UnitsEmpty,
                            CountWV06 = UnitsEmpty,
                        });
                    }
                    //Стеклопластик на ограждении
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OnFencing)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork10,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Стеклопластик на столбе
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OnPole)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }
                    //Стеклопластик на стойке знака
                    if (specp.TechName02 == SignBase.ConstGlassPlastic && specp.AccommodationSign == SignBase.OneRackExist)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsSign,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork39,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                        });
                    }

                    //Металл на трубе (дифмарки)
                    if (specp.AccommodationSign == SignBase.OneRackPipe && specp.TechName01 == S.GetParametrSign(25, 24))
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = specp.TechName01,
                            InitN01 = UnitsMark,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork20,
                            InitN02 = UnitsEmpty,
                            CountWV02 = "",
                            TechName03 = NameVolumeWork62,
                            InitN03 = UnitsTonn,
                            CountWV03 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000),
                            TechName04 = NameVolumeWork63,
                            InitN04 = UnitsEmpty,
                            CountWV04 = "",
                            TechName05 = NameVolumeWork64,
                            InitN05 = UnitsEmpty,
                            CountWV05 = "",
                        });
                    }

                    //БЕТОН
                    //Бетон на 1 стойки
                    if (specp.TechName02 == SignBase.ConstBeton && specp.AccommodationSign == SignBase.OneRack)
                    {
                        signWorkVolume.Add(new VolumeWork()
                        {
                            TechName01 = NameVolumeWork70,
                            InitN01 = UnitsStolb,
                            CountWV01 = specp.CountSpecp,
                            TechName02 = NameVolumeWork71,
                            InitN02 = UnitsTonn,
                            CountWV02 = Convert.ToString(Convert.ToDouble(specp.CountSpecp) * Convert.ToDouble(specp.Mass) / 1000)
                        });
                    }

                }
            }

            return signWorkVolume;
        }

        //Командный метод создания сводки по всем знакам
        [CommandMethod("CreatReport")]
        public void CreatReport()
        {
            //Экземпляр формы для доступа к исходным данным для доступа к полям
            FormSignOnOilReport GSAD = new FormSignOnOilReport();
            //Региональность
            CultureInfo cultures = new CultureInfo("ru-RU");
            //Открываем форму для исходных данных для расстановки знаков
            GSAD.ShowDialog();

            string climate = GSAD.ComboBox1.Text;

            //Получаем доступ к объекту Excel
            Excel.Application AppExcel = new Excel.Application() { Visible = false };
            //Добавляем книгу
            AppExcel.Workbooks.Add(Type.Missing);
            //Количество листво в книге
            for (int countListExcel = 1; countListExcel <= 3; countListExcel += 1)
            {
                AppExcel.Worksheets.Add();
            }
            //Переименовываем все листы
            AppExcel.Worksheets[1].Name = Report;
            AppExcel.Worksheets[2].Name = Specp;
            AppExcel.Worksheets[3].Name = VolumeWork;

            #region ФОРМИРОВАНИЕ СВОДКИ

            //Выбор и активация нужного листа
            Excel.Worksheet workSheet01 = AppExcel.Sheets[1];
            workSheet01.Activate();

            //Заполнение шапки таблицы
            TitleTableReport(workSheet01);

            int i = 0;  //всего нумерация
            foreach (Sign Sign in ListSignReport())
            {
                //п/п
                workSheet01.Cells[i + StartRows, 1].value = i + 1;
                //КМ 
                workSheet01.Cells[i + StartRows, 2].value = Sign.KM;
                //ПК
                workSheet01.Cells[i + StartRows, 3].value = Sign.PK;
                //Наименование знака
                workSheet01.Cells[i + StartRows, 4].value = Sign.NameSign;
                //Основание для установки знака
                workSheet01.Cells[i + StartRows, 5].value = Sign.BaseSign;
                //Кол.
                workSheet01.Cells[i + StartRows, 6].value = Sign.CountSign;
                //Тип
                workSheet01.Cells[i + StartRows, 7].value = Sign.TypeOtt;
                //Тип условный
                workSheet01.Cells[i + StartRows, 8].value = Sign.TypeConditional;
                //Глубина
                workSheet01.Cells[i + StartRows, 9].value = Sign.DeepSign;
                //Тип
                workSheet01.Cells[i + StartRows, 10].value = Sign.AccommodationSign;
                //Х
                workSheet01.Cells[i + StartRows, 11].value = Sign.XSign;
                //Y
                workSheet01.Cells[i + StartRows, 12].value = Sign.YSign;
                //Z
                workSheet01.Cells[i + StartRows, 13].value = Sign.ZSign;
                //Слой знаков
                workSheet01.Cells[i + StartRows, 14].value = Sign.LayerSign;
                //Примечание 1
                workSheet01.Cells[i + StartRows, 15].value = Sign.TypeOfRep;
                //Примечание 2
                workSheet01.Cells[i + StartRows, 16].value = Sign.DeepOfRep;
                //Примечание 3
                i++;
            }
            //Выделение объекта Range
            Excel.Range rangeAO01 = workSheet01.get_Range("A" + Convert.ToString(StartRows), "P" + Convert.ToString(StartRows + i - 1));
            //толщина линий выделенного диапазона
            rangeAO01.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //размер шрифта
            rangeAO01.Font.Size = TitleReportTextSize;
            //выравнивание по горизонтали
            rangeAO01.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO01.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Выделение объекта Range
            Excel.Range rangeDE01 = workSheet01.get_Range("D" + Convert.ToString(StartRows), "E" + Convert.ToString(StartRows + i - 1));
            //выравнивание по горизонтали
            rangeDE01.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //выравнивание по вертикали
            rangeDE01.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeDE01.WrapText = true;
            #endregion

            #region ФОРМИРОВАНИЕ СПЕЦИФИКАЦИИ

            //Выбор и активация нужного листа
            Excel.Worksheet workSheet02 = AppExcel.Sheets[2];
            workSheet02.Activate();
            //Заполнение шапки таблицы
            TitleTableSpecp(workSheet02);

            i = 0;  //всего нумерация
            foreach (Specp specp in ListSignSpecp(climate))
            {
                //п/п
                //workSheet02.Cells[i + StartRows, 1].value = i + 1;
                //00-Наименование знака
                workSheet02.Cells[i + StartRows, 2].value = specp.TechName01;
                //01-Наименование знака
                workSheet02.Cells[i + StartRows + 1, 2].value = specp.TechName02;
                //02-Наименование знака
                workSheet02.Cells[i + StartRows + 2, 2].value = specp.TechName03;
                //03-Опросный лист
                workSheet02.Cells[i + StartRows, 3].value = specp.OpList01;
                //04-Опросный лист
                workSheet02.Cells[i + StartRows + 1, 3].value = specp.OpList02;
                //05-Код АСУНСИ
                workSheet02.Cells[i + StartRows, 4].value = specp.GroupEq; 
                //06-Код оборудования
                workSheet02.Cells[i + StartRows + 1, 4].value = specp.AsuNsi;
                //07-Единицы измерения
                workSheet02.Cells[i + StartRows, 5].value = specp.InitN;
                //08-Количество
                workSheet02.Cells[i + StartRows, 6].value = specp.CountSpecp;
                //09-Масса
                workSheet02.Cells[i + StartRows, 7].value = specp.Mass;
                //10-Примечание
                workSheet02.Cells[i + StartRows, 8].value = specp.Prim;
                //10-Примечание
                workSheet02.Cells[i + StartRows+1, 8].value = specp.AddPrim;
                i += 3;
            }

            foreach (Specp specp in ListSignSpecpStop())
            {
                //п/п
                //workSheet02.Cells[i + StartRows, 1].value = i + 1;
                //00-Наименование знака
                workSheet02.Cells[i + StartRows, 2].value = specp.TechName01;
                //01-Наименование знака
                workSheet02.Cells[i + StartRows + 1, 2].value = specp.TechName02;
                //02-Наименование знака
                workSheet02.Cells[i + StartRows + 2, 2].value = specp.TechName03;
                //03-Опросный лист
                workSheet02.Cells[i + StartRows, 3].value = specp.OpList01;
                //04-Опросный лист
                workSheet02.Cells[i + StartRows + 1, 3].value = specp.OpList02;
                //05-Код АСУНСИ
                workSheet02.Cells[i + StartRows, 4].value = specp.GroupEq;
                //06-Код оборудования
                workSheet02.Cells[i + StartRows + 1, 4].value = specp.AsuNsi;
                //07-Единицы измерения
                workSheet02.Cells[i + StartRows, 5].value = specp.InitN;
                //08-Количество
                workSheet02.Cells[i + StartRows, 6].value = specp.CountSpecp;
                //09-Масса
                workSheet02.Cells[i + StartRows, 7].value = specp.Mass;
                //10-Примечание
                workSheet02.Cells[i + StartRows, 8].value = specp.Prim;

                if (specp.TechName03 != "")
                    i += 4;
                else
                    i += 3;
            }

            foreach (Specp specp in ListSignSpecpReperMK())
            {
                //п/п
                //workSheet02.Cells[i + StartRows, 1].value = i + 1;
                //00-Наименование знака
                workSheet02.Cells[i + StartRows, 2].value = specp.TechName01;
                //01-Наименование знака
                workSheet02.Cells[i + StartRows + 1, 2].value = specp.TechName02;
                //02-Наименование знака
                workSheet02.Cells[i + StartRows + 2, 2].value = specp.TechName03;
                //03-Опросный лист
                workSheet02.Cells[i + StartRows, 3].value = specp.OpList01;
                //04-Опросный лист
                workSheet02.Cells[i + StartRows + 1, 3].value = specp.OpList02;
                //05-Код АСУНСИ
                workSheet02.Cells[i + StartRows, 4].value = specp.GroupEq;
                //06-Код оборудования
                workSheet02.Cells[i + StartRows + 1, 4].value = specp.AsuNsi;
                //07-Единицы измерения
                workSheet02.Cells[i + StartRows, 5].value = specp.InitN;
                //08-Количество
                workSheet02.Cells[i + StartRows, 6].value = specp.CountSpecp;
                //09-Масса
                workSheet02.Cells[i + StartRows, 7].value = specp.Mass;
                //10-Примечание
                workSheet02.Cells[i + StartRows, 8].value = specp.Prim;

                if (specp.TechName03 != "")
                    i += 4;
                else
                    i += 3;
            }

            //Выделение объекта Range
            Excel.Range rangeAO02 = workSheet02.get_Range("A" + Convert.ToString(StartRows), "H" + Convert.ToString(StartRows + i - 1));
            //толщина линий выделенного диапазона
            rangeAO02.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //размер шрифта
            rangeAO02.Font.Size = TitleSpecpTextSize;
            //выравнивание по горизонтали
            rangeAO02.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO02.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Выделение объекта Range
            Excel.Range rangeDE02 = workSheet02.get_Range("B" + Convert.ToString(StartRows), "B" + Convert.ToString(StartRows + i - 1));
            //выравнивание по горизонтали
            rangeDE02.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //выравнивание по вертикали
            rangeDE02.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeDE02.WrapText = true;
            #endregion

            #region ФОРМИРОВАНИЕ ОБЪЕМОВ РАБОТ
            //Выбор и активация нужного листа
            Excel.Worksheet workSheet03 = AppExcel.Sheets[3];
            workSheet03.Activate();
            //Заполнение шапки таблицы
            TitleTableVolumeWork(workSheet03);
            VolumeWork volumeWorkBase = new VolumeWork();

            //всего нумерация - РАБОТЫ ПО ЗНАКАМ
            i = 0;

            List<VolumeWork> listSignWorkVolume = ListSignWorkVolume(climate);



            foreach (VolumeWork volumeWork in listSignWorkVolume)
            {
                //MessageBox.Show(volumeWork.TechName12.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //п/п
                //workSheet02.Cells[i + StartRows, 1].value = i + 1;
                workSheet03.Cells[i + StartRows + 0, 2].value = volumeWork.TechName01;
                workSheet03.Cells[i + StartRows + 1, 2].value = volumeWork.TechName02;
                workSheet03.Cells[i + StartRows + 2, 2].value = volumeWork.TechName03;
                workSheet03.Cells[i + StartRows + 3, 2].value = volumeWork.TechName04;
                workSheet03.Cells[i + StartRows + 4, 2].value = volumeWork.TechName05;
                workSheet03.Cells[i + StartRows + 5, 2].value = volumeWork.TechName06;
                workSheet03.Cells[i + StartRows + 6, 2].value = volumeWork.TechName07;
                workSheet03.Cells[i + StartRows + 7, 2].value = volumeWork.TechName08;
                workSheet03.Cells[i + StartRows + 8, 2].value = volumeWork.TechName09;
                workSheet03.Cells[i + StartRows + 9, 2].value = volumeWork.TechName10;
                workSheet03.Cells[i + StartRows + 10, 2].value = volumeWork.TechName11;
                workSheet03.Cells[i + StartRows + 11, 2].value = volumeWork.TechName12;
                workSheet03.Cells[i + StartRows + 12, 2].value = volumeWork.TechName13;
                workSheet03.Cells[i + StartRows + 13, 2].value = volumeWork.TechName14;
                //00-Единицы измерения
                workSheet03.Cells[i + StartRows + 0 , 3].value = volumeWork.InitN01;
                workSheet03.Cells[i + StartRows + 1, 3].value = volumeWork.InitN02;
                workSheet03.Cells[i + StartRows + 2, 3].value = volumeWork.InitN03;
                workSheet03.Cells[i + StartRows + 3, 3].value = volumeWork.InitN04;
                workSheet03.Cells[i + StartRows + 4, 3].value = volumeWork.InitN05;
                workSheet03.Cells[i + StartRows + 5, 3].value = volumeWork.InitN06;
                workSheet03.Cells[i + StartRows + 6, 3].value = volumeWork.InitN07;
                workSheet03.Cells[i + StartRows + 7, 3].value = volumeWork.InitN08;
                workSheet03.Cells[i + StartRows + 8, 3].value = volumeWork.InitN09;
                workSheet03.Cells[i + StartRows + 9, 3].value = volumeWork.InitN10;
                workSheet03.Cells[i + StartRows + 10, 3].value = volumeWork.InitN11;
                workSheet03.Cells[i + StartRows + 11, 3].value = volumeWork.InitN12;
                workSheet03.Cells[i + StartRows + 12, 3].value = volumeWork.InitN13;
                workSheet03.Cells[i + StartRows + 13, 3].value = volumeWork.InitN14;
                //02-Значения
                workSheet03.Cells[i + StartRows + 0, 4].value = volumeWork.CountWV01;
                workSheet03.Cells[i + StartRows + 1, 4].value = volumeWork.CountWV02;
                workSheet03.Cells[i + StartRows + 2, 4].value = volumeWork.CountWV03;
                workSheet03.Cells[i + StartRows + 3, 4].value = volumeWork.CountWV04;
                workSheet03.Cells[i + StartRows + 4, 4].value = volumeWork.CountWV05;
                workSheet03.Cells[i + StartRows + 5, 4].value = volumeWork.CountWV06;
                workSheet03.Cells[i + StartRows + 6, 4].value = volumeWork.CountWV07;
                workSheet03.Cells[i + StartRows + 7, 4].value = volumeWork.CountWV08;
                workSheet03.Cells[i + StartRows + 8, 4].value = volumeWork.CountWV09;
                workSheet03.Cells[i + StartRows + 9, 4].value = volumeWork.CountWV10;
                workSheet03.Cells[i + StartRows + 10, 4].value = volumeWork.CountWV11;
                workSheet03.Cells[i + StartRows + 11, 4].value = volumeWork.CountWV12;
                workSheet03.Cells[i + StartRows + 12, 4].value = volumeWork.CountWV13;
                workSheet03.Cells[i + StartRows + 13, 4].value = volumeWork.CountWV14;
                //i += 13;
                i += volumeWorkBase.GetCount(volumeWork)+1;
            }
            //всего нумерация - РАБОТЫ ПО МеталлЛОКОНСТРУКЦИЯМ РЕПЕРОВ
            foreach (VolumeWork volumeWork in ListSignWorkVolumeReperMK())
            {
                //п/п
                //workSheet02.Cells[i + StartRows, 1].value = i + 1;
                //00-Наименование работы
                workSheet03.Cells[i + StartRows, 2].value = volumeWork.TechName01;
                //01-Наименование работы
                workSheet03.Cells[i + StartRows + 1, 2].value = volumeWork.TechName02;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 2, 2].value = volumeWork.TechName03;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 3, 2].value = volumeWork.TechName04;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 4, 2].value = volumeWork.TechName05;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 5, 2].value = volumeWork.TechName06;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 6, 2].value = volumeWork.TechName07;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 7, 2].value = volumeWork.TechName08;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 8, 2].value = volumeWork.TechName09;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 9, 2].value = volumeWork.TechName10;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 10, 2].value = volumeWork.TechName11;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 11, 2].value = volumeWork.TechName12;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 12, 2].value = volumeWork.TechName13;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 13, 2].value = volumeWork.TechName14;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 14, 2].value = volumeWork.TechName15;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 15, 2].value = volumeWork.TechName16;
                //02-Наименование работы
                workSheet03.Cells[i + StartRows + 16, 2].value = volumeWork.TechName17;

                //00-Единицы измерения
                workSheet03.Cells[i + StartRows, 3].value = volumeWork.InitN01;
                //01-Единицы измерения
                workSheet03.Cells[i + StartRows + 1, 3].value = volumeWork.InitN02;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 2, 3].value = volumeWork.InitN03;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 3, 3].value = volumeWork.InitN04;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 4, 3].value = volumeWork.InitN05;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 5, 3].value = volumeWork.InitN06;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 6, 3].value = volumeWork.InitN07;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 7, 3].value = volumeWork.InitN08;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 8, 3].value = volumeWork.InitN09;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 9, 3].value = volumeWork.InitN10;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 10, 3].value = volumeWork.InitN11;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 11, 3].value = volumeWork.InitN12;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 12, 3].value = volumeWork.InitN13;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 13, 3].value = volumeWork.InitN14;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 14, 3].value = volumeWork.InitN15;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 15, 3].value = volumeWork.InitN16;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 16, 3].value = volumeWork.InitN17;
                //02-Значения
                workSheet03.Cells[i + StartRows, 4].value = volumeWork.CountWV01;
                //01-Единицы измерения
                workSheet03.Cells[i + StartRows + 1, 4].value = volumeWork.CountWV02;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 2, 4].value = volumeWork.CountWV03;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 3, 4].value = volumeWork.CountWV04;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 4, 4].value = volumeWork.CountWV05;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 5, 4].value = volumeWork.CountWV06;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 6, 4].value = volumeWork.CountWV07;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 7, 4].value = volumeWork.CountWV08;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 8, 4].value = volumeWork.CountWV09;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 9, 4].value = volumeWork.CountWV10;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 10, 4].value = volumeWork.CountWV11;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 11, 4].value = volumeWork.CountWV12;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 12, 4].value = volumeWork.CountWV13;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 13, 4].value = volumeWork.CountWV14;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 14, 4].value = volumeWork.CountWV15;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 15, 4].value = volumeWork.CountWV16;
                //02-Единицы измерения
                workSheet03.Cells[i + StartRows + 16, 4].value = volumeWork.CountWV17;
                i += 18;
                //i += volumeWorkBase.GetCount(volumeWork);
            }

            //Выделение объекта Range
            Excel.Range rangeAO03 = workSheet03.get_Range("A" + Convert.ToString(StartRows), "D" + Convert.ToString(StartRows + i - 1));
            //толщина линий выделенного диапазона
            rangeAO03.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //размер шрифта
            rangeAO03.Font.Size = TitleSpecpTextSize;
            //выравнивание по горизонтали
            rangeAO03.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO03.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Выделение объекта Range
            Excel.Range rangeDE03 = workSheet03.get_Range("B" + Convert.ToString(StartRows), "B" + Convert.ToString(StartRows + i - 1));
            //выравнивание по горизонтали
            rangeDE03.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //выравнивание по вертикали
            rangeDE03.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeDE03.WrapText = true;

            #endregion

            AppExcel.Visible = true;
        }

    }
}
