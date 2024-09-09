using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Windows.Forms;
using AccessToDocument = AcadMain.AccessToDocument;


namespace GlobalSign
{
    //Класс обработки данных чертежа
    public class CoordinateForPosCreatTable
    {
        #region Константы заполнения
        //Константа пустота для строк
        private const string Report = "Координаты знаков ЛО";

        #endregion

        #region Поля CreatTable
        //Поле - коэффициент сжатия высоты текста атрибута
        private string franklingothicbook;
        //Поле - размер текст заполнения
        private int textsize;
        //Поле - номер строки для заполнения таблицы эксел
        private int startrows;

        //Поле - ширина титула сводки
        private int titleReportrowheight;
        //Шапка сводки
        private string[] titleReport;
        //Разметка столбцов сводки
        private int[] titleReportSize;
        
        //Шапка сводки
        private string[] titleCoordinate;
        //Разметка столбцов сводки
        private int[] titleCoordinateSize;

        #endregion Поля CreatTable

        //Конструктор
        public CoordinateForPosCreatTable()
        {
            franklingothicbook = "Franklin Gothic Book";
            textsize = 12;
            startrows = 2;
            titleReportrowheight = 16;

            titleReport = new string[6]
            {
                "п/п",
                "Номер по атрибуту",
                "Точка 1",
                "Точка 2",
                "Точка 2",
                "Точка 2"
            };
            titleReportSize = new int[6]
            {
                6,
                12,
                24,
                24,
                24,
                24
            };

            titleReport = new string[6]
              {
                "п/п",
                "Номер по атрибуту",
                "Точка 1",
                "Точка 2",
                "Точка 3",
                "Точка 4"
              };
            titleReportSize = new int[6]
            {
                6,
                12,
                24,
                24,
                24,
                24
            };

            titleCoordinate = new string[2]
            {
                "X",
                "Y"
            };

            titleCoordinateSize = new int[2]
            {
                12,
                12
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
        public int TitleReportRowHeight
        {
            get { return titleReportrowheight; }
            set { titleReportrowheight = value; }
        }

        public string[] TitleReport
        {
            get { return titleReport; }
            set { titleReport = value; }
        }

        public int[] TitleReportSize
        {
            get { return titleReportSize; }
            set { titleReportSize = value; }
        }

        public string[] TitleCoordinate
        {
            get { return titleCoordinate; }
            set { titleCoordinate = value; }
        }

        public int[] TitleCoordinateSize
        {
            get { return titleCoordinateSize; }
            set { titleCoordinateSize = value; }
        }
        #endregion

        private static List<CoordinateForPos> ListCoordinateForPos()
        {
            //Получаем ссылку на документ
            AccessToDocument AcadDoc = new AccessToDocument();
            //получаем ссылку на БД
            Database AcadDB = AcadDoc.DBase;
            // Список имен блоков
            List<string> listNameBlock = new List<string>() 
            {
                SignBase.posNameSign + SignBase.OneRack,
                SignBase.posNameSign + SignBase.TwoRack,
                //SignBase.posNameSign + SignBase.OnFencing,
                //SignBase.posNameSign + SignBase.OnPole,
                //SignBase.posNameSign + SignBase.OneRackExist,
                //SignBase.posNameSign + SignBase.OneRackPipe,
            };

            //Список блоков
            List<CoordinateForPos> listCoordinateForPos = new List<CoordinateForPos>();
            // начинаем транзакцию
            using (Transaction tr = AcadDB.TransactionManager.StartTransaction())
            {
                // получаем таблицу блоков и проходим по всем записям таблицы блоков
                BlockTable blockTable = (BlockTable)tr.GetObject(AcadDB.BlockTableId, OpenMode.ForRead);
                // формировани списка
                foreach (string nameBlock in listNameBlock)
                {
                    if (blockTable.Has(nameBlock)) //0 - индекс имя в базе
                    {
                        //получения переменной  - таблицы блоков чертежа
                        BlockTableRecord LocalBlockRef = blockTable[nameBlock].GetObject(OpenMode.ForRead) as BlockTableRecord;
                        //выбираем все объекты
                        ObjectIdCollection C = LocalBlockRef.GetBlockReferenceIds(true, true);
                        foreach (ObjectId id in C)
                        {
                            BlockReference blockRefSign = (BlockReference)tr.GetObject(id, OpenMode.ForRead);
                            Autodesk.AutoCAD.DatabaseServices.AttributeCollection AtrCol = blockRefSign.AttributeCollection;
                            
                            //Обработка километровых знаков
                            CultureInfo cultures = new CultureInfo("ru-Ru");
                            listCoordinateForPos.Add(new CoordinateForPos(blockRefSign)
                            {
                                Number = Convert.ToInt32((AtrCol[4].GetObject(OpenMode.ForWrite) as AttributeReference).TextString, cultures),
                            });
                        }
                    }
                }
                tr.Commit();
            }

            // сортировка списка
            listCoordinateForPos.Sort(delegate (CoordinateForPos x, CoordinateForPos y) { return x.Number.CompareTo(y.Number); });

            return listCoordinateForPos;
        }

        //Метод формирования шапки сводки
        public void TitleTableReport(Excel.Worksheet workSheet)
        {
            //Установка ширины титульной строки
            workSheet.Rows[StartRows - 1].RowHeight = TitleReportRowHeight;
            workSheet.Rows[StartRows].RowHeight = TitleReportRowHeight;
            
            //Заполнение начала
            for (int i = 0; i <= 1; i++)
            {
                workSheet.Range[workSheet.Cells[StartRows, i + 1], workSheet.Cells[StartRows - 1, i + 1]].Merge();
                workSheet.Cells[StartRows - 1, i + 1] = TitleReport[i];
                workSheet.Columns[i + 1].ColumnWidth = TitleReportRowHeight;
            }
            // Заполение шапки по точкам
            for (int i = 2; i <= TitleReport.Length - 1; i++)
            {
                workSheet.Range[workSheet.Cells[StartRows - 1, 2 * i - 1], workSheet.Cells[StartRows - 1, 2*i]].Merge();
                workSheet.Cells[StartRows-1, 2 * i - 1] = TitleReport[i];
                workSheet.Columns[2 * i - 1].ColumnWidth = TitleReportRowHeight;
                workSheet.Columns[2 * i ].ColumnWidth = TitleReportRowHeight;
            }
            // заполение XY
            for (int i = 2; i <= TitleReport.Length - 1; i++)
            {
                workSheet.Cells[StartRows, 2 * i - 1] = titleCoordinate[0];
                workSheet.Cells[StartRows, 2 * i ] = titleCoordinate[1];
            }

            
            //Форматирование ячеек - просто оформление
            Excel.Range range = workSheet.get_Range("A" + Convert.ToString(StartRows - 1), "J" + Convert.ToString(StartRows));
            //тип линии таблицы
            range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //Жирность
            range.Font.Bold = true;
            //размер шрифта
            range.Font.Size = TextSize;
            //название шрифта
            range.Font.Name = FranklinGothicBook;
            //выравнивание по горизонтали
            range.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            range.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            range.WrapText = true;
        }

        [CommandMethod("PosCreatTable")]
        public void PosCreatTable()
        {
            //Получаем доступ к объекту Excel
            Excel.Application AppExcel = new Excel.Application() { Visible = false };
            AppExcel.Workbooks.Add(Type.Missing);
            //Добавляем 3 листа в новую книгу
            AppExcel.SheetsInNewWorkbook = 3;
            //Переименовываем все листы
            AppExcel.Worksheets[1].Name = Report;
            //Выбор и активация нужного листа
            Excel.Worksheet workSheet01 = AppExcel.Sheets[1];
            workSheet01.Activate();
            //Заполнение шапки таблицы
            TitleTableReport(workSheet01);
            // нумерация блоков
            int i = 1;  
            foreach (CoordinateForPos coordinateForPos in ListCoordinateForPos())
            {
                workSheet01.Cells[i + StartRows, 1].value = i;
                workSheet01.Cells[i + StartRows, 2].value = coordinateForPos.Number;
                for (int j = 0; j <= coordinateForPos.Point.Length-1; j++)
                {
                    workSheet01.Cells[i + StartRows, 2 * j + 3].value = coordinateForPos.Point[j].X;
                    workSheet01.Cells[i + StartRows, 2 * j + 4].value = coordinateForPos.Point[j].Y;
                }
                i++;
            }
            //Выделение объекта Range
            Excel.Range rangeAO01 = workSheet01.get_Range("A" + Convert.ToString(StartRows), "J" + Convert.ToString(StartRows + i-1));
            //толщина линий выделенного диапазона
            rangeAO01.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //размер шрифта
            rangeAO01.Font.Size = TextSize;
            //выравнивание по горизонтали
            rangeAO01.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeAO01.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Выделение объекта Range
            Excel.Range rangeDE01 = workSheet01.get_Range("A" + Convert.ToString(StartRows), "J" + Convert.ToString(StartRows + i-1));
            //выравнивание по горизонтали
            rangeDE01.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //выравнивание по вертикали
            rangeDE01.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            //Перенос по словам
            rangeDE01.WrapText = true;
            AppExcel.Visible = true;
        }
    }
}
//MessageBox.Show(TitleReport[i], "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);