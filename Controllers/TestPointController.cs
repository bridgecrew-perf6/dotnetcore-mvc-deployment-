using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dacon_exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dacon_exam.Controllers
{
    public class TestPointController : Controller
    {
         private readonly AppDbContext _context;

         public TestPointController(AppDbContext context){
             _context = context;
         }

        [HttpGet]
        public IActionResult Index(int id,int id2){
            
           
            var cml_all = _context.Cmls.Where(x=>x.cml_id == id).FirstOrDefault();
            var info_all = _context.infos.Where(x=>x.Info_id == cml_all.info_id).FirstOrDefault();
            var tp_all = _context.TestPoints.Where(x=>x.cml_id == id).ToList();

            ViewBag.back_id = info_all.Info_id; 
            ViewBag.data = info_all.line_number;
            ViewBag.cml_id = cml_all.cml_id;
            ViewBag.TestPoint = tp_all;
            
            return View();
        }

        [HttpGet]
        public IActionResult Create(int id,string id2){
            
            ViewBag.cml_id = id;
            ViewBag.data = id2;
          
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id,string id2,TestPoint tp){
            
            if(ModelState.IsValid){

                try{

                    await _context.TestPoints.AddAsync(tp);

                    tp.cml_id = id;
                    tp.line_number = id2;

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index","TestPoint", new {id = id });

                }catch (Exception ex){
                    
                    ModelState.AddModelError(string.Empty, $"Someting went wronmg {ex.Message}");
                }

            }
            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(tp);


        }

        [HttpPost]
        public JsonResult GetData([FromBody] TestPoint obj){
            
            TestPoint data = new TestPoint();

            if(obj.tp_id != null){

                data = _context.TestPoints.Where(x=>x.tp_id == obj.tp_id).FirstOrDefault();
            }
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TestPoint tp)
        {
                
            if (ModelState.IsValid)
                {
                try
                {
                    
                    var edit_tp = _context.TestPoints.Where(x => x.tp_id == tp.tp_id).FirstOrDefault();

                        if(edit_tp != null)
                        {
                            edit_tp.tp_number = tp.tp_number;
                            edit_tp.tp_description = tp.tp_description;
                            edit_tp.note = tp.note;
                       
                        
                        
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Index","TestPoint", new {id = edit_tp.cml_id });
                        }
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                    }
                }

                ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

                return View(tp);
            }

        [HttpGet]
        public async Task<IActionResult> Delete(int id){
            var tp_delete = _context.TestPoints.Where(x=>x.tp_number == id).FirstOrDefault();

            if(tp_delete != null){
                 _context.Remove(tp_delete);
                await _context.SaveChangesAsync();
                
            }
            
            return RedirectToAction("Index","TestPoint", new {id = tp_delete.cml_id });
        }

        [HttpGet]
        public IActionResult DataTable(){

            var all_data = (from tp in _context.TestPoints  
                            orderby tp.cml_id
                            select tp);
                        
            
            ViewBag.data = all_data;
        
            return View();
        }
    }
}