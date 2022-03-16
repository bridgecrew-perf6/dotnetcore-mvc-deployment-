using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dacon_exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dacon_exam.Controllers
{
    public class ThicknessController : Controller
    {
        private readonly AppDbContext _context;

         public ThicknessController(AppDbContext context){
             _context = context;
         }

        
        [HttpGet]
        public IActionResult Index(int id,int id2){
            
           
            var tp_all = _context.TestPoints.Where(x=>x.tp_id == id).FirstOrDefault();
            var cml_all = _context.Cmls.Where(x=>x.cml_id == tp_all.cml_id).FirstOrDefault();
            var info_all = _context.infos.Where(x=>x.Info_id == cml_all.info_id).FirstOrDefault();
            var tn_all = _context.Thicknesses.Where(x=>x.tp_number == id).ToList();
            
            ViewBag.back_id = id2; 
            ViewBag.data = info_all.line_number;
            ViewBag.cml_id = cml_all.cml_id;
            ViewBag.tp_id = tp_all.tp_id;
            // ViewBag.tp_number = tp_all.tp_number;
            ViewBag.Thickness = tn_all;

            return View();
        }

        [HttpGet]
        public IActionResult Create(int id,int id2,string id3){
          
            ViewBag.cml_id = id;
            ViewBag.tp_id = id2;
            ViewBag.data = id3;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id , int id2, string id3 ,Thickness tn){
            
            // var find_id = _context.TestPoints.Where(x=>x.tp_id == id2).FirstOrDefault();
            // var temp = find_id.tp_number;
            
            if(ModelState.IsValid){

            
                    await _context.Thicknesses.AddAsync(tn);
                    
                    tn.cml_id = id;
                    tn.tp_number = id2;
                    tn.line_number = id3;
                
                    await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index","Thickness", new {id = id2 });
        }

        [HttpPost]
        public JsonResult GetData([FromBody] Thickness obj){
            
            Thickness data = new Thickness();

            if(obj.thickness_id != null){

                data = _context.Thicknesses.Where(x=>x.thickness_id == obj.thickness_id).FirstOrDefault();
            }
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Thickness tn)
        {
                
            if (ModelState.IsValid)
                {
                try
                {
                    
                    var edit_tn = _context.Thicknesses.Where(x => x.thickness_id == tn.thickness_id).FirstOrDefault();

                        if(edit_tn != null)
                        {

                            edit_tn.inspection_date = tn.inspection_date;
                            edit_tn.actual_thickness = tn.actual_thickness;
                       
                        
                        
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Index","Thickness", new {id = edit_tn.tp_number });
                        }
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                    }
                }

                ModelState.AddModelError(string.Empty, $"Something went wrong, invalid modell");

                return View(tn);
            }

        [HttpGet]
        public async Task<IActionResult> Delete(int id){
            var tn_delete = _context.Thicknesses.Where(x=>x.thickness_id == id).FirstOrDefault();

            if(tn_delete != null){
                 _context.Remove(tn_delete);
                await _context.SaveChangesAsync();
                
            }
            
            return RedirectToAction("Index","Thickness", new {id = tn_delete.tp_number });
        }

        [HttpGet]
        public IActionResult DataTable(){

            var all_data = (from tp in _context.Thicknesses  
                            orderby tp.cml_id
                            select tp);
            
            ViewBag.data = all_data;
        
            return View();
        }

    }
}