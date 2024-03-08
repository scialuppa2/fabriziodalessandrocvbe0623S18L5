using progetto_settimanaleS18L5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace progetto_settimanaleS18L5.Controllers
{
    public class CameraController : Controller
    {
        public ActionResult CreaCamera()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreaCamera(Camera model)
        {
            if (ModelState.IsValid)
            {
                model.InserisciCamera();
                return RedirectToAction("ListaCamera");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ListaCamera()
        {
            var camera = new Camera().ListaCamera();
            return View(camera);
        }

        [HttpPost]
        public ActionResult EliminaCamera(int IdCamera)
        {
            var camera = new Camera().GetCameraById(IdCamera);
            if (camera != null)
            {
                camera.EliminaCamera();
            }
            return RedirectToAction("ListaCamera");
        }




    }
}