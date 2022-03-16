using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dacon_exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dacon_exam.Controllers
{
    public class InfoController : Controller
    {
        private readonly AppDbContext _context;


        public InfoController(AppDbContext context){
            _context= context;
        }

        [HttpGet]
        public IActionResult Index(){
            ViewBag.Info = _context.infos.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Info info){
            if(ModelState.IsValid){

                try{

                    await _context.infos.AddAsync(info);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");

                }catch (Exception ex){
                    
                    ModelState.AddModelError(string.Empty, $"Someting went wronmg {ex.Message}");
                }

            }
            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(info);


        }
        
    [HttpPost]
    public JsonResult GetData([FromBody] Info obj){
        
        Info data = new Info();

        if(obj.Info_id != null){

            data = _context.infos.Where(x=>x.Info_id == obj.Info_id).FirstOrDefault();
        }
        return Json(data);
    } 
    
    [HttpPost]
    public async Task<IActionResult> EditData(Info info)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                
                    var edit_info = _context.infos.Where(x => x.Info_id == info.Info_id).FirstOrDefault();

                    if(edit_info != null)
                    {
                        edit_info.line_number = info.line_number;
                        edit_info.location = info.location;
                        edit_info.from = info.from;
                        edit_info.to = info.to;
                        edit_info.drawing_number = info.drawing_number;
                        edit_info.service = info.service;
                        edit_info.material = info.material;
                        edit_info.inservice_date = info.inservice_date;
                        edit_info.pipe_size = info.pipe_size;
                        edit_info.original_thickness = info.original_thickness;
                        edit_info.stress = info.stress;
                        edit_info.joint_efficiency = info.joint_efficiency;
                        edit_info.ca = info.ca;
                        edit_info.design_life = info.design_life;
                        edit_info.design_pressure = info.design_pressure;
                        edit_info.design_temperature = info.design_temperature;
                        edit_info.operating_pressure = info.operating_pressure;
                        edit_info.operating_temperature = info.operating_temperature;
                    
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(info);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id){
            var info_delete = _context.infos.Where(x=>x.Info_id == id).FirstOrDefault();

            if(info_delete != null){
                _context.Remove(info_delete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DataTable(){

            ViewBag.all_data = _context.infos.ToList();
            return View();
        }


       
    }

  


}