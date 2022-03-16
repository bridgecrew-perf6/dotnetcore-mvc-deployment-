using System;
using System.Linq;
using System.Threading.Tasks;
using dacon_exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dacon_exam.Controllers
{
    public class CmlController : Controller
    {
        private readonly AppDbContext _context;

        public CmlController(AppDbContext context){
            _context = context;
        }

        
         [HttpGet]
        public ActionResult Index(int id){
            
            
            var info_all = _context.infos.Where(x=>x.Info_id == id).FirstOrDefault();
            var cml_all = _context.Cmls.Where(x=>x.info_id == id).ToList();

            if(info_all != null)
                ViewBag.data = info_all.line_number;
            ViewBag.ID = id;
            
            ViewBag.Cml = cml_all;

        
            return View();
        }

        [HttpGet]
        public IActionResult Create(int id){
            
            
            ViewBag.ID = id;
            
            
            #region หา Actual Outside diamater และ Structural thickness
            
            var info_all = _context.infos.Where(x=>x.Info_id == id).FirstOrDefault();

            ViewBag.actual_outside_diameter = 0;
            ViewBag.structural_thickness = 0;
            

            if(info_all.pipe_size <=2 )
                ViewBag.structural_thickness = 1.80;
            
            if(info_all.pipe_size == 0.125)
                ViewBag.actual_outside_diameter = 10.300;
            
            else if(info_all.pipe_size == 0.25)
                ViewBag.actual_outside_diameter = 13.700;
            
            else if(info_all.pipe_size == 0.357)
                ViewBag.actual_outside_diameter = 17.100;
            
            else if(info_all.pipe_size == 0.5)
                ViewBag.actual_outside_diameter = 21.300;
            
            else if(info_all.pipe_size == 0.75)
                ViewBag.actual_outside_diameter = 26.700;
            
            else if(info_all.pipe_size == 1)
                ViewBag.actual_outside_diameter = 33.400;
            
            else if(info_all.pipe_size == 1.25)
                ViewBag.actual_outside_diameter = 42.200;
            
            else if(info_all.pipe_size == 1.5)
                ViewBag.actual_outside_diameter = 48.300;
            
            else if(info_all.pipe_size == 2){
                ViewBag.actual_outside_diameter = 63.300;
              
            }

            else if(info_all.pipe_size == 2.5)
                ViewBag.actual_outside_diameter = 73.000;
            
            else if(info_all.pipe_size == 3){
                ViewBag.actual_outside_diameter = 88.900;
                ViewBag.structural_thickness = 2.00;
            }

            else if(info_all.pipe_size == 3.5)
                ViewBag.actual_outside_diameter = 101.600;
            
            else if(info_all.pipe_size == 4){
                ViewBag.actual_outside_diameter = 114.300;
                ViewBag.structural_thickness = 2.30;
            }
                
            
            else if(info_all.pipe_size == 5)
                ViewBag.actual_outside_diameter = 141.300;
            
            else if(info_all.pipe_size == 6){
                ViewBag.actual_outside_diameter = 168.300;
                ViewBag.structural_thickness = 2.80;
            }
                
            
            else if(info_all.pipe_size == 8){
                ViewBag.actual_outside_diameter = 219.100;
                ViewBag.structural_thickness = 2.80;;
            }
                
            
            if(info_all.pipe_size == 10){
                ViewBag.actual_outside_diameter = 273.000;
                ViewBag.structural_thickness = 2.80;
            }
               
            
            else if(info_all.pipe_size == 12){
                ViewBag.actual_outside_diameter = 323.800;
                ViewBag.structural_thickness = 2.80;
            }
            
            else if(info_all.pipe_size == 14){
                ViewBag.actual_outside_diameter = 355.600;
                ViewBag.structural_thickness = 2.80;
            }
            
            else if(info_all.pipe_size == 16){
                ViewBag.actual_outside_diameter = 406.400;
                ViewBag.structural_thickness = 2.80;;
            }
            
            else if(info_all.pipe_size == 18){
                ViewBag.actual_outside_diameter = 457.000;
                ViewBag.structural_thickness = 2.80;
            }
            
            else{
                ViewBag.structural_thickness = 3.10;
            }

            
            #endregion
            
            
            #region คำนวณ Design thickness

            double first = info_all.design_pressure * ViewBag.actual_outside_diameter;
            double second = 2*info_all.stress* info_all.joint_efficiency;
            double third = 2 * info_all.design_pressure * 0.4;

            ViewBag.design_thickness = first / (second + third);
            ViewBag.design_thickness = Math.Round(ViewBag.design_thickness, 3);

            #endregion
            

            #region Required thickness
            
            if(ViewBag.design_thickness > ViewBag.structural_thickness)
                
                ViewBag.required_thickness = ViewBag.design_thickness;
            
            else
                ViewBag.required_thickness = ViewBag.structural_thickness;
            
            #endregion
            
            return View();
        }
        
        

        [HttpPost]
        public async Task<IActionResult> Create(int id,Cml cml){
            if(ModelState.IsValid){

                try{

                    await _context.Cmls.AddAsync(cml);

                    cml.info_id = id;

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index","Cml", new {id = id });

                }catch (Exception ex){
                    
                    ModelState.AddModelError(string.Empty, $"Someting went wronmg {ex.Message}");
                }

            }
            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(cml);


        }

        [HttpPost]
        public JsonResult GetData([FromBody] Cml obj){
            
            Cml data = new Cml();

            if(obj.cml_id != null){

                data = _context.Cmls.Where(x=>x.cml_id == obj.cml_id).FirstOrDefault();
            }
            return Json(data);
        }

    
    [HttpPost]
    public async Task<IActionResult> Edit(Cml cml)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                   
                    var edit_cml = _context.Cmls.Where(x => x.cml_id == cml.cml_id).FirstOrDefault();

                    if(edit_cml != null)
                    {
                        edit_cml.cml_number = cml.cml_number;
                        edit_cml.cml_description = cml.cml_description;
                        // edit_cml.actual_outside_diameter = cml.actual_outside_diameter;
                        // edit_cml.design_thickness = cml.design_thickness;
                        // edit_cml.structural_thickness = cml.structural_thickness;
                        // edit_cml.required_thickness = cml.required_thickness;
                    
                    
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index","Cml", new {id = edit_cml.info_id });
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(cml);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id){
            var cml_delete = _context.Cmls.Where(x=>x.cml_id == id).FirstOrDefault();

            if(cml_delete != null){
                _context.Remove(cml_delete);
                await _context.SaveChangesAsync();
                
            }
    
            return RedirectToAction("Index","Cml", new {id = cml_delete.info_id });
            
        }

        
        [HttpGet]
        public IActionResult DataTable(){

            var all_data = (from info in _context.infos
                            join cml in _context.Cmls
                            on info.Info_id equals cml.info_id
                            orderby info.line_number 
                            select new Cml  {
                                line_number = info.line_number,
                                cml_number = cml.cml_number,
                                cml_description = cml.cml_description,
                                actual_outside_diameter = cml.actual_outside_diameter,
                                design_thickness = cml.design_thickness,
                                structural_thickness = cml.structural_thickness,
                                required_thickness = cml.required_thickness
                            });
            
            ViewBag.data = all_data;
        
            return View();
        }
  
        
    }
}