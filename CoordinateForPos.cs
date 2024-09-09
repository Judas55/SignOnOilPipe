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

namespace GlobalSign
{
    
    
    public class CoordinateForPos
    {
        //габариты границы землеотводав
        public const double OneRack = 1;
        //габариты границы землеотводав
        public const double TwoRack = 2;

        private Point3d[] point;

        private int number;

        // конструктор
        public CoordinateForPos(BlockReference blockRef)
        {

            point = new Point3d[4];

            if (blockRef != null)
            {
                // нулевой объект
                Polyline poly;
                //Создаем коллекцию объектов
                DBObjectCollection dbObjCol = new DBObjectCollection ();
                //Ломаем блок виртуально в колекцию
                blockRef.Explode(dbObjCol);
                //перебираем все входящие в колецию элементы
                foreach (DBObject dbObj in dbObjCol)
                {
                    if (dbObj.GetType() == typeof(Polyline))
                    {
                        poly = (Polyline)dbObj;
                        for (int i = 0; i < poly.NumberOfVertices - 1; i++)
                        {
                            point[i] = poly.GetPoint3dAt(i);
                        }
                        break;
                    }
                }
            }
            else 
            {
                MessageBox.Show("Отсутствуют заданные блоки ППО", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Point3d[] Point
        {
            get { return point; }
            set { point = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

    }

}
