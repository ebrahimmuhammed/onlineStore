
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.controllers
{   
    public class ProductsController : Controller
    {
        private readonly OnlineStoreContext _context;

        public ProductsController(OnlineStoreContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> Index(){
            var products=await _context.Products.ToListAsync();
            return View(products);

        }


        public async Task<IActionResult> Details(int? id){
            if(id==null)
            {
                return NotFound();
            }
            var product= await _context.Products.FirstOrDefaultAsync(m=>m.Id ==id);
            if(product == null){
                return NotFound();
            }
            return View(product);
        }



            public IActionResult Create(){

                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("Id,Name,Description")]Product product)
            {
                if(ModelState.IsValid)
                {
                    _context.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }


            public async Task<IActionResult> Edit(int? id)
            {
                if(id==null){
                    return NotFound();
                }
                var product=await _context.Products.FindAsync(id);
                if(product==null){
                    return NotFound();
                }
                return View(product);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Price,Description")]Product product)
            {
                if(id !=product.Id){
                    return NotFound();
                }
                if(ModelState.IsValid){
                    try{
                        _context.Update(product);
                        await  _context.SaveChangesAsync();
                    }

                    catch(DbUpdateConcurrencyException)
                    {
                        if(!ProductExists(product.Id)){
                            return NotFound();
                        }
                        else{
                            throw;
                        }

                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(product);

            }

            public async Task<IActionResult> Delete(int? id)
            {
                if(id==null){
                    return NotFound();  
                                  }
            

            var product = await _context.Products.FirstOrDefaultAsync(m=>m.Id==id);

            if(product == null)
            {
                return NotFound();
            }
             return View(product);
            }



            [HttpPost,ActionName("Delete")]
            [ValidateAntiForgeryToken]

            public async  Task<IActionResult> DeleteConfirmed(int id){

                var product= await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            private bool ProductExists(int id)
            {
                return _context.Products.Any(e=>e.Id==id);
            }
            


    }
}