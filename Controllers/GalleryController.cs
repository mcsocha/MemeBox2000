using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MemeBox2000.Models;
using System.Xml.Serialization;

namespace MemeBox2000.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Index(string genreFilter = null)
        {
            List<Meme> model = Util.DeserializeMemeXml();

            if (genreFilter != null)
                model = model.Where(m => m.Genre == genreFilter).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            Meme meme = Util.DeserializeMemeXml().FirstOrDefault(m => m.ID == Convert.ToString(id));

            return View(meme);
        }

        [HttpPost]
        public ActionResult Detail(Meme model)
        {
            Meme meme;
            if (ModelState.IsValid)
            {
                List<Meme> memes = Util.DeserializeMemeXml();

                meme = (Meme)memes.Where(m => m.ID == model.ID).FirstOrDefault();
                meme.Description = model.Description; //replace
                meme.Genre = model.Genre;
                meme.Title = model.Title;

                Util.SerializeMemeXml(memes);

                ViewBag.Message = "Meme updated! ID: " + model.ID;
            }
            else
                meme = model;


            return View(meme);
        }

        public ActionResult Delete(int id)
        {
            var memes = Util.DeserializeMemeXml();
            memes.RemoveAll(m => m.ID == Convert.ToString(id));
            Util.SerializeMemeXml(memes);

            return View("Index", memes);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(Meme model)
        {
            var file = Request.Files["file"];

            if (file == null)
                ModelState.AddModelError("file", "A file hasn't been selected");

            if (ModelState.IsValid && file != null)
            {
                List<Meme> memes = Util.DeserializeMemeXml();

                string id = Convert.ToString(Util.GetNewMemeID(memes));
                string ext = "." + Util.GetExtension(file.FileName);
                var path = Path.Combine(Util.UploadsPath, id + ext);
                file.SaveAs(path);

                model.ID = id;
                model.FileExtension = ext;
                memes.Add(model);
                Util.SerializeMemeXml(memes);

                ViewBag.Message = "Meme submitted! ID: " + id;
            }


            return View();
        }
    }
}
