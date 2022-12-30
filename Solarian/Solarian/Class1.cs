using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows.Data;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace Lab1
{
    public class Class1
    {
        [CommandMethod("HelloWorld")]
        public void HelloWorld()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            foreach (HatchRibbonItem hatchRibItm in HatchPatterns.Instance.AllPatterns)
            {
                ed.WriteMessage("\n" + hatchRibItm.ToString());
            }
        }

        [CommandMethod("solMassProp", CommandFlags.UsePickSet)]
        public static void solMassProp()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            // Ask the user to select a solid
            PromptEntityOptions peo = new PromptEntityOptions("Select a 3D solid");
            peo.SetRejectMessage("\nA 3D solid must be selected.");
            peo.AddAllowedClass(typeof(Solid3d), true);
            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK)
                return;

            Transaction tr = db.TransactionManager.StartTransaction();
            using (tr)
            {
                Solid3d sol = tr.GetObject(per.ObjectId, OpenMode.ForRead) as Solid3d;
                Solid3dMassProperties solMassProp = sol.MassProperties;
                Vector3d XAxisVec = solMassProp[0];
                Vector3d YAxisVec = solMassProp[1];
                Vector3d ZAxisVec = solMassProp[2];

                ed.WriteMessage("\n x:" + XAxisVec.X.ToString() + " y:" + XAxisVec.Y.ToString() + " z:" + XAxisVec.Z.ToString());
                ed.WriteMessage("\n x:" + YAxisVec.X.ToString() + " y:" + YAxisVec.Y.ToString() + " z:" + YAxisVec.Z.ToString());
                ed.WriteMessage("\n x:" + ZAxisVec.X.ToString() + " y:" + ZAxisVec.Y.ToString() + " z:" + ZAxisVec.Z.ToString());

            }
        }
    }
}
