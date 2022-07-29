using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Concrete.EntityFramework.Database;
using Microsoft.AspNetCore.Http;
using ENTİTY.Concrete.POCO;
using Web_UI.Areas.Admin.Data;

namespace WebApp_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MyProjectDbContext _context;

        public ProductsController(MyProjectDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var result =
                await _context.Product
                .Include(x => x.ProductCategory)
                .ThenInclude(x => x.Category)
                .ToListAsync();
            return View(result);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.CategoryGroup = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductAdminCreateDTO product)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ProductImagies.Count() > 0)
                    {
                        List<string> diziImage = new List<string>();
                        foreach (IFormFile item in product.ProductImagies)
                        {
                            var newUrl = System.IO.Path.Combine("/ProductImage/" + Guid.NewGuid().ToString() +
                                System.IO.Path.GetExtension(item.FileName)
                                );

                            var serverUrl = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot" + newUrl;
                            System.IO.FileStream stream =
                                new System.IO.FileStream(serverUrl, System.IO.FileMode.Create);
                            await item.CopyToAsync(stream);
                            stream.Close();
                            diziImage.Add(newUrl);
                        }

                        var stratgy = _context.Database.CreateExecutionStrategy();

                        await stratgy.ExecuteAsync(async () =>
                        {
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                try
                                {
                                    Product product1 = new Product
                                    {
                                        Description = product.Description,
                                        Name = product.Name,
                                        Price = product.Price,
                                        Stok = product.Stok
                                    };
                                    _context.Product.Add(product1);
                                    await _context.SaveChangesAsync();

                                    foreach (var item in product.CatgoryId)
                                    {
                                        _context.ProductCategory.Add(new ProductCategory
                                        {
                                            ProductId = product1.Id,
                                            CategoryId = item
                                        });
                                    }

                                    foreach (var item in diziImage)
                                    {
                                        _context.ProductImage.Add(new ProductImage
                                        {
                                            ImageUrl = item,
                                            ProductId = product1.Id
                                        });
                                    }
                                    await _context.SaveChangesAsync();
                                    await transaction.CommitAsync();
                                }
                                catch (Exception ex)
                                {
                                    await transaction.RollbackAsync();
                                }
                            }
                        });
                        return RedirectToAction(actionName: "Index");
                    }

                    else
                    {
                        ModelState.AddModelError("ProductImagies", "Ürün Resmi Zorunlu Alan");
                        return View(product);
                    }
                    //Product =>ProductID
                    //ProductCategory => ProductID and CategoryId
                    //ProductImage =>ProductID ProductImageUrl
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            else
            {
                return View(product);
            }
            //return View(product);
            return View();
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Price,Stok,Description,Id,Active,Deleted,Created,Update")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
/*
 
  <script>
        var container = document.getElementById('container');
        var span = document.createElement('span');
        container.appendChild(span);

        console.log(`docscrollHeight:${document.documentElement.scrollHeight}`);
        console.log("innerHeight:" + window.innerHeight);
        console.log("scrollY:" + window.scrollY);
        document.addEventListener('scroll', () => {
            console.log("***********************************");
            console.log("scrollY:" + window.scrollY);
            console.log("innerHeight:" + window.innerHeight);
            console.log(`docscrollHeight:${document.documentElement.scrollHeight}`);
            //console.log(document.documentElement.scrollHeight - window.scrollY);
            // if ((document.documentElement.scrollHeight - window.scrollY) == window.innerHeight) {
            //     var container = document.getElementById('container');
            //     var span = document.createElement('span');
            //     span.className="Test";
            //     container.appendChild(span);
            // }
            if ((document.documentElement.scrollHeight - window.innerHeight) <= window.scrollY+100) {
                var container = document.getElementById('container');
                var span = document.createElement('span');
                span.className="Test";
                container.appendChild(span);
            }
        });
    </script>
 */

/*
 <!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-KyZXEAg3QhqLMpG8r+8fhAXLRk2vvoC2f3B09zVXn8CA5QIVfZOJ3BCsw2P0p/We" crossorigin="anonymous">
    <style>
        .error {
            color: red;
        }
    </style>
</head>

<body>
    <!-- Button trigger modal -->
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
    Launch demo modal
  </button>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <form id="testForm" autocomplete="off">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <input type="text" name="firstname">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary" id="btn">Save changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>


    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js" integrity="sha384-eMNCOe7tC1doHpGoWe/6oMVemdAVTMs2xqW4mwXrXsW0L84Iytr2wi5v2QjrP/xp" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/js/bootstrap.min.js" integrity="sha384-cn7l7gDp0eyniUwwAZgrzD06kc/tftFf19TOAs2zVinnD/C7E91j9yyk5//jjpt/" crossorigin="anonymous"></script>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.3/jquery.validate.min.js" integrity="sha512-37T7leoNS06R80c8Ulq7cdCDU5MNQBwlYoy1TX/WUsLFC2eYNqtKlV0QjH7r8JpG/S0GUMZwebnVFLPd6SU5yg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <script>
        var result = $('#testForm').validate({
            rules: {
                firstname: "required"
            },
            messages: {
                firstname: "Bu özellik zorunludur lütfen doldurun"
            },
            error: () => {
                console.log("error")
            },
            submitHandler: function(form) {
                console.log("çalıştı");
                // form validates so do the ajax
                $.ajax({
                    type: $(form).attr('method'),
                    url: "../php/client/json.php",
                    data: $(form).serialize(),
                    success: function(data, status) {
                        // ajax done
                        // do whatever & close the modal
                        $(this).modal('hide');
                    }
                });
                return false; // ajax used, block the normal submit
            }
        });
        console.log(result);
    </script>

    <script>
        $.when().then()
        $(document).ready(function() {

            $('#outcomeFormDialog form').validate({ // initialize plugin
                rules: {
                    amount: {
                        // money: true, //<-- no such rule
                        required: true
                    },
                    comment: {
                        required: false // superfluous; "false" same as leaving this rule out.
                    }
                },
                highlight: function(element) {
                    $(element).closest('.control-group')
                        .removeClass('success').addClass('error');
                },
                success: function(element) {
                    element.addClass('valid').closest('.control-group')
                        .removeClass('error').addClass('success');
                },
                submitHandler: function(form) {
                    // form validates so do the ajax
                    $.ajax({
                        type: $(form).attr('method'),
                        url: "../php/client/json.php",
                        data: $(form).serialize(),
                        success: function(data, status) {
                            // ajax done
                            // do whatever & close the modal
                            $(this).modal('hide');
                        }
                    });
                    return false; // ajax used, block the normal submit
                }
            });

        });
    </script>

    <script>
        var result = $('#parg').siblings();
        console.log(result);
    </script>
</body>

</html>
 */