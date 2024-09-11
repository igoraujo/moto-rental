using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoRental.Borders.Constants;
using MotoRental.Borders.Models;
using MotoRental.UseCases.Products;

namespace MotoRental.Admin.Controllers;

public class ProductController: Controller
{
    private readonly IProductUseCase _useCase;

    private readonly ILogger _logger;

    // private readonly ILogUtil _logUtil;
    private RouteData _context;

    public ProductController(IProductUseCase useCase, ILogger<RentalController> logger)
    {
        _useCase = useCase;
        _logger = logger;
    }

    public async Task<ActionResult> Index()
    {
        IEnumerable<Product>? response = new List<Product>();

        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER] ?? throw new InvalidOperationException();

        try
        {
            response = await _useCase.FindAll();
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response?.OrderBy(x => x.NumberOfDays));
    }

    // public async Task<ActionResult> Index(int deliveryPersonId)
    // {
    //     IEnumerable<Rental>? response = new List<Rental>();
    //
    //     _context = ControllerContext.RouteData;
    //     ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];
    //
    //     try
    //     {
    //         response = await _useCase.FindByDeliveryPersonId(deliveryPersonId);
    //         // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
    //     }
    //     catch (Exception e)
    //     {
    //         // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
    //     }
    //
    //     return View(response);
    // }
    
    // [Authorize(Roles = Roles.ADMIN)]
    public ActionResult Create()
    {
        return View(new Product());
    }

    [HttpPost]
    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> CreateProduct(Product request)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER] ?? throw new InvalidOperationException();

        try
        {
            request.CreatedAt = DateTime.Now;
            await _useCase.Save(request);

            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");

        }

        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);

    }
    
    [HttpDelete]
    // // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> Delete(int id)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER] ?? throw new InvalidOperationException();
    
        try
        {
            var response = await _useCase.Delete(new Product { Id = id });
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }
        
        return Ok(); // Retorna sucesso

    }

    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> Update(int id)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER] ?? throw new InvalidOperationException();

        Product? response = null;

        try
        {
            response = await _useCase.FindById(id);
            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);

    }
    
    [HttpPost]
    // [Authorize(Roles = Roles.ADMIN)]
    public async Task<ActionResult> UpdateProduct(Product request)
    {
        _context = ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER] ?? throw new InvalidOperationException();

        try
        {
            request.UpdatedAt = DateTime.Now;
            var response = await _useCase.Update(request);

            // _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

        }
        catch (Exception e)
        {
            // _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }

        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
    }

    [HttpPost]
    public ActionResult Cancel()
    {
        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
    }

    private ActionResult Forbidden()
    {
        _context = this.ControllerContext.RouteData;
        // _logger.LogWarning(_logUtil.Forbidden(GetType().FullName,
        // _context.Values[ActionName.ACTION].ToString()));
        return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
    }
}